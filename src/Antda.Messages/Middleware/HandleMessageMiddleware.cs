namespace Antda.Messages.Middleware;

public class HandleMessageMiddleware<TMessage, TResult>(IMessageHandler<TMessage, TResult> messageHandler) : MessageMiddleware<TMessage, TResult>
  where TMessage : IMessage<TResult>
{
  public override async Task InvokeAsync(IMessageContext<TMessage, TResult> context, MessageDelegate next, CancellationToken cancellationToken)
  {
    context.Result = await messageHandler.HandleAsync(context.Message, cancellationToken).ConfigureAwait(false);

    await next(context).ConfigureAwait(false);
  }
}