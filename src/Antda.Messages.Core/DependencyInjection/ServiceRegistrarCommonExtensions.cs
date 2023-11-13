using Antda.Messages.Core.Exceptions;

namespace Antda.Messages.Core.DependencyInjection;

public static class ServiceRegistrarCommonExtensions
{
    #region Transient

    public static IServiceRegistrar TryAddTransient<TService, TImplementation>(this IServiceRegistrar serviceRegistrar)
        => serviceRegistrar.TryAddTransient(typeof(TService), typeof(TImplementation));

    public static IServiceRegistrar TryAddTransient(this IServiceRegistrar serviceRegistrar, Type service, Type implementationType)
    {
        serviceRegistrar.TryAdd(new ServiceRegistrarDescriptor(service, implementationType, ServiceRegistrarLifetime.Transient));
        return serviceRegistrar;
    }

    public static IServiceRegistrar TryAddTransient<TService>(this IServiceRegistrar serviceRegistrar)
        => serviceRegistrar.TryAddTransient(typeof(TService));

    public static IServiceRegistrar TryAddTransient(this IServiceRegistrar serviceRegistrar, Type service)
    {
        serviceRegistrar.TryAdd(new ServiceRegistrarDescriptor(service, service, ServiceRegistrarLifetime.Transient));
        return serviceRegistrar;
    }

    public static IServiceRegistrar AddTransient<TService, TImplementation>(this IServiceRegistrar serviceRegistrar)
        => serviceRegistrar.AddTransient(typeof(TService), typeof(TImplementation));

    public static IServiceRegistrar AddTransient(this IServiceRegistrar serviceRegistrar, Type service, Type implementationType)
    {
        serviceRegistrar.Add(new ServiceRegistrarDescriptor(service, implementationType, ServiceRegistrarLifetime.Transient));
        return serviceRegistrar;
    }

    public static IServiceRegistrar AddTransient<TService>(this IServiceRegistrar serviceRegistrar)
        => serviceRegistrar.AddTransient(typeof(TService));


    public static IServiceRegistrar AddTransient(this IServiceRegistrar serviceRegistrar, Type service)
    {
        serviceRegistrar.TryAdd(new ServiceRegistrarDescriptor(service, service, ServiceRegistrarLifetime.Transient));
        return serviceRegistrar;
    }

    #endregion

    #region Singleton

    public static IServiceRegistrar TryAddSingleton<TService, TImplementation>(this IServiceRegistrar serviceRegistrar)
        => serviceRegistrar.TryAddSingleton(typeof(TService), typeof(TImplementation));

    public static IServiceRegistrar TryAddSingleton(this IServiceRegistrar serviceRegistrar, Type service, Type implementationType)
    {
        serviceRegistrar.TryAdd(new ServiceRegistrarDescriptor(service, implementationType, ServiceRegistrarLifetime.Singleton));
        return serviceRegistrar;
    }

    public static IServiceRegistrar TryAddSingleton<TService>(this IServiceRegistrar serviceRegistrar)
        => serviceRegistrar.TryAddSingleton(typeof(TService));

    public static IServiceRegistrar TryAddSingleton(this IServiceRegistrar serviceRegistrar, Type service)
    {
        serviceRegistrar.TryAdd(new ServiceRegistrarDescriptor(service, service, ServiceRegistrarLifetime.Singleton));
        return serviceRegistrar;
    }

    public static IServiceRegistrar TryAddSingleton<TService>(this IServiceRegistrar serviceRegistrar, [System.Diagnostics.CodeAnalysis.NotNull] TService implementationInstance)
    {
        Throw.If.ArgumentNull(implementationInstance);
        serviceRegistrar.TryAdd(new ServiceRegistrarDescriptor(typeof(TService), implementationInstance));
        return serviceRegistrar;
    }

    public static IServiceRegistrar AddSingleton<TService, TImplementation>(this IServiceRegistrar serviceRegistrar)
        => serviceRegistrar.AddSingleton(typeof(TService), typeof(TImplementation));

    public static IServiceRegistrar AddSingleton(this IServiceRegistrar serviceRegistrar, Type service, Type implementationType)
    {
        serviceRegistrar.Add(new ServiceRegistrarDescriptor(service, implementationType, ServiceRegistrarLifetime.Singleton));
        return serviceRegistrar;
    }

    public static IServiceRegistrar AddSingleton<TService>(this IServiceRegistrar serviceRegistrar)
        => serviceRegistrar.AddSingleton(typeof(TService));

    public static IServiceRegistrar AddSingleton(this IServiceRegistrar serviceRegistrar, Type service)
    {
        serviceRegistrar.Add(new ServiceRegistrarDescriptor(service, service, ServiceRegistrarLifetime.Singleton));
        return serviceRegistrar;
    }

    public static IServiceRegistrar AddSingleton<TService>(this IServiceRegistrar serviceRegistrar, [System.Diagnostics.CodeAnalysis.NotNull] TService implementationInstance)
    {
        Throw.If.ArgumentNull(implementationInstance);
        serviceRegistrar.Add(new ServiceRegistrarDescriptor(typeof(TService), implementationInstance));
        return serviceRegistrar;
    }

    #endregion
}