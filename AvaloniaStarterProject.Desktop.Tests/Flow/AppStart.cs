using Avalonia.Controls;
using Avalonia.VisualTree;
using AvaloniaStarterProject.Desktop.Tests.Common;
using AvaloniaStarterProject.Views;
using AvaloniaStarterProject.Windows;

namespace AvaloniaStarterProject.Desktop.Tests.Flow;

public class AppStart
{
    [Fact(DisplayName = "Application startup")]
    public async Task AppStartTest()
    {
        var app = AvaloniaApp.GetApp();
        Assert.NotNull(app);

        var window = app.Windows.OfType<MainWindow>()
                                .SingleOrDefault();
        Assert.NotNull(window);

        await Task.Delay(100);

        var view = window.GetVisualDescendants()
                         .OfType<HomeView>()
                         .SingleOrDefault();

        Assert.NotNull(view);

        var greetingText = view.GetVisualDescendants()
                               .OfType<TextBlock>()
                               .FirstOrDefault();

        Assert.NotNull(greetingText);
        Assert.Equal("Welcome to Avalonia!", greetingText.Text);
    }
}
