using Antda.Messages.Tests.Data;
using Antda.Messages.Wrappers;
using Moq;

namespace Antda.Messages.Tests
{
  public class HandlerWrapperFactoryTests
  {
    [Fact]
    public async Task Create_WithValidMessage_ShouldBeCreated()
    {
      const string MessagePayload = "Test Message";
      
      var serviceProviderMock = new Mock<IServiceResolver>();

      serviceProviderMock.Setup(m => m.GetRequiredService(typeof(MessageHandlerWrapper<AddTestMessage, string>)))
        .Returns(new MessageHandlerWrapper<AddTestMessage, string>(new AddTestHandler()));

      var factory = new HandlerWrapperFactory(serviceProviderMock.Object);

      var message = (PipeMessage<string>)new AddTestMessage(MessagePayload);
      
      var handler = factory.Create<PipeMessage<string>, string>(message);

      var result = await handler.HandleAsync(message, CancellationToken.None);
      
      Assert.Equal(MessagePayload, result);
    }
  }
}