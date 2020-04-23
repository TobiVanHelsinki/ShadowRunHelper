//Author: Tobi van Helsinki

using Rg.Plugins.Popup.Services;
using ShadowRunHelperViewer.UI.Resources;
using Xamarin.Forms;

namespace ShadowRunHelperViewer
{
    public partial class SharedStyles
    {
        public static void DisplayDefaultPopUp(View v)
        {
            PopupNavigation.Instance.PushAsync(new RgPopUp(v));
        }

        public SharedStyles()
        {
            foreach (var item in typeof(StyleManager).GetProperties())
            {
                try
                {
                    Add(item.Name, item.GetValue(null));
                }
                catch (System.Exception)
                {
                }
            }

            InitializeComponent();
        }
    }
}