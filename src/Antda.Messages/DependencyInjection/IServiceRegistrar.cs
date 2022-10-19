
namespace Antda.Messages.DependencyInjection;

public interface IServiceRegistrar
{
    void TryAdd(ServiceRegistrarDescriptor descriptor);

    void Add(ServiceRegistrarDescriptor descriptor);

}