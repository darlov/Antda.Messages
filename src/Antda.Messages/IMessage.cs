namespace Antda.Messages;

public interface IBaseMessage
{
}

public interface IMessage : IMessage<Unit>
{
}

public interface IMessage<TResult> : IBaseMessage
{
}