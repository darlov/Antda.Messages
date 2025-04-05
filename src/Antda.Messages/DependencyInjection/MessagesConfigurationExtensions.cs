using Antda.Messages.Middleware;

namespace Antda.Messages.DependencyInjection;

public static class MessagesConfigurationExtensions
{
  public static MessagesConfiguration AddHandleMessagesMiddleware(this MessagesConfiguration config)
    => config.AddMiddleware(typeof(HandleMessageMiddleware<,>));

 
}