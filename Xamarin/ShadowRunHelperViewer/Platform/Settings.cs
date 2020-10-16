//Author: Tobi van Helsinki

using ShadowRunHelper.Model;
using TLIB.Settings;
using Xamarin.Essentials;

namespace ShadowRunHelperViewer.Platform.Xamarin
{
    public class Settings : IPlatformSettings
    {
        public void SetLocal(string place, object value)
        {
            Preferences.Set(place, value?.ToString());
        }

        public void SetRoaming(string place, object value)
        {
            SetLocal(place, value);
        }

        public object GetLocal(string place)
        {
            if (Preferences.ContainsKey(place))
            {
                return Preferences.Get(place, null);
            }
            else
            {
                throw new SettingNotPresentException();
            }
        }

        public object GetRoaming(string place)
        {
            return GetLocal(place);
        }

        public void RemoveAllSettings()
        {
            Preferences.Clear();
        }

        public void PrepareSettingsSavePlace()
        {
        }
    }
}