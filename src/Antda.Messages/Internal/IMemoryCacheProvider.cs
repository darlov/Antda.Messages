namespace Antda.Messages.Internal;

public interface IMemoryCacheProvider<T>
{
    T GetOrAdd<TKey>(TKey key, Func<TKey, T> factoryFunc);
}