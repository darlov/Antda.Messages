namespace Antda.Messages.Middleware;

public abstract class MessageMiddleware : IMessageMiddleware
{
  public abstract Task InvokeAsync(MessageContext context, MessageDelegate next, CancellationToken cancellationToken);
}

public abstract class MessageMiddleware<TMessage, TResult> : MessageMiddleware, IMessageMiddleware<TMessage, TResult>
  where TMessage : IMessage<TResult>
{
  public abstract Task InvokeAsync(MessageContext<TMessage, TResult> context, MessageDelegate next, CancellationToken cancellationToken);

  public override Task InvokeAsync(MessageContext context, MessageDelegate next, CancellationToken cancellationToken)
  {
    return InvokeAsync((MessageContext<TMessage, TResult>)context, next, cancellationToken);
  }
}