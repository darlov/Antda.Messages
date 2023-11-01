namespace Antda.Messages.DependencyInjection;

public interface ISupportRequiredServiceResolver
{
  object GetRequiredService(Type serviceType);
}