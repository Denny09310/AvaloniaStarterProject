using Avalonia.ReactiveUI;
using AvaloniaStarterProject.ViewModels;

namespace AvaloniaStarterProject.Views
{
    public partial class HomeView : ReactiveUserControl<HomeViewModel>
    {
        public HomeView()
        {
            InitializeComponent();
        }
    }
}