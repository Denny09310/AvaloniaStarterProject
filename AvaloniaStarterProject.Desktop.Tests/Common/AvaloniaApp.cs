using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Headless;
using Avalonia.ReactiveUI;
using Avalonia.Threading;
using AvaloniaStarterProject.Helpers;
using AvaloniaStarterProject.Windows;
using Projektanker.Icons.Avalonia;
using Projektanker.Icons.Avalonia.FontAwesome;
using Splat;

namespace AvaloniaStarterProject.Desktop.Tests.Common;

public static class AvaloniaApp
{
    public static AppBuilder BuildAvaloniaApp() =>
            AppBuilder
                .Configure<App>()
                .UseReactiveUI()
                .UsePlatformDetect()
                .WithIcons(c => c.Register<FontAwesomeIconProvider>())
                .UseHeadless(new()
                {
                    UseCompositor = true,
                });

    public static IClassicDesktopStyleApplicationLifetime? GetApp() =>
                Application.Current?.ApplicationLifetime as IClassicDesktopStyleApplicationLifetime;

    public static MainWindow? GetMainWindow() => GetApp()?.MainWindow as MainWindow;

    // DI registrations
    public static void RegisterDependencies() =>
        Locator.CurrentMutable.RegisterConfiguration()
                              .RegisterServices()
                              .RegisterRoutes();

    // stop app and cleanup
    public static void Stop()
    {
        var app = GetApp();
        if (app is IDisposable disposable)
        {
            Dispatcher.UIThread.Post(disposable.Dispose);
        }

        Dispatcher.UIThread.Post(() => app?.Shutdown());
    }
}