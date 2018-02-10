﻿using ShadowRunHelper.CharController;
using ShadowRunHelper.CharModel;
using SharedCodeBase.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using TLIB_UWPFRAME;
using TLIB_UWPFRAME.IO;
using TLIB_UWPFRAME.Model;
namespace ShadowRunHelper.Model
{
    /// <summary>
    /// Haelt einen Char mit Controlern und Daten
    /// </summary>
    public class CharHolder : IMainType, INotifyPropertyChanged
    {
        #region vars
        // Admin Version Numbers ##############################################
        public string APP_VERSION_NUMBER { get { return Constants.APP_VERSION_NUMBER_1_5; } }
        public string FILE_VERSION_NUMBER { get { return Constants.CHARFILE_VERSION_1_5; } }
        #endregion
        #region  Char Model DATA 
        // the various controlers
        public Controller<Item> CTRLItem { get; set; }
        public Controller<Programm> CTRLProgramm { get; set; }
        public Controller<Munition> CTRLMunition { get; set; }
        public Controller<Implantat> CTRLImplantat { get; set; }
        public Controller<Vorteil> CTRLVorteil { get; set; }
        public Controller<Nachteil> CTRLNachteil { get; set; }
        public Controller<Connection> CTRLConnection { get; set; }
        public Controller<Sin> CTRLSin { get; set; }

        public Controller<Adeptenkraft_KomplexeForm> CTRLAdeptenkraft_KomplexeForm { get; set; }
        public Controller<Foki_Widgets> CTRLFoki_Widgets { get; set; }
        public Controller<Geist_Sprite> CTRLGeist_Sprite { get; set; }
        public Controller<Stroemung_Wandlung> CTRLStroemung_Wandlung { get; set; }
        public Controller<Tradition_Initiation> CTRLTradition_Initiation { get; set; }
        public Controller<Zaubersprueche> CTRLZaubersprueche { get; set; }

        public AttributController CTRLAttribut { get; set; }
        [Newtonsoft.Json.JsonIgnore]
        public BerechnetController CTRLBerechnet { get; set; }

        public NahkampfwaffeController CTRLNahkampfwaffe { get; set; }
        public FernkampfwaffeController CTRLFernkampfwaffe { get; set; }
        public KommlinkController CTRLKommlink { get; set; }
        public CyberDeckController CTRLCyberDeck { get; set; }
        public VehikelController CTRLVehikel { get; set; }
        public PanzerungController CTRLPanzerung { get; set; }
        public Controller<Fertigkeit> CTRLFertigkeit { get; set; }
        public Controller<Handlung> CTRLHandlung { get; set; }
        public Person Person { get; set; }
        #endregion
        #region EASY ACCESS STUFF

        [Newtonsoft.Json.JsonIgnore]
        public List<IController> lstCTRL = new List<IController>();

        List<AllListEntry> _LinkList;
        [Newtonsoft.Json.JsonIgnore]
        public List<AllListEntry> LinkList { get => _LinkList; set => _LinkList = value; }

        List<Thing> _ThingList;
        [Newtonsoft.Json.JsonIgnore]
        public List<Thing> ThingList { get => _ThingList; set => _ThingList = value; }

        #endregion
        #region IO and Display Stuff
        /// <summary>
        /// used to "override" the std convert method. we need something more ... flexible
        /// </summary>
        [Newtonsoft.Json.JsonIgnore]
        public Func<string, string, string, IMainType> Converter => IO.CharHolderIO.ConvertWithRightVersion;

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
                strSaveName += CrossPlatformHelper.GetString("Model_Person_Runs/Text") + ",";
                strSaveName += Person.Karma_Gesamt.ToString();
                strSaveName += CrossPlatformHelper.GetString("Model_Person_Karma/Text");
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
            CTRLAdeptenkraft_KomplexeForm = new Controller<Adeptenkraft_KomplexeForm>();
            CTRLAttribut = new AttributController();
            CTRLBerechnet = new BerechnetController();
            CTRLConnection = new Controller<Connection>();
            CTRLCyberDeck = new CyberDeckController();
            CTRLFernkampfwaffe = new FernkampfwaffeController();
            CTRLFertigkeit = new Controller<Fertigkeit>();
            CTRLFoki_Widgets = new Controller<Foki_Widgets>();
            CTRLGeist_Sprite = new Controller<Geist_Sprite>();
            CTRLHandlung = new Controller<Handlung>();
            CTRLImplantat = new Controller<Implantat>();
            CTRLItem = new Controller<Item>();
            CTRLKommlink = new KommlinkController();
            CTRLMunition = new Controller<Munition>();
            CTRLNachteil = new Controller<Nachteil>();
            CTRLNahkampfwaffe = new NahkampfwaffeController();
            CTRLPanzerung = new PanzerungController();
            CTRLProgramm = new Controller<Programm>();
            CTRLSin = new Controller<Sin>();
            CTRLStroemung_Wandlung = new Controller<Stroemung_Wandlung>();
            CTRLTradition_Initiation = new Controller<Tradition_Initiation>();
            CTRLVehikel = new VehikelController();
            CTRLVorteil = new Controller<Vorteil>();
            CTRLZaubersprueche = new Controller<Zaubersprueche>();

