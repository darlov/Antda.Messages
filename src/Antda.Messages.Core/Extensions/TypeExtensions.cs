using JetBrains.Annotations;

namespace Antda.Messages.Core.Extensions;

[PublicAPI]
public static class TypeExtensions
{
  public static bool IsOpenGeneric(this Type type) => type.IsGenericTypeDefinition || type.ContainsGenericParameters;
}