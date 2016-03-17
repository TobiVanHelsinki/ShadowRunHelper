using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ShadowRunHelper.Controller
{
    /// <summary>
    /// Hält einen Char mit samst Controlern und Daten
    /// </summary>
    public class CharHolder
    {
        TSystem TSystem;


        
        // noch ein event einbauen, damit fehler nach hier oben gegeben wreden können
        // außerdem eine klasse für dinge wie kö und geist limit machen
        private void Probleme_Lösen(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Probleme_Lösen");
            foreach (var itemFehlerEintrag in HD.AlteHDEntrys)
            {
                foreach (var itemHandlung in HandlungController)
                {
                    foreach (var itemZusammensetzung in itemHandlung.Data.Zusammensetzung)
                    {
                        if (itemZusammensetzung.Key == itemFehlerEintrag.Key)
                        {
                            Model.DictionaryCharEntry Value = itemZusammensetzung.Value;
                            itemHandlung.Data.Zusammensetzung.Remove(itemFehlerEintrag.Key);
                            itemHandlung.Data.Zusammensetzung.Add(itemFehlerEintrag.Value, Value);
                        }
                    }
                    foreach (var itemZusammensetzungGrenze in itemHandlung.Data.GrenzeZusammensetzung)
                    {
                        if (itemZusammensetzungGrenze.Key == itemFehlerEintrag.Key)
                        {
                            Model.DictionaryCharEntry Value = itemZusammensetzungGrenze.Value;
                            itemHandlung.Data.Zusammensetzung.Remove(itemFehlerEintrag.Key);
                            itemHandlung.Data.Zusammensetzung.Add(itemFehlerEintrag.Value, Value);
                        }
                    }
                }
            }
            //TODO evtl. prüfen, ob alles geklappt hat
            HD.AlteHDEntrys.Clear();
            HD.Toggle -= new HDlockedHandler(Probleme_Lösen);
        }


        public HashDictionary HD = new HashDictionary();
        public string APP_VERSION_NUMBER = Variablen.APP_VERSION_NUMBER;

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
            TSystem = new TSystem();
            System.Diagnostics.Debug.WriteLine("CharHolder(): Probleme_Lösen Registrieren");
            HD.Toggle += new HDlockedHandler(Probleme_Lösen);

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

        private void HD_Toggle(object sender, EventArgs e)
        {
            throw new NotImplementedException();
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
            TSystem = new TSystem();
            System.Diagnostics.Debug.WriteLine("CharHolder(): Probleme_Lösen Registrieren");
            HD.Toggle += new HDlockedHandler(Probleme_Lösen);

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

            Person = new CharModel.Person();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        /// <summary>
        /// wird zum sauberen hinzufügen eines objectes zum Holer benutzt
        /// </summary>
        /// <param name="Controller_Name"></param>
        /// <param name="hd_ID"></param>
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
        /// <summary>
        /// Diese Methode wird zum sauberen Entfernen eines Object aus dem Holder verwendet
        /// </summary>
        /// <param name="Controller_Name"></param>
        /// <param name="hd_ID"></param>
        /// <param name="Controller_Item"></param>
        public void Remove(String Controller_Name, int hd_ID, object Controller_Item)
        {
            if (Controller_Name.Contains("Handlung"))
            {
                ((CharController.Handlung)Controller_Item).remove_from_HD();
                HandlungController.Remove((CharController.Handlung)Controller_Item);
            }
            else if (Controller_Name.Contains("Fertigkeit"))
            {
                ((CharController.Fertigkeit)Controller_Item).remove_from_HD();
                FertigkeitController.Remove((CharController.Fertigkeit)Controller_Item);
            }
            else if (Controller_Name.Contains("Attribut"))
            {
                ((CharController.Attribut)Controller_Item).remove_from_HD();
                AttributController.Remove((CharController.Attribut)Controller_Item);
            }
            else if (Controller_Name.Contains("Item"))
            {
                ((CharController.Item)Controller_Item).remove_from_HD();
                ItemController.Remove((CharController.Item)Controller_Item);
            }
            else if (Controller_Name.Contains("Programm"))
            {
                ((CharController.Programm)Controller_Item).remove_from_HD();
                ProgrammController.Remove((CharController.Programm)Controller_Item);
            }
            else if (Controller_Name.Contains("Munition"))
            {
                ((CharController.Munition)Controller_Item).remove_from_HD();
                MunitionController.Remove((CharController.Munition)Controller_Item);
            }
            else if (Controller_Name.Contains("Implantat"))
            {
                ((CharController.Implantat)Controller_Item).remove_from_HD();
                ImplantatController.Remove((CharController.Implantat)Controller_Item);
            }
            else if (Controller_Name.Contains("Vorteil"))
            {
                ((CharController.Vorteil)Controller_Item).remove_from_HD();
                VorteilController.Remove((CharController.Vorteil)Controller_Item);
            }
            else if (Controller_Name.Contains("Nachteil"))
            {
                ((CharController.Nachteil)Controller_Item).remove_from_HD();
                NachteilController.Remove((CharController.Nachteil)Controller_Item);
            }
            else if (Controller_Name.Contains("Connection"))
            {
                ConnectionController.Remove((CharController.Connection)Controller_Item);
            }
            else if (Controller_Name.Contains("Sin"))
            {
                SinController.Remove((CharController.Sin)Controller_Item);
            }
            else if (Controller_Name.Contains("Nahkampfwaffe"))
            {
              //  ((CharController.Nahkampfwaffe)Controller_Item).remove_from_HD();
                NahkampfwaffeController.Remove((CharModel.Nahkampfwaffe)Controller_Item);
            }
            else if (Controller_Name.Contains("Fernkampfwaffe"))
            {
               // ((CharController.Fernkampfwaffe)Controller_Item).remove_from_HD();
                FernkampfwaffeController.Remove((CharModel.Fernkampfwaffe)Controller_Item);
            }
            else if (Controller_Name.Contains("Kommlink"))
            {
               // ((CharController.Kommlink)Controller_Item).remove_from_HD();
                KommlinkController.Remove((CharModel.Kommlink)Controller_Item);
            }
            else if (Controller_Name.Contains("CyberDeck"))
            {
             //   ((CharController.CyberDeck)Controller_Item).remove_from_HD();
                CyberDeckController.Remove((CharModel.CyberDeck)Controller_Item);
            }
            else if (Controller_Name.Contains("Vehikel"))
            {
                //((CharController.Vehikel)Controller_Item).remove_from_HD();
                VehikelController.Remove((CharModel.Vehikel)Controller_Item);
            }
            else if (Controller_Name.Contains("Panzerung"))
            {
              //  ((CharController.Panzerung)Controller_Item).remove_from_HD();
                PanzerungController.Remove((CharModel.Panzerung)Controller_Item);
            }

        }
    }
}