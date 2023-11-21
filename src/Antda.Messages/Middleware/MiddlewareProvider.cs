using Antda.Messages.Core.Helpers;

namespace Antda.Messages.Middleware;

public class MiddlewareProvider : IMiddlewareProvider
{
  private readonly ICollection<(Type MessageType, Func<MessageDelegate, MessageDelegate> Factory)> _middlewares;

  public MiddlewareProvider(ICollection<(Type MessageType, Func<MessageDelegate, MessageDelegate> Factory)>  middlewares)
  {
    _middlewares = middlewares;
  }

  public MessageDelegate Create(Type messageType)
  {
    Task InvokeDelegate(IMessageContext _) => Task.CompletedTask;

    var middlewares = this.GetMiddlewares(messageType);

    return middlewares.Reverse().Aggregate((MessageDelegate)InvokeDelegate, static (current, middleware) => middleware(current));
  }

  private IEnumerable<Func<MessageDelegate, MessageDelegate>> GetMiddlewares(Type messageType)
  {
    foreach (var (type, middleware) in _middlewares)
    {
      if (TypeHelper.FindTypes(messageType, type).Any())
      {
        yield return middleware;
      }
    }
  }
  
  public readonly record struct MiddlewareCacheKey(Type GenericMiddlewareType, Type MessageType, Type ResultType);
}