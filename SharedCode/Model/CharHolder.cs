///Author: Tobi van Helsinki

using ShadowRunHelper.CharController;
using ShadowRunHelper.CharModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using TAPPLICATION;
using TAPPLICATION.Model;
using TLIB;

//TODO
//diese liste anzeigen im chooser, evtl gruppiert nach kats, things
//auswählen von dingen gibt liste aller CalcValues zurück, diese wird dann eingegeben

namespace ShadowRunHelper.Model
{
    /// <summary>
    /// Haelt einen Char mit Controlern und Daten
    /// </summary>
    public class CharHolder : IMainType, INotifyPropertyChanged
    {
        #region vars
        // Admin Version Numbers ##############################################
        public string APP_VERSION_NUMBER => Constants.APP_VERSION_NUMBER;

        public string FILE_VERSION_NUMBER => Constants.CHARFILE_VERSION;
        #endregion vars

        #region Char Model DATA
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
        public Controller<Note> CTRLNote { get; } = new Controller<Note>();
        [Newtonsoft.Json.JsonIgnore]
        public FavoriteController CTRLFavorite { get; } = new FavoriteController();

        public Person Person { get; } = new Person();
        public CharSettings Settings { get; } = new CharSettings();
        #endregion Char Model DATA

        #region EASY ACCESS STUFF

        [Newtonsoft.Json.JsonIgnore]
        public List<IController> CTRLList { get; } = new List<IController>();

        [Newtonsoft.Json.JsonIgnore]
        public List<AllListEntry> LinkList { get; } = new List<AllListEntry>();

        [Newtonsoft.Json.JsonIgnore]
        public List<Thing> ThingList { get; } = new List<Thing>();

        [Newtonsoft.Json.JsonIgnore]
        public IEnumerable<CharCalcProperty> ConnectronProps => LinkList.SelectMany(x => x.Object.GetType().GetProperties().Where(p => p.PropertyType == typeof(CharCalcProperty)).Select(p => p.GetValue(x.Object) as CharCalcProperty));

        [Newtonsoft.Json.JsonIgnore]
        public IEnumerable<IEnumerable<Thing>> Connectrons => LinkList.Select(x => x.Object).GroupBy(x => x.ThingType);

        [Newtonsoft.Json.JsonIgnore]
        public ObservableCollection<Thing> Favorites { get; } = new ObservableCollection<Thing>();

        #endregion EASY ACCESS STUFF

        #region IO and Display Stuff

        [Newtonsoft.Json.JsonIgnore]
        public FileInfo FileInfo { get; set; }

        public string MakeName(bool UseProgress)
        {
            var strSaveName = "";

            string AddNameAndType(string Name)
            {
                Name += string.IsNullOrEmpty(Person.Alias) ? "Unnamed" : Person.Alias;
                Name += string.IsNullOrEmpty(Person.Char_Typ) ? "" : ("," + Person.Char_Typ);
                return Name;
            }

            if (UseProgress)
            {
                strSaveName = AddNameAndType(strSaveName);
                strSaveName += ",";
                strSaveName += Person.Runs.ToString();
                strSaveName += CustomManager.GetString("Model_Person_Runs/Text");
                strSaveName += ",";
                strSaveName += Person.Karma_Gesamt.ToString();
                strSaveName += CustomManager.GetString("Model_Person_Karma/Text");
            }
            else
            {
                strSaveName += FileInfo?.Name;
            }

            if (string.IsNullOrEmpty(strSaveName) || string.IsNullOrWhiteSpace(strSaveName))
            {
                strSaveName = AddNameAndType(strSaveName);
            }

            return strSaveName.EndsWith(Constants.DATEIENDUNG_CHAR) ? strSaveName : strSaveName += Constants.DATEIENDUNG_CHAR;
        }

        public override string ToString()
        {
            return MakeName(true) + " " + base.ToString();
        }
        #endregion IO and Display Stuff

        #region INI Stuff

        public CharHolder()
        {
            SaveTimer = new Timer((x) => { SaveRequest?.Invoke(x, this); HasChanges = false; }, this, Timeout.Infinite, Timeout.Infinite);
            // To Autosave

            CTRLList = GetType().GetProperties().Where(x => typeof(IController).IsAssignableFrom(x.PropertyType)).Select(x => x.GetValue(this) as IController).ToList();

            CTRLBerechnet.SetDependencies(Person, CTRLImplantat.Data, CTRLAttribut);
            Favorites.CollectionChanged += SaveFavoritesOrdering;

            RefreshLists();
            RefreshListeners();
        }

