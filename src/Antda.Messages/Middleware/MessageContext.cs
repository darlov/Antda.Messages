using Antda.Messages.Core.DependencyInjection;
using Antda.Messages.Internal;

namespace Antda.Messages.Middleware;

public abstract class MessageContext : IMessageContext
{
  private IMemoryCacheProvider<MiddlewareProvider.MiddlewareCacheKey, Type>? _typeCache;
  protected MessageContext(IServiceResolver serviceResolver, CancellationToken cancellationToken)
  {
    this.ServiceResolver = serviceResolver;
    this.CancellationToken = cancellationToken;
  }

  public IServiceResolver ServiceResolver { get; }

  public IMemoryCacheProvider<MiddlewareProvider.MiddlewareCacheKey, Type> TypeCache 
    => _typeCache ??= ServiceResolver.GetRequiredService<IMemoryCacheProvider<MiddlewareProvider.MiddlewareCacheKey, Type>>();

  public CancellationToken CancellationToken { get; }
  
  public abstract Type MessageType { get; }
  
  public abstract Type ResultType { get; }
}

public abstract class MessageContext<TMessage> : MessageContext, IMessageContext<TMessage>
{
  protected MessageContext(TMessage message, IServiceResolver serviceResolver, CancellationToken cancellationToken) 
    : base(serviceResolver, cancellationToken)
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

  public MessageContext(TMessage message, IServiceResolver serviceResolver, CancellationToken cancellationToken)
    : base(message, serviceResolver, cancellationToken)
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