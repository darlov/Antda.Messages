namespace Antda.Messages.Tests.Data
{
  public class AddTestMessage : PipeMessage<string>
  {
    public AddTestMessage(string payload)
    {
      Payload = payload;
    }

    public string Payload { get; }
  }
}