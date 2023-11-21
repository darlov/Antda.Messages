using System.Data;
using System.Reflection;
using Antda.Messages.Core.Exceptions;
using Antda.Messages.Core.Extensions;
using Antda.Messages.Core.Helpers;
using Antda.Messages.Middleware;
using JetBrains.Annotations;
using Microsoft.Extensions.DependencyInjection;

namespace Antda.Messages.DependencyInjection;

[PublicAPI]
public class MessagesConfiguration
{
  public MessagesConfiguration(IServiceCollection serviceCollection)
  {
    MessageMiddlewares = new List<(Type MessageType, Func<MessageDelegate, MessageDelegate> Factory)>();
    MiddlewareTypes = new List<Type>();
    Handlers = new List<(Type From, Type To)>();
  }

  public ICollection<(Type MessageType, Func<MessageDelegate, MessageDelegate> Factory)> MessageMiddlewares { get; }
  public ICollection<Type> MiddlewareTypes { get; }
  public ICollection<(Type From, Type To)> Handlers { get; }

  public MessagesConfiguration RegisterHandlersFromAssembly(params Assembly[] assembliesToScan)
  {
    foreach (var typeInfo in TypeHelper.FindAllowedTypes(assembliesToScan))
    {
      AddMessageHandlerInternal(typeInfo, true);
    }

    return this;
  }
  
  public MessagesConfiguration UseMiddleware(Type messageType, Func<MessageDelegate, MessageDelegate> factory)
  {
    MessageMiddlewares.Add((messageType, factory));
    return this;
  }

  public MessagesConfiguration ClearMiddlewares()
  {
    MessageMiddlewares.Clear();
    return this;
  }

  public MessagesConfiguration AddMessageHandler<T>() => AddMessageHandler(typeof(T));

  public MessagesConfiguration AddMessageHandler(Type handlerType) => AddMessageHandlerInternal(handlerType, false);
  
  public MessagesConfiguration UseMiddleware<TMiddleware>()
    where TMiddleware : IMessageMiddleware 
    => UseMiddleware(typeof(TMiddleware));
  
  public MessagesConfiguration UseMiddleware(Type middlewareType)
  {
    Throw.If.ArgumentNull(middlewareType);

    var middlewareInterface = TypeHelper.FindTypes(middlewareType, typeof(IMessageMiddleware<,>)).FirstOrDefault()
                              ?? TypeHelper.FindTypes(middlewareType, typeof(IMessageMiddleware<>)).FirstOrDefault();

    if (middlewareInterface == null)
    {
      throw new InvalidExpressionException($"The middleware {middlewareType} is not implemented IMessageMiddleware interface");
    }

    MiddlewareTypes.Add(middlewareType);

    if (middlewareType.IsOpenGeneric())
    {
      AddGenericMiddleware(middlewareType);
    }
    else
    {
      AddMiddleware(middlewareType, middlewareInterface);
    }

    return this;
  }

  private MessagesConfiguration AddMessageHandlerInternal(Type handlerType, bool skipNotSupported)
  {
    Throw.If.ArgumentNull(handlerType);

    var types = TypeHelper.FindTypes(handlerType, typeof(IMessageHandler<,>)).ToList();
    if (!types.Any())
    {
      if (skipNotSupported)
      {
        return this;
      }

      throw new NotSupportedException("Message handler should implemented IMessageHandler interface");
    }

    if (handlerType.IsOpenGeneric())
    {
      throw new NotSupportedException($"The open generic handler is not supported for {handlerType}");
    }

    foreach (var interfaceType in types)
    {
      Handlers.Add((interfaceType, handlerType));
    }

    return this;
  }

  internal void AddGenericMiddleware(Type middlewareType)
  {
    var genericMiddlewareType = middlewareType.GetGenericTypeDefinition();

    UseMiddleware(typeof(IMessage<>), next =>
    {
      return ctx =>
      {
        var key = new MiddlewareProvider.MiddlewareCacheKey(genericMiddlewareType, ctx.MessageType, ctx.ResultType);
        var messageMiddlewareType = ctx.TypeCache.GetOrAdd(key, static (keyValue) =>
        {
          var (middlewareTypeKey, messageTypeKey, resultTypeKey) = keyValue;
          return middlewareTypeKey.MakeGenericType(messageTypeKey, resultTypeKey);
        });

        if (ctx.ServiceResolver.GetService(messageMiddlewareType) is not IMessageMiddleware middleware)
        {
          throw new InvalidOperationException($"Couldn't resolve middleware with type {messageMiddlewareType}");
        }

        return middleware.InvokeAsync(ctx, next, ctx.CancellationToken);
      };
    });
  }

  internal void AddMiddleware(Type middlewareType, Type middlewareInterface)
  {
    var messageType = middlewareInterface.GenericTypeArguments.First();

    UseMiddleware(messageType, next =>
    {
      return ctx =>
      {
        if (ctx.ServiceResolver.GetService(middlewareType) is IMessageMiddleware middleware)
        {
          return middleware.InvokeAsync(ctx, next, ctx.CancellationToken);
        }

        throw new InvalidOperationException($"Couldn't resolve middleware with type {middlewareType}");
      };
    });
  }
}