//Author: Tobi van Helsinki

using ShadowRunHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ShadowRunHelperViewer.UI.ControlsOther
{
    internal class SpacingConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value switch
            {
                Constants.SpacingCompactValue => Constants.SpacingCompact,
                Constants.SpacingMediumValue => Constants.SpacingMedium,
                Constants.SpacingWideValue => Constants.SpacingWide,
                _ => "Error",
            };
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value switch
            {
                Constants.SpacingCompact => Constants.SpacingCompactValue,
                Constants.SpacingWide => Constants.SpacingWideValue,
                _ => Constants.SpacingMediumValue,
            };
        }
    }

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsView : ContentView
    {
        public SettingsModel Settings => SettingsModel.Instance;
        public IEnumerable<string> Styles => Constants.StyleNames;
        public IEnumerable<string> Spacings => Constants.Spacings;

        public SettingsView()
        {
            BindingContext = this;
            InitializeComponent();
        }
    }
}