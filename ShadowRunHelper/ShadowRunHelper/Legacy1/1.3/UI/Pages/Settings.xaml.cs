using ShadowRunHelper1_3.Model;
using Windows.UI.Xaml.Controls;

// Die Elementvorlage "Leere Seite" ist unter http://go.microsoft.com/fwlink/?LinkId=234238 dokumentiert.

namespace ShadowRunHelper1_3
{
    /// <summary>
    /// Eine leere Seite, die eigenständig verwendet werden kann oder auf die innerhalb eines Rahmens navigiert werden kann.
    /// </summary>
    public sealed partial class Settings : Page
    {
        private OptionViewModel Optionen = new OptionViewModel();

        bool OrdnerModeGeladen = false;

        public Settings()
        {
            this.InitializeComponent();
            
            
        }

        private async void UI_Optionen_OrdnerMode_Toggled(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            if (((ToggleSwitch)sender).IsOn && OrdnerModeGeladen)
            {
                try
                {
                    Windows.Storage.StorageFolder StoreFolder = await IO.CharVerwaltung.getExternFolder();
                    Optionen.ORDNERMODE_PFAD = StoreFolder.Path;
                    
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
