using Antda.Core.Exceptions;

namespace Antda.Messages.DependencyInjection;

public class ServiceRegistrarDescriptor
{
    public ServiceRegistrarDescriptor(Type serviceType, Type implementationType, ServiceRegistrarLifetime lifetime)
        : this(serviceType, lifetime)
    {
        Throw.If.ArgumentNull(serviceType);
        Throw.If.ArgumentNull(implementationType);

        this.ImplementationType = implementationType;
    }

    public ServiceRegistrarDescriptor(Type serviceType, object instance)
        : this(serviceType, ServiceRegistrarLifetime.Singleton)
    {
        Throw.If.ArgumentNull(serviceType);
        Throw.If.ArgumentNull(instance);

        this.ImplementationInstance = instance;
    }

    private ServiceRegistrarDescriptor(Type serviceType, ServiceRegistrarLifetime lifetime)
    {
        this.Lifetime = lifetime;
        this.ServiceType = serviceType;
    }

    public ServiceRegistrarLifetime Lifetime { get; }

    public Type ServiceType { get; }

    public Type? ImplementationType { get; }

    public object? ImplementationInstance { get; }
}