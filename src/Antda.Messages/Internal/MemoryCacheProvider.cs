using System.Collections.Concurrent;
using Antda.Core.Exceptions;

namespace Antda.Messages.Internal;

public class MemoryCacheProvider<T> : IMemoryCacheProvider<T>
{
    private readonly ConcurrentDictionary<object, T> _dictionary;

    public MemoryCacheProvider()
    {
        _dictionary = new ConcurrentDictionary<object, T>();
    }

    public T GetOrAdd<TKey>(TKey key, Func<TKey, T> factoryFunc)
    {
        Throw.If.ArgumentNull(key);
        Throw.If.ArgumentNull(factoryFunc);

        return _dictionary.GetOrAdd(key, static (keyValue, factoryFuncValue) => factoryFuncValue((TKey)keyValue), factoryFunc);
    }
}