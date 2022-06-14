using Antda.Messages.Extensions.Microsoft.DependencyInjection.Tests.Data;
using Microsoft.Extensions.DependencyInjection;

namespace Antda.Messages.Extensions.Microsoft.DependencyInjection.Tests;

public class MessagesServiceCollectionExtensionsTests
{
    [Fact]
    public async Task AddAntdaMessages_WithValidAssemblies_ShouldAddTypes()
    {
        var services = new ServiceCollection();

        services.AddAntdaMessages(new[] { GetType().Assembly });
        var provider = services.BuildServiceProvider();

       var sender =  provider.GetRequiredService<IMessageSender>();

       var defaultTestMessage = new DefaultMessage("Test");
       var defaultResult = await sender.SendAsync(defaultTestMessage);
       Assert.Equal("Test", defaultResult);
       
       var emptyMessage = new NoResultMessage();
       var emptyResult = await sender.SendAsync(emptyMessage);
       Assert.Equal(Unit.Value, emptyResult);
    }
}