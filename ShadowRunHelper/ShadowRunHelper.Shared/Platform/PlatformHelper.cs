//Author: Tobi van Helsinki

using ShadowRunHelper.Helper;
using System;
using System.ComponentModel;
using Windows.ApplicationModel.Core;
using Windows.UI.Core;

namespace ShadowRunHelperViewer.Platform.UWP
{
    public class PlatformHelper : IPlatformHelper
    {
        public void CallPropertyChanged(PropertyChangedEventHandler Event, object o, string property)
        {
            try
            {
                Event?.Invoke(o, new PropertyChangedEventArgs(property));
            }
            catch (Exception)
            {
                ExecuteOnUIThreadAsync(() =>
                {
                    Event?.Invoke(o, new PropertyChangedEventArgs(property));
                });
            }
        }

        public void ExecuteOnUIThreadAsync(Action p)
        {
            try
            {
                CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                    p?.Invoke();
                });
            }
            catch (Exception)
            {
            }
        }
    }
}