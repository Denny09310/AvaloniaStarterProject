using AvaloniaStarterProject.Generation.Models;
using Microsoft.CodeAnalysis;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AvaloniaStarterProject.Generation.Extensions
{
    internal static class SymbolExtensions
    {
        public static KeyValuePair<string, TypedConstant> GetCommandAttributeReference(this IMethodSymbol method)
        {
            return method.GetAttributes()[0].NamedArguments.FirstOrDefault(x => x.Key == nameof(ReactiveCommandPartsModel.CanExecute));
        }

        public static ReactiveCommandPartsModel GetCommandDefaults(this IMethodSymbol method)
        {
            return new ReactiveCommandPartsModel()
            {
                TParam = method.Parameters.Any() ? method.Parameters[0].Type.Name : ReactiveCommandPartsModel.UnitTypeName,
                CommandName = $"{method.Name}Command",
                MethodName = method.Name,
            };
        }

        public static string? NewMethod(this IMethodSymbol method, KeyValuePair<string, TypedConstant> attribute)
        {
            if (attribute.Value.Value is not string canExecuteMethodName)
                return default;

            return ((INamedTypeSymbol)method.ContainingSymbol.OriginalDefinition)
                                                             .GetMembers()
                                                             .OfType<IMethodSymbol>()
                                                             .FirstOrDefault(x => x.Name == canExecuteMethodName).Name;
        }

        public static bool TryGetTaskResult(this IMethodSymbol method, out string tResult)
        {
            INamedTypeSymbol returnTypeSymbol = (INamedTypeSymbol)method.ReturnType;
            bool isTask = returnTypeSymbol.Name == typeof(Task).Name;

            tResult = returnTypeSymbol.TypeArguments.Any()
                ? returnTypeSymbol.TypeArguments[0].Name
                : ReactiveCommandPartsModel.UnitTypeName;

            return isTask;
        }
    }
}
