namespace Antda.Messages
{
  public abstract class MessageHandler<T, TResult>  : IMessageHandler<T, TResult>
    where T: PipeMessage<TResult>
  {
    public abstract Task<TResult> HandleAsync(T message, CancellationToken cancellationToken); 
  }
  
  public abstract class MessageHandler<T> : MessageHandler<T, Unit>, IMessageHandler<T>
    where T: PipeMessage<Unit>
  {
    public abstract override Task<Unit> HandleAsync(T message, CancellationToken cancellationToken);
  }
}