            lstCTRL.Add(CTRLAttribut);
            lstCTRL.Add(CTRLBerechnet);
            lstCTRL.Add(CTRLFertigkeit);
            lstCTRL.Add(CTRLItem);

            lstCTRL.Add(CTRLFernkampfwaffe);
            lstCTRL.Add(CTRLNahkampfwaffe);
            lstCTRL.Add(CTRLPanzerung);

            lstCTRL.Add(CTRLImplantat);

            lstCTRL.Add(CTRLAdeptenkraft_KomplexeForm);
            lstCTRL.Add(CTRLZaubersprueche);
            lstCTRL.Add(CTRLFoki_Widgets);

            lstCTRL.Add(CTRLCyberDeck);
            lstCTRL.Add(CTRLProgramm);

            lstCTRL.Add(CTRLVehikel);

            lstCTRL.Add(CTRLNachteil);
            lstCTRL.Add(CTRLVorteil);
            lstCTRL.Add(CTRLTradition_Initiation);
            lstCTRL.Add(CTRLStroemung_Wandlung);
            lstCTRL.Add(CTRLGeist_Sprite);

            lstCTRL.Add(CTRLMunition);
            lstCTRL.Add(CTRLConnection);
            lstCTRL.Add(CTRLKommlink);
            lstCTRL.Add(CTRLSin);

            lstCTRL.Add(CTRLHandlung);


            Person = new Person();
            CTRLAttribut.SetDependencies(Person, CTRLImplantat.Data);
            CTRLBerechnet.SetDependencies(Person, CTRLImplantat.Data, CTRLAttribut);
            _LinkList = new List<AllListEntry>();
            _ThingList = new List<Thing>();
            RefreshLists();
            RefreshListeners();
        }
        #endregion
        #region DATA HANDLING STUFF 
        public event PropertyChangedEventHandler PropertyChanged;
        void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            ModelHelper.CallPropertyChangedAtDispatcher(PropertyChanged, this, propertyName);
        }
        public void Repair()
        {
            //declare submethod
            void RepairThingListRefs(ObservableCollection<AllListEntry> SourceCollection, List<AllListEntry> lstThings)
            {
                var TargetCollection = new ObservableCollection<AllListEntry>();
                foreach (var item in SourceCollection)
                {
                    AllListEntry NewEntry;
                    NewEntry = lstThings.Find(x => x.Object.Equals(item.Object) && x.PropertyID == item.PropertyID);
                    if (NewEntry == null)
                    {
                        NewEntry = lstThings.Find(x => x.Object.Bezeichner == item.Object.Bezeichner && x.Object.ThingType.Equals(item.Object.ThingType) && x.PropertyID.Equals(item.PropertyID));
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
            // start repair
            RefreshLists();
            foreach (var item in CTRLHandlung.Data)
            {
                RepairThingListRefs(item.WertZusammensetzung, LinkList);
                RepairThingListRefs(item.GegenZusammensetzung, LinkList);
                RepairThingListRefs(item.GrenzeZusammensetzung, LinkList);
            }
            foreach (var item in CTRLFertigkeit.Data)
            {
                RepairThingListRefs(item.PoolZusammensetzung, LinkList);
            }
        }
        public IController ThingDef2CTRL(ThingDefs tag)
        {
            return lstCTRL.First(c => c.eDataTyp == tag);
        }

        /// <summary>
        /// Adds a Thing of the given ThingDef to the right controller and register it and returns it, if all is ok, otherways throw
        /// </summary>
        /// <exception cref="NotSupportedException" />
        /// <param name="thingDefs"></param>
        /// <returns></returns>
        public Thing Add(ThingDefs thingDefs)
        {
            Thing returnThing = lstCTRL.First(c => c.eDataTyp == thingDefs).AddNewThing();
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
            lstCTRL.First(c => c.eDataTyp == NewThing.ThingType).AddNewThing(NewThing);
            NewThing.PropertyChanged += (x, y) => AnyPropertyChanged();
        }

        public void Remove(Thing tToRemove)
        {
            lstCTRL.First(c => c.eDataTyp == tToRemove.ThingType).RemoveThing(tToRemove);
            tToRemove.PropertyChanged -= (x, y) => AnyPropertyChanged();
            _LinkList.RemoveAll((x) => x.Object == tToRemove);
            _ThingList.RemoveAll((x) => x == tToRemove); //TODO changed to Remove all, check it
        }

        public void RefreshListeners()
        {
            // Don't register AnyPropertyChanged() at the PropertyChanged  Event of this Class -> endless loop;
            Person.PropertyChanged -= (x, y) => AnyPropertyChanged();
            Person.PropertyChanged += (x, y) => AnyPropertyChanged();
            foreach (var item in lstCTRL)
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
            _LinkList.Clear();
            _LinkList.AddRange(lstCTRL.Aggregate(new List<AllListEntry>(),(l,c)=>l.Concat(c.GetElementsForThingList()).ToList()));
            _ThingList.Clear();
            _ThingList.AddRange(lstCTRL.Aggregate(new List<Thing>(), (l, c) => l.Concat(c.GetElements()).ToList()));
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
                if (!value)
                {
                    //SaveTimer.Change(System.Threading.Timeout.Infinite, System.Threading.Timeout.Infinite);
                }
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
    }
}
