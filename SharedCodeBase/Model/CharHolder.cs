using ShadowRunHelper.CharController;
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
        List<IController> lstCTRL = new List<IController>();

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

        /// <summary>
        /// Adds a Thing of the given ThingDef to the right controller and register it and returns it, if all is ok, otherways throw
        /// </summary>
        /// <exception cref="NotSupportedException" />
        /// <param name="thingDefs"></param>
        /// <returns></returns>
        public Thing Add(ThingDefs thingDefs)
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
                case ThingDefs.Stroemung_Wandlung:
                    returnThing = CTRLStroemung_Wandlung.AddNewThing();
                    break;
                case ThingDefs.Tradition_Initiation:
                    returnThing = CTRLTradition_Initiation.AddNewThing();
                    break;
                case ThingDefs.Zaubersprueche:
                    returnThing = CTRLZaubersprueche.AddNewThing();
                    break;
                default:
                    throw new NotSupportedException();
            }
            RefreshLists();
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
            switch (NewThing.ThingType)
            {
                case ThingDefs.Handlung:
                    CTRLHandlung.AddNewThing((Handlung)NewThing);
                    break;
                case ThingDefs.Fertigkeit:
                    CTRLFertigkeit.AddNewThing((Fertigkeit)NewThing);
                    break;
                case ThingDefs.Item:
                    CTRLItem.AddNewThing((Item)NewThing);
                    break;
                case ThingDefs.Programm:
                    CTRLProgramm.AddNewThing((Programm)NewThing);
                    break;
                case ThingDefs.Munition:
                    CTRLMunition.AddNewThing((Munition)NewThing);
                    break;
                case ThingDefs.Implantat:
                    CTRLImplantat.AddNewThing((Implantat)NewThing);
                    break;
                case ThingDefs.Vorteil:
                    CTRLVorteil.AddNewThing((Vorteil)NewThing);
                    break;
                case ThingDefs.Nachteil:
                    CTRLNachteil.AddNewThing((Nachteil)NewThing);
                    break;
                case ThingDefs.Connection:
                    CTRLConnection.AddNewThing((Connection)NewThing);
                    break;
                case ThingDefs.Sin:
                    CTRLSin.AddNewThing((Sin)NewThing);
                    break;
                case ThingDefs.Nahkampfwaffe:
                    CTRLNahkampfwaffe.AddNewThing((Nahkampfwaffe)NewThing);
                    break;
                case ThingDefs.Fernkampfwaffe:
                    CTRLFernkampfwaffe.AddNewThing((Fernkampfwaffe)NewThing);
                    break;
                case ThingDefs.Kommlink:
                    CTRLKommlink.AddNewThing((Kommlink)NewThing);
                    break;
                case ThingDefs.CyberDeck:
                    CTRLCyberDeck.AddNewThing((CyberDeck)NewThing);
                    break;
                case ThingDefs.Vehikel:
                    CTRLVehikel.AddNewThing((Vehikel)NewThing);
                    break;
                case ThingDefs.Panzerung:
                    CTRLPanzerung.AddNewThing((Panzerung)NewThing);
                    break;
                case ThingDefs.Adeptenkraft_KomplexeForm:
                    CTRLAdeptenkraft_KomplexeForm.AddNewThing((Adeptenkraft_KomplexeForm)NewThing);
                    break;
                case ThingDefs.Geist_Sprite:
                    CTRLGeist_Sprite.AddNewThing((Geist_Sprite)NewThing);
                    break;
                case ThingDefs.Foki_Widgets:
                    CTRLFoki_Widgets.AddNewThing((Foki_Widgets)NewThing);
                    break;
                case ThingDefs.Stroemung_Wandlung:
                    CTRLStroemung_Wandlung.AddNewThing((Stroemung_Wandlung)NewThing);
                    break;
                case ThingDefs.Tradition_Initiation:
                    CTRLTradition_Initiation.AddNewThing((Tradition_Initiation)NewThing);
                    break;
                case ThingDefs.Zaubersprueche:
                    CTRLZaubersprueche.AddNewThing((Zaubersprueche)NewThing);
                    break;
                default:
                    throw new NotSupportedException();
            }
            RefreshLists();
            NewThing.PropertyChanged += (x, y) => AnyPropertyChanged();
        }

        public void Remove(Thing tToRemove)
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
                case ThingDefs.Berechnet:
                    CTRLBerechnet.RemoveThing((Berechnet)tToRemove);
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
                case ThingDefs.Stroemung_Wandlung:
                    CTRLStroemung_Wandlung.RemoveThing((Stroemung_Wandlung)tToRemove);
                    break;
                case ThingDefs.Tradition_Initiation:
                    CTRLTradition_Initiation.RemoveThing((Tradition_Initiation)tToRemove);
                    break;
                case ThingDefs.Zaubersprueche:
                    CTRLZaubersprueche.RemoveThing((Zaubersprueche)tToRemove);
                    break;
                default:
                    break;
            }
            tToRemove.PropertyChanged -= (x, y) => AnyPropertyChanged();
            _LinkList.Remove(_LinkList.Find((x) => x.Object == tToRemove));
            _ThingList.Remove(_ThingList.Find((x) => x == tToRemove));
        }

        public void RefreshListeners()
        {
            // Don't register AnyPropertyChanged() at the PropertyChanged  Event of this Class -> endless loop;
            Person.PropertyChanged -= (x, y) => AnyPropertyChanged();
            Person.PropertyChanged += (x, y) => AnyPropertyChanged();
            foreach (var item in lstCTRL)
            {
                item.RegisterEventAtData(AnyPropertyChanged);
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
            foreach (var item in lstCTRL)
            {
                _LinkList.AddRange(item.GetElementsForThingList());
            }
            _ThingList.Clear();
            foreach (var item in lstCTRL)
            {
                _ThingList.AddRange(item.GetElements());
            }
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
        #region IMPORT / EXPORT STUFF ##############################################
        public IEnumerable<(string ThingType, string Content)> ToCSV(string strDelimiter)
        {
            const string strNewLine = "\n";
            string strNew = "sep=" + strDelimiter + strNewLine;
            return lstCTRL.Select(item => item.MultipleCSVExport(strDelimiter, strNewLine, strNew));
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
                case ThingDefs.Berechnet:
                    CTRLBerechnet.MultipleCSVImport(strDelimiter, strNewLine, strImport);
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
                case ThingDefs.Stroemung_Wandlung:
                    CTRLStroemung_Wandlung.MultipleCSVImport(strDelimiter, strNewLine, strImport);
                    break;
                case ThingDefs.Tradition_Initiation:
                    CTRLTradition_Initiation.MultipleCSVImport(strDelimiter, strNewLine, strImport);
                    break;
                case ThingDefs.Zaubersprueche:
                    CTRLZaubersprueche.MultipleCSVImport(strDelimiter, strNewLine, strImport);
                    break;
                default:
                    break;
            }
            RefreshLists();
        }

        #endregion
    }
}
