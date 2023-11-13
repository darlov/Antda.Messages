
namespace Antda.Messages.Core.DependencyInjection;

public interface IServiceRegistrar
{
    void TryAdd(ServiceRegistrarDescriptor descriptor);

    void Add(ServiceRegistrarDescriptor descriptor);

}