namespace Antda.Messages.Extensions.Microsoft.DependencyInjection.Tests.Data
{
  public class DefaultMessage : PipeMessage<string>
  {
    public DefaultMessage(string payload)
    {
      Payload = payload;
    }

    public string Payload { get; }
  }
}