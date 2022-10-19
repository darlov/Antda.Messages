using Antda.Messages.DependencyInjection;
using Antda.Messages.Extensions.Microsoft.DependencyInjection.Internal;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Antda.Messages.Extensions.Microsoft.DependencyInjection;

public class MicrosoftDependencyInjectionServiceRegistrar : IServiceRegistrar
{
    private readonly IServiceCollection _services;
    
    public MicrosoftDependencyInjectionServiceRegistrar(IServiceCollection services)
    {
        _services = services;
    }

    public void TryAdd(ServiceRegistrarDescriptor descriptor)
    {
        var serviceDescriptor = descriptor.ToServiceDescriptor();
       _services.TryAdd(serviceDescriptor);
    }

    public void Add(ServiceRegistrarDescriptor descriptor)
    {
        var serviceDescriptor = descriptor.ToServiceDescriptor();
        _services.Add(serviceDescriptor);
    }
}