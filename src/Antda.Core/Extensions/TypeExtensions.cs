namespace Antda.Core.Extensions;

public static class TypeExtensions
{
  public static bool IsOpenGeneric(this Type type)
  {
    return type.IsGenericTypeDefinition || type.ContainsGenericParameters;
  }
}