using Antda.Messages.Extensions.Microsoft.DependencyInjection.Tests.Messages;

namespace Antda.Messages.Extensions.Microsoft.DependencyInjection.Tests.Handlers;

public class NoResultHandler : MessageHandler<NoResultMessage>
{
  public override Task<Unit> HandleAsync(NoResultMessage message, CancellationToken cancellationToken)
  {
    return Task.FromResult(Unit.Value);
  }
}