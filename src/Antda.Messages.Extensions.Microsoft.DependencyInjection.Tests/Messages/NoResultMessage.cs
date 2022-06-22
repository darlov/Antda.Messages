namespace Antda.Messages.Extensions.Microsoft.DependencyInjection.Tests.Messages;

public class NoResultMessage : IMessage
{
}

public class NoResultWithCustomMessage : NoResultMessage, ICustomInterface
{
  public string? CustomProp { get; set; }
}