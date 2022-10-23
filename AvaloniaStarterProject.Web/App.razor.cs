using Avalonia.ReactiveUI;
using Avalonia.Web.Blazor;
using Projektanker.Icons.Avalonia;
using Projektanker.Icons.Avalonia.FontAwesome;

namespace AvaloniaStarterProject.Web
{
    public partial class App
    {
        protected override void OnParametersSet()
        {
            base.OnParametersSet();

            WebAppBuilder.Configure<AvaloniaStarterProject.App>()
                         .UseReactiveUI()
                         .WithIcons(container =>
                                    container.Register<FontAwesomeIconProvider>())
                         .SetupWithSingleViewLifetime();
        }
    }
}