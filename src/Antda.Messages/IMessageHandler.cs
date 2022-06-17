namespace Antda.Messages;

public interface IMessageHandler<in TMessage, TResult>
  where TMessage : IMessage<TResult>
{
  Task<TResult> HandleAsync(TMessage message, CancellationToken cancellationToken);
}