namespace Antda.Messages.Tests.Data;

public class AddTestMessage : IMessage<string>
{
  public AddTestMessage(string payload)
  {
    Payload = payload;
  }

  public string Payload { get; }
}