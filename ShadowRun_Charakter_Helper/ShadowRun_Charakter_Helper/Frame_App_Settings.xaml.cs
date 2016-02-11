using ShadowRun_Charakter_Helper.Models;
using Windows.UI.Xaml.Controls;

// Die Elementvorlage "Leere Seite" ist unter http://go.microsoft.com/fwlink/?LinkId=234238 dokumentiert.

namespace ShadowRun_Charakter_Helper
{
    /// <summary>
    /// Eine leere Seite, die eigenständig verwendet werden kann oder auf die innerhalb eines Rahmens navigiert werden kann.
    /// </summary>
    public sealed partial class Frame_App_Settings : Page
    {
        public CharViewModel ViewModel { get; set; }

        public Frame_App_Settings()
        {
            this.ViewModel = new CharViewModel();
            this.InitializeComponent();

            int id = ViewModel.Current.Handlungen[1].DicCD_ID;
        }
    }
}
