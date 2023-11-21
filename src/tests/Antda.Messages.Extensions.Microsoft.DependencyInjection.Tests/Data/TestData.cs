namespace Antda.Messages.Extensions.Microsoft.DependencyInjection.Tests.Data;

public class TestData<T>
{
  public TestData(T value)
  {
    Value = value;
  }

  public T Value { get; init; }
}