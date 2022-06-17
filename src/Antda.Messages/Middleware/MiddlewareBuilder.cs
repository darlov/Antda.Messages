namespace Antda.Messages.Middleware;

public class MiddlewareBuilder : IMiddlewareBuilder, IMiddlewareProvider
{
  private readonly IList<Func<MessageDelegate, MessageDelegate>> _commonMiddlewares = new List<Func<MessageDelegate, MessageDelegate>>();
  private readonly IDictionary<Type, IList<Func<MessageDelegate, MessageDelegate>>> _messageMiddlewares = new Dictionary<Type, IList<Func<MessageDelegate, MessageDelegate>>>();

  public IMiddlewareBuilder Use(Type messageType, Func<MessageDelegate, MessageDelegate> next)
  {
    if (messageType == typeof(IMessage<>))
    {
      _commonMiddlewares.Add(next);

      foreach (var middlewares in _messageMiddlewares.Values)
      {
        middlewares.Add(next);
      }
    }
    else
    {
      if (!_messageMiddlewares.TryGetValue(messageType, out var middlewares))
      {
        middlewares = new List<Func<MessageDelegate, MessageDelegate>>(_commonMiddlewares);
        _messageMiddlewares[messageType] = middlewares;
      }

      middlewares.Add(next);
    }

    return this;
  }

  public MessageDelegate Create(Type messageType)
  {
    MessageDelegate invokeDelegate = _ => Task.CompletedTask;

    if (!_messageMiddlewares.TryGetValue(messageType, out var middlewares))
    {
      middlewares = _commonMiddlewares;
    }

    foreach (var middleware in middlewares.Reverse())
    {
      invokeDelegate = middleware(invokeDelegate);
    }

    return invokeDelegate;
  }
}