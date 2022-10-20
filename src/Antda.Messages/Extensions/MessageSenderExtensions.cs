using JetBrains.Annotations;

namespace Antda.Messages.Extensions;

public static class MessageSenderExtensions
{
  [PublicAPI]
  public static Task SendAsync<T>(this IMessageSender messageSender, CancellationToken cancellationToken = default)
    where T : IMessage, new()
  {
    return messageSender.SendAsync(new T(), cancellationToken);
  }
}