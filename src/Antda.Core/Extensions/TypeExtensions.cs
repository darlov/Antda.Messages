namespace Antda.Core.Extensions;

public static class TypeExtensions
{
    public static bool IsOpenGeneric(this Type type) 
        => type.IsGenericTypeDefinition || type.ContainsGenericParameters;
}