namespace Antda.Messages.Extensions.Microsoft.DependencyInjection.Tests.Data;

public class NoResultHandler : MessageHandler<NoResultMessage>
{
    public override Task<Unit> HandleAsync(NoResultMessage message, CancellationToken cancellationToken)
    {
        return Task.FromResult(Unit.Value);
    }
}