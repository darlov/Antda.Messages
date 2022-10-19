using Antda.Messages.DependencyInjection;
using Antda.Messages.Internal;
using Antda.Messages.Middleware;
using Antda.Messages.Tests.Data;
using Moq;

namespace Antda.Messages.Tests;

public class HandlerWrapperFactoryTests
{
  [Fact]
  public void Create_WithValidMessage_ShouldBeCreated()
  {
    var serviceProviderMock = new Mock<IServiceResolver>();
    var middlewareProviderMock = new Mock<IMiddlewareProvider>();
    var typeCacheProviderMock = new Mock<IMemoryCacheProvider<Type>>();
    var delegateCacheProviderMock = new Mock<IMemoryCacheProvider<MessageDelegate>>();

    typeCacheProviderMock.Setup(m => m.GetOrAdd(It.IsAny<Type>(), It.IsAny<Func<Type, Type>>()))
      .Returns<Type, Func<Type, Type>>((key, factoryFunc) => factoryFunc(key));

    var expectedMessageProcessor = new MessageProcessor<AddTestMessage, string>(serviceProviderMock.Object, typeCacheProviderMock.Object, delegateCacheProviderMock.Object, middlewareProviderMock.Object);

    serviceProviderMock.Setup(m => m.GetService(typeof(IMessageProcessor<AddTestMessage, string>)))
      .Returns(expectedMessageProcessor);

    var factory = new MessageProcessorFactory(serviceProviderMock.Object, typeCacheProviderMock.Object);
    var message = new AddTestMessage("Bla-bla");
    var messageProcessor = factory.Create(message);
    
    Assert.Equal(expectedMessageProcessor, messageProcessor);
  }
}