using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;

namespace Antda.Core.Exceptions;

public static class Throw
{
  public static class If
  {
    [ContractAnnotation("argument:null=>halt;")]
    public static void ArgumentNull([System.Diagnostics.CodeAnalysis.NotNull] [NoEnumeration] object? argument, [CallerArgumentExpression("argument")] string? paramName = null)
    {
      if (argument is null)
      {
        ThrowArgumentNull(paramName);
      }
    }

    [ContractAnnotation("argument:null=>halt;")]
    public static void ArgumentNullOrEmpty([System.Diagnostics.CodeAnalysis.NotNull] string? argument, [CallerArgumentExpression("argument")] string? paramName = null)
    {
      if (argument is null)
      {
        ThrowArgumentNull(paramName);
      }

      if (argument is { Length: 0 })
      {
        throw new ArgumentException("Argument cannot be empty.", paramName);
      }
    }

    [DoesNotReturn]
    private static void ThrowArgumentNull(string? paramName)
    {
      throw new ArgumentNullException(paramName);
    }
  }
}