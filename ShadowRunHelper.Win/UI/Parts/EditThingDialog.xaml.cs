using ShadowRunHelper.CharModel;
using System.Collections.Generic;
using TLIB;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

// Die Elementvorlage "Inhaltsdialog" ist unter http://go.microsoft.com/fwlink/?LinkId=234238 dokumentiert.

namespace ShadowRunHelper.UI
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
                    EditType.ContentTemplate = Handlung;
                    break;
                case ThingDefs.Fertigkeit:
                    SecondaryButtonClick += CreateHandlung;
                    SecondaryButtonText = StringHelper.GetString("Model_Fertigkeit_CreateHandlung");
                    break;
                case ThingDefs.Item:
                    EditType.ContentTemplate = Item;
                    break;
                case ThingDefs.Programm:
                    EditType.ContentTemplate = Programm;
                    break;
                case ThingDefs.Munition:
                    EditType.ContentTemplate = Munition;
                    WertLabel.Text = TLIB.StringHelper.GetString("Model_Waffe_Wert/Text");
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
                    EditType.ContentTemplate = Attribut;

                    Col1.Width = new GridLength(0, GridUnitType.Star);
                    Col2.Width = new GridLength(0, GridUnitType.Star);
                    Col3.Width = new GridLength(1, GridUnitType.Star);
                    break;
                case ThingDefs.Nahkampfwaffe:
                    EditType.ContentTemplate = Nahkampfwaffe;
                    WertLabel.Text = TLIB.StringHelper.GetString("Model_Waffe_Wert/Text");
                    break;
                case ThingDefs.Fernkampfwaffe:
                    EditType.ContentTemplate = Fernkampfwaffe;
                    WertLabel.Text = TLIB.StringHelper.GetString("Model_Waffe_Wert/Text");
                    break;
                case ThingDefs.Kommlink:
                    EditType.ContentTemplate = Kommlink;
                    break;
                case ThingDefs.CyberDeck:
                    EditType.ContentTemplate = CyberDeck;
                    break;
                case ThingDefs.Vehikel:
                    EditType.ContentTemplate = Vehikel;
                    WertLabel.Text = TLIB.StringHelper.GetString("Model_Vehikel_Wert/Text");
                    break;
                case ThingDefs.Panzerung:
                    EditType.ContentTemplate = Panzerung;
                    break;
                case ThingDefs.Adeptenkraft:
                    EditType.ContentTemplate = Adeptenkraft;
                    break;
                case ThingDefs.Geist:
                    EditType.ContentTemplate = Geist;
                    break;
                case ThingDefs.Foki:
                    EditType.ContentTemplate = Foki;
                    break;
                case ThingDefs.Stroemung:
                    EditType.ContentTemplate = Stroemung;
                    break;
                case ThingDefs.Tradition:
                    EditType.ContentTemplate = Tradition;
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

        void CreateHandlung(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            var Fert = Data as Fertigkeit;
            var Handl = Model.AppModel.Instance.MainObject.Add(ThingDefs.Handlung);
            if (Fert.TryCopy(Handl))
            {
                Model.AppModel.Instance.lstNotifications.Add(new TAPPLICATION.Model.Notification(StringHelper.GetString("Error_ObjectCopy")) { IsRead = true});
            }
            Handl.Wert = 0;
            var FertEntry = Model.AppModel.Instance.MainObject.LinkList.Find(x=>x.Object == Fert);
            if (FertEntry != null)
            {
                Handl.LinkedThings.Add(FertEntry);
            }
        }

        private void MainGrid_Loaded(object sender, RoutedEventArgs e)
        {
            ((FrameworkElement)sender).DataContext = Data;
        }

        void AttributZusammensetzungBearbeiten(object sender, RoutedEventArgs e)
        {
            Auswahl dialog = new Auswahl(Model.AppModel.Instance.MainObject.LinkList, ((Attribut)((Button)sender).DataContext).LinkedThings, Filter: CharModel.Attribut.Filter);
            Hide();
#pragma warning disable CS4014
            dialog.ShowAsync();
#pragma warning restore CS4014
        }

        void EditBox_GotFocus(object sender, RoutedEventArgs e) => SharePageFunctions.EditBox_SelectAll(sender, e);

        void EditBox_PreviewKeyDown(object sender, KeyRoutedEventArgs e) => SharePageFunctions.EditBox_UpDownKeys(sender, e);

    }
}
