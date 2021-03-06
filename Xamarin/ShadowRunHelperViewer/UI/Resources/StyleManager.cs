﻿//Author: Tobi van Helsinki

using ShadowRunHelper;
using TLIB;
using Xamarin.Forms;

namespace ShadowRunHelperViewer.UI.Resources
{
    public static class StyleManager
    {
        //Spacings
        public static double Spacing => SettingsModel.I.CurrentSpacingStrategy switch
        {
            Constants.SpacingWide => 5,
            Constants.SpacingCompact => 1,
            _ => 3,
        };

        public static double SpacingS => (Spacing - 1).LowerB(0);
        public static double SpacingM => Spacing;
        public static double SpacingL => Spacing + 2;

        //Sizes
        public static int CornerRadiusN => 3;  //TODO introduce option

        public static float CornerRadiusF => CornerRadiusN;  //TODO introduce option

        //Colors
        public static Color CosmicLatte = Color.FromRgb(255, 248, 231);
        public static Color BackgroundColor => SettingsModel.I.CurrentStyleName switch
        {
            Constants.StyleDark => Color.Black,
            Constants.StyleScaryGreen => Color.DarkGreen,
            _ => Color.White,
        };

        public static Color AltBackgroundColor => SettingsModel.I.CurrentStyleName switch
        {
            Constants.StyleDark => Color.FromHex("FF333333"),
            Constants.StyleScaryGreen => Color.DarkGreen,
            _ => CosmicLatte,
        };

        public static Color ElementBackgroundColor => SettingsModel.I.CurrentStyleName switch
        {
            Constants.StyleDark => Color.SlateGray,
            Constants.StyleScaryGreen => Color.Green,
            _ => Color.LightGray, //this was from sf: #D8D6D7
        };

        public static Color ElementBackgroundColorSemi => Color.Transparent;
        //SettingsModel.I.CurrentStyleName switch
        //{
        //    Constants.StyleDark => Color.FromRgba(Color.SlateGray.R, Color.SlateGray.G, Color.SlateGray.B, 210),
        //    Constants.StyleScaryGreen => Color.Green,
        //    _ => Color.FromRgba(Color.LightGray.R, Color.LightGray.G, Color.LightGray.B, 30),
        //};

        public static Color ElementBorderColor => SettingsModel.I.CurrentStyleName switch
        {
            Constants.StyleDark => Color.SlateGray.AddLuminosity(.2),
            Constants.StyleScaryGreen => Color.Green,
            _ => Color.LightGray.AddLuminosity(-.2),
        };

        public static Color ForegroundColor => SettingsModel.I.CurrentStyleName switch
        {
            Constants.StyleDark => CosmicLatte,
            Constants.StyleScaryGreen => Color.GreenYellow,
            _ => Color.DarkSlateGray,
        };

        public static Color TextColor => SettingsModel.I.CurrentStyleName switch
        {
            Constants.StyleDark => Color.White,
            Constants.StyleScaryGreen => Color.GreenYellow,
            _ => Color.Black,
        };

        public static Color AltTextColor => SettingsModel.I.CurrentStyleName switch
        {
            Constants.StyleDark => Color.White.AddLuminosity(-.2),
            Constants.StyleScaryGreen => Color.GreenYellow,
            _ => Color.Black.AddLuminosity(.2),
        };

        public static Color UnimportandTextColor => SettingsModel.I.CurrentStyleName switch
        {
            Constants.StyleDark => Color.LightGray,
            Constants.StyleScaryGreen => Color.DarkGreen,
            _ => Color.DarkGray,
        };

        public static Color AccentColor => Color.Accent; //#2eb82e
        public static Color AccentHighColor => Color.Accent.AddLuminosity(.06); //#33cc33
        public static Color AccentLowColor => Color.Accent.AddLuminosity(-.06); //#29a329
        public static Color AccentInverseBinary => Color.Accent.Luminosity > .5 ? Color.Black : Color.White; //TODO mit TextColor verbinden
        public static Color AltAccentInverseBinary => AccentInverseBinary.AddLuminosity(-.2);
        public static Color AccentInverse => Color.Accent.WithSaturation((Color.Accent.Saturation + 0.5) % 1).WithLuminosity((Color.Accent.Luminosity + 0.5) % 1).WithHue((Color.Accent.Hue + 0.5) % 1);
    }
}