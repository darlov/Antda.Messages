using System.Data;
using Antda.Core.Extensions;
using Antda.Core.Helpers;
using Antda.Messages.Middleware;

namespace Antda.Messages.Extensions;

public static class MiddlewareBuilderExtensions
{
  public static IMiddlewareBuilder Use<TMessage>(this IMiddlewareBuilder builder, Func<MessageDelegate, MessageDelegate> next)
    => builder.Use(typeof(TMessage), next);

  public static IMiddlewareBuilder UseHandleMessages(this IMiddlewareBuilder builder) 
    => builder.UseMiddleware(typeof(HandleMessageMiddleware<,>));

  public static IMiddlewareBuilder UseMiddleware<TMiddleware>(this IMiddlewareBuilder builder)
    where TMiddleware : IMessageMiddleware
  {
    return builder.UseMiddleware(typeof(TMiddleware));
  }

  public static IMiddlewareBuilder UseMiddleware(this IMiddlewareBuilder builder, Type middlewareType)
  {
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
        var messageMiddlewareType = genericMiddlewareType.MakeGenericType(ctx.GetType().GenericTypeArguments);

        if (ctx.ServiceResolver.GetService(messageMiddlewareType) is not IMessageMiddleware middleware)
        {
          throw new InvalidOperationException($"Can't resolve middleware with type {messageMiddlewareType}");
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
        if (ctx.ServiceResolver.GetService(middlewareType) is not IMessageMiddleware middleware)
        {
          throw new InvalidOperationException($"Can't resolve middleware with type {middlewareType}");
        }

        return middleware.InvokeAsync(ctx, next, ctx.CancellationToken);
      };
    });
  }
}