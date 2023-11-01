using Antda.Core.Exceptions;
using Antda.Messages.DependencyInjection;
using Antda.Messages.Internal;
using Antda.Messages.Middleware;
using JetBrains.Annotations;

namespace Antda.Messages;

[PublicAPI]
public class MessageSender : IMessageSender
{
  private readonly IMiddlewareProvider _middlewareProvider;
  private readonly IMemoryCacheProvider<Type, IMessageProcessor> _messageProcessorCache;
  private readonly IServiceResolver _serviceResolver;

  public MessageSender(IMiddlewareProvider middlewareProvider,
    IMemoryCacheProvider<Type, IMessageProcessor> messageProcessorCache,
    IServiceResolver serviceResolver)
  {
    _middlewareProvider = middlewareProvider;
    _messageProcessorCache = messageProcessorCache;
    _serviceResolver = serviceResolver;
  }

  public Task<TResult> SendAsync<TResult>(IMessage<TResult> message, CancellationToken cancellationToken = default)
  {
    Throw.If.ArgumentNull(message);

    var messageProcessor = (IMessageProcessor<TResult>) _messageProcessorCache.GetOrAdd(message.GetType(), static (messageType, middlewareProvider) =>
    {
      var processorType = typeof(MessageProcessor<,>).MakeGenericType(messageType, typeof(TResult));
      return (IMessageProcessor)(Activator.CreateInstance(processorType, middlewareProvider)
                                 ?? new InvalidOperationException($"Couldn't create message processor with type {processorType}"));
    }, _middlewareProvider);

    return messageProcessor.ProcessAsync(message, _serviceResolver, cancellationToken);
  }

  public Task SendAsync(IMessage message, CancellationToken cancellationToken = default)
    => this.SendAsync<Unit>(message, cancellationToken);
}