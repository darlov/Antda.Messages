namespace Antda.Messages.Internal;

public interface IMessageProcessorFactory
{
  IMessageProcessor<TResult> Create<TMessage, TResult>(TMessage message)
    where TMessage : IMessage<TResult>;
}