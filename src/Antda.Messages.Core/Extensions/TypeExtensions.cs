using JetBrains.Annotations;

namespace Antda.Messages.Core.Extensions;

[PublicAPI]
public static class TypeExtensions
{
  public static bool IsOpenGeneric(this Type type)
  {
    return type.IsGenericTypeDefinition || type.ContainsGenericParameters;
  }
}