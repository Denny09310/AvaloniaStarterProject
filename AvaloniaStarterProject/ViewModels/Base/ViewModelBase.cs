using AvaloniaStarterProject.Helpers;
using AvaloniaStarterProject.Services.Contracts;
using ReactiveUI;
using System.Reactive.Disposables;
using System.Threading.Tasks;

namespace AvaloniaStarterProject.ViewModels.Base
{
    public class ViewModelBase : ReactiveObject, IActivatableViewModel
    {
        #region Dependencies

        [ResolverDependency]
        protected readonly INavigationService _navigationService = null!;

        #endregion

        public ViewModelBase()
        {
            this.WhenActivated(async (d) =>
            {
                await HandleActivation();

                Disposable
                    .Create(async () =>
                    {
                        await HandleDeactivation();
                    })
                    .DisposeWith(d);
            });
        }

        public ViewModelActivator Activator { get; } = new();

        protected virtual Task HandleActivation()
        {
            this.ResolveDependencies();

            return Task.CompletedTask;
        }

        protected virtual Task HandleDeactivation()
        {
            return Task.CompletedTask;
        }
    }
}
