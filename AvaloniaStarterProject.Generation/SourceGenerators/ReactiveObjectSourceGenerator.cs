using AvaloniaStarterProject.Generation.Extensions;
using AvaloniaStarterProject.Generation.Templates;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AvaloniaStarterProject.Generation.SourceGenerators;

[Generator]
internal class ReactiveObjectSourceGenerator : ISourceGenerator
{
    public void Execute(GeneratorExecutionContext context)
    {
        if (context.SyntaxReceiver is not AttributeSyntaxReceiver<ReactiveGeneratedObjectAttribute> syntaxReceiver)
            return;

        foreach (var classSyntax in syntaxReceiver.Classes)
        {
            var model = context.Compilation.GetSemanticModel(classSyntax.SyntaxTree);
            var symbol = model.GetDeclaredSymbol(classSyntax);

            var attribute = classSyntax.AttributeLists.SelectMany(sm => sm.Attributes)
                .First(x => x.Name.ToString()
                                  .EnsureEndsWith("Attribute")
                                  .Equals(nameof(ReactiveGeneratedObjectAttribute)));

            var sourceCode = GetSourceCodeFor(symbol as INamedTypeSymbol);
            context.AddSource($"{symbol?.Name}.g.cs", SourceText.From(sourceCode, Encoding.UTF8));
        }
    }

    public void Initialize(GeneratorInitializationContext context)
    {
        context.RegisterForSyntaxNotifications(() =>
            new AttributeSyntaxReceiver<ReactiveGeneratedObjectAttribute>());
    }

    private string GetCommandInitialization(IEnumerable<IMethodSymbol> reactiveMethods)
    {
        return string.Join(Environment.NewLine, reactiveMethods.Select(x => $"\t{x.ParseCommandParts()}"));
    }

    private string GetEmbededResource(string path)
    {
        using var stream = GetType().Assembly.GetManifestResourceStream(path);

        using var streamReader = new StreamReader(stream);

        return streamReader.ReadToEnd();
    }

    private string? GetNamespaceRecursively(INamespaceSymbol? symbol)
    {
        if (symbol?.ContainingNamespace == null)
        {
            return symbol?.Name;
        }

        return (GetNamespaceRecursively(symbol.ContainingNamespace) + "." + symbol.Name).Trim('.');
    }

    private string GetSourceCodeFor(INamedTypeSymbol? symbol)
    {
        // If template isn't provieded, use default one from embeded resources.
        var template = GetEmbededResource($"AvaloniaStarterProject.Generation.Templates.ReactiveGeneratedObjectTemplate.txt");

        var reactiveMethods = symbol?.GetMembers()
                                     .OfType<IMethodSymbol>()
                                     .Where(x => x.GetAttributes()
                                                  .Any(a => a.AttributeClass?.Name?.EnsureEndsWith("Attribute")
                                                                                   .Equals(nameof(ReactiveCommandAttribute)) ?? false))
                                     ?? Array.Empty<IMethodSymbol>();

        // Can't use scriban at the moment, make it manually for now.
        return template
            .Replace("{{" + nameof(ReactiveGeneratedObjectTemplateParameters.ClassName) + "}}", symbol?.Name)
            .Replace("{{" + nameof(ReactiveGeneratedObjectTemplateParameters.Namespace) + "}}", GetNamespaceRecursively(symbol?.ContainingNamespace))
            .Replace("{{" + nameof(ReactiveGeneratedObjectTemplateParameters.PreferredNamespace) + "}}", symbol?.ContainingAssembly.Name)
            .Replace("{{CommandsDeclaration}}", GetCommandInitialization(reactiveMethods));
    }
}

public class AttributeSyntaxReceiver<TAttribute> : ISyntaxReceiver
   where TAttribute : Attribute
{
    public IList<ClassDeclarationSyntax> Classes { get; } = new List<ClassDeclarationSyntax>();

    public void OnVisitSyntaxNode(SyntaxNode syntaxNode)
    {
        if (syntaxNode is ClassDeclarationSyntax classDeclarationSyntax &&
            classDeclarationSyntax.AttributeLists.Count > 0 &&
            classDeclarationSyntax.AttributeLists
                .Any(al => al.Attributes
                    .Any(a => a.Name.ToString()
                                    .EnsureEndsWith("Attribute")
                                    .Equals(typeof(TAttribute).Name))))
        {
            Classes.Add(classDeclarationSyntax);
        }
    }
}