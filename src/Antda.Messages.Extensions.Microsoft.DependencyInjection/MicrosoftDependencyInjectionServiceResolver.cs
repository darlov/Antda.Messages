using Antda.Messages.Core.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

namespace Antda.Messages.Extensions.Microsoft.DependencyInjection;

public class MicrosoftDependencyInjectionServiceResolver(IServiceProvider serviceProvider) : IServiceResolver, ISupportRequiredServiceResolver
{
  public object? GetService(Type serviceType) => serviceProvider.GetService(serviceType);

  public object GetRequiredService(Type serviceType) => serviceProvider.GetRequiredService(serviceType);

}