using Antda.Messages.Core.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

namespace Antda.Messages.Extensions.Microsoft.DependencyInjection;

public class MicrosoftDependencyInjectionServiceResolver : IServiceResolver, ISupportRequiredServiceResolver
{
  private readonly IServiceProvider _serviceProvider;

  public MicrosoftDependencyInjectionServiceResolver(IServiceProvider serviceProvider)
  {
    _serviceProvider = serviceProvider;
  }

  public object? GetService(Type serviceType) => _serviceProvider.GetService(serviceType);

  public object GetRequiredService(Type serviceType) => _serviceProvider.GetRequiredService(serviceType);

}