using ShadowRunHelper.CharModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Windows.UI.Xaml.Controls;

// Die Elementvorlage "Inhaltsdialog" ist unter http://go.microsoft.com/fwlink/?LinkId=234238 dokumentiert.

namespace ShadowRunHelper.UI.Edit
{
    public sealed partial class Edit_Dialog : ContentDialog
    {
        public Thing Data;
        //public ObservableCollection<CharThing> lstAll;
        public List<string> MyStringOptions { get; set; }
        //public List<TextBox> CurrentTextBoxes { get; set; }

        public Edit_Dialog(Thing data)
        {
            //CurrentTextBoxes = new List<TextBox>();
            this.Data = data;
            this.InitializeComponent();
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            ContentDialogButtonClickDeferral deferral = args.GetDeferral();
            deferral.Complete();
        }

        private void EditType_Loaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            switch (Data.ThingType)
            {
                case ThingDefs.Handlung:
                    //EditType.ContentTemplate = Handlung;
                    Wert.IsEnabled = false;
                    break;
                case ThingDefs.Fertigkeit:
                    //EditType.ContentTemplate = Fertigkeit;
                    break;
                case ThingDefs.Item:
                    EditType.ContentTemplate = Item;
                    break;
                case ThingDefs.Programm:
                    EditType.ContentTemplate = Programm;
                    break;
                case ThingDefs.Munition:
                    EditType.ContentTemplate = Munition;
                    break;
                case ThingDefs.Implantat:
                    EditType.ContentTemplate = Implantat;
                    break;
                case ThingDefs.Vorteil:
                    EditType.ContentTemplate = Eigenschaft;
                    break;
                case ThingDefs.Nachteil:
                    EditType.ContentTemplate = Eigenschaft;
                    break;
                case ThingDefs.Connection:
                    EditType.ContentTemplate = Connection;
                    break;
                case ThingDefs.Sin:
                    //EditType.ContentTemplate = Sin;
                    break;
                case ThingDefs.Attribut:
                    //EditType.ContentTemplate = Attribut;
                    //Wert.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                    break;
                case ThingDefs.Nahkampfwaffe:
                    EditType.ContentTemplate = Nahkampfwaffe;
                    break;
                case ThingDefs.Fernkampfwaffe:
                    EditType.ContentTemplate = Fernkampfwaffe;
                    break;
                case ThingDefs.Kommlink:
                    EditType.ContentTemplate = Kommlink;
                    break;
                case ThingDefs.CyberDeck:
                    EditType.ContentTemplate = CyberDeck;
                    break;
                case ThingDefs.Vehikel:
                    EditType.ContentTemplate = Vehikel;
                    break;
                case ThingDefs.Panzerung:
                    EditType.ContentTemplate = Panzerung;
                    break;
                default:
                    break;
            }
        }
        
        private void MainGrid_Loaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            ((Grid)sender).DataContext = Data;
        }
    }
}
