namespace Antda.Messages.Wrappers
{
  public interface IHandlerWrapperFactory
  {
    MessageHandlerWrapper<TResult> Create<T, TResult>(T message) 
      where T : PipeMessage<TResult>;
    
  }
}