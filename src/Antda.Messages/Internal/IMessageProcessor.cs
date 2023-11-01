using Antda.Messages.DependencyInjection;

namespace Antda.Messages.Internal;

public interface IMessageProcessor
{
}

internal interface IMessageProcessor<TResult> : IMessageProcessor
{
  Task<TResult> ProcessAsync(IMessage<TResult> message, IServiceResolver serviceResolver, CancellationToken cancellationToken);
}

internal interface IMessageProcessor<in TMessage, TResult> : IMessageProcessor<TResult>
  where TMessage : IMessage<TResult>
{
  Task<TResult> ProcessAsync(TMessage message, IServiceResolver serviceResolver, CancellationToken cancellationToken);
}