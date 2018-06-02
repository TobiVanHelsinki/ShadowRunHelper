﻿using Microsoft.AppCenter.Analytics;
using ShadowRunHelper.CharController;
using ShadowRunHelper.CharModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using TAMARIN.IO;
using TAPPLICATION.Model;
using TLIB;

namespace ShadowRunHelper.Model
{
    /// <summary>
    /// Haelt einen Char mit Controlern und Daten
    /// </summary>
    public class CharHolder : IMainType, INotifyPropertyChanged
    {
        #region vars
        // Admin Version Numbers ##############################################
        public string APP_VERSION_NUMBER { get { return Constants.APP_VERSION_NUMBER; } }
        public string FILE_VERSION_NUMBER { get { return Constants.CHARFILE_VERSION; } }
        #endregion
        #region  Char Model DATA 
        // the various controlers
        // First Gen
        public Controller<Item> CTRLItem { get; } = new Controller<Item>();
        public Controller<Programm> CTRLProgramm { get; } = new Controller<Programm>();
        public Controller<Munition> CTRLMunition { get; } = new Controller<Munition>();
        public Controller<Vorteil> CTRLVorteil { get; } = new Controller<Vorteil>();
        public Controller<Nachteil> CTRLNachteil { get; } = new Controller<Nachteil>();
        public Controller<Connection> CTRLConnection { get; } = new Controller<Connection>();
        public Controller<Sin> CTRLSin { get; } = new Controller<Sin>();
        // Second Gen
        public Controller<Adeptenkraft> CTRLAdeptenkraft { get; } = new Controller<Adeptenkraft>();
        public Controller<Foki> CTRLFoki { get; } = new Controller<Foki>();
        public Controller<Geist> CTRLGeist { get; } = new Controller<Geist>();
        public Controller<Stroemung> CTRLStroemung { get; } = new Controller<Stroemung>();
        public Controller<Tradition> CTRLTradition { get; } = new Controller<Tradition>();
        public Controller<Zaubersprueche> CTRLZaubersprueche { get; } = new Controller<Zaubersprueche>();
        // Third Gen
        public Controller<KomplexeForm> CTRLKomplexeForm { get; } = new Controller<KomplexeForm>();
        public Controller<Widgets> CTRLWidgets { get; } = new Controller<Widgets>();
        public Controller<Sprite> CTRLSprite { get; } = new Controller<Sprite>();
        public Controller<Wandlung> CTRLWandlung { get; } = new Controller<Wandlung>();
        public Controller<Initiation> CTRLInitiation { get; } = new Controller<Initiation>();

        // Importand ordering
        public AttributController CTRLAttribut { get; } = new AttributController();
        public BerechnetController CTRLBerechnet { get; } = new BerechnetController();
        public Controller<Implantat> CTRLImplantat { get; } = new Controller<Implantat>();

        public NahkampfwaffeController CTRLNahkampfwaffe { get; } = new NahkampfwaffeController();
        public FernkampfwaffeController CTRLFernkampfwaffe { get; } = new FernkampfwaffeController();
        public KommlinkController CTRLKommlink { get; } = new KommlinkController();
        public CyberDeckController CTRLCyberDeck { get; } = new CyberDeckController();
        public VehikelController CTRLVehikel { get; } = new VehikelController();
        public PanzerungController CTRLPanzerung { get; } = new PanzerungController();
        public Controller<Fertigkeit> CTRLFertigkeit { get; } = new Controller<Fertigkeit>();
        public Controller<Handlung> CTRLHandlung { get; } = new Controller<Handlung>();
        public Person Person { get; } = new Person();
        public CharSettings Settings { get; } = new CharSettings();
        #endregion
        #region EASY ACCESS STUFF

        [Newtonsoft.Json.JsonIgnore]
        public List<IController> CTRLList { get; } = new List<IController>();
        [Newtonsoft.Json.JsonIgnore]
        public List<AllListEntry> LinkList { get; } = new List<AllListEntry>();
        [Newtonsoft.Json.JsonIgnore]
        public List<Thing> ThingList { get; } = new List<Thing>();

        #endregion
        #region IO and Display Stuff

        [Newtonsoft.Json.JsonIgnore]
        public FileInfoClass FileInfo { get; set; } = new FileInfoClass();

