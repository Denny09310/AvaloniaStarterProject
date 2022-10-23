using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using AvaloniaStarterProject.Helpers;
using AvaloniaStarterProject.Views;
using AvaloniaStarterProject.Windows;
using ReactiveUI;
using Splat;

namespace AvaloniaStarterProject
{
    public partial class App : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            var dataContext = Resolver.GetService<IScreen>();

            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.MainWindow = new MainWindow
                {
                    DataContext = dataContext
                };
            }
            else if (ApplicationLifetime is ISingleViewApplicationLifetime singleViewPlatform)
            {
                singleViewPlatform.MainView = new MainView
                {
                    DataContext = dataContext
                };
            }

            base.OnFrameworkInitializationCompleted();
        }

        public override void RegisterServices()
        {
            base.RegisterServices();

            Locator.Current.Register();

            Locator.CurrentMutable.RegisterConfiguration()
                                  .RegisterServices()
                                  .RegisterRoutes();
        }
    }
}