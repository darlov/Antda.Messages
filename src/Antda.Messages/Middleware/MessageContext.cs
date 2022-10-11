using Antda.Messages.DependencyInjection;
using Antda.Messages.Internal;

namespace Antda.Messages.Middleware;


public abstract class MessageContext : IMessageContext
{
  protected MessageContext(IServiceResolver serviceResolver, IMemoryCacheProvider<Type> typeCache, CancellationToken cancellationToken)
  {
    this.ServiceResolver = serviceResolver;
    this.TypeCache = typeCache;
    this.CancellationToken = cancellationToken;
  }

  public IServiceResolver ServiceResolver { get; }

  public IMemoryCacheProvider<Type> TypeCache { get; }

  public CancellationToken CancellationToken { get; }
  
  public abstract Type MessageType { get; }
  
  public abstract Type ResultType { get; }
}

public abstract class MessageContext<TMessage> : MessageContext, IMessageContext<TMessage>
{
  protected MessageContext(TMessage message, IServiceResolver serviceResolver, IMemoryCacheProvider<Type> typeCache, CancellationToken cancellationToken) 
    : base(serviceResolver, typeCache, cancellationToken)
  {
    this.Message = message;
  }

  public override Type MessageType => typeof(TMessage);

  public TMessage Message { get; }
}

public class MessageContext<TMessage, TResult> : MessageContext<TMessage>, IMessageContext<TMessage, TResult>
  where TMessage : IMessage<TResult>
{
  private bool _hasResult;
  private TResult? _result;

  public MessageContext(TMessage message, IServiceResolver serviceResolver, IMemoryCacheProvider<Type> typeCache, CancellationToken cancellationToken)
    : base(message, serviceResolver, typeCache, cancellationToken)
  {
  }

  public bool HasResult => _hasResult;

  public override Type ResultType => typeof(TResult);

  public TResult? Result
  {
    get => _result;
    set
    {
      _hasResult = true;
      _result = value;
    }
  }
}