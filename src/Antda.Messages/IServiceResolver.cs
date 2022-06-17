namespace Antda.Messages;

public interface IServiceResolver
{
  object? GetService(Type serviceType);
}