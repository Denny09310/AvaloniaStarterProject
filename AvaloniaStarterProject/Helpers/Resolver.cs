using Splat;
using System;
using System.Linq;
using System.Reflection;

namespace AvaloniaStarterProject.Helpers;

internal static class Resolver
{
    private static IReadonlyDependencyResolver? _resolver;

    public static IReadonlyDependencyResolver Register(this IReadonlyDependencyResolver resolver)
    {
        _resolver = resolver;

        return resolver;
    }

    public static T? GetService<T>()
    {
        if (_resolver is null) return default;

        return _resolver.GetService<T>();
    }

    public static void ResolveDependencies<T>(this T obj) where T : class
    {
        if (_resolver is null) return;

        var dependencyResolver = typeof(Resolver).GetMethod(nameof(GetService));
        var dependencies = typeof(T).GetRuntimeFields()
                                    .Where(f => Attribute.IsDefined(f, typeof(ResolverDependencyAttribute)));

        foreach (var dependency in dependencies)
        {
            dependency.SetValue(obj, dependencyResolver?
                .MakeGenericMethod(dependency.FieldType)
                .Invoke(_resolver, null));
        }
    }
}

[AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
internal sealed class ResolverDependencyAttribute : Attribute
{
    public ResolverDependencyAttribute()
    {
    }
}