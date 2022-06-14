namespace Antda.Messages;

public interface IMessageSender
{
    public Task<TResult> SendAsync<TResult>(PipeMessage<TResult> message, CancellationToken cancellationToken = default);
}