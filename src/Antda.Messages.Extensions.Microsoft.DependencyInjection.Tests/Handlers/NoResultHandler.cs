using Antda.Messages.Extensions.Microsoft.DependencyInjection.Tests.Messages;

namespace Antda.Messages.Extensions.Microsoft.DependencyInjection.Tests.Handlers;

public class NoResultHandler<T> : MessageHandler<T> where T : IMessage<Unit>
{
  public override Task<Unit> HandleAsync(T message, CancellationToken cancellationToken)
  {
    return Task.FromResult(Unit.Value);
  }
}