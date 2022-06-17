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

  public async Task<TResult?> SendAsync<TResult>(IMessage<TResult> message, CancellationToken cancellationToken = default)
  {
    Throw.If.ArgumentNull(message);

    var messageProcessor = _messageProcessorFactory.Create<IMessage<TResult>, TResult>(message);
    return await messageProcessor.ProcessAsync(message, cancellationToken);
  }
}