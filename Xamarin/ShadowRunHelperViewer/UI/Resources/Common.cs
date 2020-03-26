//Author: Tobi van Helsinki

namespace ShadowRunHelperViewer.UI.Resources
{
    internal static class Common
    {
        const int WidthBorder = 450;
        const int HeightBorder = 600;

        public static (double, double) MaximumDimensions(double width, double height)
        {
            return (width > WidthBorder ? WidthBorder : width, height > HeightBorder ? HeightBorder : height);
        }
    }
}