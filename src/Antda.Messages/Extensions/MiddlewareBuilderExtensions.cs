using System.Data;
using Antda.Core.Exceptions;
using Antda.Core.Extensions;
using Antda.Core.Helpers;
using Antda.Messages.Middleware;
using JetBrains.Annotations;

namespace Antda.Messages.Extensions;

public static class MiddlewareBuilderExtensions
{
  [PublicAPI]
  public static IMiddlewareBuilder Use<TMessage>(this IMiddlewareBuilder builder, Func<MessageDelegate, MessageDelegate> next)
    => builder.Use(typeof(TMessage), next);

  public static IMiddlewareBuilder UseHandleMessages(this IMiddlewareBuilder builder) 
    => builder.UseMiddleware(typeof(HandleMessageMiddleware<,>));

  public static IMiddlewareBuilder UseMiddleware<TMiddleware>(this IMiddlewareBuilder builder)
    where TMiddleware : IMessageMiddleware
  {
    Throw.If.ArgumentNull(builder);
    return builder.UseMiddleware(typeof(TMiddleware));
  }

  public static IMiddlewareBuilder UseMiddleware(this IMiddlewareBuilder builder, Type middlewareType)
  {
    Throw.If.ArgumentNull(builder);
    Throw.If.ArgumentNull(middlewareType);
    var middlewareInterface = TypeHelper.FindTypes(middlewareType, typeof(IMessageMiddleware<,>)).FirstOrDefault()
                              ?? TypeHelper.FindTypes(middlewareType, typeof(IMessageMiddleware<>)).FirstOrDefault();

    if (middlewareInterface == null)
    {
      throw new InvalidExpressionException($"The middleware {middlewareType} is not implemented IMessageMiddleware interface");
    }


    if (middlewareType.IsOpenGeneric())
    {
      builder.AddGenericMiddleware(middlewareType);
    }
    else
    {
      builder.AddMiddleware(middlewareType, middlewareInterface);
    }

    return builder;
  }

  private static void AddGenericMiddleware(this IMiddlewareBuilder builder, Type middlewareType)
  {
    var genericMiddlewareType = middlewareType.GetGenericTypeDefinition();

    builder.Use(typeof(IMessage<>), next =>
    {
      return ctx =>
      {
        var key = new MiddlewareCacheKey(genericMiddlewareType, ctx.MessageType, ctx.ResultType);
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

  private static void AddMiddleware(this IMiddlewareBuilder builder, Type middlewareType, Type middlewareInterface)
  {
    var messageType = middlewareInterface.GenericTypeArguments.First();

    builder.Use(messageType, next =>
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
  
  private record MiddlewareCacheKey(Type GenericMiddlewareType, Type MessageType, Type ResultType);
}