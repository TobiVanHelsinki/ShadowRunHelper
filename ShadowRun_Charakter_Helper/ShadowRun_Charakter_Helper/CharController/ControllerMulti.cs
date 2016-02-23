using System;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace ShadowRun_Charakter_Helper.CharController
{
    public class ControllerMulti<T> : CharController.Controller<T> where T : CharModel.Model, new()
    {
        public ObservableCollection<T> DataList { get; set; } //für Mulit-Controller

        public ControllerMulti()
        {
            DataList = new ObservableCollection<T>();
        }

        public ControllerMulti(ObservableCollection<T> obj)
        {
            DataList = obj;
            //todo wird datacahnged auch aufgerufen, wenn dieser construktior genutzt wird? das wäre für das laden einfacher
        }

        public void add()
        {
            DataList.Add(new T());
        }

        public void add(T obj)
        {
            DataList.Add(obj);
        }

        public void remove(T obj)
        {
            DataList.Remove(obj);
        }

    }
}