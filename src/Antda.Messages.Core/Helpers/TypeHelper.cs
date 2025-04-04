using System.Reflection;
using Antda.Messages.Core.Extensions;
using JetBrains.Annotations;

namespace Antda.Messages.Core.Helpers;

[PublicAPI]
public static class TypeHelper
{
  public static IEnumerable<Type> FindAllowedTypes(IEnumerable<Assembly> assemblies) => assemblies.SelectMany(FindAllowedTypes);

  public static IEnumerable<Type> FindAllowedTypes(Assembly assembly) => assembly.DefinedTypes.Where(t => !t.IsOpenGeneric());

  public static IEnumerable<Type> FindTypes(Type? typeToScan, Type acceptableType)
    => FindTypesInternal(typeToScan, acceptableType).Distinct();

  private static IEnumerable<Type> FindTypesInternal(Type? typeToScan, Type acceptableType)
  {
    if (typeToScan == null || typeToScan == typeof(object))
    {
      yield break;
    }
    
    if (typeToScan == acceptableType)
    {
      yield return typeToScan;
    }

    if (acceptableType.IsInterface)
    {
      foreach (var @interface in typeToScan.GetInterfaces())
      {
        if (acceptableType.IsGenericType && @interface.IsGenericType)
        {
          if (acceptableType.IsOpenGeneric())
          {
            if (acceptableType.GetGenericTypeDefinition() == @interface.GetGenericTypeDefinition())
            {
              yield return @interface;
            }
          }
          else if (acceptableType == @interface)
          {
            yield return @interface;
          }
        }
        else if(acceptableType == @interface)
        {
          yield return @interface;
        }
      }
    }
    else if (acceptableType.IsGenericType && typeToScan.IsGenericType)
    {
      if (acceptableType.IsOpenGeneric())
      {
        if (typeToScan.GetGenericTypeDefinition() == acceptableType.GetGenericTypeDefinition())
        {
          yield return typeToScan;
        }
      }
    }

    foreach (var foundBaseType in FindTypes(typeToScan.BaseType, acceptableType))
    {
      yield return foundBaseType;
    }
  }
}