namespace Antda.Messages.Middleware;

public abstract class MessageMiddleware : IMessageMiddleware
{
  public abstract Task InvokeAsync(IMessageContext context, MessageDelegate next, CancellationToken cancellationToken);
}

public abstract class MessageMiddleware<TMessage> : MessageMiddleware, IMessageMiddleware<TMessage>
{
  public abstract Task InvokeAsync(IMessageContext<TMessage> context, MessageDelegate next, CancellationToken cancellationToken);

  public override Task InvokeAsync(IMessageContext context, MessageDelegate next, CancellationToken cancellationToken)
    => InvokeAsync((IMessageContext<TMessage>)context, next, cancellationToken);
}

public abstract class MessageMiddleware<TMessage, TResult> : MessageMiddleware<TMessage>, IMessageMiddleware<TMessage, TResult>
  where TMessage : IMessage<TResult>
{
  public abstract Task InvokeAsync(IMessageContext<TMessage, TResult> context, MessageDelegate next, CancellationToken cancellationToken);

  public override Task InvokeAsync(IMessageContext<TMessage> context, MessageDelegate next, CancellationToken cancellationToken)
    => InvokeAsync((IMessageContext<TMessage, TResult>)context, next, cancellationToken);
}