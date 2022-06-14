namespace Antda.Messages
{
  public interface IMessageHandler<in TMessage, TResult> 
    where TMessage: PipeMessage<TResult>
  {
    public Task<TResult> HandleAsync(TMessage message, CancellationToken cancellationToken);
  }
  
  public interface IMessageHandler<in TMessage> : IMessageHandler<TMessage, Unit> 
    where TMessage: PipeMessage<Unit>
  {
  }
}