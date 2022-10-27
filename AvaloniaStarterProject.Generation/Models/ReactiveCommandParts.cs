using AvaloniaStarterProject.Generation.Extensions;
using System.Linq;
using System.Text;

namespace AvaloniaStarterProject.Generation.Models;

internal class ReactiveCommandPartsModel
{
    public const string UnitTypeName = "Unit";

    public string? CanExecute { get; set; }
    public string CommandName { get; set; } = string.Empty;
    public bool IsTask { get; set; }
    public string MethodName { get; set; } = string.Empty;
    public string TParam { get; set; } = string.Empty;
    public string TResult { get; set; } = string.Empty;

    public override string ToString()
    {
        StringBuilder commandBuilder = new();
        string privateCommandName = $"_{CommandName.ToCamelCase()}";

        commandBuilder.AppendLine($"private ReactiveCommand<{TParam},{TResult}> {privateCommandName};");
        commandBuilder.AppendLine($"\tpublic ReactiveCommand<{TParam},{TResult}> {CommandName.ToPascalCase()} " +
                                  $"=> {privateCommandName} ??= ReactiveCommand.{GetCommandFactoryMethod()};");

        return commandBuilder.ToString();
    }

    private string GetCommandFactoryMethod()
    {
        var types = new[] { IsUnit(TResult), IsUnit(TParam) }.Where(x => x != null);

        string genericTypes = types.Any() ? $"<{string.Join(",", types)}>" : string.Empty;
        string methodCallback = CanExecute is { } ? $"{MethodName}, {CanExecute}()" : $"{MethodName}";
        string factoryType = IsTask ? "CreateFromTask" : "Create";

        return $"{factoryType}{genericTypes}({methodCallback})";
    }

    private string? IsUnit(string? value) => value == UnitTypeName ? null : value;
}