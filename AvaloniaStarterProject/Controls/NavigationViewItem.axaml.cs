using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Styling;
using System;

namespace AvaloniaStarterProject.Controls
{
    public class NavigationViewItem : RadioButton, IStyleable
    {
        Type IStyleable.StyleKey => typeof(NavigationViewItem);

        public static readonly StyledProperty<string?> IconProperty =
        AvaloniaProperty.Register<NavigationViewItem, string?>(nameof(Icon));

        public string? Icon
        {
            get { return GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }

        public static readonly StyledProperty<string?> HeaderProperty =
        AvaloniaProperty.Register<NavigationViewItem, string?>(nameof(Header));

        public string? Header
        {
            get { return GetValue(HeaderProperty); }
            set { SetValue(HeaderProperty, value); }
        }

        public static readonly StyledProperty<Orientation> OrientationProperty =
        AvaloniaProperty.Register<NavigationViewItem, Orientation>(nameof(Orientation));

        public Orientation Orientation
        {
            get { return GetValue(OrientationProperty); }
            set { SetValue(OrientationProperty, value); }
        }

        public static readonly StyledProperty<int> IconSizeProperty =
        AvaloniaProperty.Register<NavigationViewItem, int>(nameof(IconSize), 16);

        public int IconSize
        {
            get { return GetValue(IconSizeProperty); }
            set { SetValue(IconSizeProperty, value); }
        }

        public static readonly StyledProperty<int> SpacingProperty =
        AvaloniaProperty.Register<NavigationViewItem, int>(nameof(Spacing));

        public int Spacing
        {
            get { return GetValue(SpacingProperty); }
            set { SetValue(SpacingProperty, value); }
        }
    }
}