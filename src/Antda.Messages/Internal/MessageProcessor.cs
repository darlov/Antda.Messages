using Antda.Messages.DependencyInjection;
using Antda.Messages.Exceptions;
using Antda.Messages.Middleware;

namespace Antda.Messages.Internal;

public class MessageProcessor<TMessage, TResult> : IMessageProcessor<TMessage, TResult>
  where TMessage : IMessage<TResult>
{
  private readonly IServiceResolver _serviceResolver;
  private readonly IMemoryCacheProvider<MessageDelegate> _messageDelegateCacheProvider;
  private readonly IMiddlewareProvider _middlewareProvider;

  public MessageProcessor(
    IServiceResolver serviceResolver,
    IMemoryCacheProvider<MessageDelegate> messageDelegateCacheProvider,
    IMiddlewareProvider middlewareProvider)
  {
    _serviceResolver = serviceResolver;
    _messageDelegateCacheProvider = messageDelegateCacheProvider;
    _middlewareProvider = middlewareProvider;
  }

  public async Task<TResult> ProcessAsync(TMessage message, CancellationToken cancellationToken)
  {
    var context = new MessageContext<TMessage, TResult>(message, _serviceResolver, cancellationToken);

    var messageDelegate = _messageDelegateCacheProvider.GetOrAdd(typeof(TMessage), _middlewareProvider.GetFactory());
    await messageDelegate(context).ConfigureAwait(false);
    
    if (!context.HasResult)
    {
      throw new MessageProcessingException($"The result of message has no set for {typeof(TMessage)} message type", message);
    }

    return (TResult)context.Result!;
  }

  public Task<TResult> ProcessAsync(IMessage<TResult> message, CancellationToken cancellationToken) 
    => this.ProcessAsync((TMessage)message, cancellationToken);

  public async Task<object?> ProcessAsync(object message, CancellationToken cancellationToken)
    => await this.ProcessAsync((TMessage)message, cancellationToken).ConfigureAwait(false);
}