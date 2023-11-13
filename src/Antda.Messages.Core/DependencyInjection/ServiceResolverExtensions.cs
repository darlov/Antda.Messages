using Antda.Messages.Core.Exceptions;

namespace Antda.Messages.Core.DependencyInjection;

public static class ServiceResolverExtensions
{
  public static T GetRequiredService<T>(this IServiceResolver resolver) where T : notnull
  {
    Throw.If.ArgumentNull(resolver);

    return (T)resolver.GetRequiredService(typeof(T));
  }

  public static object GetRequiredService(this IServiceResolver resolver, Type serviceType)
  {
    Throw.If.ArgumentNull(resolver);
    Throw.If.ArgumentNull(serviceType);

    if (resolver is ISupportRequiredServiceResolver requiredServiceSupportingProvider)
    {
      return requiredServiceSupportingProvider.GetRequiredService(serviceType);
    }

    var service = resolver.GetService(serviceType);
    if (service == null)
    {
      throw new InvalidOperationException($"No service for type '{serviceType}' has been registered.");
    }

    return service;
  }
}