using JetBrains.Annotations;

namespace Antda.Messages;

[PublicAPI]
public abstract class MessageHandler<T, TResult> : IMessageHandler<T, TResult>
  where T : IMessage<TResult>
{
  public abstract Task<TResult> HandleAsync(T message, CancellationToken cancellationToken);
}

[PublicAPI]
public abstract class MessageHandler<T> : IMessageHandler<T, Unit>
  where T : IMessage<Unit>
{
  public abstract Task<Unit> HandleAsync(T message, CancellationToken cancellationToken);
}