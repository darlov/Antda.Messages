using Antda.Messages.Extensions.Microsoft.DependencyInjection.Tests.Handlers;
using Antda.Messages.Extensions.Microsoft.DependencyInjection.Tests.Messages;
using Antda.Messages.Extensions.Microsoft.DependencyInjection.Tests.Middlewares;
using Microsoft.Extensions.DependencyInjection;

namespace Antda.Messages.Extensions.Microsoft.DependencyInjection.Tests;

public class MessagesServiceCollectionExtensionsTests
{
  [Fact]
  public async Task AddAntdaMessages_WithValidAssemblies_ShouldAddTypes()
  {
    var services = new ServiceCollection();

    services.AddAntdaMessages(GetType().Assembly);
    var provider = services.BuildServiceProvider();

    var sender = provider.GetRequiredService<IMessageSender>();

    var defaultTestMessage = new DefaultMessage("Test");
    var defaultResult = await sender.SendAsync(defaultTestMessage);

    Assert.Equal("Test", defaultResult);
  }
  
  [Fact]
  public async Task AddAntdaMessages_WithMiddleware_ShouldModifyResult()
  {
    const string messageText = "Message text";
    const string additionalText = " Bla Bla";
    
    var services = new ServiceCollection();
    services.AddTransient(_ => new ModifyResultMiddleware(additionalText));

    services.AddAntdaMessages(GetType().Assembly)
      .UseMiddleware<ModifyResultMiddleware>();
    
    var provider = services.BuildServiceProvider();
    
    var sender = provider.GetRequiredService<IMessageSender>();

    var defaultTestMessage = new DefaultMessage(messageText);
    var defaultResult = await sender.SendAsync(defaultTestMessage);

    Assert.Equal(messageText + additionalText, defaultResult);
  }
  
  [Fact]
  public async Task AddAntdaMessages_WithHandlers_ShouldResolve()
  {
    var services = new ServiceCollection();

    services.AddAntdaMessagesCore();

    services.AddTransient<IMessageHandler<DefaultMessage, string>, DefaultHandler>();
    services.AddTransient<IMessageHandler<NoResultMessage, Unit>, NoResultHandler>();
    
    var provider = services.BuildServiceProvider();

    var sender = provider.GetRequiredService<IMessageSender>();

    var defaultMessage = new DefaultMessage("Test");
    var defaultResult = await sender.SendAsync(defaultMessage);

    Assert.Equal("Test", defaultResult);
    
    var noResultMessage = new NoResultMessage();
    await sender.SendAsync(noResultMessage);
  }
}