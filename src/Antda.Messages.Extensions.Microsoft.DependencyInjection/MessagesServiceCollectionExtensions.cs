using System.Reflection;
using Antda.Messages.DependencyInjection;
using Antda.Messages.Middleware;
using JetBrains.Annotations;
using Microsoft.Extensions.DependencyInjection;

namespace Antda.Messages.Extensions.Microsoft.DependencyInjection;

public static class MessagesServiceCollectionExtensions
{
  [PublicAPI]
  public static IMiddlewareBuilder AddAntdaMessages(this IServiceCollection services, params Assembly[] assembliesToScan)
  {
    var registrar = new MicrosoftDependencyInjectionServiceRegistrar(services);
    return registrar.AddAntdaMessages<MicrosoftDependencyInjectionServiceResolver>(assembliesToScan);
  }
  
  [PublicAPI]
  public static IMiddlewareBuilder AddAntdaMessagesCore(this IServiceCollection services)
  {
    var registrar = new MicrosoftDependencyInjectionServiceRegistrar(services);
    return registrar.AddAntdaMessagesCore<MicrosoftDependencyInjectionServiceResolver>();
  }
  
  [PublicAPI]
  public static IServiceCollection AddMessageHandler<T>(this IServiceCollection services)
  {
    return services.AddMessageHandler(typeof(T));
  }

  [PublicAPI]
  public static IServiceCollection AddMessageHandler(this IServiceCollection services, Type handlerType)
  {
    new MicrosoftDependencyInjectionServiceRegistrar(services).AddMessageHandler(handlerType);
    return services;
  }
}