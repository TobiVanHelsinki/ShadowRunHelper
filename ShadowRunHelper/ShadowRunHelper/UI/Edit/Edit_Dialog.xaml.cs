using System.Collections.Generic;
using System.Collections.ObjectModel;
using Windows.UI.Xaml.Controls;

// Die Elementvorlage "Inhaltsdialog" ist unter http://go.microsoft.com/fwlink/?LinkId=234238 dokumentiert.

namespace ShadowRunHelper.UI.Edit
{
    public sealed partial class Edit_Dialog : ContentDialog
    {
        public CharModel.Thing Data;
        //public ObservableCollection<CharModel.Thing> lstAll;
        public List<string> MyStringOptions { get; set; }

        public Edit_Dialog(CharModel.Thing data)
        {
            this.InitializeComponent();
            this.Data = data;
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
                case Model.ThingDefs.Handlung:
                    break;
                case Model.ThingDefs.Fertigkeit:
                    break;
                case Model.ThingDefs.Item:
                    break;
                case Model.ThingDefs.Programm:
                    break;
                case Model.ThingDefs.Munition:
                    break;
                case Model.ThingDefs.Implantat:
                    break;
                case Model.ThingDefs.Vorteil:
                    break;
                case Model.ThingDefs.Nachteil:
                    break;
                case Model.ThingDefs.Connection:
                    break;
                case Model.ThingDefs.Sin:
                    break;
                case Model.ThingDefs.Attribut:
                    EditType.ContentTemplate = Attribut;
                    break;
                case Model.ThingDefs.Nahkampfwaffe:
                    break;
                case Model.ThingDefs.Fernkampfwaffe:
                    break;
                case Model.ThingDefs.Kommlink:
                    break;
                case Model.ThingDefs.CyberDeck:
                    EditType.ContentTemplate = CyberDeck;
                    break;
                case Model.ThingDefs.Vehikel:
                    break;
                case Model.ThingDefs.Panzerung:
                    break;
                default:
                    break;
            }
        }
    }
}
