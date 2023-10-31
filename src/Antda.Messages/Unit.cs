using JetBrains.Annotations;

namespace Antda.Messages;

[PublicAPI]
public record Unit
{
  // ReSharper disable once InconsistentNaming
  private static readonly Unit _value = new();

  public static ref readonly Unit Value => ref _value;
}