        public string MakeName(bool UseProgress = false, bool UseDate = false, string prefix = "", string postfix = "")
        {
            string strSaveName = "";
            strSaveName += prefix;
            if (!UseProgress)
            {
                strSaveName += FileInfo.Filename;
            }
            else
            {
                strSaveName += Person.Alias == string.Empty ? "$$" : Person.Alias;
                strSaveName += ",";
                strSaveName += Person.Char_Typ == string.Empty ? "$$" : Person.Char_Typ;
                strSaveName += ",";
                strSaveName += Person.Runs.ToString();
                strSaveName += StringHelper.GetString("Model_Person_Runs/Text") + ",";
                strSaveName += Person.Karma_Gesamt.ToString();
                strSaveName += StringHelper.GetString("Model_Person_Karma/Text");
            }
            strSaveName += UseDate ? "_" + DateTime.Now.Year + DateTime.Now.Month + DateTime.Now.Day + "_" + DateTime.Now.Hour + DateTime.Now.Minute + DateTime.Now.Second : "";
            strSaveName += postfix;

            if (strSaveName == null || strSaveName.Equals("") || strSaveName.Length == 0)
            {
                strSaveName = Person.Alias;
            }

            return strSaveName.EndsWith(Constants.DATEIENDUNG_CHAR) ? strSaveName : strSaveName += Constants.DATEIENDUNG_CHAR;
        }

        public string MakeName()
        {
            return MakeName(SettingsModel.I.FileNameUseProgres, SettingsModel.I.FileNameUseDate);
        }

        public override string ToString()
        {
            return MakeName(false, false) + " " + base.ToString();
        }
        #endregion
        #region INI Stuff
        public CharHolder()
        {
            SaveTimer = new System.Threading.Timer((x) => { SaveRequest?.Invoke(x, new EventArgs()); }, this, System.Threading.Timeout.Infinite, System.Threading.Timeout.Infinite);
            AppModel.Instance.MainObjectSaved += (x, y) => { SettingsModel.I.CountSavings++; };
            // To Autosave
            CTRLList.Add(CTRLAttribut);
            CTRLList.Add(CTRLBerechnet);
            CTRLList.Add(CTRLFertigkeit);
            CTRLList.Add(CTRLItem);

            CTRLList.Add(CTRLFernkampfwaffe);
            CTRLList.Add(CTRLNahkampfwaffe);
            CTRLList.Add(CTRLPanzerung);

            CTRLList.Add(CTRLImplantat);

            CTRLList.Add(CTRLAdeptenkraft);
            CTRLList.Add(CTRLZaubersprueche);
            CTRLList.Add(CTRLFoki);
            CTRLList.Add(CTRLWidgets);

            CTRLList.Add(CTRLCyberDeck);
            CTRLList.Add(CTRLProgramm);

            CTRLList.Add(CTRLVehikel);

            CTRLList.Add(CTRLNachteil);
            CTRLList.Add(CTRLVorteil);
            CTRLList.Add(CTRLTradition);
            CTRLList.Add(CTRLStroemung);
            CTRLList.Add(CTRLGeist);

            CTRLList.Add(CTRLInitiation);
            CTRLList.Add(CTRLWandlung);
            CTRLList.Add(CTRLSprite);


            CTRLList.Add(CTRLMunition);
            CTRLList.Add(CTRLConnection);
            CTRLList.Add(CTRLKommlink);
            CTRLList.Add(CTRLSin);

            CTRLList.Add(CTRLHandlung);
            CTRLList.Add(CTRLKomplexeForm);


            CTRLBerechnet.SetDependencies(Person, CTRLImplantat.Data, CTRLAttribut);
            RefreshLists();
            RefreshListeners();
        }
        #endregion
        #region DATA HANDLING STUFF 
        public event PropertyChangedEventHandler PropertyChanged;
        void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            ModelHelper.CallPropertyChanged(PropertyChanged, this, propertyName);
        }

        public void AfterLoad()
        {
            Repair();
            Settings.Refresh();
            RefreshListeners();
        }

#if DEBUG
        public void CustomProgrammerStuff()
        {
            //foreach (var item in CTRLHandlung.Data)
            //{
            //    item.Wert = 0;
            //    item.Gegen = 0;
            //    item.Grenze = 0;
            //}
        }
#endif

