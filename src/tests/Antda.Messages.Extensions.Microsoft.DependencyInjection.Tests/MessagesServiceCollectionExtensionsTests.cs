using Antda.Messages.DependencyInjection;
using Antda.Messages.Extensions.Microsoft.DependencyInjection.Tests.Data;
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

    services.AddAntdaMessages(cfg => cfg.RegisterHandlersFromAssembly(this.GetType().Assembly));

    await using var provider = services.BuildServiceProvider();

    var sender = provider.GetRequiredService<IMessageSender>();

    var defaultTestMessage = new DefaultMessage("Test");
    var defaultResult = await sender.SendAsync(defaultTestMessage);

    Assert.Equal("Test", defaultResult);
  }

  [Fact]
  public async Task AddAntdaMessagesWithScoped_WithValidAssemblies_ShouldAddTypes()
  {
    var services = new ServiceCollection();

    services.AddAntdaMessages(cfg => cfg.RegisterHandlersFromAssembly(this.GetType().Assembly)
      .WithLifetime(ServiceLifetime.Scoped));

    await using var provider = services.BuildServiceProvider();

    await using var scope = provider.CreateAsyncScope();
    var sender = scope.ServiceProvider.GetRequiredService<IMessageSender>();

    var defaultTestMessage = new DefaultMessage("Test");
    var defaultResult = await sender.SendAsync(defaultTestMessage);

    Assert.Equal("Test", defaultResult);
  }

  [Fact]
  public async Task AddAntdaMessages_WithMiddleware_ShouldModifyResult()
  {
    const string MessageText = "Message text";
    const string AdditionalText = " Bla Bla";

    var services = new ServiceCollection();
    services.AddTransient(_ => new TestData<string>(AdditionalText));

    services.AddAntdaMessages(cfg =>
      cfg
        .RegisterHandlersFromAssembly(this.GetType().Assembly)
        .AddMiddleware<ModifyResultMiddleware>());

    await using var provider = services.BuildServiceProvider();

    var sender = provider.GetRequiredService<IMessageSender>();

    var defaultTestMessage = new DefaultMessage(MessageText);
    var defaultResult = await sender.SendAsync(defaultTestMessage);

    Assert.Equal(MessageText + AdditionalText, defaultResult);
  }

  [Fact]
  public async Task AddAntdaMessages_WithHandlers_ShouldResolve()
  {
    var services = new ServiceCollection();

    services.AddAntdaMessages(s => s
      .AddMessageHandler<DefaultHandler>()
      .AddMessageHandler<NoResultHandler<NoResultMessage>>());

    await using var provider = services.BuildServiceProvider();

    var sender = provider.GetRequiredService<IMessageSender>();

    var defaultMessage = new DefaultMessage("Test");
    var defaultResult = await sender.SendAsync(defaultMessage);

    Assert.Equal("Test", defaultResult);
    
    await sender.SendAsync<NoResultMessage>();
  }
  
  [Fact]
  public async Task AddAntdaMessages_WitCustomMiddleware_ShouldResolve()
  {
    const string CustomPropText = "Custom text";
    
    var services = new ServiceCollection();
    services.AddTransient((_) => new TestData<string>(CustomPropText));
    
    services.AddAntdaMessages(cfg => cfg
        .ClearMiddlewares()
        .AddMiddleware<CustomTypeMiddleware>()
        .AddHandleMessagesMiddleware()
        .AddMessageHandler<DefaultHandler>()
        .AddMessageHandler<NoResultHandler<NoResultMessage>>()
        .AddMessageHandler<NoResultHandler<NoResultWithCustomMessage>>());

    await using var provider = services.BuildServiceProvider();

    var sender = provider.GetRequiredService<IMessageSender>();

    var defaultMessage = new DefaultMessage("Test");

    _ = await sender.SendAsync(defaultMessage);

    Assert.Equal(CustomPropText, defaultMessage.CustomProp);
    
    var noResultWithCustomMessage = new NoResultWithCustomMessage();
    await sender.SendAsync(noResultWithCustomMessage);
    
    Assert.Equal(CustomPropText, noResultWithCustomMessage.CustomProp);
    
    var noResultMessage = new NoResultMessage();
    await sender.SendAsync(noResultMessage);
  }
}