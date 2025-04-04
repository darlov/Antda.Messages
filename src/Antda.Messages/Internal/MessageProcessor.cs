﻿using Antda.Messages.Core.DependencyInjection;
using Antda.Messages.Exceptions;
using Antda.Messages.Middleware;

namespace Antda.Messages.Internal;

internal class MessageProcessor<TMessage, TResult>(IMiddlewareProvider middlewareProvider) : IMessageProcessor<TMessage, TResult>
  where TMessage : IMessage<TResult>
{
  private readonly MessageDelegate _messageDelegate = middlewareProvider.Create(typeof(TMessage));

  public async Task<TResult> ProcessAsync(TMessage message, IServiceResolver serviceResolver, CancellationToken cancellationToken)
  {
    var context = new MessageContext<TMessage, TResult>(message, serviceResolver, cancellationToken);
    await _messageDelegate(context).ConfigureAwait(false);
    
    if (!context.HasResult)
    {
      throw new MessageProcessingException($"The result of message has no set for {typeof(TMessage)} message type", message);
    }

    return (TResult)context.Result!;
  }

  public Task<TResult> ProcessAsync(IMessage<TResult> message, IServiceResolver serviceResolver, CancellationToken cancellationToken) 
    => ProcessAsync((TMessage)message, serviceResolver, cancellationToken);
}