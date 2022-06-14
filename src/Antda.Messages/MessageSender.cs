using Antda.Core.Exceptions;
using Antda.Messages.Wrappers;

namespace Antda.Messages;

public class MessageSender : IMessageSender
{
    private readonly IHandlerWrapperFactory _handlerWrapperFactory;

    public MessageSender(IHandlerWrapperFactory handlerWrapperFactory)
    {
        _handlerWrapperFactory = handlerWrapperFactory;
    }

    public async Task<TResult> SendAsync<TResult>(PipeMessage<TResult> message, CancellationToken cancellationToken = default)
    {
        Throw.If.ArgumentNull(message);

        var handlerWrapper = _handlerWrapperFactory.Create<PipeMessage<TResult>, TResult>(message);

        var result = await handlerWrapper.HandleAsync(message, cancellationToken);
        return result;
    }
}