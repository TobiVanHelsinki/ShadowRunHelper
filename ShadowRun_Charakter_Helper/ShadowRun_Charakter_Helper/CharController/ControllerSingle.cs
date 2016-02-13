using System.Collections.ObjectModel;

namespace ShadowRun_Charakter_Helper.CharController
{
    public class ControllerSingle<T> : CharController.Controller<T> where T : CharModel.Model, new()
    {
        public T Data { get; set; } //für Einfache-Controller

        public ControllerSingle()
        {
            Data = new T();
        }

        public ControllerSingle(T obj)
        {
            Data = obj;
        }

        ~ControllerSingle()
        {
            remove_from_HD();
        }
    }
}