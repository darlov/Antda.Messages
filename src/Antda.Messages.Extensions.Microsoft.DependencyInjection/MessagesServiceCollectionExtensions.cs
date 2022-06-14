using System.Reflection;
using Antda.Core.Extensions;
using Antda.Messages.Wrappers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Antda.Messages.Extensions.Microsoft.DependencyInjection;

public static class MessagesServiceCollectionExtensions
{
    public static IServiceCollection AddAntdaMessages(this IServiceCollection services,  IEnumerable<Assembly> assembliesToScan)
    {
        services.TryAddTransient<IMessageSender, MessageSender>();
        services.TryAddTransient<IServiceResolver, MicrosoftServiceResolver>();
        services.TryAddTransient<IHandlerWrapperFactory, HandlerWrapperFactory>();
        services.TryAddTransient(typeof(MessageHandlerWrapper<,>));

        foreach (var typeInfo in assembliesToScan.SelectMany(s => s.DefinedTypes).Where(t => !t.IsOpenGeneric()))
        {
            foreach (var interfaceType in FindConcreteInterfaces(typeInfo, typeof(IMessageHandler<,>)))
            {
                services.AddTransient(interfaceType, typeInfo);
            }
        }

        return services;
    }

    private static IEnumerable<Type> FindConcreteInterfaces(Type? type, Type interfaceToFind)
    {
        if (type == null)
        {
            yield break;
        }

        if (type.IsAbstract || type.IsInterface || type == typeof(object))
        {
            yield break;
        }

        foreach (var interfaceType in type.GetInterfaces().Where(t => t.IsGenericType && t.GetGenericTypeDefinition() == interfaceToFind))
        {
            yield return interfaceType;
        }

        foreach (var interfaceType in FindConcreteInterfaces(type.BaseType, interfaceToFind))
        {
            yield return interfaceType;
        }
    }
}