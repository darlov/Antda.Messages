namespace Antda.Messages.Tests.Data
{
  public class AddTestHandler: MessageHandler<AddTestMessage, string>
  {
    public override Task<string> HandleAsync(AddTestMessage message, CancellationToken cancellationToken)
    {
      return Task.FromResult(message.Payload);
    }
  }
}