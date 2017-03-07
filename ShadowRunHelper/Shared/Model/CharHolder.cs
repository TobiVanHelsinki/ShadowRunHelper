using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using ShadowRunHelper.CharModel;
using ShadowRunHelper.CharController;
using System.Collections.ObjectModel;

namespace ShadowRunHelper.Model
{
    /// <summary>
    /// Hält einen Char mit Controlern und Daten
    /// </summary>
    public class CharHolder
    {
        public readonly string APP_VERSION_NUMBER = Konstanten.APP_VERSION_NUMBER_1_5;
        public readonly string FILE_VERSION_NUMBER = Konstanten.CHARFILE_VERSION_1_5;
        public cController<Item> CTRLItem { get; set; }
        public cController<Programm> CTRLProgramm { get; set; }
        public cController<Munition> CTRLMunition { get; set; }
        public cController<Implantat> CTRLImplantat { get; set; }
        public cController<Vorteil> CTRLVorteil { get; set; }
        public cController<Nachteil> CTRLNachteil { get; set; }
        public cController<Connection> CTRLConnection { get; set; }
        public cController<Sin> CTRLSin { get; set; }

        public cController<Adeptenkraft_KomplexeForm> CTRLAdeptenkraft_KomplexeForm { get; set; }
        public cController<Foki_Widgets> CTRLFoki_Widgets { get; set; }
        public cController<Geist_Sprite> CTRLGeist_Sprite { get; set; }
        public cController<Strömung_Wandlung> CTRLStrömung_Wandlung { get; set; }
        public cController<Tradition_Initiation> CTRLTradition_Initiation { get; set; }
        public cController<Zaubersprüche> CTRLZaubersprüche { get; set; }

        public cAttributController CTRLAttribut { get; set; }
        public cNahkampfwaffeController CTRLNahkampfwaffe { get; set; }
        public cFernkampfwaffeController CTRLFernkampfwaffe { get; set; }
        public cKommlinkController CTRLKommlink { get; set; }
        public cCyberDeckController CTRLCyberDeck { get; set; }
        public cVehikelController CTRLVehikel { get; set; }
        public cPanzerungController CTRLPanzerung { get; set; }
        public cController<Fertigkeit> CTRLFertigkeit { get; set; }
        public cController<Handlung> CTRLHandlung { get; set; }

        [Newtonsoft.Json.JsonIgnore]
        List<ThingListEntry> _lstThings;
        [Newtonsoft.Json.JsonIgnore]
        public List<ThingListEntry> lstThings { get { return _lstThings; } set { _lstThings = value; } }
        //List<cController<Thing>> lstCTRL;
            //List<cController<T>> bla, where T : Thing, new();

