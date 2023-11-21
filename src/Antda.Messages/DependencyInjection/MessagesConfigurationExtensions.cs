using Antda.Messages.Middleware;

namespace Antda.Messages.DependencyInjection;

public static class MessagesConfigurationExtensions
{
  public static MessagesConfiguration UseHandleMessagesMiddleware(this MessagesConfiguration config) 
    => config.UseMiddleware(typeof(HandleMessageMiddleware<,>));

 
}