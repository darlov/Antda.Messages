namespace Antda.Messages.Middleware;

public abstract class MessageContext
{
  public MessageContext(IServiceResolver serviceResolver, CancellationToken cancellationToken)
  {
    ServiceResolver = serviceResolver;
    CancellationToken = cancellationToken;
  }

  public IServiceResolver ServiceResolver { get; }

  public CancellationToken CancellationToken { get; }
}

public class MessageContext<TMessage, TResult> : MessageContext
  where TMessage : IMessage<TResult>
{
  public MessageContext(TMessage message, IServiceResolver serviceResolver, CancellationToken cancellationToken)
    : base(serviceResolver, cancellationToken)
  {
    Message = message;
  }

  public TMessage Message { get; }

  public TResult? Result { get; set; }
}