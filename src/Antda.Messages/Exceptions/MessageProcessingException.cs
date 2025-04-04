namespace Antda.Messages.Exceptions;

[Serializable]
public class MessageProcessingException : Exception
{
  public MessageProcessingException(string message, IBaseMessage sendMessage) : base(message)
  {
    SendMessage = sendMessage;
  }

  public MessageProcessingException(string message, IBaseMessage sendMessage, Exception innerException) : base(message, innerException)
  {
    SendMessage = sendMessage;
  }

  public IBaseMessage SendMessage { get; }
}