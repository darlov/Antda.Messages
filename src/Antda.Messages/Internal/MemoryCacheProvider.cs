using System.Collections.Concurrent;
using Antda.Core.Exceptions;

namespace Antda.Messages.Internal;

public class MemoryCacheProvider<TKey, T> : IMemoryCacheProvider<TKey, T> 
    where TKey : notnull
{
    private readonly ConcurrentDictionary<TKey, T> _cache = new();

    public T GetOrAdd(TKey key, Func<TKey, T> factoryFunc)
    {
        Throw.If.ArgumentNull(key);
        Throw.If.ArgumentNull(factoryFunc);
        return _cache.GetOrAdd(key, factoryFunc);
    }
    
    public T GetOrAdd<TArg>(TKey key, Func<TKey, TArg, T> factoryFunc, TArg factoryArgument)
    {
        Throw.If.ArgumentNull(key);
        Throw.If.ArgumentNull(factoryFunc);
        Throw.If.ArgumentNull(factoryArgument);

        return _cache.GetOrAdd(key, factoryFunc, factoryArgument);
    }
}