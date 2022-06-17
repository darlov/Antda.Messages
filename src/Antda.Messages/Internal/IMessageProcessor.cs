namespace Antda.Messages.Internal;

public interface IMessageProcessor
{
  Task<object?> ProcessAsync(object message, CancellationToken cancellationToken);
}

public interface IMessageProcessor<TResult> : IMessageProcessor
{
  Task<TResult?> ProcessAsync(IMessage<TResult> message, CancellationToken cancellationToken);
}

public interface IMessageProcessor<in TMessage, TResult> : IMessageProcessor<TResult>
  where TMessage : IMessage<TResult>
{
  Task<TResult?> ProcessAsync(TMessage message, CancellationToken cancellationToken);
}