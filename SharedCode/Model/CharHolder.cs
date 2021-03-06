﻿//Author: Tobi van Helsinki

using ShadowRunHelper.CharController;
using ShadowRunHelper.CharModel;
using ShadowRunHelper.Helper;
using SharedCode.Resources;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using TLIB;

namespace ShadowRunHelper.Model
{
    /// <summary>
    /// Haelt einen Char mit Controlern und Daten
    /// </summary>
    public class CharHolder : IMainType, INotifyPropertyChanged
    {
        #region vars
        public string APP_VERSION_NUMBER => "ND";

        public string FILE_VERSION_NUMBER => Constants.CHARFILE_VERSION;
        #endregion vars

        #region Char Model DATA
        // the various controlers First Gen
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
        public ActiveItemController<Nahkampfwaffe> CTRLNahkampfwaffe { get; } = new ActiveItemController<Nahkampfwaffe>();
        public ActiveItemController<Fernkampfwaffe> CTRLFernkampfwaffe { get; } = new ActiveItemController<Fernkampfwaffe>();
        public ActiveItemController<Kommlink> CTRLKommlink { get; } = new ActiveItemController<Kommlink>();
        public ActiveItemController<CyberDeck> CTRLCyberDeck { get; } = new ActiveItemController<CyberDeck>();
        public ActiveItemController<Vehikel> CTRLVehikel { get; } = new ActiveItemController<Vehikel>();
        public ActiveItemController<Drones> CTRLDrones { get; } = new ActiveItemController<Drones>();
        public ActiveItemController<Panzerung> CTRLPanzerung { get; } = new ActiveItemController<Panzerung>();
        public Controller<Fertigkeit> CTRLFertigkeit { get; } = new Controller<Fertigkeit>();
        public Controller<Handlung> CTRLHandlung { get; } = new Controller<Handlung>();
        public Controller<Note> CTRLNote { get; } = new Controller<Note>();
        [Newtonsoft.Json.JsonIgnore]
        public FavoriteController CTRLFavorite { get; } = new FavoriteController();

        public Person Person { get; } = new Person();
        public CharSettings Settings { get; } = new CharSettings();
        #endregion Char Model DATA

        #region EASY ACCESS STUFF
        /// <summary>
        /// Gets the controlers.
        /// </summary>
        [Newtonsoft.Json.JsonIgnore]
        public List<IController> Controlers { get; } = new List<IController>();

        /// <summary> Gets all things
        [Newtonsoft.Json.JsonIgnore]
        public List<Thing> Things { get; } = new List<Thing>();

        /// <summary>
        /// Gets all things grouped after ThingType Important BindingTarget
        /// </summary>
        [Newtonsoft.Json.JsonIgnore]
        public IEnumerable<IEnumerable<Thing>> GroupedThings => Things.GroupBy(x => x.ThingType);

        /// <summary>
        /// Gets all connects of all things
        /// </summary>
        [Newtonsoft.Json.JsonIgnore]
        public List<ConnectProperty> Connects { get; } = new List<ConnectProperty>();

        /// <summary>
        /// Gets all the things that have the favorites property set
        /// </summary>
        [Newtonsoft.Json.JsonIgnore]
        public ObservableCollection<Thing> Favorites { get; } = new ObservableCollection<Thing>();

        #endregion EASY ACCESS STUFF

        #region IO and Display Stuff

        [Newtonsoft.Json.JsonIgnore]
        public FileInfo FileInfo { get; set; }

