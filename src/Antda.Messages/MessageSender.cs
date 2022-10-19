using Antda.Core.Exceptions;
using Antda.Messages.Internal;

namespace Antda.Messages;

public class MessageSender : IMessageSender
{
  private readonly IMessageProcessorFactory _messageProcessorFactory;

  public MessageSender(IMessageProcessorFactory messageProcessorFactory)
  {
    _messageProcessorFactory = messageProcessorFactory;
  }

  public Task<TResult> SendAsync<TResult>(IMessage<TResult> message, CancellationToken cancellationToken = default)
  {
    Throw.If.ArgumentNull(message);

    var messageProcessor = _messageProcessorFactory.Create(message);
    return messageProcessor.ProcessAsync(message, cancellationToken);
  }

  public Task SendAsync<TResult>(IMessage message, CancellationToken cancellationToken = default)
    => this.SendAsync(message, cancellationToken);
}