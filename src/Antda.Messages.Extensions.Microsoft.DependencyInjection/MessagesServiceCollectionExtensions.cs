using System.Reflection;
using Antda.Core.Helpers;
using Antda.Messages.Internal;
using Antda.Messages.Middleware;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Antda.Messages.Extensions.Microsoft.DependencyInjection;

public static class MessagesServiceCollectionExtensions
{
  public static IMiddlewareBuilder AddAntdaMessages(this IServiceCollection services, params Assembly[] assembliesToScan)
  {
    return services.AddAntdaMessages((IEnumerable<Assembly>)assembliesToScan);
  }

  public static IMiddlewareBuilder AddAntdaMessagesCore(this IServiceCollection services)
  {
    services.TryAddTransient<IMessageSender, MessageSender>();
    services.TryAddTransient<IServiceResolver, MicrosoftServiceResolver>();
    services.TryAddTransient<IMessageProcessorFactory, MessageProcessorFactory>();
    services.TryAddSingleton(typeof(IMessageProcessor<,>), typeof(MessageProcessor<,>));

    var middlewareBuilder = new MiddlewareBuilder();
    services.AddSingleton<IMiddlewareProvider>(middlewareBuilder);
    services.AddSingleton<IMiddlewareBuilder>(middlewareBuilder);

    services.TryAddTransient(typeof(HandleMessageMiddleware<,>));

    return middlewareBuilder;
  }

  public static IMiddlewareBuilder AddAntdaMessages(this IServiceCollection services, IEnumerable<Assembly> assembliesToScan)
  {
    var builder =  services.AddAntdaMessagesCore();
    
    foreach (var typeInfo in TypeHelper.FindAllowedTypes(assembliesToScan))
    {
      foreach (var interfaceType in TypeHelper.FindGenericInterfaces(typeInfo, typeof(IMessageHandler<,>)))
      {
        services.AddTransient(interfaceType, typeInfo);
      }
    }

    return builder;
  }
}