        #endregion INI Stuff

        #region DATA HANDLING STUFF
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PlatformHelper.CallPropertyChanged(PropertyChanged, this, propertyName);
        }

        public void AfterLoad()
        {
            Repair();
            Settings.Refresh();
            RefreshListeners();
            HasChanges = false;
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
                    var NewEntry = LinkList.Find(
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
                        Features.Analytics.TrackEvent("Err_CharRepair_Soft");
                    }
                    if (NewEntry == null)
                    {
                        NewEntry = LinkList.Find(x =>
                        x.Object.ThingType == item.Object.ThingType &&
                        x.PropertyID == item.PropertyID);
                        Features.Analytics.TrackEvent("Err_CharRepair_Soft");
                    }
                    if (NewEntry != null)
                    {
                        TargetCollection.Add(NewEntry);
                    }
                    else
                    {
                        Features.Analytics.TrackEvent("Err_CharRepair_Hard");
                        Log.Write(string.Format(CustomManager.GetString("Error_RepairLinkList"), item.Object.Bezeichner + item.PropertyID));
                    }
                }
                foreach (var item in TargetCollection)
                {
                    if (!SourceCollection.Contains(item))
                    {
                        SourceCollection.Add(item);
                    }
                }
                var tmpcol = SourceCollection.Except(TargetCollection).ToList();
                foreach (var item in tmpcol)
                {
                    SourceCollection.Remove(item);
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

        /// <summary>
        /// ThingDef2CTRL
        /// </summary>
        /// <param name="tag"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException">Ignore.</exception>
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
        /// <exception cref="InvalidOperationException">Ignore.</exception>
        public Thing Add(ThingDefs thingDefs)
        {
            var returnThing = CTRLList.First(c => c.eDataTyp == thingDefs).AddNewThing();
            RegisterNewThing(returnThing);
            return returnThing;
        }

        /// <summary>
        /// Adds the Thing to the right Controller and register it
        /// </summary>
        /// <exception cref="NotSupportedException" />
        /// <param name="thingDefs"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException">Ignore.</exception>
        public void Add(Thing NewThing)
        {
            CTRLList.First(c => c.eDataTyp == NewThing.ThingType).AddNewThing(NewThing);
            RegisterNewThing(NewThing);
        }

        private void RegisterNewThing(Thing NewThing)
        {
            NewThing.PropertyChanged += AnyPropertyChanged;
            NewThing.PropertyChanged += (x, y) =>
            {
                if (y.PropertyName == nameof(Thing.IsFavorite))
                {
                    RefreshLists();
                }
            };
        }

        /// <summary>
        /// Remove
        /// </summary>
        /// <param name="tToRemove"></param>
        /// <exception cref="InvalidOperationException">Ignore.</exception>
        public void Remove(Thing tToRemove)
        {
            CTRLList.First(c => c.eDataTyp == tToRemove.ThingType).RemoveThing(tToRemove);
            tToRemove.PropertyChanged -= AnyPropertyChanged;
            LinkList.RemoveAll((x) => x.Object == tToRemove);
            ThingList.RemoveAll((x) => x == tToRemove);
        }

        public void RefreshListeners()
        {
            // Don't register AnyPropertyChanged() at the PropertyChanged  Event of this Class -> endless loop;
            Person.PropertyChanged -= AnyPropertyChanged;
            Settings.PropertyChanged -= AnyPropertyChanged;
            Person.PropertyChanged += AnyPropertyChanged;
            Person.PropertyChanged -= RefreshFileName;
            Person.PropertyChanged += RefreshFileName;
            Settings.PropertyChanged += AnyPropertyChanged;
            foreach (var item in CTRLList)
            {
                item.RegisterEventAtData(AnyPropertyChanged);
                foreach (var item2 in item.GetElements())
                {
                    item2.PropertyChanged -= AnyPropertyChanged;
                    item2.PropertyChanged += AnyPropertyChanged;
                }
            }
        }

        private void RefreshFileName(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Person.Alias) || e.PropertyName == nameof(Person.Char_Typ))
            {
                if (AppModel.Instance?.IsFileActivated == false) // Should not be automated, ask user
                {
                    FileInfo?.ChangeName(MakeName(false));
                }
            }
        }

        public void RefreshLists()
        {
            LinkList.Clear();
            LinkList.AddRange(CTRLList.Aggregate(new List<AllListEntry>(), (l, c) => l.Concat(c.GetElementsForThingList()).ToList()));
            ThingList.Clear();
            ThingList.AddRange(CTRLList.Aggregate(new List<Thing>(), (l, c) => l.Concat(c.GetElements()).ToList()));
            RefreshListFav();
        }

        private bool RefreshInProgress;

        public void RefreshListFav()
        {
            if (!RefreshInProgress)
            {
                RefreshInProgress = true;
                Favorites.Clear();
                Favorites.AddRange(ThingList.Where(x => x?.IsFavorite == true).OrderBy(x => x.FavoriteIndex));
                CTRLFavorite.ClearData();
                foreach (var item in Favorites)
                {
                    CTRLFavorite.AddNewThing(item);
                }
                RefreshInProgress = false;
            }
        }

        private void SaveFavoritesOrdering(object sender, NotifyCollectionChangedEventArgs e)
        {
            for (var i = 0; i < Favorites.Count; i++)
            {
                Favorites[i].FavoriteIndex = i;
            }
            HasChanges = true;
        }

        #endregion DATA HANDLING STUFF

        #region AUTO_SAVE_STUFF

        [Newtonsoft.Json.JsonIgnore]
        private bool _HasChanges = false;
        [Newtonsoft.Json.JsonIgnore]
        public bool HasChanges
        {
            get => _HasChanges;
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
        private readonly System.Threading.Timer SaveTimer;
        /// <summary>
        /// fire if you want to get the char saved
        /// </summary>
        public event EventHandler<IMainType> SaveRequest;

        /// <summary>
        /// handler method if any property get's changed
        /// note: as this method handles the saves, the "HasChanges" var should be excepted
        /// </summary>
        private void AnyPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Thing.IsFavorite))
            {
                RefreshListFav();
            }
            HasChanges = true;
            if (SettingsModel.I?.AUTO_SAVE == true)
            {
                SetSaveTimerTo(SettingsModel.I.AUTO_SAVE_INTERVAL_MS);
            }
        }

        public bool SetSaveTimerTo(int Time = 0, bool ForceSave = false)
        {
            if (HasChanges || ForceSave)
            {
                try
                {
                    SaveTimer.Change(Time, Timeout.Infinite);
                    return true;
                }
                catch (ObjectDisposedException ex)
                {
                    Log.Write("Could not set save timer", ex, logType: LogType.Error);
                }
            }
            return false;
        }
        #endregion AUTO_SAVE_STUFF

        #region DnD
        private readonly List<Thing> MoveList = new List<Thing>();
        private bool _IsItemsPrepared;
        public bool IsItemsPrepared
        {
            get => _IsItemsPrepared;
            set { if (_IsItemsPrepared != value) { _IsItemsPrepared = value; NotifyPropertyChanged(); } }
        }

        public bool? IsItemsMove { get; set; }

        public void ClearPreparedItems()
        {
            MoveList.Clear();
            IsItemsPrepared = false;
            IsItemsMove = null;
        }

        public void PrepareToMoveOrCopy(Thing item)
        {
            if (item != null && !MoveList.Contains(item))
            {
                MoveList.Add(item);
                IsItemsPrepared = true;
            }
        }

        /// <summary>
        /// CopyPreparedItems
        /// </summary>
        /// <param name="NEW_CTRL"></param>
        /// <exception cref="InvalidOperationException">Ignore.</exception>
        public void CopyPreparedItems(ThingDefs NEW_CTRL)
        {
            foreach (var OLD_THING in MoveList)
            {
                var OLD_CTRL = CTRLList.First(x => x.eDataTyp == OLD_THING.ThingType);
                var NEW_THING = Add(NEW_CTRL);
                OLD_THING.TryCopy(NEW_THING);
            }
            MoveList.Clear();
            IsItemsPrepared = false;
            IsItemsMove = null;
        }

        /// <summary>
        /// MovePreparedItems
        /// </summary>
        /// <param name="NEW_CTRL"></param>
        /// <exception cref="InvalidOperationException">Ignore.</exception>
        public void MovePreparedItems(ThingDefs NEW_CTRL)
        {
            foreach (var OLD_THING in MoveList)
            {
                var OLD_CTRL = CTRLList.First(x => x.eDataTyp == OLD_THING.ThingType);
                var NEW_THING = Add(NEW_CTRL);
                OLD_THING.TryCopy(NEW_THING);
                OLD_CTRL.RemoveThing(OLD_THING);
            }
            MoveList.Clear();
            IsItemsPrepared = false;
            IsItemsMove = null;
        }
        #endregion DnD

        public void SubtractLifeStyleCost()
        {
            if (Person != null)
            {
                Person.Kontostand -= Person.LifeStyleCost;
            }
        }
    }
}