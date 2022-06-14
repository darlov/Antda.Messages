using System.Collections.Concurrent;
using Antda.Core.Exceptions;

namespace Antda.Messages.Wrappers
{
  public class HandlerWrapperFactory : IHandlerWrapperFactory
  {
    private readonly ConcurrentDictionary<Type, Type> _messageHandlerMapping;
    private readonly IServiceResolver _serviceProvider;

    public HandlerWrapperFactory(IServiceResolver serviceProvider)
    {
      _serviceProvider = serviceProvider;
      _messageHandlerMapping = new ConcurrentDictionary<Type, Type>();
    }

    public MessageHandlerWrapper<TResult> Create<TMessage, TResult>(TMessage message) where TMessage : PipeMessage<TResult>
    {
      Throw.If.ArgumentNull(message);

      var wrapperType = _messageHandlerMapping.GetOrAdd(message.GetType(), m => typeof(MessageHandlerWrapper<,>).MakeGenericType(m, typeof(TResult)));
      var wrapper = (MessageHandlerWrapper<TResult>) _serviceProvider.GetRequiredService(wrapperType);
      return wrapper;
    }
  }
}