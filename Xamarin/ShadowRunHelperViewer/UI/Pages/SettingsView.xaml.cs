//Author: Tobi van Helsinki

using ShadowRunHelper;
using ShadowRunHelper.Model;
using ShadowRunHelperViewer.Platform.Xam;
using ShadowRunHelperViewer.UI.Pages;
using SharedCode.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
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
                Constants.SpacingCompact => UiResources.SpacingCompact,
                Constants.SpacingWide => UiResources.SpacingWide,
                _ => UiResources.SpacingMedium,
            };
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value as string == UiResources.SpacingCompact)
            {
                return Constants.SpacingCompact;
            }
            else if (value as string == UiResources.SpacingWide)
            {
                return Constants.SpacingWide;
            }
            else
            {
                return Constants.SpacingMedium;
            }
        }
    }

    internal class StyleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value switch
            {
                Constants.StyleDark => UiResources.StyleDark,
                Constants.StyleScaryGreen => UiResources.StyleScaryGreen,
                _ => UiResources.StyleBrigth,
            };
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value as string == UiResources.StyleDark)
            {
                return Constants.StyleDark;
            }
            else if (value as string == UiResources.StyleScaryGreen)
            {
                return Constants.StyleScaryGreen;
            }
            else
            {
                return Constants.StyleBrigth;
            }
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
        public string[] Styles => Constants.StyleNames.Select(x => UiResources.ResourceManager.GetStringSafe(x)).ToArray();
        public string[] Spacings => Constants.Spacings.Select(x => UiResources.ResourceManager.GetStringSafe(x)).ToArray();

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