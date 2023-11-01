using MediatR;

namespace Antda.Messages.Benchmarks.Handlers;

public class BaseMessage : IMessage<string>, IRequest<string>
{
}