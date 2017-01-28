using System.Collections.Generic;
using System.Collections.ObjectModel;
using Windows.UI.Xaml.Controls;
using static ShadowRunHelper.Ressourcen.TypNamen;

// Die Elementvorlage "Inhaltsdialog" ist unter http://go.microsoft.com/fwlink/?LinkId=234238 dokumentiert.

namespace ShadowRunHelper.UI.Edit
{
    public sealed partial class Edit_Dialog : ContentDialog
    {
        public CharModel.Thing Data;
        //public ObservableCollection<CharThing> lstAll;
        public List<string> MyStringOptions { get; set; }

        public Edit_Dialog(CharModel.Thing data)
        {
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
                    EditType.ContentTemplate = Handlung;
                    break;
                case ThingDefs.Fertigkeit:
                    EditType.ContentTemplate = Fertigkeit;
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
                    EditType.ContentTemplate = Vorteil;
                    break;
                case ThingDefs.Nachteil:
                    EditType.ContentTemplate = Nachteil;
                    break;
                case ThingDefs.Connection:
                    EditType.ContentTemplate = Connection;
                    break;
                case ThingDefs.Sin:
                    EditType.ContentTemplate = Sin;
                    break;
                case ThingDefs.Attribut:
                    EditType.ContentTemplate = Attribut;
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
                    break;
                case ThingDefs.Panzerung:
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
