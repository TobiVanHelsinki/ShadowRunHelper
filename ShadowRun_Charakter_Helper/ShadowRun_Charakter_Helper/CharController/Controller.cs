using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShadowRun_Charakter_Helper.CharController
{
    public class Controller<T> where T : new()
    {
        public ObservableCollection<T> DataList { get; set; }
        

        public void add()
        {
            DataList.Add(new T());
        }

        public void remove(T del)
        {
            DataList.Remove((T)del);
        }

        public Controller()
        {

        }

        //public void Constructor<T>()
        //{
        //    //DataList = new ObservableCollection<T>();
        //    DataList = new ObservableCollection<T>();
        //}


    }
}



