//Author: Tobi van Helsinki

using Rg.Plugins.Popup.Pages;
using Xamarin.Forms.Xaml;

namespace ShadowRunHelperViewer.UI.ControlsChar
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CharSettingsPopupPage : PopupPage
    {
        public CharSettingsPopupPage(ShadowRunHelper.Model.CharHolder myChar)
        {
            InitializeComponent();
            MainFrame.Content = new CharSettingsView(myChar);
        }
    }
}