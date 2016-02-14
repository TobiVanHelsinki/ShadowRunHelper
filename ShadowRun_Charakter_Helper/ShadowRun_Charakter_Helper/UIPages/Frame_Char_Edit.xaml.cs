using ShadowRun_Charakter_Helper.Models;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Navigation;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using ShadowRun_Charakter_Helper.IO;

// Die Elementvorlage "Leere Seite" ist unter http://go.microsoft.com/fwlink/?LinkId=234238 dokumentiert.

namespace ShadowRun_Charakter_Helper
{
    /// <summary>
    /// Eine leere Seite, die eigenständig verwendet werden kann oder auf die innerhalb eines Rahmens navigiert werden kann.
    /// </summary>
    public sealed partial class Frame_Char_Edit : Page
    {
        public CharViewModel ViewModel { get; set; }
        private int selected_Fähigkeit;
        private bool selected_Fähigkeit_IsOn;
        private int selected_Fähigkeit_OLD_ID;
        private bool selected_Fähigkeit_OLD_ID_IsOn;


        public Frame_Char_Edit()
        {
            this.InitializeComponent();
            
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            ViewModel = (CharViewModel)e.Parameter;
            // Todo Testdaten ---------------------------------------------------
            //ViewModel.DefaultChar.Char_Fähigkeiten[0].Zusammensetzung_A_neu = null;
            //ViewModel.DefaultChar.Char_Fähigkeiten[0].Zusammensetzung_A_neu = new List<Attribut_ID>();
            //Attribut_ID Test = new Attribut_ID();
            //Test.ID = 1;
            //ViewModel.DefaultChar.Char_Fähigkeiten[0].Zusammensetzung_A_neu.Add(Test);
            //// Todo Testdaten ---------------------------------------------------
            this.InitializeComponent();
            //this.DataContext = ViewModel.DefaultChar;
        }

        private void Item_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            FrameworkElement element = sender as FrameworkElement;
            if (element != null)
            {
                FlyoutBase.ShowAttachedFlyout(element);
            }
        }

        private void Fähigkeiten_Add(object sender, RoutedEventArgs e)
        {
            ViewModel.DefaultChar.Char_Fähigkeiten.Add(new Char_Fähigkeit(ViewModel.DefaultChar.Char_Fähigkeiten));
        }

        private void Items_Add(object sender, RoutedEventArgs e)
        {
            ViewModel.DefaultChar.Char_Items.Add(new Char_Item(ViewModel.DefaultChar.Char_Items));
        }

        private void Fertigkeiten_Add(object sender, RoutedEventArgs e)
        {
           ViewModel.DefaultChar.Char_Fertigkeiten.Add(new Char_Fertigkeit(ViewModel.DefaultChar.Char_Fertigkeiten));
        }


        private void Connections_Add(object sender, RoutedEventArgs e)
        {
           ViewModel.DefaultChar.Char_Connections.Add(new Char_Connection(ViewModel.DefaultChar.Char_Connections));
        }

        private void Fernkampfwaffen_Add(object sender, RoutedEventArgs e)
        {
           ViewModel.DefaultChar.Char_Fernkampfwaffen.Add(new Char_Fernkampfwaffe(ViewModel.DefaultChar.Char_Fernkampfwaffen));
        }

        private void Nachkampfwaffen_Add(object sender, RoutedEventArgs e)
        {
           ViewModel.DefaultChar.Char_Nahkampfwaffen.Add(new Char_Nahkampfwaffe(ViewModel.DefaultChar.Char_Nahkampfwaffen));
        }

        private void Kommlinks_Add(object sender, RoutedEventArgs e)
        {
           ViewModel.DefaultChar.Char_Kommlinks.Add(new Char_Kommlink(ViewModel.DefaultChar.Char_Kommlinks));
        }

        private void Implantate_Add(object sender, RoutedEventArgs e)
        {
           ViewModel.DefaultChar.Char_Implantate.Add(new Char_Implantat(ViewModel.DefaultChar.Char_Implantate));
        }
        private void Programme_Add(object sender, RoutedEventArgs e)
        {
           ViewModel.DefaultChar.Char_Programme.Add(new Char_Programm(ViewModel.DefaultChar.Char_Programme));
        }

        private void Vorteile_Add(object sender, RoutedEventArgs e)
        {
           ViewModel.DefaultChar.Char_Vorteile.Add(new Char_Vorteil(ViewModel.DefaultChar.Char_Vorteile));
        }

        private void Nachteile_Add(object sender, RoutedEventArgs e)
        {
           ViewModel.DefaultChar.Char_Nachteile.Add(new Char_Nachteil(ViewModel.DefaultChar.Char_Nachteile));
        }

        private void DronenUndFahrzeuge_Add(object sender, RoutedEventArgs e)
        {
           ViewModel.DefaultChar.Char_Dronen_Fahrzeuge.Add(new Char_Drone_Fahrzeug(ViewModel.DefaultChar.Char_Dronen_Fahrzeuge));
        }

        private void Panzerungen_Add(object sender, RoutedEventArgs e)
        {
            ViewModel.DefaultChar.Char_Panzerungen.Add(new Char_Panzerung(ViewModel.DefaultChar.Char_Panzerungen));
        }

        private void Sins_Add(object sender, RoutedEventArgs e)
        {
            ViewModel.DefaultChar.Char_Sins.Add(new Char_Sin(ViewModel.DefaultChar.Char_Sins));
        }

        private void Save_BTN(object sender, RoutedEventArgs e)
        {
            CharIO.Save_JSONChar_to_Data(ViewModel.DefaultChar);
        }

