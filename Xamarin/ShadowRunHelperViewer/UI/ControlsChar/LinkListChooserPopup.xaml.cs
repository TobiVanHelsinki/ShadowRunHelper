//Author: Tobi van Helsinki

using System;
using Rg.Plugins.Popup.Pages;
using ShadowRunHelper.CharModel;
using ShadowRunHelper.Model;
using ShadowRunHelperViewer.UI.Resources;
using Xamarin.Forms.Xaml;

namespace ShadowRunHelperViewer.UI.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LinkListChooserPopup : PopupPage
    {
        public LinkListChooserPopup(CharHolder myChar, ConnectProperty property)
        {
            InitializeComponent();
            BindingContext = this;
            Content = new LinkListChooser(myChar, property);
        }

        private void PopupPage_SizeChanged(object sender, EventArgs e)
        {
            (MainFrame.WidthRequest, MainFrame.HeightRequest) = Common.MaximumDimensions(Width, Height);
        }
    }
}