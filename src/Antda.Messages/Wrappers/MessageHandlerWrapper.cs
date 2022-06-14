using Antda.Core.Exceptions;

namespace Antda.Messages.Wrappers
{
  public abstract class MessageHandlerWrapper
  {
    public abstract Task<object?> HandleAsync(object message, CancellationToken cancellationToken);
  }
  
  public abstract class MessageHandlerWrapper<TResult> : MessageHandlerWrapper
  {
    public abstract Task<TResult> HandleAsync(PipeMessage<TResult> message, CancellationToken cancellationToken);
  }
  
  public class MessageHandlerWrapper<TMessage, TResult> : MessageHandlerWrapper<TResult>
    where TMessage : PipeMessage<TResult>
  {
    private readonly IMessageHandler<TMessage, TResult> _messageHandler;
    
    public MessageHandlerWrapper(IMessageHandler<TMessage, TResult> messageHandler)
    {
      _messageHandler = messageHandler;
    }
    
    public override async Task<TResult> HandleAsync(PipeMessage<TResult> message, CancellationToken cancellationToken)
    {
      Throw.If.ArgumentNull(message);
      return await _messageHandler.HandleAsync((TMessage)message, cancellationToken);
    }

    public override async Task<object?> HandleAsync(object message, CancellationToken cancellationToken)
      => await HandleAsync((PipeMessage<TResult>) message, cancellationToken).ConfigureAwait(false);
  }
}