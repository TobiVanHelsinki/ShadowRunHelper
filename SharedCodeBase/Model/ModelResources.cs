using System.ComponentModel;

namespace SharedCodeBase.Model
{
    class ModelResources
    {
        public static void CallPropertyChanged(PropertyChangedEventHandler Event, object o, string property)
        {
            //await AppModel.Instance.Dispatcher.RunAsync(CoreDispatcherPriority.Low,
            //() =>
            //{
            Event?.Invoke(o, new PropertyChangedEventArgs(property));
            //});
        }
    }
}
