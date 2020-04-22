//Author: Tobi van Helsinki

using System;
using Rg.Plugins.Popup.Pages;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ShadowRunHelperViewer.UI.Resources
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RgPopUp : PopupPage
    {
        public RgPopUp(View v)
        {
            InitializeComponent();
            BindingContext = this;
            MainFrame.Content = v;
        }

        private void PopupPage_SizeChanged(object sender, EventArgs e)
        {
            (MainFrame.WidthRequest, MainFrame.HeightRequest) = Common.MaximumDimensions(Width, Height);
        }
    }
}