        public Person Person { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public CharHolder()
        {
            CTRLFertigkeit = new cController<Fertigkeit>();
            CTRLItem = new cController<Item>();
            CTRLProgramm = new cController<Programm>();
            CTRLMunition = new cController<Munition>();
            CTRLImplantat = new cController<Implantat>();
            CTRLVorteil = new cController<Vorteil>();
            CTRLNachteil = new cController<Nachteil>();
            CTRLConnection = new cController<Connection>();
            CTRLSin = new cController<Sin>();
            CTRLAttribut = new cAttributController();
            CTRLNahkampfwaffe = new cNahkampfwaffeController();
            CTRLFernkampfwaffe = new cFernkampfwaffeController();
            CTRLKommlink = new cKommlinkController();
            CTRLCyberDeck = new cCyberDeckController();
            CTRLVehikel = new cVehikelController();
            CTRLPanzerung = new cPanzerungController();

            CTRLAdeptenkraft_KomplexeForm = new cController<Adeptenkraft_KomplexeForm>();
            CTRLFoki_Widgets = new cController<Foki_Widgets>();
            CTRLGeist_Sprite = new cController<Geist_Sprite>();
            CTRLStrömung_Wandlung = new cController<Strömung_Wandlung>();
            CTRLTradition_Initiation = new cController<Tradition_Initiation>();
            CTRLZaubersprüche = new cController<Zaubersprüche>();

            CTRLHandlung = new cController<Handlung>();
            Person = new Person();
            CTRLAttribut.SetDependencies(Person, CTRLImplantat.Data);

            _lstThings = new List<ThingListEntry>();
            RefreshThingList();
        }

        internal Thing Add(ThingDefs thingDefs)
        {
            Thing returnThing = null;
            switch (thingDefs)
            {
                case ThingDefs.Handlung:
                    returnThing = CTRLHandlung.AddNewThing();
                    break;
                case ThingDefs.Fertigkeit:
                    returnThing = CTRLFertigkeit.AddNewThing();
                    break;
                case ThingDefs.Item:
                    returnThing = CTRLItem.AddNewThing();
                    break;
                case ThingDefs.Programm:
                    returnThing = CTRLProgramm.AddNewThing();
                    break;
                case ThingDefs.Munition:
                    returnThing = CTRLMunition.AddNewThing();
                    break;
                case ThingDefs.Implantat:
                    returnThing = CTRLImplantat.AddNewThing();
                    break;
                case ThingDefs.Vorteil:
                    returnThing = CTRLVorteil.AddNewThing();
                    break;
                case ThingDefs.Nachteil:
                    returnThing = CTRLNachteil.AddNewThing();
                    break;
                case ThingDefs.Connection:
                    returnThing = CTRLConnection.AddNewThing();
                    break;
                case ThingDefs.Sin:
                    returnThing = CTRLSin.AddNewThing();
                    break;
                case ThingDefs.Attribut:
                    returnThing = CTRLAttribut.AddNewThing();
                    break;
                case ThingDefs.Nahkampfwaffe:
                    returnThing = CTRLNahkampfwaffe.AddNewThing();
                    break;
                case ThingDefs.Fernkampfwaffe:
                    returnThing = CTRLFernkampfwaffe.AddNewThing();
                    break;
                case ThingDefs.Kommlink:
                    returnThing = CTRLKommlink.AddNewThing();
                    break;
                case ThingDefs.CyberDeck:
                    returnThing = CTRLCyberDeck.AddNewThing();
                    break;
                case ThingDefs.Vehikel:
                    returnThing = CTRLVehikel.AddNewThing();
                    break;
                case ThingDefs.Panzerung:
                    returnThing = CTRLPanzerung.AddNewThing();
                    break;
                case ThingDefs.Adeptenkraft_KomplexeForm:
                    returnThing = CTRLAdeptenkraft_KomplexeForm.AddNewThing();
                    break;
                case ThingDefs.Geist_Sprite:
                    returnThing = CTRLGeist_Sprite.AddNewThing();
                    break;
                case ThingDefs.Foki_Widgets:
                    returnThing = CTRLFoki_Widgets.AddNewThing();
                    break;
                case ThingDefs.Strömung_Wandlung:
                    returnThing = CTRLStrömung_Wandlung.AddNewThing();
                    break;
                case ThingDefs.Tradition_Initiation:
                    returnThing = CTRLTradition_Initiation.AddNewThing();
                    break;
                case ThingDefs.Zaubersprüche:
                    returnThing = CTRLZaubersprüche.AddNewThing();
                    break;
                default:
                    break;
            }
            RefreshThingList();
            return returnThing;
        }

        public void RefreshThingList()
        {
            _lstThings.Clear();
            _lstThings.AddRange(CTRLAttribut.GetElementsForThingList()); //liefert immer das selbe
            _lstThings.AddRange(CTRLFertigkeit.GetElementsForThingList()); //liefert immer das selbe
            _lstThings.AddRange(CTRLFernkampfwaffe.GetElementsForThingList()); //liefert immer das selbe
            _lstThings.AddRange(CTRLNahkampfwaffe.GetElementsForThingList()); //liefert immer das selbe
            _lstThings.AddRange(CTRLPanzerung.GetElementsForThingList()); //liefert immer das selbe

            _lstThings.AddRange(CTRLItem.GetElementsForThingList());

            _lstThings.AddRange(CTRLCyberDeck.GetElementsForThingList()); //liefert immer das selbe
            _lstThings.AddRange(CTRLKommlink.GetElementsForThingList()); //liefert immer das selbe
            _lstThings.AddRange(CTRLVehikel.GetElementsForThingList()); //liefert immer das selbe

            _lstThings.AddRange(CTRLProgramm.GetElementsForThingList());
            _lstThings.AddRange(CTRLMunition.GetElementsForThingList());
            _lstThings.AddRange(CTRLImplantat.GetElementsForThingList());
            _lstThings.AddRange(CTRLVorteil.GetElementsForThingList());
            _lstThings.AddRange(CTRLNachteil.GetElementsForThingList());
            _lstThings.AddRange(CTRLConnection.GetElementsForThingList());
            _lstThings.AddRange(CTRLSin.GetElementsForThingList());

            _lstThings.AddRange(CTRLAdeptenkraft_KomplexeForm.GetElementsForThingList());
            _lstThings.AddRange(CTRLFoki_Widgets.GetElementsForThingList());
            _lstThings.AddRange(CTRLGeist_Sprite.GetElementsForThingList());
            _lstThings.AddRange(CTRLStrömung_Wandlung.GetElementsForThingList());
            _lstThings.AddRange(CTRLTradition_Initiation.GetElementsForThingList());
            _lstThings.AddRange(CTRLZaubersprüche.GetElementsForThingList());

            //_lstThings.AddRange(CTRLHandlung.GetElementsForThingList()); // nötig?
        }

        internal void Remove(Thing tToRemove)
        {
            switch (tToRemove.ThingType)
            {
                case ThingDefs.Handlung:
                    CTRLHandlung.RemoveThing((Handlung)tToRemove);
                    break;
                case ThingDefs.Fertigkeit:
                    CTRLFertigkeit.RemoveThing((Fertigkeit)tToRemove);
                    break;
                case ThingDefs.Item:
                    CTRLItem.RemoveThing((Item)tToRemove);
                    break;
                case ThingDefs.Programm:
                    CTRLProgramm.RemoveThing((Programm)tToRemove);
                    break;
                case ThingDefs.Munition:
                    CTRLMunition.RemoveThing((Munition)tToRemove);
                    break;
                case ThingDefs.Implantat:
                    CTRLImplantat.RemoveThing((Implantat)tToRemove);
                    break;
                case ThingDefs.Vorteil:
                    CTRLVorteil.RemoveThing((Vorteil)tToRemove);
                    break;
                case ThingDefs.Nachteil:
                    CTRLNachteil.RemoveThing((Nachteil)tToRemove);
                    break;
                case ThingDefs.Connection:
                    CTRLConnection.RemoveThing((Connection)tToRemove);
                    break;
                case ThingDefs.Sin:
                    CTRLSin.RemoveThing((Sin)tToRemove);
                    break;
                case ThingDefs.Attribut:
                    CTRLAttribut.RemoveThing((Attribut)tToRemove);
                    break;
                case ThingDefs.Nahkampfwaffe:
                    CTRLNahkampfwaffe.RemoveThing((Nahkampfwaffe)tToRemove);
                    break;
                case ThingDefs.Fernkampfwaffe:
                    CTRLFernkampfwaffe.RemoveThing((Fernkampfwaffe)tToRemove);
                    break;
                case ThingDefs.Kommlink:
                    CTRLKommlink.RemoveThing((Kommlink)tToRemove);
                    break;
                case ThingDefs.CyberDeck:
                    CTRLCyberDeck.RemoveThing((CyberDeck)tToRemove);
                    break;
                case ThingDefs.Vehikel:
                    CTRLVehikel.RemoveThing((Vehikel)tToRemove);
                    break;
                case ThingDefs.Panzerung:
                    CTRLPanzerung.RemoveThing((Panzerung)tToRemove);
                    break;
                case ThingDefs.Adeptenkraft_KomplexeForm:
                    CTRLAdeptenkraft_KomplexeForm.RemoveThing((Adeptenkraft_KomplexeForm)tToRemove);
                    break;
                case ThingDefs.Geist_Sprite:
                    CTRLGeist_Sprite.RemoveThing((Geist_Sprite)tToRemove);
                    break;
                case ThingDefs.Foki_Widgets:
                    CTRLFoki_Widgets.RemoveThing((Foki_Widgets)tToRemove);
                    break;
                case ThingDefs.Strömung_Wandlung:
                    CTRLStrömung_Wandlung.RemoveThing((Strömung_Wandlung)tToRemove);
                    break;
                case ThingDefs.Tradition_Initiation:
                    CTRLTradition_Initiation.RemoveThing((Tradition_Initiation)tToRemove);
                    break;
                case ThingDefs.Zaubersprüche:
                    CTRLZaubersprüche.RemoveThing((Zaubersprüche)tToRemove);
                    break;
                default:
                    break;
            }
            _lstThings.Remove(_lstThings.Find((x) => x.Object == tToRemove));
        }

        public string MakeName()
        {
            return Person.strMakeName;
        }

        /// <summary>
        /// retval: List of Pairs: Key is Name for waht is at the string (attribut or cyberdeck orso)
        /// </summary>
        /// <param name="strDelimiter"></param>
        /// <returns></returns>
        /// 
        public List<KeyValuePair<string, string>> ToCSV(string strDelimiter)
        {
            var lstReturn = new List<KeyValuePair<string, string>>();
            const string strNewLine = "\n";
            string strNew = "sep="+strDelimiter+strNewLine;
        
            lstReturn.Add( CTRLAttribut.MultipleCSVExport(strDelimiter, strNewLine, strNew));
            lstReturn.Add( CTRLConnection.MultipleCSVExport(strDelimiter, strNewLine, strNew));
            lstReturn.Add( CTRLCyberDeck.MultipleCSVExport(strDelimiter, strNewLine, strNew));
            lstReturn.Add( CTRLFernkampfwaffe.MultipleCSVExport(strDelimiter, strNewLine, strNew));
            lstReturn.Add( CTRLFertigkeit.MultipleCSVExport(strDelimiter, strNewLine, strNew));
            lstReturn.Add( CTRLHandlung.MultipleCSVExport(strDelimiter, strNewLine, strNew));
            lstReturn.Add( CTRLImplantat.MultipleCSVExport(strDelimiter, strNewLine, strNew));
            lstReturn.Add( CTRLItem.MultipleCSVExport(strDelimiter, strNewLine, strNew));
            lstReturn.Add( CTRLKommlink.MultipleCSVExport(strDelimiter, strNewLine, strNew));
            lstReturn.Add( CTRLMunition.MultipleCSVExport(strDelimiter, strNewLine, strNew));
            lstReturn.Add( CTRLNachteil.MultipleCSVExport(strDelimiter, strNewLine, strNew));
            lstReturn.Add( CTRLNahkampfwaffe.MultipleCSVExport(strDelimiter, strNewLine, strNew));
            lstReturn.Add( CTRLPanzerung.MultipleCSVExport(strDelimiter, strNewLine, strNew));
            lstReturn.Add( CTRLProgramm.MultipleCSVExport(strDelimiter, strNewLine, strNew));
            lstReturn.Add( CTRLSin.MultipleCSVExport(strDelimiter, strNewLine, strNew));
            lstReturn.Add( CTRLVehikel.MultipleCSVExport(strDelimiter, strNewLine, strNew));
            lstReturn.Add( CTRLVorteil.MultipleCSVExport(strDelimiter, strNewLine, strNew));

            lstReturn.Add(CTRLAdeptenkraft_KomplexeForm.MultipleCSVExport(strDelimiter, strNewLine, strNew));
            lstReturn.Add(CTRLGeist_Sprite.MultipleCSVExport(strDelimiter, strNewLine, strNew));
            lstReturn.Add(CTRLFoki_Widgets.MultipleCSVExport(strDelimiter, strNewLine, strNew));
            lstReturn.Add(CTRLStrömung_Wandlung.MultipleCSVExport(strDelimiter, strNewLine, strNew));
            lstReturn.Add(CTRLTradition_Initiation.MultipleCSVExport(strDelimiter, strNewLine, strNew));
            lstReturn.Add(CTRLZaubersprüche.MultipleCSVExport(strDelimiter, strNewLine, strNew));

            return lstReturn;
        }

        public void FromCSV(char strDelimiter, string strImport, ThingDefs eThing)
        {
            const char strNewLine = '\n';
            switch (eThing)
            {
                case ThingDefs.UndefTemp:
                    break;
                case ThingDefs.Undef:
                    break;
                case ThingDefs.Handlung:
                    CTRLHandlung.MultipleCSVImport(strDelimiter, strNewLine, strImport);
                    break;
                case ThingDefs.Fertigkeit:
                    CTRLFertigkeit.MultipleCSVImport(strDelimiter, strNewLine, strImport);
                    break;
                case ThingDefs.Item:
                    CTRLItem.MultipleCSVImport(strDelimiter, strNewLine, strImport);
                    break;
                case ThingDefs.Programm:
                    CTRLProgramm.MultipleCSVImport(strDelimiter, strNewLine, strImport);
                    break;
                case ThingDefs.Munition:
                    CTRLMunition.MultipleCSVImport(strDelimiter, strNewLine, strImport);
                    break;
                case ThingDefs.Implantat:
                    CTRLImplantat.MultipleCSVImport(strDelimiter, strNewLine, strImport);
                    break;
                case ThingDefs.Vorteil:
                    CTRLVorteil.MultipleCSVImport(strDelimiter, strNewLine, strImport);
                    break;
                case ThingDefs.Nachteil:
                    CTRLNachteil.MultipleCSVImport(strDelimiter, strNewLine, strImport);
                    break;
                case ThingDefs.Connection:
                    CTRLConnection.MultipleCSVImport(strDelimiter, strNewLine, strImport);
                    break;
                case ThingDefs.Sin:
                    CTRLSin.MultipleCSVImport(strDelimiter, strNewLine, strImport);
                    break;
                case ThingDefs.Attribut:
                    CTRLAttribut.MultipleCSVImport(strDelimiter, strNewLine, strImport);
                    break;
                case ThingDefs.Nahkampfwaffe:
                    CTRLNahkampfwaffe.MultipleCSVImport(strDelimiter, strNewLine, strImport);
                    break;
                case ThingDefs.Fernkampfwaffe:
                    CTRLFernkampfwaffe.MultipleCSVImport(strDelimiter, strNewLine, strImport);
                    break;
                case ThingDefs.Kommlink:
                    CTRLKommlink.MultipleCSVImport(strDelimiter, strNewLine, strImport);
                    break;
                case ThingDefs.CyberDeck:
                    CTRLCyberDeck.MultipleCSVImport(strDelimiter, strNewLine, strImport);
                    break;
                case ThingDefs.Vehikel:
                    CTRLVehikel.MultipleCSVImport(strDelimiter, strNewLine, strImport);
                    break;
                case ThingDefs.Panzerung:
                    CTRLPanzerung.MultipleCSVImport(strDelimiter, strNewLine, strImport);
                    break;
                case ThingDefs.Eigenschaft:
                    break;
                case ThingDefs.Adeptenkraft_KomplexeForm:
                    CTRLAdeptenkraft_KomplexeForm.MultipleCSVImport(strDelimiter, strNewLine, strImport);
                    break;
                case ThingDefs.Geist_Sprite:
                    CTRLGeist_Sprite.MultipleCSVImport(strDelimiter, strNewLine, strImport);
                    break;
                case ThingDefs.Foki_Widgets:
                    CTRLFoki_Widgets.MultipleCSVImport(strDelimiter, strNewLine, strImport);
                    break;
                case ThingDefs.Strömung_Wandlung:
                    CTRLStrömung_Wandlung.MultipleCSVImport(strDelimiter, strNewLine, strImport);
                    break;
                case ThingDefs.Tradition_Initiation:
                    CTRLTradition_Initiation.MultipleCSVImport(strDelimiter, strNewLine, strImport);
                    break;
                case ThingDefs.Zaubersprüche:
                    CTRLZaubersprüche.MultipleCSVImport(strDelimiter, strNewLine, strImport);
                    break;
                default:
                    break;
            }
            RefreshThingList();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        public void Repair()
        {
            RefreshThingList();
            foreach (var item in CTRLHandlung.Data)
            {
                RepairThingListRefs(item.WertZusammensetzung, lstThings);
                RepairThingListRefs(item.GegenZusammensetzung, lstThings);
                RepairThingListRefs(item.GrenzeZusammensetzung, lstThings);
            }
            foreach (var item in CTRLFertigkeit.Data)
            {
                RepairThingListRefs(item.PoolZusammensetzung, lstThings);
            }
        }

        private static void RepairThingListRefs(ObservableCollection<ThingListEntry> SourceCollection, List<ThingListEntry> lstThings)
        {
            var TargetCollection = new ObservableCollection<ThingListEntry>();
            foreach (var item in SourceCollection)
            {
                ThingListEntry NewEntry;
                NewEntry = lstThings.Find(x => x.Object.Equals(item.Object) && x.strProperty == item.strProperty);
                if (NewEntry == null)
                {
                    NewEntry = lstThings.Find(x => x.Object.Bezeichner == item.Object.Bezeichner && x.strProperty == item.strProperty);
                }
                if (NewEntry == null)
                {
                    NewEntry = lstThings.Find(x => x.Object.Bezeichner == item.Object.Bezeichner);
                }
                if (NewEntry != null)
                {
                    TargetCollection.Add(NewEntry);
                }
            }
            SourceCollection.Clear();
            foreach (var item in TargetCollection)
            {
                SourceCollection.Add(item);
            }
        }
    }
}
