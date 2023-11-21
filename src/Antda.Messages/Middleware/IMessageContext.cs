using Antda.Messages.Core.DependencyInjection;
using Antda.Messages.Internal;

namespace Antda.Messages.Middleware;

public interface IMessageContext
{
  IServiceResolver ServiceResolver { get; }
  
  IMemoryCacheProvider<MiddlewareProvider.MiddlewareCacheKey, Type> TypeCache { get; }

  CancellationToken CancellationToken { get; }
  
  Type MessageType { get; }
  
  Type ResultType { get; }
}

public interface IMessageContext<out TMessage> : IMessageContext
{
  TMessage Message { get; }
}

public interface IMessageContext<out TMessage, TResult> : IMessageContext<TMessage>
  where TMessage : IMessage<TResult>
{
  TResult? Result { get; set; }
  
  bool HasResult { get; }
}