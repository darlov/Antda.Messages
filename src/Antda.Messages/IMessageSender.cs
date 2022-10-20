namespace Antda.Messages;

public interface IMessageSender
{
  public Task<TResult> SendAsync<TResult>(IMessage<TResult> message, CancellationToken cancellationToken = default);
  
  public Task SendAsync(IMessage message, CancellationToken cancellationToken = default);
}