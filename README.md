# Antda

Set of libraries. 

| Package | Stable | Pre-release |
|:--:|:--:|:--:|
| Antda.Core |[![Nuget](https://img.shields.io/nuget/v/Antda.Core.svg)](https://www.nuget.org/packages/Antda.Core)|[![Nuget (with prereleases)](https://img.shields.io/nuget/vpre/Antda.Core)](https://www.nuget.org/packages/Antda.Core)|
| Antda.Messages |[![Nuget](https://img.shields.io/nuget/v/Antda.Messages.svg)](https://www.nuget.org/packages/Antda.Messages)|[![Nuget (with prereleases)](https://img.shields.io/nuget/vpre/Antda.Messages)](https://www.nuget.org/packages/Antda.Messages)|
| Antda.Messages.Extensions.Microsoft.DependencyInjection |[![Nuget](https://img.shields.io/nuget/v/Antda.Messages.Extensions.Microsoft.DependencyInjection.svg)](https://www.nuget.org/packages/Antda.Messages.Extensions.Microsoft.DependencyInjection)|[![Nuget (with prereleases)](https://img.shields.io/nuget/vpre/Antda.Messages.Extensions.Microsoft.DependencyInjection)](https://www.nuget.org/packages/Antda.Messages.Extensions.Microsoft.DependencyInjection)|


## Antda.Core

The core library collect common functionality.

TODO

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
    
    public Task SendMessageAsync()
    {
        var myMessage = new HelloMessage
        {
            Message = "Hello world!"
        }
        
        HelloResponse response =  _messageSender.SendAsync(myMessage);
        
        // use response data
        //...
    }
}
```

## Antda.Messages.Extensions.Microsoft.DependencyInjection
Extensions library for to use Microsoft DI as service provider.
Example of usage: 

```csharp 
services
    .AddAntdaMessages(typeof(Program).Assembly)
    .UseHandleMessages();
```