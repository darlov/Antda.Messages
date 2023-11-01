using MediatR;

namespace Antda.Messages.Benchmarks.Handlers;

public class BaseMessageHandler : MessageHandler<BaseMessage, string>, IRequestHandler<BaseMessage, string>
{
  public override Task<string> HandleAsync(BaseMessage message, CancellationToken cancellationToken) => Run();

  public Task<string> Handle(BaseMessage request, CancellationToken cancellationToken) => Run();

  private Task<string> Run()
  {
    return Task.FromResult(string.Empty);
  }
}