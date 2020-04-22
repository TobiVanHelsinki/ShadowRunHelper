//Author: Tobi van Helsinki

using ShadowRunHelper;
using TLIB;
using Xamarin.Forms;

namespace ShadowRunHelperViewer.UI.Resources
{
    public static class StyleManager
    {
        public static double Spacing => SettingsModel.I.CurrentSpacingStrategy;
        public static double SpacingS => (SettingsModel.I.CurrentSpacingStrategy - 1).LowerB(0);
        public static double SpacingM => SettingsModel.I.CurrentSpacingStrategy;
        public static double SpacingL => SettingsModel.I.CurrentSpacingStrategy + 2;

        //Colors
        public static Color CosmicLatte = Color.FromRgb(255, 248, 231);
        public static Color BackgroundColor => SettingsModel.I.CurrentStyleName switch
        {
            Constants.StyleBrigth => Color.White,
            Constants.StyleDark => Color.Black,
            Constants.StyleScaryGreen => Color.DarkGreen,
            _ => Color.Transparent,
        };

        public static Color AltBackgroundColor => SettingsModel.I.CurrentStyleName switch
        {
            Constants.StyleBrigth => CosmicLatte,
            Constants.StyleDark => Color.FromHex("FF333333"),
            Constants.StyleScaryGreen => Color.DarkGreen,
            _ => Color.Transparent,
        };

        public static Color ElementBackgroundColor => SettingsModel.I.CurrentStyleName switch
        {
            Constants.StyleBrigth => Color.LightGray,
            Constants.StyleDark => Color.SlateGray,
            Constants.StyleScaryGreen => Color.Green,
            _ => Color.Transparent,
        };

        public static Color ElementBackgroundColorSemi => SettingsModel.I.CurrentStyleName switch
        {
            Constants.StyleBrigth => Color.FromRgba(Color.LightGray.R, Color.LightGray.G, Color.LightGray.B, 210),
            Constants.StyleDark => Color.FromRgba(Color.SlateGray.R, Color.SlateGray.G, Color.SlateGray.B, 210),
            Constants.StyleScaryGreen => Color.Green,
            _ => Color.Transparent,
        };

        public static Color ElementBorderColor => SettingsModel.I.CurrentStyleName switch
        {
            Constants.StyleBrigth => Color.LightGray.AddLuminosity(-.2),
            Constants.StyleDark => Color.SlateGray.AddLuminosity(.2),
            Constants.StyleScaryGreen => Color.Green,
            _ => Color.Transparent,
        };

        public static Color ForegroundColor => SettingsModel.I.CurrentStyleName switch
        {
            Constants.StyleBrigth => Color.DarkSlateGray,
            Constants.StyleDark => CosmicLatte,
            Constants.StyleScaryGreen => Color.GreenYellow,
            _ => Color.Transparent,
        };

        public static Color TextColor => SettingsModel.I.CurrentStyleName switch
        {
            Constants.StyleBrigth => Color.Black,
            Constants.StyleDark => Color.White,
            Constants.StyleScaryGreen => Color.GreenYellow,
            _ => Color.Transparent,
        };

        public static Color AltTextColor => SettingsModel.I.CurrentStyleName switch
        {
            Constants.StyleBrigth => Color.Black.AddLuminosity(.2),
            Constants.StyleDark => Color.White.AddLuminosity(-.2),
            Constants.StyleScaryGreen => Color.GreenYellow,
            _ => Color.Transparent,
        };

        public static Color AccentColor => Color.Accent;
        public static Color AccentHighColor => Color.Accent.AddLuminosity(.2);
        public static Color AccentLowColor => Color.Accent.AddLuminosity(-.2);
        public static Color AccentInverseBinary => Color.Accent.Luminosity > .5 ? Color.Black : Color.White; //TODO mit TextColor verbinden
        public static Color AccentInverse => Color.Accent.WithSaturation((Color.Accent.Saturation + 0.5) % 1).WithLuminosity((Color.Accent.Luminosity + 0.5) % 1).WithHue((Color.Accent.Hue + 0.5) % 1);

        public static int CornerRadiusN => 3;  //TODO introduce option
        public static float CornerRadiusF => CornerRadiusN;  //TODO introduce option
    }
}