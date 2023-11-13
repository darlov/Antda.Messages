namespace Antda.Messages.Core.DependencyInjection;

public interface IServiceResolver
{
  object? GetService(Type serviceType);
}