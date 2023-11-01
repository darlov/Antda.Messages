using JetBrains.Annotations;

namespace Antda.Messages;

[PublicAPI]
public interface IBaseMessage
{
}

[PublicAPI]
public interface IMessage<TResult> : IBaseMessage
{
}

[PublicAPI]
public interface IMessage : IMessage<Unit>
{
}