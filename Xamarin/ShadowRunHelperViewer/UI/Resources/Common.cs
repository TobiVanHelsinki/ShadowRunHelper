namespace ShadowRunHelperViewer.UI.Resources
{
    static class Common
    {
        const int WidthBorder = 650;
        const int HeightBorder = 650;
        public static (double, double) MaximumDimensions(double width, double height)
        {
            return (width > WidthBorder ? WidthBorder : width, height > HeightBorder ? HeightBorder : height);
        }
    }
}
