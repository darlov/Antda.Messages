using JetBrains.Annotations;

namespace Antda.Messages;

[PublicAPI]
public readonly record struct Unit : IComparable<Unit>, IComparable
{

  // ReSharper disable once InconsistentNaming
  private static readonly Unit _value;

  public static ref readonly Unit Value => ref _value;

  public int CompareTo(Unit other) => 0;

  public int CompareTo(object? other)
  {
    if (other is null)
    {
      return 1;
    }
    
    if (other.GetType() != this.GetType())
    {
      throw new ArgumentException($"Object must be of type {this.GetType()}", nameof(other));
    }

    return CompareTo((Unit)other);
  }
}