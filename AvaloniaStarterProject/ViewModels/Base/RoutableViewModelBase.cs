using AvaloniaStarterProject.Helpers;
using ReactiveUI;

namespace AvaloniaStarterProject.ViewModels.Base;

public class RoutableViewModelBase : ViewModelBase, IRoutableViewModel
{
    public string? UrlPathSegment => GetType().Name.Replace("ViewModel", string.Empty);

    public IScreen HostScreen => Resolver.GetService<IScreen>()!;
}
