//Author: Tobi van Helsinki

using ShadowRunHelper.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ShadowRunHelperViewer.UI.ControlsChar
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CharSettingsView : ContentView
    {
        public CharHolder MyChar { get; }

        public CharSettingsView(CharHolder myChar)
        {
            MyChar = myChar;
            InitializeComponent();
            BindingContext = this;
        }

        private void Reset(object sender, System.EventArgs e)
        {
            MyChar.Settings.ResetCategoryOptions();
        }

        private void Close(object sender, System.EventArgs e)
        {
            Rg.Plugins.Popup.Services.PopupNavigation.Instance.PopAsync();
        }
    }
}