using ShadowRunHelper.CharModel;
using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// Die Elementvorlage "Inhaltsdialog" ist unter http://go.microsoft.com/fwlink/?LinkId=234238 dokumentiert.

namespace ShadowRunHelper.UI.Edit
{
    public sealed partial class EditThingDialog : ContentDialog
    {
        public Thing Data;
        public List<string> MyStringOptions { get; set; }

        public EditThingDialog(Thing data)
        {
            Data = data;
            InitializeComponent();
            Title = TypeHelper.ThingDefToString(Data.ThingType, false);
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            ContentDialogButtonClickDeferral deferral = args.GetDeferral();
            deferral.Complete();
        }

        void EditType_Loaded(object sender, RoutedEventArgs e)
        {
            switch (Data.ThingType)
            {
                case ThingDefs.Handlung:
                    Col3.Width = new GridLength(0, GridUnitType.Star);
                    Wert.Visibility = Visibility.Collapsed;
                    break;
                case ThingDefs.Fertigkeit:
                    break;
                case ThingDefs.Item:
                    EditType.ContentTemplate = Item;
                    break;
                case ThingDefs.Programm:
                    EditType.ContentTemplate = Programm;
                    break;
                case ThingDefs.Munition:
                    EditType.ContentTemplate = Munition;
                    WertLabel.Text = TLIB_UWPFRAME.CrossPlatformHelper.GetString("Model_Waffe_Wert/Text");
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
                    Wert.Visibility = Visibility.Collapsed;
                    Zusatz.Visibility = Visibility.Collapsed;
                    Col2.Width = new GridLength(0, GridUnitType.Pixel);
                    Col3.Width = new GridLength(0, GridUnitType.Pixel);
                    break;
                case ThingDefs.Sin:
                    break;
                case ThingDefs.Attribut:
                    Zusatz.Visibility = Visibility.Collapsed;
                    Typ.Visibility = Visibility.Collapsed;
                    Bezeichner.Visibility = Visibility.Collapsed;
                    Type.Height = new GridLength(0);
                    Type_U.Height = new GridLength(0);
                    Notes.Height = new GridLength(0);
                    Notes_U.Height = new GridLength(0);
                    Col1.Width = new GridLength(0, GridUnitType.Star);
                    Col2.Width = new GridLength(0, GridUnitType.Star);
                    Col3.Width = new GridLength(1, GridUnitType.Star);
                    break;
                case ThingDefs.Nahkampfwaffe:
                    EditType.ContentTemplate = Nahkampfwaffe;
                    WertLabel.Text = TLIB_UWPFRAME.CrossPlatformHelper.GetString("Model_Waffe_Wert/Text");
                    break;
                case ThingDefs.Fernkampfwaffe:
                    EditType.ContentTemplate = Fernkampfwaffe;
                    WertLabel.Text = TLIB_UWPFRAME.CrossPlatformHelper.GetString("Model_Waffe_Wert/Text");
                    break;
                case ThingDefs.Kommlink:
                    EditType.ContentTemplate = Kommlink;
                    break;
                case ThingDefs.CyberDeck:
                    EditType.ContentTemplate = CyberDeck;
                    break;
                case ThingDefs.Vehikel:
                    EditType.ContentTemplate = Vehikel;
                    WertLabel.Text = TLIB_UWPFRAME.CrossPlatformHelper.GetString("Model_Vehikel_Wert/Text");
                    break;
                case ThingDefs.Panzerung:
                    EditType.ContentTemplate = Panzerung;
                    break;
                case ThingDefs.Adeptenkraft_KomplexeForm:
                    EditType.ContentTemplate = Adeptenkraft_KomplexeForm;
                    break;
                case ThingDefs.Geist_Sprite:
                    EditType.ContentTemplate = Geist_Sprite;
                    break;
                case ThingDefs.Foki_Widgets:
                    EditType.ContentTemplate = Foki_Widgets;
                    break;
                case ThingDefs.Stroemung_Wandlung:
                    EditType.ContentTemplate = Stroemung_Wandlung;
                    break;
                case ThingDefs.Tradition_Initiation:
                    EditType.ContentTemplate = Tradition_Initiation;
                    break;
                case ThingDefs.Zaubersprueche:
                    EditType.ContentTemplate = Zaubersprueche;
                    break;
                case ThingDefs.KomplexeForm:
                    EditType.ContentTemplate = KomplexeForm;
                    break;
                case ThingDefs.Sprite:
                    EditType.ContentTemplate = Sprite;
                    break;
                case ThingDefs.Widgets:
                    EditType.ContentTemplate = Widgets;
                    break;
                case ThingDefs.Wandlung:
                    EditType.ContentTemplate = Wandlung;
                    break;
                case ThingDefs.Initiation:
                    EditType.ContentTemplate = Initiation;
                    break;
                default:
                    break;
            }
        }

        private void MainGrid_Loaded(object sender, RoutedEventArgs e)
        {
            ((Grid)sender).DataContext = Data;
        }
    }
}
