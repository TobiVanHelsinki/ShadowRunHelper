using ShadowRun_Charakter_Helper.Model;
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

        private void Flyout_Fähig_Close(object sender, object e)
        {
            selected_Fähigkeit_IsOn = false;
            selected_Fähigkeit_OLD_ID_IsOn = false;
        }

        private void Flyout_Fähig_Opened(object sender, object e)
        {
         //   Char_Fähigkeit Data = (Char_Fähigkeit)((FrameworkElement)((Flyout)sender).Content).DataContext;
        //    selected_Fähigkeit = Data.ID;
            selected_Fähigkeit_IsOn = true;
        }

        //------------------------------------------------------------------------------------------------------------
        private void Zusammensetzung_A_CBB_GotFocus(object sender, RoutedEventArgs e)
        {
            try
            {
              //  Attribut_ID Data = (Attribut_ID)(((ComboBox)sender).DataContext);
            //    selected_Fähigkeit_OLD_ID = Data.ID;
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
       //             index_F = ViewModel.DefaultChar.Char_Fähigkeiten.IndexOf(Char_Fähigkeit.findByID(selected_Fähigkeit, ViewModel.DefaultChar.Char_Fähigkeiten));
       //             index_AID = ViewModel.DefaultChar.Char_Fähigkeiten[index_F].Zusammensetzung_A.IndexOf(Attribut_ID.findByID(selected_Fähigkeit_OLD_ID, ViewModel.DefaultChar.Char_Fähigkeiten[index_F].Zusammensetzung_A));


                }
                catch (System.Exception)
                {

                    throw;
                }

        //        Char_Attribut Data = (Char_Attribut)(((ComboBox)sender).SelectedItem);

                //try
                //{
        //            ViewModel.DefaultChar.Char_Fähigkeiten[index_F].Zusammensetzung_A[index_AID].ID = Data.ID;
                //}
                //catch (System.Exception)
                //{

                   
                //}
                
            }
            selected_Fähigkeit_OLD_ID_IsOn = false;
        }


    }
}
