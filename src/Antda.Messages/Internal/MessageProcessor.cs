using Antda.Messages.DependencyInjection;
using Antda.Messages.Exceptions;
using Antda.Messages.Middleware;

namespace Antda.Messages.Internal;

public class MessageProcessor<TMessage, TResult> : IMessageProcessor<TMessage, TResult>
  where TMessage : IMessage<TResult>
{
  private readonly IServiceResolver _serviceResolver;
  private readonly IMemoryCacheProvider<Type> _typeCacheProvider;
  private readonly IMemoryCacheProvider<MessageDelegate> _messageDelegateCacheProvider;
  private readonly Func<Type, MessageDelegate> _createMessageDelegate;

  public MessageProcessor(
    IServiceResolver serviceResolver,
    IMemoryCacheProvider<Type> typeCacheProvider,
    IMemoryCacheProvider<MessageDelegate> messageDelegateCacheProvider,
    IMiddlewareProvider middlewareProvider)
  {
    _serviceResolver = serviceResolver;
    _typeCacheProvider = typeCacheProvider;
    _messageDelegateCacheProvider = messageDelegateCacheProvider;

    _createMessageDelegate = middlewareProvider.Create;
  }

  public async Task<TResult> ProcessAsync(TMessage message, CancellationToken cancellationToken)
  {
    var context = new MessageContext<TMessage, TResult>(message, _serviceResolver, _typeCacheProvider, cancellationToken);

    var messageDelegate = _messageDelegateCacheProvider.GetOrAdd(typeof(TMessage), _createMessageDelegate);
    await messageDelegate(context);
    
    if (!context.HasResult)
    {
      throw new MessageProcessingException($"The result of message has no set for {typeof(TMessage)} message type", message);
    }

    return (TResult)context.Result!;
  }

  public Task<TResult> ProcessAsync(IMessage<TResult> message, CancellationToken cancellationToken)
  {
    return this.ProcessAsync((TMessage)message, cancellationToken);
  }

  public async Task<object?> ProcessAsync(object message, CancellationToken cancellationToken)
  {
    return await this.ProcessAsync((TMessage)message, cancellationToken);
  }
}