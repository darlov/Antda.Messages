namespace Antda.Messages.DependencyInjection;

public interface IServiceResolver
{
  object? GetService(Type serviceType);
}