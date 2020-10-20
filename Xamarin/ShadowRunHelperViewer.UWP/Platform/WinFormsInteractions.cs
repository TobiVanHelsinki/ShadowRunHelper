//Author: Tobi van Helsinki

using ShadowRunHelperViewer.Platform.UWP;
using ShadowRunHelperViewer.Platform.Xam;
using Xamarin.Forms;
using Xamarin.Forms.Platform.UWP;

[assembly: Dependency(typeof(WinFormsInteractions))]

namespace ShadowRunHelperViewer.Platform.UWP
{
    public class WinFormsInteractions : IFormsInteractions
    {
        public object GetRenderer(VisualElement source)
        {
            return source.GetOrCreateRenderer().ContainerElement;
        }
    }
}