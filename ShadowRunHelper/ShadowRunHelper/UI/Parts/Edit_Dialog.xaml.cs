﻿using ShadowRunHelper.CharModel;
using System.Collections.Generic;
using Windows.UI.Xaml.Controls;

// Die Elementvorlage "Inhaltsdialog" ist unter http://go.microsoft.com/fwlink/?LinkId=234238 dokumentiert.

namespace ShadowRunHelper.UI.Edit
{
    public sealed partial class Edit_Dialog : ContentDialog
    {
        public Thing Data;
        public List<string> MyStringOptions { get; set; }

        public Edit_Dialog(Thing data)
        {
            Data = data;
            InitializeComponent();
            Title = TypenHelper.ThingDefToString(Data.ThingType, false);
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
                    Wert.IsEnabled = false;
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
                    break;
                case ThingDefs.Attribut:
                    Bezeichner.IsEnabled = false;
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
