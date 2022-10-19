namespace Antda.Messages.Internal;

public interface IMessageProcessorFactory
{
  IMessageProcessor<TResult> Create<TResult>(IMessage<TResult> message);
}