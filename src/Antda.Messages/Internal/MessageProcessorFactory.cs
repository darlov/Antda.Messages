using Antda.Core.Exceptions;
using Antda.Messages.DependencyInjection;
using Antda.Messages.Exceptions;

namespace Antda.Messages.Internal;

public class MessageProcessorFactory : IMessageProcessorFactory
{
  private readonly IServiceResolver _serviceProvider;
  private readonly IMemoryCacheProvider<Type> _memoryCacheProvider;

  public MessageProcessorFactory(IServiceResolver serviceProvider, IMemoryCacheProvider<Type> memoryCacheProvider)
  {
    _serviceProvider = serviceProvider;
    _memoryCacheProvider = memoryCacheProvider;
  }

  public IMessageProcessor<TResult> Create<TResult>(IMessage<TResult> message)
  {
    Throw.If.ArgumentNull(message);
    return this.CreateProcessor(message);
  }

  private IMessageProcessor<TResult> CreateProcessor<TResult>(IMessage<TResult> message)
  {
    var processorType = _memoryCacheProvider.GetOrAdd(message.GetType(), static messageType => typeof(IMessageProcessor<,>).MakeGenericType(messageType, typeof(TResult)));

    if (_serviceProvider.GetService(processorType) is IMessageProcessor<TResult> processor)
    {
      return processor;
    }

    throw new MessageProcessingException($"Couldn't resolve message processor with type {processorType}", message);
  }
}