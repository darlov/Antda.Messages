namespace Antda.Messages.Core.DependencyInjection;

public interface ISupportRequiredServiceResolver
{
  object GetRequiredService(Type serviceType);
}