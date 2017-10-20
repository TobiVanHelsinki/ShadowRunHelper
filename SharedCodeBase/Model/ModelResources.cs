using System;
using System.ComponentModel;
using Windows.ApplicationModel.Core;
using Windows.UI.Core;
using Windows.UI.Xaml;

namespace SharedCodeBase.Model
{
    class ModelResources
    {
        public static async void CallPropertyChangedAtDispatcher(PropertyChangedEventHandler Event, object o, string property)
        {
            //Window.Current.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () => {
            //    Event?.Invoke(o, new PropertyChangedEventArgs(property));
            //});
        //  await Window.Current.Dispatcher.RunAsync(CoreDispatcherPriority.High,
          //() =>
          //{
          //    Event?.Invoke(o, new PropertyChangedEventArgs(property));
          //});

            await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
              Event?.Invoke(o, new PropertyChangedEventArgs(property));
            });

            //await AppModel.Instance.Dispatcher.RunAsync(CoreDispatcherPriority.Low,
            //() =>
            //{
            //    Event?.Invoke(o, new PropertyChangedEventArgs(property));
            //});
        }
    }
}