        public string MakeName(bool UseProgress)
        {
            string strSaveName = "";

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
                strSaveName += ModelResources.Person_Runs;
                strSaveName += ",";
                strSaveName += Person.Karma_Gesamt.ToString();
                strSaveName += ModelResources.Person_Karma;
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

            Controlers = GetType().GetProperties().Where(x => typeof(IController).IsAssignableFrom(x.PropertyType)).Select(x => x.GetValue(this) as IController).ToList();

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

        /// <summary>
        /// Repair
        /// </summary>
        /// <exception cref="System.Security.SecurityException">Ignore.</exception>
        public void Repair()
        {
            RefreshLists();
            //repair implicit created items
            foreach (Thing thing in Things)
            {
                foreach (System.Reflection.PropertyInfo prop in thing.GetPropertiesConnects())
                {
                    if (prop.GetValue(thing) is ConnectProperty calc)
                    {
                        calc.Owner = thing;
#if DEBUG
                        if (calc.Owner is null)
                        {
                            if (System.Diagnostics.Debugger.IsAttached)
                            {
                                System.Diagnostics.Debugger.Break();
                            }
                        }
#endif
                        calc.Name = prop.Name;
                        calc.DisplayName = ModelResources.ResourceManager.GetStringSafe(prop.DeclaringType.Name + "_" + prop.Name);
                    }
#if DEBUG
                    else
                    {
                        if (System.Diagnostics.Debugger.IsAttached)
                        {
                            System.Diagnostics.Debugger.Break();
                        }
                    }
#endif
                }
            }
            //repair lost connections
            foreach (ConnectProperty origconnect in Connects)
            {
                List<ConnectProperty> repairedConnects = new List<ConnectProperty>();
                foreach (ConnectProperty reconnect in origconnect.Connected)
                {
                    ConnectProperty newconnect = Connects.FirstOrDefault(
                        x => x.Owner == reconnect.Owner &&
                        x.Name == reconnect.Name);
                    if (newconnect is null)
                    {
#if DEBUG
                        if (System.Diagnostics.Debugger.IsAttached)
                        {
                            System.Diagnostics.Debugger.Break();
                        }
#endif
                        Log.Write("Cannot find the correct connection reference for " + reconnect.Owner.Bezeichner + " with the property: " + reconnect.Name);
                        newconnect = Connects.FirstOrDefault(x =>
                        x.Owner.ThingType == reconnect.Owner.ThingType &&
                        x.Owner.Bezeichner == reconnect.Owner.Bezeichner &&
                        x.Name == reconnect.Name);
                    }
                    if (newconnect is null)
                    {
                        Log.Write("Cannot find a bezeichner-thingtype-matching connection for " + reconnect.Owner.Bezeichner + " with the property: " + reconnect.Name);
                        newconnect = Connects.FirstOrDefault(x =>
                        x.Owner.Bezeichner == reconnect.Owner.Bezeichner &&
                        x.Name == reconnect.Name);
                    }
                    if (newconnect is null)
                    {
                        Log.Write("Cannot find a bezeichner-matching connection for " + reconnect.Owner.Bezeichner + " with the property: " + reconnect.Name);
                        newconnect = Connects.FirstOrDefault(x =>
                        x.Owner.ThingType == reconnect.Owner.ThingType &&
                        x.Name == reconnect.Name);
                    }
                    if (newconnect != null)
                    {
                        repairedConnects.Add(newconnect);
                    }
                    else
                    {
                        Log.Write("Cannot find a connect for " + reconnect.Owner.Bezeichner + " with the property: " + reconnect.Name);
                    }
                }
                //Add new Connects to the connect list
                foreach (ConnectProperty item in repairedConnects)
                {
                    if (!origconnect.Connected.Contains(item))
                    {
                        origconnect.Connected.Add(item);
                    }
                }
                //Remove the wrong items, for wich we found the correct ones
                foreach (ConnectProperty item in origconnect.Connected.Except(repairedConnects).ToList())
                {
                    origconnect.Connected.Remove(item);
                }
            }
            //TODO user notification. über das ergebnis
        }

        /// <summary>
        /// ThingDef2CTRL
        /// </summary>
        /// <param name="tag"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException">Ignore.</exception>
        public IController ThingDef2CTRL(ThingDefs tag)
        {
            return Controlers.First(c => c.eDataTyp == tag);
        }

        /// <summary>
        /// Adds a Thing of the given ThingDef to the right controller and register it and returns
        /// it, if all is ok, otherways throw
        /// </summary>
        /// <exception cref="NotSupportedException"/>
        /// <param name="thingDefs"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException">Ignore.</exception>
        public Thing Add(ThingDefs thingDefs)
        {
            Thing returnThing = Controlers.First(c => c.eDataTyp == thingDefs).AddNewThing();
            RegisterNewThing(returnThing);
            return returnThing;
        }

        /// <summary>
        /// Adds the Thing to the right Controller and register it
        /// </summary>
        /// <exception cref="NotSupportedException"/>
        /// <param name="thingDefs"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException">Ignore.</exception>
        public void Add(Thing NewThing)
        {
            Controlers.First(c => c.eDataTyp == NewThing.ThingType).AddNewThing(NewThing);
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
            Controlers.First(c => c.eDataTyp == tToRemove.ThingType).RemoveThing(tToRemove);
            tToRemove.PropertyChanged -= AnyPropertyChanged;
            Connects.RemoveAll((x) => x.Owner == tToRemove);
            Things.RemoveAll((x) => x == tToRemove);
        }

        public void RefreshListeners()
        {
            // Don't register AnyPropertyChanged() at the PropertyChanged Event of this Class ->
            // endless loop;
            Person.PropertyChanged -= AnyPropertyChanged;
            Settings.PropertyChanged -= AnyPropertyChanged;
            Person.PropertyChanged += AnyPropertyChanged;
            Person.PropertyChanged -= RefreshFileName;
            Person.PropertyChanged += RefreshFileName;
            Settings.PropertyChanged += AnyPropertyChanged;
            foreach (IController item in Controlers)
            {
                item.RegisterEventAtData(AnyPropertyChanged);
                foreach (Thing item2 in item.GetAllData())
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
            Things.Clear();
            Things.AddRange(Controlers.SelectMany(x => x.GetAllData()));
            Connects.Clear();
            Connects.AddRange(Things.SelectMany(x => x.GetConnects()));
            RefreshListFav();
        }

        private bool RefreshInProgress;

        public void RefreshListFav()
        {
            if (!RefreshInProgress)
            {
                RefreshInProgress = true;
                Favorites.Clear();
                Favorites.AddRange(Things.Where(x => x?.IsFavorite == true).OrderBy(x => x.FavoriteIndex));
                CTRLFavorite.ClearData();
                foreach (Thing item in Favorites)
                {
                    CTRLFavorite.AddNewThing(item);
                }
                RefreshInProgress = false;
            }
        }

        private void SaveFavoritesOrdering(object sender, NotifyCollectionChangedEventArgs e)
        {
            for (int i = 0; i < Favorites.Count; i++)
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
        private readonly Timer SaveTimer;
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

        #region Copying and Moving of items
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
            foreach (Thing OLD_THING in MoveList)
            {
                IController OLD_CTRL = Controlers.First(x => x.eDataTyp == OLD_THING.ThingType);
                Thing NEW_THING = Add(NEW_CTRL);
                OLD_THING.TryCloneInto(NEW_THING);
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
            foreach (Thing OLD_THING in MoveList)
            {
                IController OLD_CTRL = Controlers.First(x => x.eDataTyp == OLD_THING.ThingType);
                Thing NEW_THING = Add(NEW_CTRL);
                OLD_THING.TryCloneInto(NEW_THING);
                OLD_CTRL.RemoveThing(OLD_THING);
            }
            MoveList.Clear();
            IsItemsPrepared = false;
            IsItemsMove = null;
        }
        #endregion Copying and Moving of items

        public void SubtractLifeStyleCost()
        {
            if (Person != null)
            {
                Person.Kontostand -= Person.LifeStyleCost;
            }
        }
    }
}