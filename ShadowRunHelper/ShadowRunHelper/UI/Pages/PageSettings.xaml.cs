using ShadowRunHelper.Model;
using TLIB;
using TLIB.IO;
using Windows.UI.Xaml.Controls;

// Die Elementvorlage "Leere Seite" ist unter http://go.microsoft.com/fwlink/?LinkId=234238 dokumentiert.

namespace ShadowRunHelper
{
    /// <summary>
    /// Eine leere Seite, die eigenständig verwendet werden kann oder auf die innerhalb eines Rahmens navigiert werden kann.
    /// </summary>
    public sealed partial class PageSettings : Page
    {
        private AppSettings Optionen = AppSettings.Instance;
        private readonly string AppVersionBuild = Constants.APP_VERSION_BUILD_DELIM;
        //private readonly string AppVersionNumber = Constants.APP_VERSION_NUMBER;

        bool OrdnerModeGeladen = false;

        public PageSettings()
        {
            this.InitializeComponent();
        }

        private async void UI_Optionen_OrdnerMode_Toggled(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            if (((ToggleSwitch)sender).IsOn && OrdnerModeGeladen)
            {
                try
                {
                    Optionen.ORDNERMODE_PFAD = (await WinIO.FolderPicker()).Path;
                }
                catch (System.Exception)
                {

                }
            }

            if (((ToggleSwitch)sender).IsOn)
            {
                UI_Optionen_OrdnerModePfad.Visibility = Windows.UI.Xaml.Visibility.Visible;
            }
            else
            {
                UI_Optionen_OrdnerModePfad.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            }
        }

        private void UI_Optionen_OrdnerMode_GotFocus(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            OrdnerModeGeladen = true;
        }
    }
}
