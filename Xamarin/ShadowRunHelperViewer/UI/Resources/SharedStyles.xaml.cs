//Author: Tobi van Helsinki

using Rg.Plugins.Popup.Services;
using ShadowRunHelper;
using ShadowRunHelperViewer.UI.Resources;
using TLIB;
using Xamarin.Forms;

namespace ShadowRunHelperViewer
{
    public partial class SharedStyles
    {
        public static void DisplayDefaultPopUp(View v)
        {
            PopupNavigation.Instance.PushAsync(new RgPopUp(v));
        }

        public SharedStyles()
        {
            Add("Spacing", SettingsModel.I.CurrentSpacingStrategy);
            Add("SpacingS", (SettingsModel.I.CurrentSpacingStrategy - 1).LowerB(0));
            Add("SpacingM", SettingsModel.I.CurrentSpacingStrategy);
            Add("SpacingL", SettingsModel.I.CurrentSpacingStrategy + 2);
            Add("CurrentBackgroundColor", StyleManager.BackgroundColor);
            Add("CurrentForegroundColor", StyleManager.ForegroundColor);
            Add("CurrentTextColor", StyleManager.TextColor);
            Add("CurrentAccentColor", StyleManager.AccentColor);
            Add("CurrentAccentHighColor", StyleManager.AccentHighColor);
            Add("CurrentAccentLowColor", StyleManager.AccentLowColor);
            Add("CurrentButtonColor", StyleManager.ButtonColor);
            Add("CornerRadiusSize", StyleManager.CornerRadius);

            InitializeComponent();
        }
    }
}