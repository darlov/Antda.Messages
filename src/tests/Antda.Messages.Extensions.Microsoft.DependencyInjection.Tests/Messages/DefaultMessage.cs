namespace Antda.Messages.Extensions.Microsoft.DependencyInjection.Tests.Messages;

public class DefaultMessage : IMessage<string>, ICustomInterface
{
  public DefaultMessage(string payload)
  {
    Payload = payload;
  }

  public string Payload { get; }
  
  public string? CustomProp { get; set; }
}