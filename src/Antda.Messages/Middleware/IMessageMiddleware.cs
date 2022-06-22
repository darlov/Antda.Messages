namespace Antda.Messages.Middleware;

public interface IMessageMiddleware
{
  Task InvokeAsync(IMessageContext context, MessageDelegate next, CancellationToken cancellationToken);
}

public interface IMessageMiddleware<in TMessage> : IMessageMiddleware
{
  Task InvokeAsync(IMessageContext<TMessage> context, MessageDelegate next, CancellationToken cancellationToken);
}

public interface IMessageMiddleware<in TMessage, TResult> : IMessageMiddleware
  where TMessage : IMessage<TResult>
{
  Task InvokeAsync(IMessageContext<TMessage, TResult> context, MessageDelegate next, CancellationToken cancellationToken);
}