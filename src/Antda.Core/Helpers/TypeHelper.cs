using System.Reflection;
using Antda.Core.Extensions;

namespace Antda.Core.Helpers;

public static class TypeHelper
{
  public static IEnumerable<Type> FindAllowedTypes(IEnumerable<Assembly> assemblies)
  {
    return assemblies.SelectMany(FindAllowedTypes);
  }

  public static IEnumerable<Type> FindAllowedTypes(Assembly assembly)
  {
    return assembly.DefinedTypes.Where(t => !t.IsOpenGeneric());
  }

  public static IEnumerable<Type> FindGenericInterfaces(Type? typeToScan, Type acceptableInterface)
  {
    if (typeToScan == null)
    {
      yield break;
    }

    if (typeToScan.IsAbstract || typeToScan.IsInterface || typeToScan == typeof(object))
    {
      yield break;
    }

    foreach (var interfaceType in typeToScan.GetInterfaces().Where(t => t.IsGenericType && t.GetGenericTypeDefinition() == acceptableInterface))
    {
      yield return interfaceType;
    }

    foreach (var interfaceType in FindGenericInterfaces(typeToScan.BaseType, acceptableInterface))
    {
      yield return interfaceType;
    }
  }
}