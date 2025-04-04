namespace Antda.Messages.Tests.Data;

public class AddTestMessage(string payload) : IMessage<string>
{
  public string Payload { get; } = payload;
}