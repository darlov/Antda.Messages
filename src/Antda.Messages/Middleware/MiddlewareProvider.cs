using Antda.Messages.Core.Helpers;

namespace Antda.Messages.Middleware;

public class MiddlewareProvider(ICollection<(Type MessageType, Func<MessageDelegate, MessageDelegate> Factory)> middlewares)
  : IMiddlewareProvider
{
  public MessageDelegate Create(Type messageType)
  {
    var messageMiddlewares = GetMiddlewares(messageType);

    return messageMiddlewares.Reverse().Aggregate((MessageDelegate)InvokeDelegate, static (current, middleware) => middleware(current));

    Task InvokeDelegate(IMessageContext _) => Task.CompletedTask;
  }

  private IEnumerable<Func<MessageDelegate, MessageDelegate>> GetMiddlewares(Type messageType)
  {
    foreach (var (type, middleware) in middlewares)
    {
      if (TypeHelper.FindTypes(messageType, type).Any())
      {
        yield return middleware;
      }
    }
  }
  
  public readonly record struct MiddlewareCacheKey(Type GenericMiddlewareType, Type MessageType, Type ResultType);
}