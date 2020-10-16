//Author: Tobi van Helsinki

using ShadowRunHelper.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using TLIB;
using TLIB.Settings;

namespace ShadowRunHelper.Model
{
    public class SharedSettingsModel : INotifyPropertyChanged
    {
        private IEnumerable<PropertyInfo> Settings => ReflectionHelper.GetProperties(this, typeof(SettingAttribute));

        #region Attributes

        protected enum SaveType
        {
            Roaming, Local, Nothing
        }

        [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
        protected sealed class SettingAttribute : Attribute
        {
            public string SaveString;
            public object DefaultValue;
            public SaveType Sync;
            public Type DeviatingType;

            public SettingAttribute(string saveString, object defaultValue, SaveType sync, Type deviatingType = null)
            {
                SaveString = saveString;
                DefaultValue = defaultValue;
                Sync = sync;
                DeviatingType = deviatingType;
            }
        }

        #endregion Attributes

        #region Generic Set / Get
        public static IPlatformSettings PlatformSettings;

        protected dynamic Get([CallerMemberName] string Name = "")
        {
            var Setting = Settings?.FirstOrDefault(x => x.Name == Name);
            var Attribute = Setting?.GetCustomAttribute<SettingAttribute>(true);
            if (PlatformSettings == null)
            {
                return Attribute.DefaultValue;
            }
            try
            {
                Func<string, object> UsedFunction;
                switch (Attribute?.Sync)
                {
                    case SaveType.Roaming:
                        UsedFunction = PlatformSettings.GetRoaming;
                        break;
                    case SaveType.Local:
                        UsedFunction = PlatformSettings.GetLocal;
                        break;
                    default:
                        throw new Exception();
                }
                switch (Attribute.DeviatingType ?? Setting.PropertyType)
                {
                    case Type namedType when namedType.IsEnum:
                        var no = int.Parse(UsedFunction(Attribute.SaveString)?.ToString());
                        if (Enum.IsDefined(namedType, no))
                        {
                            return Enum.ToObject(namedType, no);
                        }
                        else
                        {
                            return Attribute.DefaultValue;
                        }
                    case Type namedType when namedType == typeof(int):
                        return int.Parse(UsedFunction(Attribute.SaveString)?.ToString());
                    case Type namedType when namedType == typeof(bool):
                        return bool.Parse(UsedFunction(Attribute.SaveString)?.ToString());
                    case Type namedType when namedType == typeof(string):
                        return UsedFunction(Attribute.SaveString)?.ToString();
                    default:
                        return Attribute.DefaultValue;
                }
            }
            catch (Exception)
            {
                return Attribute?.DefaultValue;
            }
        }

        /// <summary>
        /// Saves an value under the key provided by their attribute
        /// supports: string, int, double, float, enum. (enum ist castet to ints)
        /// </summary>
        /// <param name="value"></param>
        /// <param name="Name"></param>
        protected void Set(object value, [CallerMemberName] string Name = "")
        {
            var Setting = Settings?.FirstOrDefault(x => x.Name == Name);
            var Attribute = Setting?.GetCustomAttribute<SettingAttribute>(true);
            if (Setting.PropertyType.IsEnum)
            {
                value = Convert.ChangeType(value, typeof(int));
            }
            dynamic oldvalue = Convert.ChangeType(Setting.GetValue(this), Setting.PropertyType);
            switch (Attribute.Sync)
            {
                case SaveType.Roaming:
                    PlatformSettings?.SetRoaming(Attribute.SaveString, value);
                    break;
                case SaveType.Local:
                    PlatformSettings?.SetLocal(Attribute.SaveString, value);
                    break;
                default:
                    break;
            }
            dynamic newvalue = Setting.PropertyType.IsEnum ? Enum.ToObject(Setting.PropertyType, value) : Convert.ChangeType(value, Setting.PropertyType);
            if (oldvalue != newvalue)
            {
                Instance.NotifyPropertyChanged(Name);
            }
        }
        #endregion Generic Set / Get

        #region Settings

        [Setting("SETTINGS_FORMAT_SAVEFILE", false, SaveType.Roaming)]
        public bool FORMAT_SAVEFILE { get => Get(); set => Set(value); }

        [Setting("SETTINGS_INTERN_SYNC", true, SaveType.Local)]
        public bool INTERN_SYNC { get => Get(); set => Set(value); }

        [Setting("SETTINGS_DEBUG_FEATURES", false, SaveType.Local)]
        public bool DEBUG_FEATURES { get => Get(); set => Set(value); }

        [Setting("SETTINGS_BETA_FEATURES", false, SaveType.Local)]
        public bool BETA_FEATURES { get => Get(); set => Set(value); }

        [Setting("SETTINGS_DISPLAY_REQUEST", true, SaveType.Local)]
        public bool DISPLAY_REQUEST { get => Get(); set => Set(value); }

        [Setting("SETTINGS_FOLDERMODE", false, SaveType.Local)]
        public bool FOLDERMODE { get => Get(); set => Set(value); }

        [Setting("SETTINGS_FOLDERMODE_PATH", "", SaveType.Local)]
        public string FOLDERMODE_PATH { get => Get(); set => Set(value); }

        [Setting("LAST_SAVE_INFO", null, SaveType.Nothing)]
        public FileInfo LAST_SAVE_INFO
        {
            get => new FileInfo(LAST_SAVE_INFO_PATH);
            set
            {
                LAST_SAVE_INFO_PATH = value?.FullName ?? "";
                Instance.NotifyPropertyChanged();
            }
        }

        [Setting("LAST_SAVE_INFO_PATH", "", SaveType.Local)]
        public string LAST_SAVE_INFO_PATH { get => Get(); set => Set(value); }

        #endregion Settings

        #region Methods

        public List<(string, object)> ExportAllSettings()
        {
            var propertyinfos = GetType().GetRuntimeProperties();
            var ret = new List<(string, object)>();
            foreach (var item in propertyinfos)
            {
                var result = item.GetValue(this);
                ret.Add((item.Name, result));
            }
            return ret;
        }

        public void InitSettings()
        {
            foreach (var SettingInfo in Settings)
            {
                try
                {
                    var Attribute = SettingInfo?.GetCustomAttribute<SettingAttribute>(true);
                    SettingInfo.SetMethod.Invoke(this, new object[] { Attribute.DefaultValue });
                }
                catch (Exception)
                {
                    if (System.Diagnostics.Debugger.IsAttached)
                    {
                        System.Diagnostics.Debugger.Break();
                    }
                }
            }
            //var settings = ReflectionHelper.GetProperties(this, typeof(LocalSettingAttribute)).ToList();
            //var stdconst = UsedConstants.GetRuntimeFields()
            //    .Concat(typeof(SharedConstants).GetRuntimeFields())
            //    .Where(fi => fi.IsLiteral && !fi.IsInitOnly && fi.IsStatic && fi.IsPublic).ToList();

            //foreach (var item in settings)
            //{
            //    var stdcontent = stdconst.FirstOrDefault(x=>x.Name == SharedConstants.SettingsPrefix + item.Name + SharedConstants.SettingsSTDPostfix);
            //    if (stdcontent != null)
            //    {
            //        var value = stdcontent.GetValue(null);
            //        item.SetValue(this, value);
            //    }
            //    else
            //    {
            //        //item.Name has no STD Value
            //        if (System.Diagnostics.Debugger.IsAttached) System.Diagnostics.Debugger.Break();
            //    }
            //}
        }
        #endregion Methods

        #region Singleton Model Thigns

        public static SharedSettingsModel Initialize()
        {
            if (instance == null)
            {
                instance = new SharedSettingsModel
                {
                    //UsedConstants = typeof(SharedConstants)
                };
            }
            return Instance;
        }
        public static SharedSettingsModel Instance => instance;

        public static SharedSettingsModel I => instance;

        protected static SharedSettingsModel instance;

        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PlatformHelper.CallPropertyChanged(PropertyChanged, this, propertyName);
        }
        #endregion Singleton Model Thigns
    }
}