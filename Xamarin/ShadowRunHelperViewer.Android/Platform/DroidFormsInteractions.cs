using ShadowRunHelperViewer.Platform;
using ShadowRunHelperViewer.Android.Platform;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: Dependency(typeof(AndroidFormsInteractions))]
namespace ShadowRunHelperViewer.Android.Platform
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
