using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Interactivity;
using Avalonia.Media;
using AvaloniaStarterProject.Helpers;
using AvaloniaStarterProject.Models;
using AvaloniaStarterProject.Services.Contracts;
using AvaloniaStarterProject.ViewModels;
using System.Collections.Generic;

namespace AvaloniaStarterProject.Controls
{
    public class ResponsiveNavigationView : ContentControl
    {
        public static readonly StyledProperty<bool> IsPaneOpenProperty =
        AvaloniaProperty.Register<ResponsiveNavigationView, bool>(nameof(IsPaneOpen));

        public bool IsPaneOpen
        {
            get { return GetValue(IsPaneOpenProperty); }
            set { SetValue(IsPaneOpenProperty, value); }
        }

        public static readonly StyledProperty<IEnumerable<NavigationModel>> MenuItemsProperty =
         AvaloniaProperty.Register<ResponsiveNavigationView, IEnumerable<NavigationModel>>(nameof(MenuItems),
              new NavigationModel[]
              {
                  new() { Icon = "fa-home", Header = "Home", ViewModel = typeof(HomeViewModel) },
                  new() { Icon = "fa-cog", Header = "Settings", ViewModel = typeof(SettingsViewModel) }
              });

        public IEnumerable<NavigationModel> MenuItems
        {
            get { return GetValue(MenuItemsProperty); }
            set { SetValue(MenuItemsProperty, value); }
        }


        public static readonly StyledProperty<INavigationService> NavigationServiceProperty =
        AvaloniaProperty.Register<ResponsiveNavigationView, INavigationService>(nameof(NavigationService),
            Resolver.GetService<INavigationService>()!);

        public INavigationService NavigationService
        {
            get { return GetValue(NavigationServiceProperty); }
            set { SetValue(NavigationServiceProperty, value); }
        }

        protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
        {
            var togglePaneButton = e.NameScope.Find<Button>("PART_TogglePane");
            togglePaneButton?.AddHandler(Button.ClickEvent, (s, e) => IsPaneOpen = !IsPaneOpen);

            SetMenuItemsNavigation(e, "PART_DesktopTabControl");
            SetMenuItemsNavigation(e, "PART_MobileTabControl");

            base.OnApplyTemplate(e);
        }

        private void SetMenuItemsNavigation(TemplateAppliedEventArgs e, string controlName)
        {
            var tabControl = e.NameScope.Find<ItemsControl>(controlName);
            bool firstSelected = false;

            if (tabControl?.Items != null)
                foreach (NavigationModel item in tabControl.Items)
                {
                    if (!firstSelected)
                    {
                        item.IsSelected = true;
                        firstSelected = true;
                    }

                    item.SetNavigationService(NavigationService);
                }
        }
    }
}
