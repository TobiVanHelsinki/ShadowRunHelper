//Author: Tobi van Helsinki

using System;
using System.ComponentModel;
using TLIB;

namespace ShadowRunHelper.Helper
{
    public interface IPlatformHelper
    {
        void CallPropertyChanged(PropertyChangedEventHandler Event, object o, string property);

        void ExecuteOnUIThreadAsync(Action p);
    }

    public static class PlatformHelper
    {
        public static IPlatformHelper Platform { get; set; }
        static IPlatformHelper StandardPlatform = new StandardPlatformHelper();

        public static void CallPropertyChanged(PropertyChangedEventHandler Event, object o, string property)
        {
            if (Platform != null)
            {
                Platform.CallPropertyChanged(Event, o, property);
            }
            else
            {
                StandardPlatform.CallPropertyChanged(Event, o, property);
            }
        }

        public static void ExecuteOnUIThreadAsync(Action p)
        {
            if (Platform != null)
            {
                Platform.ExecuteOnUIThreadAsync(p);
            }
            else
            {
                StandardPlatform.ExecuteOnUIThreadAsync(p);
            }
        }
    }

    internal class StandardPlatformHelper : IPlatformHelper
    {
        public void CallPropertyChanged(PropertyChangedEventHandler Event, object o, string property)
        {
            try
            {
                Event?.Invoke(o, new PropertyChangedEventArgs(property));
            }
            catch (Exception ex)
            {
                Log.Write("Could not CallPropertyChanged", ex, logType: LogType.Error);
            }
        }

        public void ExecuteOnUIThreadAsync(Action p)
        {
        }
    }
}