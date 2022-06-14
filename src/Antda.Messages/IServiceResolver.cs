namespace Antda.Messages;

public interface IServiceResolver
{
    object GetRequiredService(Type serviceType);
}