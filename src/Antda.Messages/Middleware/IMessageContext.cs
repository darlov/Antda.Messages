namespace Antda.Messages.Middleware;

public interface IMessageContext
{
  IServiceResolver ServiceResolver { get; }

  CancellationToken CancellationToken { get; }
}

public interface IMessageContext<out TMessage> : IMessageContext
{
  TMessage Message { get; }
}

public interface IMessageContext<out TMessage, TResult> : IMessageContext<TMessage>
  where TMessage : IMessage<TResult>
{

  TResult? Result { get; set; }
}