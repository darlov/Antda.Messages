namespace Antda.Messages.Extensions.Microsoft.DependencyInjection.Tests.Messages;

public class DefaultMessage : IMessage<string>
{
  public DefaultMessage(string payload)
  {
    Payload = payload;
  }

  public string Payload { get; }
}