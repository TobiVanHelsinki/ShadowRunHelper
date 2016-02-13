using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ShadowRun_Charakter_Helper.Controller
{
    public class CharHolder
    {
        public HashDictionary HD = new HashDictionary();
        public List<String> RessourcesMultiple = new List<String>();
        public List<String> RessourcesSingle = new List<String>();

        public ObservableCollection<CharController.Handlung> HandlungsController { get; set; }
        public ObservableCollection<CharController.Fertigkeit> FertigkeitenController { get; set; }
        public ObservableCollection<CharController.Fertigkeit> AttributsController { get; set; }
        public ObservableCollection<CharController.Handlung> ItemController { get; set; }
        public ObservableCollection<CharController.Fertigkeit> MunitionsController { get; set; }
        public ObservableCollection<CharController.Handlung> ImplantateController { get; set; }
        public ObservableCollection<CharController.Fertigkeit> VorteilsController { get; set; }
        public ObservableCollection<CharController.Handlung> NachteilsController { get; set; }
        public ObservableCollection<CharController.Fertigkeit> ConnectionsController { get; set; }
        public ObservableCollection<CharController.Handlung> SinsController { get; set; }
        public CharController.Panzerung NahkampfwaffenController { get; set; }
        public CharController.Panzerung FernkampfwaffenController { get; set; }
        public CharController.Panzerung KommlinkController { get; set; }
        public CharController.Panzerung DeckController { get; set; }
        public CharController.Panzerung VehikelController { get; set; }
        public CharController.Panzerung PanzerungsController { get; set; }


        public static T GetInstance<T>(params object[] args)
        {
            return (T)Activator.CreateInstance(typeof(T), args);
        }


        // für "neu"
        public CharHolder()
        {
            addRessources();
            foreach (String element in RessourcesSingle)
            {
                Type loopType = Type.GetType(element);
                var HandlungsController = Activator.CreateInstance(loopType);
            }
            foreach (String element in RessourcesMultiple)
            {
                Type loopType = Type.GetType(element);
            }



            //HandlungsController = new ObservableCollection<CharController.Handlung>();
            //PanzerungsController = new CharController.Panzerung();
            //    PanzerungsController.setHD(HD);
        }

        // für "laden"
        public CharHolder(
                ObservableCollection<CharController.Fertigkeit> F,
                ObservableCollection<CharController.Handlung> H,
                CharController.Panzerung P)
        {
            FertigkeitenController = F;
            HandlungsController = H;
            PanzerungsController = P;
            PanzerungsController.setHD(HD);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private void addRessources()
        {
            RessourcesSingle.Add("ShadowRun_Charakter_Helper.CharController.Fertigkeit");
            RessourcesSingle.Add("ShadowRun_Charakter_Helper.CharController.Handlung");
            RessourcesSingle.Add("ShadowRun_Charakter_Helper.CharController.Attribut");
            RessourcesSingle.Add("ShadowRun_Charakter_Helper.CharController.Item");
            RessourcesSingle.Add("ShadowRun_Charakter_Helper.CharController.Programm");
            RessourcesSingle.Add("ShadowRun_Charakter_Helper.CharController.Vorteil");
            RessourcesSingle.Add("ShadowRun_Charakter_Helper.CharController.Nachteil");
            RessourcesMultiple.Add("ShadowRun_Charakter_Helper.CharController.Panzerung");
            RessourcesMultiple.Add("ShadowRun_Charakter_Helper.CharController.CyberDecks");
            RessourcesMultiple.Add("ShadowRun_Charakter_Helper.CharController.Kommlink");
            RessourcesMultiple.Add("ShadowRun_Charakter_Helper.CharController.Nahkampfwaffen");
            RessourcesMultiple.Add("ShadowRun_Charakter_Helper.CharController.Fernkampfwaffen");

            
        }
    }
}