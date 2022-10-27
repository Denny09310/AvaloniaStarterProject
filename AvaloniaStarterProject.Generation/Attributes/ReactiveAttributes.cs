using System;

namespace ReactiveUI;

[AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
public sealed class ReactiveGeneratedObjectAttribute : Attribute
{
}

/// <inheritdoc/>
[AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
public sealed class ReactiveCommandAttribute : Attribute
{
    public string? CanExecute { get; set; }
}