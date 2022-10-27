using AvaloniaStarterProject.Generation.Models;
using Microsoft.CodeAnalysis;

namespace AvaloniaStarterProject.Generation.Extensions;

internal static class ReactiveCommandPartsExtensions
{
    internal static ReactiveCommandPartsModel ParseCommandParts(this IMethodSymbol method)
    {
        ReactiveCommandPartsModel commandParts = method.GetCommandDefaults();

        var attribute = method.GetCommandAttributeReference();
        commandParts.CanExecute = method.NewMethod(attribute);

        if (method.TryGetTaskResult(out string tResult))
        {
            commandParts.IsTask = true;
            commandParts.TResult = tResult;
        }
        else
        {
            commandParts.TResult = method.ReturnsVoid ? ReactiveCommandPartsModel.UnitTypeName : method.ReturnType.Name;
        }

        return commandParts;
    }
}