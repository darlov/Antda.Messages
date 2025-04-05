﻿using Antda.Messages.Core.DependencyInjection;
using Antda.Messages.Core.Exceptions;
using Antda.Messages.DependencyInjection;
using Antda.Messages.Internal;
using Antda.Messages.Middleware;
using JetBrains.Annotations;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Antda.Messages.Extensions.Microsoft.DependencyInjection;

public static class MessagesServiceCollectionExtensions
{
  [PublicAPI]
  public static IServiceCollection AddAntdaMessages(this IServiceCollection services, Action<MessagesConfiguration> setup)
    => services.AddAntdaMessagesCore((_, config) => setup(config));
  
  [PublicAPI]
  public static IServiceCollection AddAntdaMessages(this IServiceCollection services, Action<IServiceCollection, MessagesConfiguration> setup) 
    => services.AddAntdaMessagesCore(setup);

  private static IServiceCollection AddAntdaMessagesCore(this IServiceCollection services, Action<IServiceCollection, MessagesConfiguration> setup)
  {
    Throw.If.ArgumentNull(services);
    Throw.If.ArgumentNull(setup);

    var messageConfiguration = new MessagesConfiguration(services);

    services.TryAdd(new ServiceDescriptor(typeof(IServiceResolver), typeof(MicrosoftDependencyInjectionServiceResolver), messageConfiguration.Lifetime));
    services.TryAdd(new ServiceDescriptor(typeof(IMessageSender), typeof(MessageSender), messageConfiguration.Lifetime));
    services.TryAddSingleton(typeof(IMemoryCacheProvider<,>), typeof(MemoryCacheProvider<,>));
    
    setup.Invoke(services, messageConfiguration);
    messageConfiguration.AddHandleMessagesMiddleware();

    if (!messageConfiguration.Handlers.Any())
    {
      throw new InvalidOperationException("No handlers found to register. Supply at least one handler");
    }
    
    if (!messageConfiguration.MessageMiddlewares.Any())
    {
      throw new InvalidOperationException("No middlewares found to register. Supply at least one middleware");
    }
    
    var middlewareBuilder = new MiddlewareProvider(messageConfiguration.MessageMiddlewares);
    services.AddSingleton<IMiddlewareProvider>(middlewareBuilder);

    var middlewareDescriptors = messageConfiguration.MiddlewareTypes.Select(m => new ServiceDescriptor(m, m, messageConfiguration.Lifetime));
    services.Add(middlewareDescriptors);

    var handlerDescriptors = messageConfiguration.Handlers.Select(m => new ServiceDescriptor(m.From, m.To, messageConfiguration.Lifetime));
    services.Add(handlerDescriptors);

    return services;
  }
}