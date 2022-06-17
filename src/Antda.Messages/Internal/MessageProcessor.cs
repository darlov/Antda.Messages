using Antda.Messages.Middleware;

namespace Antda.Messages.Internal;

public class MessageProcessor<TMessage, TResult> : IMessageProcessor<TMessage, TResult>
  where TMessage : IMessage<TResult>
{
  private readonly IServiceResolver _serviceResolver;
  private readonly MessageDelegate _messageDelegate;

  public MessageProcessor(IServiceResolver serviceResolver, IMiddlewareProvider middlewareBuilder)
  {
    _serviceResolver = serviceResolver;
    _messageDelegate = middlewareBuilder.Create(typeof(TMessage));
  }

  public async Task<TResult?> ProcessAsync(TMessage message, CancellationToken cancellationToken)
  {
    var context = new MessageContext<TMessage, TResult>(message, _serviceResolver, cancellationToken);
    await _messageDelegate(context);

    return context.Result;
  }

  public Task<TResult?> ProcessAsync(IMessage<TResult> message, CancellationToken cancellationToken)
  {
    return ProcessAsync((TMessage)message, cancellationToken);
  }

  public async Task<object?> ProcessAsync(object message, CancellationToken cancellationToken)
  {
    return await ProcessAsync((TMessage)message, cancellationToken);
  }
}