        private void Fähigkeiten_Del(object sender, RoutedEventArgs e)
        {
            ViewModel.DefaultChar.Char_Fähigkeiten.Remove((Char_Fähigkeit)((Button)sender).DataContext);
        }

        private void Fertigkeiten_Del(object sender, RoutedEventArgs e)
        {
            ViewModel.DefaultChar.Char_Fertigkeiten.Remove((Char_Fertigkeit)((Button)sender).DataContext);
        }

        private void Items_Del(object sender, RoutedEventArgs e)
        {
            ViewModel.DefaultChar.Char_Items.Remove((Char_Item)((Button)sender).DataContext);
        }

        private void Programme_Del(object sender, RoutedEventArgs e)
        {
            ViewModel.DefaultChar.Char_Programme.Remove((Char_Programm)((Button)sender).DataContext);
        }

        private void FernWaff_Del(object sender, RoutedEventArgs e)
        {
            ViewModel.DefaultChar.Char_Fernkampfwaffen.Remove((Char_Fernkampfwaffe)((Button)sender).DataContext);
        }

        private void NahWaff_Del(object sender, RoutedEventArgs e)
        {
            ViewModel.DefaultChar.Char_Nahkampfwaffen.Remove((Char_Nahkampfwaffe)((Button)sender).DataContext);
        }

        private void Panzerung_Del(object sender, RoutedEventArgs e)
        {
            ViewModel.DefaultChar.Char_Panzerungen.Remove((Char_Panzerung)((Button)sender).DataContext);
        }

        private void DroneFahrz_Del(object sender, RoutedEventArgs e)
        {
            ViewModel.DefaultChar.Char_Dronen_Fahrzeuge.Remove((Char_Drone_Fahrzeug)((Button)sender).DataContext);
        }

        private void Connection_Del(object sender, RoutedEventArgs e)
        {
            ViewModel.DefaultChar.Char_Connections.Remove((Char_Connection)((Button)sender).DataContext);
        }

        private void Sins_Del(object sender, RoutedEventArgs e)
        {
            ViewModel.DefaultChar.Char_Sins.Remove((Char_Sin)((Button)sender).DataContext);
        }

        private void Nachteile_Del(object sender, RoutedEventArgs e)
        {
            ViewModel.DefaultChar.Char_Nachteile.Remove((Char_Nachteil)((Button)sender).DataContext);
        }

        private void Vorteile_Del(object sender, RoutedEventArgs e)
        {
            ViewModel.DefaultChar.Char_Vorteile.Remove((Char_Vorteil)((Button)sender).DataContext);
        }

        private void Implantat_Del(object sender, RoutedEventArgs e)
        {
            ViewModel.DefaultChar.Char_Implantate.Remove((Char_Implantat)((Button)sender).DataContext);
        }

        private void Kommlinks_Del(object sender, RoutedEventArgs e)
        {
            ViewModel.DefaultChar.Char_Kommlinks.Remove((Char_Kommlink)((Button)sender).DataContext);
        }

        private void Attribute_Add(object sender, RoutedEventArgs e)
        {
            ViewModel.DefaultChar.Char_Attribute.Add(new Char_Attribut(ViewModel.DefaultChar.Char_Attribute));
        }

        private void Attribute_Del(object sender, RoutedEventArgs e)
        {
            ViewModel.DefaultChar.Char_Attribute.Remove((Char_Attribut)((Button)sender).DataContext);
        }

        private void Flyout_Fähig_Close(object sender, object e)
        {
            selected_Fähigkeit_IsOn = false;
            selected_Fähigkeit_OLD_ID_IsOn = false;
        }

        private void Flyout_Fähig_Opened(object sender, object e)
        {
            Char_Fähigkeit Data = (Char_Fähigkeit)((FrameworkElement)((Flyout)sender).Content).DataContext;
            selected_Fähigkeit = Data.ID;
            selected_Fähigkeit_IsOn = true;
        }

        //------------------------------------------------------------------------------------------------------------
        private void Zusammensetzung_A_CBB_GotFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                Attribut_ID Data = (Attribut_ID)(((ComboBox)sender).DataContext);
                selected_Fähigkeit_OLD_ID = Data.ID;
                selected_Fähigkeit_OLD_ID_IsOn = true;
            }
            catch (System.Exception)
            {
            }
           
        }

        private void Zusammensetzung_A_CBB_LostFocus(object sender, RoutedEventArgs e)
        {
            if (selected_Fähigkeit_IsOn && selected_Fähigkeit_OLD_ID_IsOn)
            {

                int index_F = 0;
                int index_AID = 0;
                try
                {
                    index_F = ViewModel.DefaultChar.Char_Fähigkeiten.IndexOf(Char_Fähigkeit.findByID(selected_Fähigkeit, ViewModel.DefaultChar.Char_Fähigkeiten));
                    index_AID = ViewModel.DefaultChar.Char_Fähigkeiten[index_F].Zusammensetzung_A.IndexOf(Attribut_ID.findByID(selected_Fähigkeit_OLD_ID, ViewModel.DefaultChar.Char_Fähigkeiten[index_F].Zusammensetzung_A));


                }
                catch (System.Exception)
                {

                    throw;
                }

                Char_Attribut Data = (Char_Attribut)(((ComboBox)sender).SelectedItem);

                //try
                //{
                    ViewModel.DefaultChar.Char_Fähigkeiten[index_F].Zusammensetzung_A[index_AID].ID = Data.ID;
                //}
                //catch (System.Exception)
                //{

                   
                //}
                
            }
            selected_Fähigkeit_OLD_ID_IsOn = false;
        }


    }
}
