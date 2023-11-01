namespace Antda.Messages.Internal;

public interface IMemoryCacheProvider<TKey, T>
  where TKey : notnull
{
  T GetOrAdd(TKey key, Func<TKey, T> factoryFunc);

  T GetOrAdd<TArg>(TKey key, Func<TKey, TArg, T> factoryFunc, TArg factoryArgument);
}