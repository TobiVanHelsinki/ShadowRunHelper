using Microsoft.AppCenter.Analytics;
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
        public Controller<Item> CTRLItem { get; set; }
        public Controller<Programm> CTRLProgramm { get; set; }
        public Controller<Munition> CTRLMunition { get; set; }
        public Controller<Vorteil> CTRLVorteil { get; set; }
        public Controller<Nachteil> CTRLNachteil { get; set; }
        public Controller<Connection> CTRLConnection { get; set; }
        public Controller<Sin> CTRLSin { get; set; }
        // Second Gen
        public Controller<Adeptenkraft> CTRLAdeptenkraft { get; set; }
        public Controller<Foki> CTRLFoki { get; set; }
        public Controller<Geist> CTRLGeist { get; set; }
        public Controller<Stroemung> CTRLStroemung { get; set; }
        public Controller<Tradition> CTRLTradition { get; set; }
        public Controller<Zaubersprueche> CTRLZaubersprueche { get; set; }
        // Third Gen
        public Controller<KomplexeForm> CTRLKomplexeForm { get; set; }
        public Controller<Widgets> CTRLWidgets { get; set; }
        public Controller<Sprite> CTRLSprite { get; set; }
        public Controller<Wandlung> CTRLWandlung { get; set; }
        public Controller<Initiation> CTRLInitiation { get; set; }

        // Importand ordering
        public AttributController CTRLAttribut { get; set; }
        public BerechnetController CTRLBerechnet { get; set; }
        public Controller<Implantat> CTRLImplantat { get; set; }

        public NahkampfwaffeController CTRLNahkampfwaffe { get; set; }
        public FernkampfwaffeController CTRLFernkampfwaffe { get; set; }
        public KommlinkController CTRLKommlink { get; set; }
        public CyberDeckController CTRLCyberDeck { get; set; }
        public VehikelController CTRLVehikel { get; set; }
        public PanzerungController CTRLPanzerung { get; set; }
        public Controller<Fertigkeit> CTRLFertigkeit { get; set; }
        public Controller<Handlung> CTRLHandlung { get; set; }
        public Person Person { get; set; }
        public CharSettings Settings { get; set; }
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
            CTRLAdeptenkraft = new Controller<Adeptenkraft>();
            CTRLAttribut = new AttributController();
            CTRLBerechnet = new BerechnetController();
            CTRLConnection = new Controller<Connection>();
            CTRLCyberDeck = new CyberDeckController();
            CTRLFernkampfwaffe = new FernkampfwaffeController();
            CTRLFertigkeit = new Controller<Fertigkeit>();
            CTRLFoki = new Controller<Foki>();
            CTRLGeist = new Controller<Geist>();
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
            CTRLStroemung = new Controller<Stroemung>();
            CTRLTradition = new Controller<Tradition>();
            CTRLVehikel = new VehikelController();
            CTRLVorteil = new Controller<Vorteil>();
            CTRLZaubersprueche = new Controller<Zaubersprueche>();

            CTRLKomplexeForm = new Controller<KomplexeForm>();
            CTRLWidgets = new Controller<Widgets>();
            CTRLSprite = new Controller<Sprite>();
            CTRLWandlung = new Controller<Wandlung>();
            CTRLInitiation = new Controller<Initiation>();

            lstCTRL.Add(CTRLAttribut);
            lstCTRL.Add(CTRLBerechnet);
            lstCTRL.Add(CTRLFertigkeit);
            lstCTRL.Add(CTRLItem);

            lstCTRL.Add(CTRLFernkampfwaffe);
            lstCTRL.Add(CTRLNahkampfwaffe);
            lstCTRL.Add(CTRLPanzerung);

            lstCTRL.Add(CTRLImplantat);

            lstCTRL.Add(CTRLAdeptenkraft);
            lstCTRL.Add(CTRLKomplexeForm);
            lstCTRL.Add(CTRLZaubersprueche);
            lstCTRL.Add(CTRLFoki);
            lstCTRL.Add(CTRLWidgets);

            lstCTRL.Add(CTRLCyberDeck);
            lstCTRL.Add(CTRLProgramm);

            lstCTRL.Add(CTRLVehikel);

            lstCTRL.Add(CTRLNachteil);
            lstCTRL.Add(CTRLVorteil);
            lstCTRL.Add(CTRLTradition);
            lstCTRL.Add(CTRLStroemung);
            lstCTRL.Add(CTRLGeist);

            lstCTRL.Add(CTRLInitiation);
            lstCTRL.Add(CTRLWandlung);
            lstCTRL.Add(CTRLSprite);


            lstCTRL.Add(CTRLMunition);
            lstCTRL.Add(CTRLConnection);
            lstCTRL.Add(CTRLKommlink);
            lstCTRL.Add(CTRLSin);

            lstCTRL.Add(CTRLHandlung);


            Person = new Person();
            Settings = new CharSettings();
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
            foreach (var ctrl in lstCTRL)
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
            _ThingList.RemoveAll((x) => x == tToRemove);
        }

        public void RefreshListeners()
        {
            // Don't register AnyPropertyChanged() at the PropertyChanged  Event of this Class -> endless loop;
            Person.PropertyChanged -= (x, y) => AnyPropertyChanged();
            Settings.PropertyChanged -= (x, y) => AnyPropertyChanged();
            Person.PropertyChanged += (x, y) => AnyPropertyChanged();
            Settings.PropertyChanged += (x, y) => AnyPropertyChanged();
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
