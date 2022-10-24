using Microsoft.Extensions.Configuration;

namespace AvaloniaStarterProject.Options.Base;

public class OptionsBase
{
    public string SectionName => GetType().Name.Replace("Options", string.Empty);
}

internal static class OptionsExtensions
{
    public static T AddOption<T>(this IConfiguration configuration) where T : OptionsBase, new()
    {
        T option = new();
        configuration.GetSection(option.SectionName)
                     .Bind(option);

        return option;
    }
}
