//Author: Tobi van Helsinki

using Xamarin.Forms;

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

        public static T FindParent<T>(Element start)
        {
            if (start is null)
            {
                return default;
            }
            else if (start.Parent is T t)
            {
                return t;
            }
            else
            {
                return FindParent<T>(start.Parent);
            }
        }
    }
}