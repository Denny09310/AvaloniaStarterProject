using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
using AvaloniaStarterProject.Helpers;
using AvaloniaStarterProject.Views;
using AvaloniaStarterProject.Windows;
using ReactiveUI;
using Splat;
using System;
using System.Reactive;

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
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.MainWindow = new MainWindow
                {
                    DataContext = GetDataContext()
                };
            }
            else if (ApplicationLifetime is ISingleViewApplicationLifetime singleViewPlatform)
            {
                singleViewPlatform.MainView = new MainView
                {
                    DataContext = GetDataContext()
                }; ;
            }

            RxApp.DefaultExceptionHandler = Observer.Create<Exception>(Console.WriteLine);
            ExpressionObserver.DataValidators.RemoveAll(x => x is DataAnnotationsValidationPlugin);

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

        private static object? GetDataContext() => Resolver.GetService<IScreen>();
    }
}