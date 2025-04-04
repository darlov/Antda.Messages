using Antda.Messages.Extensions.Microsoft.DependencyInjection.Tests.Data;
using Antda.Messages.Extensions.Microsoft.DependencyInjection.Tests.Messages;
using Antda.Messages.Middleware;

namespace Antda.Messages.Extensions.Microsoft.DependencyInjection.Tests.Middlewares;

public class CustomTypeMiddleware(TestData<string> data) : MessageMiddleware<ICustomInterface>
{
  private readonly string _modifyText = data.Value;

  public override Task InvokeAsync(IMessageContext<ICustomInterface> context, MessageDelegate next, CancellationToken cancellationToken)
  {
    context.Message.CustomProp = _modifyText;

    return next(context);
  }
}