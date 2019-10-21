///Author: Tobi van Helsinki

using ShadowRunHelperViewer.UWP.Renderers;
using System.IO;
using System.Linq;
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
                //var count = (defaultview.ItemsSource as System.Collections.Generic.IEnumerable<object>)?.Count().ToString() ?? "";
                //var name = "Key";
                zoom.ZoomedOutView = new wincontrol.ListView() { ItemsSource = defaultview.ItemsSource, /*ItemTemplate = Create(name, count)*/ };
            }
        }

        /// <summary>
        /// Creates the custom templte for the zoomed out view
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="count">The count.</param>
        /// <returns></returns>
        public static Windows.UI.Xaml.DataTemplate Create(string key, string count)
        {
            var stringReader =
@"<DataTemplate
    xmlns=""http://schemas.microsoft.com/winfx/2006/xaml/presentation"">
    <StackPanel Orientation=""Horizontal"">
        <TextBlock Text=""{Binding  }""/>
        <TextBlock Margin=""4,0,0,0"">
            <Run Text=""("" Margin=""0""/>
            <Run Text=""" + count + @""" Margin=""0""/>
            <Run Text="")"" Margin=""0""/>
        </TextBlock>
    </StackPanel>
</DataTemplate>";
            return Windows.UI.Xaml.Markup.XamlReader.Load(stringReader) as Windows.UI.Xaml.DataTemplate;
        }
    }
}