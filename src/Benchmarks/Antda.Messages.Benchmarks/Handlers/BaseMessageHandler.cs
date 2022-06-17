namespace Antda.Messages.Benchmarks.Handlers;

public class BaseMessageHandler : MessageHandler<BaseMessage, string>
{
  public override Task<string> HandleAsync(BaseMessage message, CancellationToken cancellationToken)
  {
    return Task.FromResult(string.Empty);
  }
}