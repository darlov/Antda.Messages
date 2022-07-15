namespace Antda.Messages.Extensions.Microsoft.DependencyInjection;

public class MicrosoftServiceResolver : IServiceResolver
{
  private readonly IServiceProvider _serviceProvider;

  public MicrosoftServiceResolver(IServiceProvider serviceProvider)
  {
    _serviceProvider = serviceProvider;
  }

  public object? GetService(Type serviceType)
  {
    return _serviceProvider.GetService(serviceType);
  }
}