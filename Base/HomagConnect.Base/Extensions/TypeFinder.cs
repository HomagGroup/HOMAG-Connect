using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace HomagConnect.Base.Extensions;

/// <summary>
/// Generic type finder.
/// </summary>
public static class TypeFinder
{
    /// <summary>
    /// Gets all non-abstract derived types of the specified base type from the provided assemblies.
    /// </summary>
    public static IEnumerable<Type> FindDerivedTypes(this Type baseType, params Assembly[] assemblies)
    {
        return assemblies
            .SelectMany(a => a.GetTypes())
            .Where(t => baseType.IsAssignableFrom(t) && t != baseType && !t.IsAbstract);
    }

    /// <summary>
    /// Gets all non-abstract derived types of the specified base type from the provided assemblies.
    /// </summary>
    public static IEnumerable<Type> FindDerivedTypes<TBase>(params Assembly[] assemblies)
    {
        var baseType = typeof(TBase);

        return assemblies
            .SelectMany(a => a.GetTypes())
            .Where(t => baseType.IsAssignableFrom(t) && t != baseType && !t.IsAbstract);
    }
}