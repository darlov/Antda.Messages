# Antda

Set of libraries. 

| Package | Stable | Pre-release |
|:--|:--:|:--:|
| Antda.Messages |[![Nuget](https://img.shields.io/nuget/v/Antda.Messages.svg)](https://www.nuget.org/packages/Antda.Messages)|[![Nuget (with prereleases)](https://img.shields.io/nuget/vpre/Antda.Messages)](https://www.nuget.org/packages/Antda.Messages)|
| Antda.Messages.Core |[![Nuget](https://img.shields.io/nuget/v/Antda.Messages.Core.svg)](https://www.nuget.org/packages/Antda.Messages.Core)|[![Nuget (with prereleases)](https://img.shields.io/nuget/vpre/Antda.Messages.Core)](https://www.nuget.org/packages/Antda.Messages.Core)|
| Antda.Messages.Extensions.Microsoft.DependencyInjection |[![Nuget](https://img.shields.io/nuget/v/Antda.Messages.Extensions.Microsoft.DependencyInjection.svg)](https://www.nuget.org/packages/Antda.Messages.Extensions.Microsoft.DependencyInjection)|[![Nuget (with prereleases)](https://img.shields.io/nuget/vpre/Antda.Messages.Extensions.Microsoft.DependencyInjection)](https://www.nuget.org/packages/Antda.Messages.Extensions.Microsoft.DependencyInjection)|


## Antda.Messages
Messaging library follow Mediator pattern.

Example of usage:

```csharp 
public class SomeService {
    private readonly IMessageSender _messageSender;

    public SomeService(IMessageSender messageSender)
    {
        _messageSender = messageSender;
    }
    
    public async Task SendMessageAsync()
    {
        var myMessage = new HelloMessage
        {
            Message = "Hello world!"
        }
        
        HelloResponse response = await _messageSender.SendAsync(myMessage);
        
        // use response data
        //...
    }
}
```

## Antda.Messages.Core

The core library collect common functionality.

TODO


## Antda.Messages.Extensions.Microsoft.DependencyInjection
Extensions library for to use Microsoft DI as service provider.
Example of usage: 

```csharp 
services
    .AddAntdaMessages(cfg => cfg.RegisterHandlersFromAssembly(typeof(Program).Assembly));
```
