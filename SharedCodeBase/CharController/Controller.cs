using ShadowRunHelper.CharModel;
using ShadowRunHelper.Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System;

namespace ShadowRunHelper.CharController
{
    public class cController<T> : IController<T> where T : Thing, new()
    {
        /// <summary>
        /// GUI-Binding Target
        /// </summary>
        public ObservableCollection<T> Data { get; protected set; }
        protected readonly ThingDefs _eDataTyp;
        public ThingDefs eDataTyp { get => _eDataTyp; }

        public virtual void RegisterEventAtData(Action Method)
        {
            Data.CollectionChanged += (x, y) => Method();
        }

        public virtual T AddNewThing()
        {
            T newTee = new T();
            Data.Add(newTee);
            return newTee;
        }
        public virtual T AddNewThing(T newTee)
        {
            Data.Add(newTee);
            return newTee;
        }
        public virtual void RemoveThing(T tRem)
        {
            Data.Remove(tRem);
        }

        public virtual List<AllListEntry> GetElementsForThingList()
        {
            var lstReturn = new List<AllListEntry>();
            foreach (var item in Data)
            {
                lstReturn.Add(new AllListEntry(item, ""));
            }
            return lstReturn;
        }

        /// <summary>
        /// Konstruktor fuer neu
        /// </summary>
        public cController()
        {
            Data = new ObservableCollection<T>();
            switch (typeof(T).Name)
            {
                case "UndefTemp":
                    break; 
                case "Undef":
                    break;
                case "Handlung":
                    this._eDataTyp = ThingDefs.Handlung;
                    break;
                case "Fertigkeit":
                    this._eDataTyp = ThingDefs.Fertigkeit;
                    break;
                case "Item":
                    this._eDataTyp = ThingDefs.Item;
                    break;
                case "Programm":
                    this._eDataTyp = ThingDefs.Programm;
                    break;
                case "Munition":
                    this._eDataTyp = ThingDefs.Munition;
                    break;
                case "Implantat":
                    this._eDataTyp = ThingDefs.Implantat;
                    break;
                case "Vorteil":
                    this._eDataTyp = ThingDefs.Vorteil;
                    break;
                case "Nachteil":
                    this._eDataTyp = ThingDefs.Nachteil;
                    break;
                case "Connection":
                    this._eDataTyp = ThingDefs.Connection;
                    break;
                case "Sin":
                    this._eDataTyp = ThingDefs.Sin;
                    break;
                case "Attribut":
                    this._eDataTyp = ThingDefs.Attribut;
                    break;
                case "Nahkampfwaffe":
                    this._eDataTyp = ThingDefs.Nahkampfwaffe;
                    break;
                case "Fernkampfwaffe":
                    this._eDataTyp = ThingDefs.Fernkampfwaffe;
                    break;
                case "Kommlink":
                    this._eDataTyp = ThingDefs.Kommlink;
                    break;
                case "CyberDeck":
                    this._eDataTyp = ThingDefs.CyberDeck;
                    break;
                case "Vehikel":
                    this._eDataTyp = ThingDefs.Vehikel;
                    break;
                case "Panzerung":
                    this._eDataTyp = ThingDefs.Panzerung;
                    break;
                case "Eigenschaft":
                    this._eDataTyp = ThingDefs.Eigenschaft;
                    break;
                case "Adeptenkraft_KomplexeForm":
                    this._eDataTyp = ThingDefs.Adeptenkraft_KomplexeForm;
                    break;
                case "Foki_Widgets":
                    this._eDataTyp = ThingDefs.Foki_Widgets;
                    break;
                case "Geist_Sprite":
                    this._eDataTyp = ThingDefs.Geist_Sprite;
                    break;
                case "Stroemung_Wandlung":
                    this._eDataTyp = ThingDefs.Stroemung_Wandlung;
                    break;
                case "Tradition_Initiation":
                    this._eDataTyp = ThingDefs.Tradition_Initiation;
                    break;
                case "Zaubersprueche":
                    this._eDataTyp = ThingDefs.Zaubersprueche;
                    break;
                default :
                    break;
            }
        }

        public virtual (string ThingType, string Content) MultipleCSVExport(string strDelimiter, string strNewLine, string strNew)
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
            return (TypenHelper.ThingDefToString(eDataTyp, true), strTemp);
        }

        public virtual void MultipleCSVImport(char strDelimiter, char strNewLine, string strReadFile)
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

        public List<Thing> GetElements()
        {
            var ret = new List<Thing>();
            foreach (var item in Data)
            {
                ret.Add(item);
            }
            return ret;
        }

    }
}