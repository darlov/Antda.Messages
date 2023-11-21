using Antda.Messages.Extensions.Microsoft.DependencyInjection.Tests.Data;
using Antda.Messages.Extensions.Microsoft.DependencyInjection.Tests.Messages;
using Antda.Messages.Middleware;

namespace Antda.Messages.Extensions.Microsoft.DependencyInjection.Tests.Middlewares;

public class ModifyResultMiddleware : MessageMiddleware<DefaultMessage, string>
{
  private readonly string _additionalText;
  
  public ModifyResultMiddleware(TestData<string> data)
  {
    _additionalText = data.Value;
  }
  
  public override Task InvokeAsync(IMessageContext<DefaultMessage, string> context, MessageDelegate next, CancellationToken cancellationToken)
  {
    context.Result += _additionalText;

    return next(context);
  }
}