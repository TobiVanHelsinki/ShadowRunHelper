//Author: Tobi van Helsinki

using ShadowRunHelperViewer.Platform.Android;
using ShadowRunHelperViewer.Platform.Xamarin;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: Dependency(typeof(AndroidFormsInteractions))]

namespace ShadowRunHelperViewer.Platform.Android
{
    public class AndroidFormsInteractions : IFormsInteractions
    {
        public object GetRenderer(VisualElement source)
        {
            try
            {
                return source.GetRenderer()?.Element;
            }
            catch (System.Exception)
            {
                return null;
            }
        }
    }
}