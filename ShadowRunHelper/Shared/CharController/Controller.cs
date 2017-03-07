using ShadowRunHelper.CharModel;
using ShadowRunHelper.Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ShadowRunHelper.CharController
{
    public class cController<T> : IController<T> where T : Thing, new()
    {
        /// <summary>
        /// GUI-Binding Target
        /// </summary>
        public ObservableCollection<T> Data { get; protected set; }
        public readonly ThingDefs eDataTyp;

        public T AddNewThing()
        {
            T newTee = new T();
            Data.Add(newTee);
            return newTee;
        }
        public void RemoveThing(T tRem)
        {
            Data.Remove(tRem);
        }

        public List<ThingListEntry> GetElementsForThingList()
        {
            var lstReturn = new List<ThingListEntry>();
            foreach (var item in Data)
            {
                lstReturn.Add(new ThingListEntry(item, ""));
            }
            return lstReturn;
        }

        /// <summary>
        /// Konstruktor für neu
        /// </summary>
        public cController()
        {
            Data = new ObservableCollection<T>();
            Data.CollectionChanged += Data_CollectionChanged;
            switch (typeof(T).Name)
            {
                case "UndefTemp":
                    break; 
                case "Undef":
                    break;
                case "Handlung":
                    this.eDataTyp = ThingDefs.Handlung;
                    break;
                case "Fertigkeit":
                    this.eDataTyp = ThingDefs.Fertigkeit;
                    break;
                case "Item":
                    this.eDataTyp = ThingDefs.Item;
                    break;
                case "Programm":
                    this.eDataTyp = ThingDefs.Programm;
                    break;
                case "Munition":
                    this.eDataTyp = ThingDefs.Munition;
                    break;
                case "Implantat":
                    this.eDataTyp = ThingDefs.Implantat;
                    break;
                case "Vorteil":
                    this.eDataTyp = ThingDefs.Vorteil;
                    break;
                case "Nachteil":
                    this.eDataTyp = ThingDefs.Nachteil;
                    break;
                case "Connection":
                    this.eDataTyp = ThingDefs.Connection;
                    break;
                case "Sin":
                    this.eDataTyp = ThingDefs.Sin;
                    break;
                case "Attribut":
                    this.eDataTyp = ThingDefs.Attribut;
                    break;
                case "Nahkampfwaffe":
                    this.eDataTyp = ThingDefs.Nahkampfwaffe;
                    break;
                case "Fernkampfwaffe":
                    this.eDataTyp = ThingDefs.Fernkampfwaffe;
                    break;
                case "Kommlink":
                    this.eDataTyp = ThingDefs.Kommlink;
                    break;
                case "CyberDeck":
                    this.eDataTyp = ThingDefs.CyberDeck;
                    break;
                case "Vehikel":
                    this.eDataTyp = ThingDefs.Vehikel;
                    break;
                case "Panzerung":
                    this.eDataTyp = ThingDefs.Panzerung;
                    break;
                case "Eigenschaft":
                    this.eDataTyp = ThingDefs.Eigenschaft;
                    break;
                case "Adeptenkraft_KomplexeForm":
                    this.eDataTyp = ThingDefs.Adeptenkraft_KomplexeForm;
                    break;
                case "Foki_Widgets":
                    this.eDataTyp = ThingDefs.Foki_Widgets;
                    break;
                case "Geist_Sprite":
                    this.eDataTyp = ThingDefs.Geist_Sprite;
                    break;
                case "Strömung_Wandlung":
                    this.eDataTyp = ThingDefs.Strömung_Wandlung;
                    break;
                case "Tradition_Initiation":
                    this.eDataTyp = ThingDefs.Tradition_Initiation;
                    break;
                case "Zaubersprüche":
                    this.eDataTyp = ThingDefs.Zaubersprüche;
                    break;
                default :
                    break;
            }
        }

        private void Data_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
        }

        public KeyValuePair<string, string> MultipleCSVExport(string strDelimiter, string strNewLine, string strNew)
        {
            string strTemp = strNew;
            if (this.Data.Count >= 1)
            {
                strTemp += this.Data[0].HeaderToCSV(strDelimiter);
            }
            strTemp += strNewLine;
            foreach (T item in this.Data)
            {
                strTemp += item.ToCSV(strDelimiter);
                strTemp += strNewLine;
            }
            return new KeyValuePair<string, string>(strTemp, TypenHelper.ThingDefToString(eDataTyp, true));
        }



        public void MultipleCSVImport(char strDelimiter, char strNewLine, string strReadFile)
        {
            string[] Lines = strReadFile.Split(strNewLine);
            string[] Headar = { };
            for (int i = 0; i < Lines.Length; i++) //start at 2 to overjump first lines
            {
                // key = propertie name, value = value
                string[] CSVEntries = Lines[i].Split(strDelimiter);
                if (CSVEntries.Length < 5)
                {
                    continue;
                }
                if (Headar.Length < CharModel.Thing.nThingPropertyCount) 
                {
                    Headar = Lines[i].Split(strDelimiter);
                    continue;
                }
                Dictionary<string, string> Dic = new Dictionary<string, string>();
                int j = 0;
                foreach (var itemstring in CSVEntries)
                {
                    Dic.Add(Headar[j], itemstring);
                    j++;
                }
                (this.AddNewThing())?.FromCSV(Dic);
            }
        }
    }
}