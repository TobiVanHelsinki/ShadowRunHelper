using ShadowRun_Charakter_Helper.Model;
using Windows.UI.Xaml.Controls;

// Die Elementvorlage "Leere Seite" ist unter http://go.microsoft.com/fwlink/?LinkId=234238 dokumentiert.

namespace ShadowRun_Charakter_Helper
{
    /// <summary>
    /// Eine leere Seite, die eigenständig verwendet werden kann oder auf die innerhalb eines Rahmens navigiert werden kann.
    /// </summary>
    public sealed partial class Settings : Page
    {
        private OptionViewModel Optionen = new OptionViewModel();


        public Settings()
        {
            this.InitializeComponent();
        }
    }
}
