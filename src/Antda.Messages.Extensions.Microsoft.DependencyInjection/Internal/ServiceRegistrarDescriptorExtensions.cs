using Antda.Messages.Core.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

namespace Antda.Messages.Extensions.Microsoft.DependencyInjection.Internal;

internal static class ServiceRegistrarDescriptorExtensions
{
  public static ServiceDescriptor ToServiceDescriptor(this ServiceRegistrarDescriptor descriptor)
  {
    return descriptor.Lifetime switch
    {
      ServiceRegistrarLifetime.Transient when descriptor.ImplementationType != null
        => ServiceDescriptor.Transient(descriptor.ServiceType, descriptor.ImplementationType),
      ServiceRegistrarLifetime.Singleton when descriptor.ImplementationType != null
        => ServiceDescriptor.Singleton(descriptor.ServiceType, descriptor.ImplementationType),
      ServiceRegistrarLifetime.Singleton when descriptor.ImplementationInstance != null
        => ServiceDescriptor.Singleton(descriptor.ServiceType, descriptor.ImplementationInstance),
      _ => throw new ArgumentOutOfRangeException(nameof(descriptor.Lifetime))
    };
  }
}