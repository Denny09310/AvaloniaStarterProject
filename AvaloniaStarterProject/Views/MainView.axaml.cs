using Avalonia.ReactiveUI;
using AvaloniaStarterProject.ViewModels;

namespace AvaloniaStarterProject.Views
{
    public partial class MainView : ReactiveUserControl<MainViewModel>
    {
        public MainView()
        {
            InitializeComponent();
        }
    }
}