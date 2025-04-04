using Antda.Messages.Core.DependencyInjection;
using Antda.Messages.Internal;

namespace Antda.Messages.Middleware;

public abstract class MessageContext(IServiceResolver serviceResolver, CancellationToken cancellationToken) : IMessageContext
{
  private IMemoryCacheProvider<MiddlewareProvider.MiddlewareCacheKey, Type>? _typeCache;

  public IServiceResolver ServiceResolver { get; } = serviceResolver;

  public IMemoryCacheProvider<MiddlewareProvider.MiddlewareCacheKey, Type> TypeCache 
    => _typeCache ??= ServiceResolver.GetRequiredService<IMemoryCacheProvider<MiddlewareProvider.MiddlewareCacheKey, Type>>();

  public CancellationToken CancellationToken { get; } = cancellationToken;

  public abstract Type MessageType { get; }
  
  public abstract Type ResultType { get; }
}

public abstract class MessageContext<TMessage>(TMessage message, IServiceResolver serviceResolver, CancellationToken cancellationToken)
  : MessageContext(serviceResolver, cancellationToken), IMessageContext<TMessage>
{
  public override Type MessageType => typeof(TMessage);

  public TMessage Message { get; } = message;
}

public class MessageContext<TMessage, TResult>(TMessage message, IServiceResolver serviceResolver, CancellationToken cancellationToken)
  : MessageContext<TMessage>(message, serviceResolver, cancellationToken), IMessageContext<TMessage, TResult>
  where TMessage : IMessage<TResult>
{
  private TResult? _result;

  public bool HasResult { get; private set; }

  public override Type ResultType => typeof(TResult);

  public TResult? Result
  {
    get => _result;
    set
    {
      HasResult = true;
      _result = value;
    }
  }
}