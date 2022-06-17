using Antda.Messages.Internal;
using Antda.Messages.Middleware;
using Antda.Messages.Tests.Data;
using Moq;

namespace Antda.Messages.Tests;

public class HandlerWrapperFactoryTests
{
  [Fact]
  public async Task Create_WithValidMessage_ShouldBeCreated()
  {
    var serviceProviderMock = new Mock<IServiceResolver>();
    var middlewareProviderMock = new Mock<IMiddlewareProvider>();

    var expectedMessageProcessor = new MessageProcessor<AddTestMessage, string>(serviceProviderMock.Object, middlewareProviderMock.Object);

    serviceProviderMock.Setup(m => m.GetService(typeof(IMessageProcessor<AddTestMessage, string>)))
      .Returns(expectedMessageProcessor);

    var factory = new MessageProcessorFactory(serviceProviderMock.Object);
    var message = new AddTestMessage("Bla-bla");
    var messageProcessor = factory.Create<IMessage<string>, string>(message);
    
    Assert.Equal(expectedMessageProcessor, messageProcessor);
  }
}