///Author: Tobi van Helsinki

using ShadowRunHelperViewer.UWP.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.UWP;
using wincontrol = Windows.UI.Xaml.Controls;

[assembly: ExportRenderer(typeof(ListView), typeof(CustomSemanticZoomListView))]

namespace ShadowRunHelperViewer.UWP.Renderers
{
    public class CustomSemanticZoomListView : ListViewRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<ListView> e)
        {
            base.OnElementChanged(e);

            if (Control == null)
            {
                return;
            }

            if (Control is wincontrol.SemanticZoom zoom)
            {
                var defaultview = zoom.ZoomedOutView as wincontrol.GridView;
                zoom.ZoomedOutView = new wincontrol.ListView() { ItemsSource = defaultview.ItemsSource };
            }
        }
    }
}