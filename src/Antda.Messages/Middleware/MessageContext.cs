namespace Antda.Messages.Middleware;


public class MessageContext : IMessageContext
{
  protected MessageContext(IServiceResolver serviceResolver, CancellationToken cancellationToken)
  {
    ServiceResolver = serviceResolver;
    CancellationToken = cancellationToken;
  }

  public IServiceResolver ServiceResolver { get; }

  public CancellationToken CancellationToken { get; }
}


public class MessageContext<TMessage>  : MessageContext, IMessageContext<TMessage>
{
  protected MessageContext(TMessage message, IServiceResolver serviceResolver, CancellationToken cancellationToken) 
    : base(serviceResolver, cancellationToken)
  {
    Message = message;
  }
 
  public TMessage Message { get; }
}

public class MessageContext<TMessage, TResult> : MessageContext<TMessage>, IMessageContext<TMessage, TResult>
  where TMessage : IMessage<TResult>
{
  public MessageContext(TMessage message, IServiceResolver serviceResolver, CancellationToken cancellationToken)
    : base(message, serviceResolver, cancellationToken)
  {
  }

  public TResult? Result { get; set; }
}