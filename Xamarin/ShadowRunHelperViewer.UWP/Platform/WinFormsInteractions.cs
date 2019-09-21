using ShadowRunHelperViewer.Platform;
using ShadowRunHelperViewer.UWP.Platform;
using Xamarin.Forms;
using Xamarin.Forms.Platform.UWP;

[assembly: Dependency(typeof(WinFormsInteractions))]
namespace ShadowRunHelperViewer.UWP.Platform
{
    public class WinFormsInteractions : IFormsInteractions
    {
        public object GetRenderer(VisualElement source)
        {
            return source.GetOrCreateRenderer().ContainerElement;
        }
    }
}
