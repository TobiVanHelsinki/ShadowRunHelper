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
    //    public int Char_ID = 0;

        public ObservableCollection<CharController.Handlung> HandlungController { get; set; }
        public ObservableCollection<CharController.Fertigkeit> FertigkeitController { get; set; }
        public ObservableCollection<CharController.Attribut> AttributController { get; set; }
        public ObservableCollection<CharController.Item> ItemController { get; set; }
        public ObservableCollection<CharController.Programm> ProgrammController { get; set; }
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

        public CharModel.Person Person { get; set; }

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
            ProgrammController = new ObservableCollection<CharController.Programm>();
            MunitionController = new ObservableCollection<CharController.Munition>();
            ImplantatController = new ObservableCollection<CharController.Implantat>();
            VorteilController = new ObservableCollection<CharController.Vorteil>();
            NachteilController = new ObservableCollection<CharController.Nachteil>();
            ConnectionController = new ObservableCollection<CharController.Connection>();
            SinController = new ObservableCollection<CharController.Sin>();

            NahkampfwaffeController = new CharController.Nahkampfwaffe(HD, 0);
            FernkampfwaffeController = new CharController.Fernkampfwaffe(HD, 0);
            KommlinkController = new CharController.Kommlink(HD, 0);
            CyberDeckController = new CharController.CyberDeck(HD, 0);
            VehikelController = new CharController.Vehikel(HD, 0);
            PanzerungController = new CharController.Panzerung(HD, 0);

            Person = new CharModel.Person();
        }

/// <summary>
/// Konstruktor nutzen, wenn Daten der Controller und Objekte bereits vorhanden, Parmas sind die ID der MultiController
/// </summary>
        public CharHolder(
                        int nahkampfwaffe,
                        int fernkampfwaffe,
                        int kommlink,
                        int cyberdeck,
                        int vehikel,
                        int panzerung
            )
        {
            HandlungController = new ObservableCollection<CharController.Handlung>();
            HandlungController = new ObservableCollection<CharController.Handlung>();
            FertigkeitController = new ObservableCollection<CharController.Fertigkeit>();
            AttributController = new ObservableCollection<CharController.Attribut>();
            ItemController = new ObservableCollection<CharController.Item>();
            ProgrammController = new ObservableCollection<CharController.Programm>();
            MunitionController = new ObservableCollection<CharController.Munition>();
            ImplantatController = new ObservableCollection<CharController.Implantat>();
            VorteilController = new ObservableCollection<CharController.Vorteil>();
            NachteilController = new ObservableCollection<CharController.Nachteil>();
            ConnectionController = new ObservableCollection<CharController.Connection>();
            SinController = new ObservableCollection<CharController.Sin>();

            NahkampfwaffeController = new CharController.Nahkampfwaffe(HD, nahkampfwaffe);
            FernkampfwaffeController = new CharController.Fernkampfwaffe(HD, fernkampfwaffe);
            KommlinkController = new CharController.Kommlink(HD, kommlink);
            CyberDeckController = new CharController.CyberDeck(HD, cyberdeck);
            VehikelController = new CharController.Vehikel(HD, vehikel);
            PanzerungController = new CharController.Panzerung(HD, panzerung);
            //todo theoretisch müsste jder multiC für alle eigenschaften, auf die man würfelt ein eigenen HDE haben
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public void Add(String Controller_Name, int hd_ID)
        {
            if (Controller_Name.Contains("Handlung"))
            {
                HandlungController.Add(new CharController.Handlung(this.HD, hd_ID));
            }
            else if (Controller_Name.Contains("Fertigkeit"))
            {
                FertigkeitController.Add(new CharController.Fertigkeit(this.HD, hd_ID));
            }
            else if (Controller_Name.Contains("Attribut"))
            {
                AttributController.Add(new CharController.Attribut(this.HD, hd_ID));
            }
            else if (Controller_Name.Contains("Item"))
            {
                ItemController.Add(new CharController.Item(this.HD, hd_ID));
            }
            else if (Controller_Name.Contains("Programm"))
            {
                ProgrammController.Add(new CharController.Programm(this.HD, hd_ID));
            }
            else if (Controller_Name.Contains("Munition"))
            {
                MunitionController.Add(new CharController.Munition(this.HD, hd_ID));
            }
            else if (Controller_Name.Contains("Implantat"))
            {
                ImplantatController.Add(new CharController.Implantat(this.HD, hd_ID));
            }
            else if (Controller_Name.Contains("Vorteil"))
            {
                VorteilController.Add(new CharController.Vorteil(this.HD, hd_ID));
            }
            else if (Controller_Name.Contains("Nachteil"))
            {
                NachteilController.Add(new CharController.Nachteil(this.HD, hd_ID));
            }
            else if (Controller_Name.Contains("Connection"))
            {
                ConnectionController.Add(new CharController.Connection());
            }
            else if (Controller_Name.Contains("Sin"))
            {
                SinController.Add(new CharController.Sin());
            }
            else if (Controller_Name.Contains("Nahkampfwaffe"))
            {
                NahkampfwaffeController.add();
            }
            else if (Controller_Name.Contains("Fernkampfwaffe"))
            {
                FernkampfwaffeController.add();
            }
            else if (Controller_Name.Contains("Kommlink"))
            {
                KommlinkController.add();
            }
            else if (Controller_Name.Contains("CyberDeck"))
            {
                CyberDeckController.add();
            }
            else if (Controller_Name.Contains("Vehikel"))
            {
                VehikelController.add();
            }
            else if (Controller_Name.Contains("Panzerung"))
            {
                PanzerungController.add();
            }

        }
    }



}