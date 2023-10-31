using Antda.Core.Helpers;

namespace Antda.Messages.Middleware;

public class MiddlewareBuilder : IMiddlewareBuilder, IMiddlewareProvider
{
  private readonly IList<(Type MessageType, Func<MessageDelegate, MessageDelegate> Factory)> _middlewares = new List<(Type Type, Func<MessageDelegate, MessageDelegate> Delegate)>();

  private readonly Func<Type, MessageDelegate> _factory;

  public MiddlewareBuilder()
  {
    _factory = this.Create;
  }

  public IMiddlewareBuilder Use(Type messageType, Func<MessageDelegate, MessageDelegate> factory)
  {
    _middlewares.Add((messageType, factory));
    return this;
  }

  public MessageDelegate Create(Type messageType)
  {
    Task InvokeDelegate(IMessageContext _) => Task.CompletedTask;

    var middlewares = this.GetMiddlewares(messageType);

    return middlewares.Reverse().Aggregate((MessageDelegate)InvokeDelegate, static (current, middleware) => middleware(current));
  }

  public Func<Type, MessageDelegate> GetFactory() => _factory;

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
}