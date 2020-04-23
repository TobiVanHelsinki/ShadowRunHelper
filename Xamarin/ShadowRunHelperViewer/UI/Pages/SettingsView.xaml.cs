//Author: Tobi van Helsinki

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using ShadowRunHelper;
using ShadowRunHelper.Model;
using ShadowRunHelperViewer.Platform;
using ShadowRunHelperViewer.UI.Pages;
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
    public partial class SettingsView : ContentView, INotifyPropertyChanged
    {
        #region NotifyPropertyChanged
        public new event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion NotifyPropertyChanged

        public AppModel Model => AppModel.Instance;
        public SettingsModel Settings => SettingsModel.Instance;
        public IEnumerable<string> Styles => Constants.StyleNames;
        public IEnumerable<string> Spacings => Constants.Spacings;

        public SettingsView()
        {
            BindingContext = this;
            InitializeComponent();
        }

        public IEnumerable<SubMenuAction> AfterLoad(ProjectPagesOptions pageOptions)
        {
            Features.Ui.IsCustomTitleBarEnabled = true; //TODO Dispse?
            Features.Ui.SetCustomTitleBar(DependencyService.Get<IFormsInteractions>().GetRenderer(TitleBar));
            Features.Ui.CustomTitleBarChanges += Ui_CustomTitleBarChanges; ; //TODO Dispose
            Features.Ui.TriggerCustomTitleBarChanges();
            return Array.Empty<SubMenuAction>();
        }

        private void Ui_CustomTitleBarChanges(double LeftSpace, double RigthSpace, double Heigth)
        {
            TitleBar.MinimumHeightRequest = Heigth;
            Intro1Text.Margin = new Thickness(Math.Abs(LeftSpace), 0, Math.Abs(RigthSpace), 0);
        }

        private void ScrollView_BindingContextChanged(object sender, EventArgs e)
        {

        }
    }
}