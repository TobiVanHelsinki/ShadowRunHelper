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
            _ => Color.White,
        };

        internal static Color ForegroundColor => SettingsModel.I.CurrentStyleName switch
        {
            Constants.StyleBrigth => Color.DarkSlateGray,
            Constants.StyleDark => Color.Beige,
            Constants.StyleScaryGreen => Color.GreenYellow,
            _ => Color.White,
        };
    }
}