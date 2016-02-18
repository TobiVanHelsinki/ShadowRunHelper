using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ShadowRun_Charakter_Helper.Controller
{
    /// <summary>
    /// Hält einen Char mit samst Controlern und Daten
    /// </summary>
    public class CharHolder
    {
        public HashDictionary HD = new HashDictionary();

        public ObservableCollection<CharController.Handlung> HandlungController { get; set; }
        public ObservableCollection<CharController.Fertigkeit> FertigkeitController { get; set; }
        public ObservableCollection<CharController.Attribut> AttributController { get; set; }
        public ObservableCollection<CharController.Item> ItemController { get; set; }
        public ObservableCollection<CharController.Munition> MunitionController { get; set; }
        public ObservableCollection<CharController.Implantat> ImplantatController { get; set; }
        public ObservableCollection<CharController.Vorteil> VorteilController { get; set; }
        public ObservableCollection<CharController.Nachteil> NachteilController { get; set; }
        public ObservableCollection<CharController.Connection> ConnectionController { get; set; }
        public ObservableCollection<CharController.Sin> SinController { get; set; }

        public CharController.Nahkampfwaffe NahkampfwaffeController { get; set; }
        public CharController.Fernkampfwaffe FernkampfwaffeController { get; set; }
        public CharController.Kommlink KommlinkController { get; set; }
        public CharController.CyberDeck CyberDeckController { get; set; }
        public CharController.Vehikel VehikelController { get; set; }
        public CharController.Panzerung PanzerungController { get; set; }

        /// <summary>
        /// Konstruktor nutzen, um neue Controller und Objekte zu erhalten
        /// </summary>
        public CharHolder()
        {

            HandlungController = new ObservableCollection<CharController.Handlung>();
            HandlungController = new ObservableCollection<CharController.Handlung>();
            FertigkeitController = new ObservableCollection<CharController.Fertigkeit>();
            AttributController = new ObservableCollection<CharController.Attribut>();
            ItemController = new ObservableCollection<CharController.Item>();
            MunitionController = new ObservableCollection<CharController.Munition>();
            ImplantatController = new ObservableCollection<CharController.Implantat>();
            VorteilController = new ObservableCollection<CharController.Vorteil>();
            NachteilController = new ObservableCollection<CharController.Nachteil>();
            ConnectionController = new ObservableCollection<CharController.Connection>();
            SinController = new ObservableCollection<CharController.Sin>();

            NahkampfwaffeController = new CharController.Nahkampfwaffe();
            FernkampfwaffeController = new CharController.Fernkampfwaffe();
            KommlinkController = new CharController.Kommlink();
            CyberDeckController = new CharController.CyberDeck();
            VehikelController = new CharController.Vehikel();
            PanzerungController = new CharController.Panzerung();

            NahkampfwaffeController.setHD(HD);
            FernkampfwaffeController.setHD(HD);
            KommlinkController.setHD(HD);
            CyberDeckController.setHD(HD);
            VehikelController.setHD(HD);
            PanzerungController.setHD(HD);
        }

/// <summary>
/// Konstruktor nutzen, wenn Daten der Controller und Objekte bereits vorhanden
/// </summary>
/// <param name="handlung"></param>
/// <param name="fertigkeit"></param>
/// <param name="attribut"></param>
/// <param name="item"></param>
/// <param name="munition"></param>
/// <param name="implantat"></param>
/// <param name="vorteil"></param>
/// <param name="nachteil"></param>
/// <param name="connection"></param>
/// <param name="sin"></param>
/// <param name="nahkampfwaffe"></param>
/// <param name="fernkampfwaffe"></param>
/// <param name="kommlink"></param>
/// <param name="cyberdeck"></param>
/// <param name="vehikel"></param>
/// <param name="panzerung"></param>
        public CharHolder(
                        ObservableCollection<CharController.Handlung> handlung,
                        ObservableCollection<CharController.Fertigkeit> fertigkeit,
                        ObservableCollection<CharController.Attribut> attribut,
                        ObservableCollection<CharController.Item> item,
                        ObservableCollection<CharController.Munition> munition,
                        ObservableCollection<CharController.Implantat> implantat,
                        ObservableCollection<CharController.Vorteil> vorteil,
                        ObservableCollection<CharController.Nachteil> nachteil,
                        ObservableCollection<CharController.Connection> connection,
                        ObservableCollection<CharController.Sin> sin,
                        CharController.Nahkampfwaffe nahkampfwaffe,
                        CharController.Fernkampfwaffe fernkampfwaffe,
                        CharController.Kommlink kommlink,
                        CharController.CyberDeck cyberdeck,
                        CharController.Vehikel vehikel,
                        CharController.Panzerung panzerung
            )
        {
            HandlungController = handlung;
            FertigkeitController = fertigkeit;
            AttributController = attribut;
            ItemController = item;
            MunitionController = munition;
            ImplantatController = implantat;
            VorteilController = vorteil;
            NachteilController = nachteil;
            ConnectionController = connection;
            SinController = sin;

            NahkampfwaffeController = nahkampfwaffe;
            FernkampfwaffeController = fernkampfwaffe;
            KommlinkController = kommlink;
            CyberDeckController = cyberdeck;
            VehikelController = vehikel;
            PanzerungController = panzerung;

            NahkampfwaffeController.setHD(HD);
            FernkampfwaffeController.setHD(HD);
            KommlinkController.setHD(HD);
            CyberDeckController.setHD(HD);
            VehikelController.setHD(HD);
            PanzerungController.setHD(HD);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}