        public void Repair()
        {
            //declare submethod
            void RepairThingListRefs(ObservableCollection<AllListEntry> SourceCollection)
            {
                var TargetCollection = new ObservableCollection<AllListEntry>();
                foreach (var item in SourceCollection)
                {
                    AllListEntry NewEntry = LinkList.Find(
                        x => x.Object == item.Object && 
                        x.PropertyID == item.PropertyID);
                    if (NewEntry == null)
                    {
                        NewEntry = LinkList.Find(x =>
                        x.Object.Bezeichner == item.Object.Bezeichner &&
                        x.Object.ThingType == item.Object.ThingType &&
                        x.PropertyID == item.PropertyID);
                    }
                    if (NewEntry == null)
                    {
                        NewEntry = LinkList.Find(x =>
                        x.Object.Bezeichner == item.Object.Bezeichner &&
                        x.PropertyID == item.PropertyID);
                        Analytics.TrackEvent("Err_CharRepair_Soft");
                    }
                    if (NewEntry == null)
                    {
                        NewEntry = LinkList.Find(x =>
                        x.Object.ThingType == item.Object.ThingType &&
                        x.PropertyID == item.PropertyID);
                        Analytics.TrackEvent("Err_CharRepair_Soft");
                    }
                    if (NewEntry != null)
                    {
                        TargetCollection.Add(NewEntry);
                    }
                    else
                    {
                        Analytics.TrackEvent("Err_CharRepair_Hard");
                        AppModel.Instance.NewNotification(String.Format(StringHelper.GetString("Error_RepairLinkList"),item.Object.Bezeichner + item.PropertyID));
                    }
                }
                SourceCollection.Clear();
                foreach (var item in TargetCollection)
                {
                    SourceCollection.Add(item);
                }
            }
            // start repair
            RefreshLists();
            foreach (var ctrl in CTRLList)
            {
                foreach (var thing in ctrl.GetElements())
                {
                    foreach (var list in Thing.GetPropertiesLists(thing))
                    {
                        RepairThingListRefs(list.GetValue(thing) as LinkList);
                    }
                }
            }
#if DEBUG
            CustomProgrammerStuff();
#endif
        }
        public IController ThingDef2CTRL(ThingDefs tag)
        {
            return CTRLList.First(c => c.eDataTyp == tag);
        }

        /// <summary>
        /// Adds a Thing of the given ThingDef to the right controller and register it and returns it, if all is ok, otherways throw
        /// </summary>
        /// <exception cref="NotSupportedException" />
        /// <param name="thingDefs"></param>
        /// <returns></returns>
        public Thing Add(ThingDefs thingDefs)
        {
            Thing returnThing = CTRLList.First(c => c.eDataTyp == thingDefs).AddNewThing();
            returnThing.PropertyChanged += (x, y) => AnyPropertyChanged();
            return returnThing;
        }

        /// <summary>
        /// Adds the Thing to the right Controller and register it
        /// </summary>
        /// <exception cref="NotSupportedException" />
        /// <param name="thingDefs"></param>
        /// <returns></returns>
        public void Add(Thing NewThing)
        {
            CTRLList.First(c => c.eDataTyp == NewThing.ThingType).AddNewThing(NewThing);
            NewThing.PropertyChanged += (x, y) => AnyPropertyChanged();
        }

        public void Remove(Thing tToRemove)
        {
            CTRLList.First(c => c.eDataTyp == tToRemove.ThingType).RemoveThing(tToRemove);
            tToRemove.PropertyChanged -= (x, y) => AnyPropertyChanged();
            LinkList.RemoveAll((x) => x.Object == tToRemove);
            ThingList.RemoveAll((x) => x == tToRemove);
        }

