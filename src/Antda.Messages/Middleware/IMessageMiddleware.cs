namespace Antda.Messages.Middleware;

public interface IMessageMiddleware
{
  Task InvokeAsync(MessageContext context, MessageDelegate next, CancellationToken cancellationToken);
}

public interface IMessageMiddleware<TMessage, TResult> : IMessageMiddleware
  where TMessage : IMessage<TResult>
{
  Task InvokeAsync(MessageContext<TMessage, TResult> context, MessageDelegate next, CancellationToken cancellationToken);
}