//Author: Tobi van Helsinki

using System;
using ShadowRunHelperViewer.UI.Pages;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ShadowRunHelperViewer.UI.ControlsOther
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HamburgerButton : ContentView
    {
        public HamburgerButton() => InitializeComponent();

        private void ToggleMenu(object sender, EventArgs e) => MainPage.Instance.ToggleMenu(this, e);
    }
}