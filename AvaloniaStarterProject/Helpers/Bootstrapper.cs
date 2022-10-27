using AvaloniaStarterProject.Services;
using AvaloniaStarterProject.Services.Contracts;
using AvaloniaStarterProject.ViewModels;
using Microsoft.Extensions.Configuration;
using ReactiveUI;
using Splat;
using System;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("AvaloniaStarterProject.Desktop.Tests")]
[assembly: InternalsVisibleTo("AvaloniaStarterProject.Android.Tests")]
namespace AvaloniaStarterProject.Helpers;

internal static class Bootstrapper
{
    public static IConfiguration Configuration => Resolver.GetService<IConfiguration>()
        ?? throw new InvalidOperationException("The configuration are not registered in the DI Container");

    public static IMutableDependencyResolver RegisterConfiguration(this IMutableDependencyResolver resolver)
    {
        return resolver.RegisterConstantAnd(BuildConfiguration());
    }

    public static IMutableDependencyResolver RegisterServices(this IMutableDependencyResolver resolver)
    {
        return resolver.RegisterLazySingletonAnd<INavigationService>(() => new NavigationService());
    }

    public static IMutableDependencyResolver RegisterRoutes(this IMutableDependencyResolver resolver)
    {
        resolver.RegisterConstantAnd<IScreen>(new MainViewModel())
                       .RegisterAnd<HomeViewModel>()
                       .RegisterAnd<CounterViewModel>()
                       .RegisterAnd<SettingsViewModel>()
                       .RegisterViewsForViewModels(Assembly.GetAssembly(typeof(App))!);

        return resolver;
    }

    private static IConfiguration BuildConfiguration()
    {
        var assembly = Assembly.GetAssembly(typeof(App));
        var configuration = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddInMemoryCollection();

        string? name = assembly?.GetName().Name;

        using Stream? appSettingsStream = assembly?.GetManifestResourceStream($"{name}.appsettings.json");
        configuration.AddJsonStream(appSettingsStream);

        return configuration.Build();
    }

    // TODO: Per aggiungere configurazioni dall'appsettings 
    //       utilizzare il seguente metodo
    //private static OptionsBase BuildOptions()
    //    => Configuration.AddOption<OptionsBase>();
}