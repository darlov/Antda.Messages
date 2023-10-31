using System.Collections;
using System.Collections.Concurrent;
using Antda.Core.Exceptions;

namespace Antda.Messages.Internal;

public class MemoryCacheProvider<T> : IMemoryCacheProvider<T>
{
    private readonly ConcurrentDictionary<Type, IDictionary> _caches = new();

    public T GetOrAdd<TKey>(TKey key, Func<TKey, T> factoryFunc) where TKey : notnull
    {
        Throw.If.ArgumentNull(key);
        Throw.If.ArgumentNull(factoryFunc);

        var cache = (ConcurrentDictionary<TKey, T>)_caches.GetOrAdd(typeof(TKey), static (_) => new ConcurrentDictionary<TKey, T>());
        return cache.GetOrAdd(key, static (keyValue, factoryFuncValue) => factoryFuncValue(keyValue), factoryFunc);
    }
}