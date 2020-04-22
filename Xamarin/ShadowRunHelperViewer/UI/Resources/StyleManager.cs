//Author: Tobi van Helsinki

using ShadowRunHelper;
using Xamarin.Forms;

namespace ShadowRunHelperViewer.UI.Resources
{
    internal static class StyleManager
    {
        internal static Color BackgroundColor => SettingsModel.I.CurrentStyleName switch
        {
            Constants.StyleBrigth => Color.Beige,
            Constants.StyleDark => Color.DarkSlateGray,
            Constants.StyleScaryGreen => Color.DarkGreen,
            _ => Color.Transparent,
        };

        internal static Color ForegroundColor => SettingsModel.I.CurrentStyleName switch
        {
            Constants.StyleBrigth => Color.DarkSlateGray,
            Constants.StyleDark => Color.FromRgb(255, 248, 231), //Cosmic Latte
            Constants.StyleScaryGreen => Color.GreenYellow,
            _ => Color.Transparent,
        };

        internal static Color TextColor => SettingsModel.I.CurrentStyleName switch
        {
            Constants.StyleBrigth => Color.Black,
            Constants.StyleDark => Color.FromRgb(255, 248, 231), //Cosmic Latte
            Constants.StyleScaryGreen => Color.Yellow,
            _ => Color.Transparent,
        };

        internal static Color ButtonColor => SettingsModel.I.CurrentStyleName switch
        {
            Constants.StyleBrigth => Color.Gray,
            Constants.StyleDark => Color.LightGray,
            Constants.StyleScaryGreen => Color.PaleGreen,
            _ => Color.Transparent,
        };

        internal static Color AccentColor => Color.Accent;
        internal static Color AccentHighColor => Color.Accent.AddLuminosity(.2);
        internal static Color AccentLowColor => Color.Accent.AddLuminosity(-.2);

        public static float CornerRadius => 3;  //TODO introduce option
    }
}