using JetBrains.Annotations;

namespace Antda.Messages;

[PublicAPI]
public interface IMessageSender
{
  [PublicAPI]
  public Task<TResult> SendAsync<TResult>(IMessage<TResult> message, CancellationToken cancellationToken = default);
  
  [PublicAPI]
  public Task SendAsync(IMessage message, CancellationToken cancellationToken = default);
}