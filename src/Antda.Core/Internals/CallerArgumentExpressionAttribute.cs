using System.Diagnostics.CodeAnalysis;

#if !NET5_0_OR_GREATER
// ReSharper disable once CheckNamespace
namespace System.Runtime.CompilerServices;

/// <summary>
/// Allows capturing of the expressions passed to a method.
/// </summary>
[AttributeUsage(AttributeTargets.Parameter)]
[ExcludeFromCodeCoverage]
internal sealed class CallerArgumentExpressionAttribute : Attribute
{
  public CallerArgumentExpressionAttribute(string parameterName)
  {
    ParameterName = parameterName;
  }

  public string ParameterName { get; }
}
#endif