        public void RefreshListeners()
        {
            // Don't register AnyPropertyChanged() at the PropertyChanged  Event of this Class -> endless loop;
            Person.PropertyChanged -= (x, y) => AnyPropertyChanged();
            Settings.PropertyChanged -= (x, y) => AnyPropertyChanged();
            Person.PropertyChanged += (x, y) => AnyPropertyChanged();
            Settings.PropertyChanged += (x, y) => AnyPropertyChanged();
            foreach (var item in CTRLList)
            {
                item.RegisterEventAtData(AnyPropertyChanged);
                item.RegisterEventAtData(RefreshLists);
                foreach (var item2 in item.GetElements())
                {
                    item2.PropertyChanged -= (x, y) => AnyPropertyChanged();
                    item2.PropertyChanged += (x, y) => AnyPropertyChanged();
                }
            }
        }

        public void RefreshLists()
        {
            LinkList.Clear();
            LinkList.AddRange(CTRLList.Aggregate(new List<AllListEntry>(),(l,c)=>l.Concat(c.GetElementsForThingList()).ToList()));
            ThingList.Clear();
            ThingList.AddRange(CTRLList.Aggregate(new List<Thing>(), (l, c) => l.Concat(c.GetElements()).ToList()));
        }


        #endregion
        #region AUTO_SAVE_STUFF 
        [Newtonsoft.Json.JsonIgnore]
        bool _HasChanges = false;
        [Newtonsoft.Json.JsonIgnore]
        public bool HasChanges
        {
            get { return _HasChanges; }
            set
            {
                if (value != _HasChanges)
                {
                    _HasChanges = value;
                    NotifyPropertyChanged();
                }
            }
        }
        [Newtonsoft.Json.JsonIgnore]
        System.Threading.Timer SaveTimer;
        /// <summary>
        /// fire if you want to get the char saved
        /// </summary>
        public event EventHandler SaveRequest;

        /// <summary>
        /// handler method if any property get's changed
        /// note: as this method handles the saves, the "HasChanges" var should be excepted
        /// </summary>
        void AnyPropertyChanged()
        {
            HasChanges = true;
            if (SettingsModel.I.AutoSave)
            {
                SetSaveTimerTo(SettingsModel.I.AutoSaveInterval);
            }
        }

        public bool SetSaveTimerTo(int Time = 0, bool ForceSave = false)
        {
            if (HasChanges || ForceSave)
            {
                HasChanges = false;
                SaveTimer.Change(Time, System.Threading.Timeout.Infinite);
                return true;
            }
            return false;
        }
        #endregion
        #region DnD
        readonly List<Thing> MoveList = new List<Thing>();
        public void PrepareToMove(Thing item)
        {
            if (item != null && !MoveList.Contains(item))
            {
                MoveList.Add(item);
            }
        }
        public void MovePreparedItems(IController NEW_CTRL)
        {
            foreach (var OLD_THING in MoveList)
            {
                var OLD_CTRL = CTRLList.First(x => x.eDataTyp == OLD_THING.ThingType);
                var NEW_THING = NEW_CTRL.AddNewThing();
                OLD_THING.TryCopy(NEW_THING);
                OLD_CTRL.RemoveThing(OLD_THING);
            }
            MoveList.Clear();
        }
        #endregion
        public static CharHolder CreateCharWithStandardContent()
        {
            var ret = new CharHolder();
            var item = new Handlung();
            item.Bezeichner = StringHelper.GetString("Content_Selbstbeherrschung");
            item.LinkedThings.Add(ret.CTRLAttribut.MI_Charisma);
            item.LinkedThings.Add(ret.CTRLAttribut.MI_Willen);
            ret.Add(item);
            item = new Handlung();
            item.Bezeichner = StringHelper.GetString("Content_Menschenkenntnis");
            item.LinkedThings.Add(ret.CTRLAttribut.MI_Intuition);
            item.LinkedThings.Add(ret.CTRLAttribut.MI_Charisma);
            ret.Add(item);
            item = new Handlung();
            item.Bezeichner = StringHelper.GetString("Content_Erinnerung");
            item.LinkedThings.Add(ret.CTRLAttribut.MI_Logik);
            item.LinkedThings.Add(ret.CTRLAttribut.MI_Willen);
            ret.Add(item);
            item = new Handlung();
            item.Bezeichner = StringHelper.GetString("Content_Schadenswiderstand");
            item.LinkedThings.Add(ret.CTRLAttribut.MI_Konsti);
            item.LinkedThings.Add(ret.CTRLPanzerung.MI_Wert);
            ret.Add(item);
            return ret;
        }
    }
}
