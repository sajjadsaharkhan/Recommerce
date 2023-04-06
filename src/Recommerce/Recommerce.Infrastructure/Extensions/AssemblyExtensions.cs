using System.Reflection;
using JetBrains.Annotations;

namespace Project.Infrastructure.Extensions;

[PublicAPI]
public static class AssemblyExtensions
{
    /// <summary>
    /// Get a list of all types matching whereClause from all loadable assemblies 
    /// </summary>
    /// <param name="whereClause"></param>
    /// <returns></returns>
    public static IEnumerable<Type> GetAllLoadableAssemblyTypes(Func<Type, bool> whereClause)
    {
        return AppDomain.CurrentDomain
            .GetAssemblies()
            .SelectMany(GetLoadableTypes)
            .Where(whereClause!);
    }

    /// <summary>
    /// Get all types included in an assembly which are loadable
    /// </summary>
    /// <param name="assembly"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static IEnumerable<Type> GetLoadableTypes(this Assembly assembly)
    {
        if (assembly is null)
            throw new ArgumentNullException(nameof(assembly));

        try
        {
            return assembly.GetTypes();
        }
        catch (ReflectionTypeLoadException e)
        {
            return e.Types.Where(t => t != null);
        }
    }

    /// <summary>
    /// Is used to determine if a type (Class A) implemented & inherited from another type (Class B)
    /// </summary>
    /// <param name="type"></param>
    /// <param name="lookup">target type (class B) to check if inherited (by class A)</param>
    /// <returns></returns>
    public static bool IsInheritedFrom(this Type type, Type lookup)
    {
        while (true)
        {
            var baseType = type.BaseType;
            if (baseType is null)
                return false;

            if (baseType.IsGenericType && baseType.GetGenericTypeDefinition() == lookup)
                return true;

            type = baseType;
        }
    }
}