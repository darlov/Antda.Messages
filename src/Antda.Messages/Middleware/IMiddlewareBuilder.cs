namespace Antda.Messages.Middleware;

public interface IMiddlewareBuilder
{
  IMiddlewareBuilder Use(Type messageType, Func<MessageDelegate, MessageDelegate> next);
}