namespace Antda.Messages.Extensions.Microsoft.DependencyInjection.Tests.Data
{
  public class WithResultHandler: MessageHandler<DefaultMessage, string>
  {
    public override Task<string> HandleAsync(DefaultMessage message, CancellationToken cancellationToken)
    {
      return Task.FromResult(message.Payload);
    }
  }
}