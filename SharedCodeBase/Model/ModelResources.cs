using System;
using System.ComponentModel;
using Windows.ApplicationModel.Core;
using Windows.UI.Core;

namespace SharedCodeBase.Model
{
    class ModelResources
    {
        public static async void CallPropertyChangedAtDispatcher(PropertyChangedEventHandler Event, object o, string property)
        {
            await Windows.UI.Xaml.Window.Current.Dispatcher.RunAsync(CoreDispatcherPriority.High,
            () =>
            {
                Event?.Invoke(o, new PropertyChangedEventArgs(property));
            });

            //await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            //{
            //  Event?.Invoke(o, new PropertyChangedEventArgs(property));
            //});

        }
        
        public static async void AtGui(Action x, CoreDispatcherPriority Priority = CoreDispatcherPriority.Low)
        {
            //await Windows.UI.Xaml.Window.Current.Dispatcher.RunAsync(
            await ShadowRunHelper.Model.AppModel.Instance.Dispatcher.RunAsync(
                Priority, () => {
                x();
            });
        }
    }
}
