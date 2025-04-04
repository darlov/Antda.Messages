using Antda.Messages.Core.DependencyInjection;
using Antda.Messages.Core.Exceptions;
using Antda.Messages.Internal;
using Antda.Messages.Middleware;
using JetBrains.Annotations;

namespace Antda.Messages;

[PublicAPI]
public class MessageSender(
  IMiddlewareProvider middlewareProvider,
  IMemoryCacheProvider<Type, IMessageProcessor> messageProcessorCache,
  IServiceResolver serviceResolver)
  : IMessageSender
{
  public Task<TResult> SendAsync<TResult>(IMessage<TResult> message, CancellationToken cancellationToken = default)
  {
    Throw.If.ArgumentNull(message);

    var messageProcessor = (IMessageProcessor<TResult>) messageProcessorCache.GetOrAdd(message.GetType(), static (messageType, middlewareProvider) =>
    {
      var processorType = typeof(MessageProcessor<,>).MakeGenericType(messageType, typeof(TResult));
      return (IMessageProcessor)(Activator.CreateInstance(processorType, middlewareProvider)
                                 ?? new InvalidOperationException($"Couldn't create message processor with type {processorType}"));
    }, middlewareProvider);

    return messageProcessor.ProcessAsync(message, serviceResolver, cancellationToken);
  }

  public Task SendAsync(IMessage message, CancellationToken cancellationToken = default)
    => SendAsync<Unit>(message, cancellationToken);
}