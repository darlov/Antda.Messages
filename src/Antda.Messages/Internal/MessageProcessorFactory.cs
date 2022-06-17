using System.Collections.Concurrent;
using Antda.Core.Exceptions;

namespace Antda.Messages.Internal;

public class MessageProcessorFactory : IMessageProcessorFactory
{
  private readonly ConcurrentDictionary<Type, Type> _messageProcessorTypes;
  private readonly IServiceResolver _serviceProvider;

  public MessageProcessorFactory(IServiceResolver serviceProvider)
  {
    _serviceProvider = serviceProvider;
    _messageProcessorTypes = new ConcurrentDictionary<Type, Type>();
  }

  public IMessageProcessor<TResult> Create<TMessage, TResult>(TMessage message)
    where TMessage : IMessage<TResult>
  {
    Throw.If.ArgumentNull(message);

    var processor = CreateProcessor<TResult>(message.GetType());
    return (IMessageProcessor<TResult>)processor;
  }

  private IMessageProcessor CreateProcessor<TResult>(Type messageType)
  {
    var processorType = _messageProcessorTypes.GetOrAdd(messageType, m => typeof(IMessageProcessor<,>).MakeGenericType(m, typeof(TResult)));

    if (_serviceProvider.GetService(processorType) is not IMessageProcessor processor)
    {
      throw new InvalidOperationException($"Can't resolve message processor with type {processorType}");
    }

    return processor;
  }
}