using System.Collections.ObjectModel;

namespace ShadowRun_Charakter_Helper.CharController
{
    public class Controller<T> where T : CharModel.Model, new()
    {
        public ObservableCollection<T> DataList { get; set; }
        public Controller.HashDictionary DicCD;
        public int Dic_ID { get; set; }

        public void add()
        {
            DataList.Add(new T());
        }

        public void remove(T obj)
        {
            DicCD.Data.Remove(obj.DicCD_ID);
            DataList.Remove((T)obj);
        }


        public void ToggleActive(T obj)
        {

        }

        public Controller()
        {
            DataList = new ObservableCollection<T>();
        }

    }
}