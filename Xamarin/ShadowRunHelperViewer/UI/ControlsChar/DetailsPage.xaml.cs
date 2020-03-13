//Author: Tobi van Helsinki

using System;
using Rg.Plugins.Popup.Pages;
using ShadowRunHelper.CharModel;
using ShadowRunHelper.Model;
using ShadowRunHelperViewer.UI.ControlsChar;
using ShadowRunHelperViewer.UI.Resources;
using Xamarin.Forms.Xaml;

namespace ShadowRunHelperViewer
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetailsPage : PopupPage
    {
        private readonly Thing thing;
        private readonly CharHolder mychar;

        public DetailsPage(Thing thing, CharHolder mychar)
        {
            InitializeComponent();
            MainFrame.Content = new DetailsView();
            this.thing = thing;
            this.mychar = mychar;
        }

        /// <summary>
        /// to prevent ugly visual at big screens border is set to feel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PopupPage_SizeChanged(object sender, EventArgs e)
        {
            (MainFrame.WidthRequest, MainFrame.HeightRequest) = Common.MaximumDimensions(Width, Height);
        }

        protected override void OnAppearing()
        {
            if (MainFrame.Content is DetailsView view)
            {
                view.Activate(thing, mychar);
            }
            base.OnAppearing();
        }
    }
}