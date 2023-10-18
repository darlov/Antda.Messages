namespace Antda.Messages;

public interface IBaseMessage
{
}

public interface IMessage<TResult> : IBaseMessage
{
}

public interface IMessage : IMessage<Unit>
{
}