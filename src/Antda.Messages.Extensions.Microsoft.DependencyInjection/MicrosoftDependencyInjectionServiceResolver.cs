using Antda.Messages.DependencyInjection;

namespace Antda.Messages.Extensions.Microsoft.DependencyInjection;

public class MicrosoftDependencyInjectionServiceResolver : IServiceResolver
{
  private readonly IServiceProvider _serviceProvider;

  public MicrosoftDependencyInjectionServiceResolver(IServiceProvider serviceProvider)
  {
    _serviceProvider = serviceProvider;
  }

  public object? GetService(Type serviceType) => _serviceProvider.GetService(serviceType);
}