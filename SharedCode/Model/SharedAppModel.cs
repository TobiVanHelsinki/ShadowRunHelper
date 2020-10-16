//Author: Tobi van Helsinki

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using TLIB;

namespace ShadowRunHelper.Model
{
    public class SharedAppModel : INotifyPropertyChanged
    {
        #region NotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion NotifyPropertyChanged

        protected static SharedAppModel instance;
        public static SharedAppModel Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SharedAppModel();
                }
                return instance;
            }
        }

        public ObservableCollection<LogMessage> lstNotifications = new ObservableCollection<LogMessage>();

        public SharedAppModel() => Log.NewLogArrived += (logmessage) => lstNotifications.Insert(0, logmessage);
    }

    public class SharedAppModel<MainType> : SharedAppModel where MainType : IMainType, new()
    {
        public event EventHandler<IMainType> MainObjectSaved;

        public static new SharedAppModel<MainType> Instance => (SharedAppModel<MainType>)instance;

        public MainType MainObject
        {
            get => _MainObjects.FirstOrDefault();
            set
            {
                foreach (var item in _MainObjects.ToArray())
                {
                    RemoveMainObject(item);
                };
                AddMainObject(value);
            }
        }

        private readonly List<MainType> _MainObjects = new List<MainType>();
        public IEnumerable<MainType> MainObjects => _MainObjects;

        public void AddMainObject(MainType sender)
        {
            if (!_MainObjects.Contains(sender))
            {
                _MainObjects.Add(sender);
                sender.SaveRequest += SaveMainType;
                NotifyPropertyChanged(nameof(MainObject));
            }
        }

        public void RemoveMainObject(MainType sender)
        {
            if (_MainObjects.Contains(sender))
            {
                _MainObjects.Remove(sender);
                sender.SaveRequest -= SaveMainType;
                NotifyPropertyChanged(nameof(MainObject));
            }
        }

        public SharedAppModel() : base() => PropertyChanged += SharedAppModel_PropertyChanged;

        private void SharedAppModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(MainObject))
            {
                if (MainObject != null)
                {
                    SharedSettingsModel.I.LAST_SAVE_INFO = MainObject.FileInfo;
                }
                else
                {
                    SharedSettingsModel.I.LAST_SAVE_INFO = null;
                }
            }
        }

        public void SaveMainType(object sender2, IMainType MainObject)
        {
            try
            {
                Log.Write("SharedAppModel_PropertyChanged save");
                var T = IO.SharedIO.SaveAtOriginPlace(MainObject);
                T.Wait();
                SharedSettingsModel.I.LAST_SAVE_INFO = T.Result;
                MainObjectSaved?.Invoke(MainObject, MainObject);
                Log.Write("MainObject Saved " + MainObject.ToString());
            }
            catch (Exception ex)
            {
                Log.Write("Could not save mainmodel", ex, logType: LogType.Error);
            }
        }

        public MainType NewMainType()
        {
            MainObject = new MainType();
            return MainObject;
        }

        public override string ToString()
        {
            return MainObject.ToString() + " " + base.ToString();
        }
    }
}