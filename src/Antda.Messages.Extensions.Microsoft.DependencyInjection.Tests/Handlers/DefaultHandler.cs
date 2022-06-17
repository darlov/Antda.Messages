using Antda.Messages.Extensions.Microsoft.DependencyInjection.Tests.Messages;

namespace Antda.Messages.Extensions.Microsoft.DependencyInjection.Tests.Handlers;

public class DefaultHandler : MessageHandler<DefaultMessage, string>
{
  public override Task<string> HandleAsync(DefaultMessage message, CancellationToken cancellationToken)
  {
    return Task.FromResult(message.Payload);
  }
}