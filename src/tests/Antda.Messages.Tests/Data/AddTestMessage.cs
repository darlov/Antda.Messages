namespace Antda.Messages.Tests.Data;

public class AddTestMessage : IMessage<string>
{
  public AddTestMessage(string payload)
  {
    this.Payload = payload;
  }

  public string Payload { get; }
}