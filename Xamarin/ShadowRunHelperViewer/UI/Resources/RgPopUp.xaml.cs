//Author: Tobi van Helsinki

using System;
using System.Threading.Tasks;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ShadowRunHelperViewer.UI.Resources
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RgPopUp : PopupPage
    {
        public static async Task DisplayDefaultPopUp(View v)
        {
            try
            {
                await PopupNavigation.Instance.PushAsync(new RgPopUp(v));
            }
            catch (Exception)
            {
            }
        }

        public RgPopUp(View v)
        {
            InitializeComponent();
            MainFrame.Content = v;
            BindingContext = this;
        }

        private void PopupPage_SizeChanged(object sender, EventArgs e)
        {
            (MainFrame.WidthRequest, MainFrame.HeightRequest) = Common.MaximumDimensions(Width, Height);
        }
    }
}