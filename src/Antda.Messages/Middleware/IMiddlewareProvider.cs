namespace Antda.Messages.Middleware;

public interface IMiddlewareProvider
{
  MessageDelegate Create(Type messageType);
}