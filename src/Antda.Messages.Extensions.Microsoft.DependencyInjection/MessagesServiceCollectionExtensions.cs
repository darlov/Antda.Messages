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
    services.TryAddSingleton<IMessageProcessorFactory, MessageProcessorFactory>();
    services.TryAddSingleton(typeof(IMessageProcessor<,>), typeof(MessageProcessor<,>));

    var middlewareBuilder = new MiddlewareBuilder();
    services.AddSingleton<IMiddlewareProvider>(middlewareBuilder);

    services.TryAddTransient(typeof(HandleMessageMiddleware<,>));

    return middlewareBuilder;
  }
  
  public static IServiceCollection AddMessageHandler<T>(this IServiceCollection services) 
    => services.AddMessageHandler(typeof(T));

  public static IServiceCollection AddMessageHandler(this IServiceCollection services, Type handlerType)
    => services.AddMessageHandlerInternal(handlerType, false);

  public static IMiddlewareBuilder AddAntdaMessages(this IServiceCollection services, IEnumerable<Assembly> assembliesToScan)
  {
    var builder =  services.AddAntdaMessagesCore();
    
    foreach (var typeInfo in TypeHelper.FindAllowedTypes(assembliesToScan))
    {
      services.AddMessageHandlerInternal(typeInfo, true);
    }

    return builder;
  }
  
  private static IServiceCollection AddMessageHandlerInternal(this IServiceCollection services, Type handlerType, bool skipNotSupported)
  {
    var types = TypeHelper.FindTypes(handlerType, typeof(IMessageHandler<,>)).ToList();
    if (!types.Any())
    {
      if (skipNotSupported)
      {
        return services;
      }
      
      throw new NotSupportedException("Message handler should implemented IMessageHandler<in TMessage, TResult> interface.");
    }
    
    foreach (var interfaceType in types)
    {
      services.AddTransient(interfaceType, handlerType);
    }

    return services;
  }
}