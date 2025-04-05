using Antda.Messages.Extensions.Microsoft.DependencyInjection.Tests.Data;
using Antda.Messages.Extensions.Microsoft.DependencyInjection.Tests.Messages;
using Antda.Messages.Middleware;

namespace Antda.Messages.Extensions.Microsoft.DependencyInjection.Tests.Middlewares;

public class ModifyResultMiddleware(TestData<string> data) : MessageMiddleware<DefaultMessage, string>
{
  private readonly string _additionalText = data.Value;

  public override async Task InvokeAsync(IMessageContext<DefaultMessage, string> context, MessageDelegate next, CancellationToken cancellationToken)
  {
    await next(context);

    context.Result += _additionalText;
  }
}