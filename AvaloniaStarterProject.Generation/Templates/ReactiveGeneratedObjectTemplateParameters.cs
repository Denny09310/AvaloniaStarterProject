namespace AvaloniaStarterProject.Generation.Templates;

internal class ReactiveGeneratedObjectTemplateParameters
{
    public ReactiveGeneratedObjectTemplateParameters(string? typeName, string? typeNamespace, string? rootNamespace)
    {
        ClassName = typeName;
        Namespace = typeNamespace;
        PreferredNamespace = rootNamespace;
    }

    public string? ClassName { get; set; }
    public string? Namespace { get; set; }
    public string? PreferredNamespace { get; set; }
}