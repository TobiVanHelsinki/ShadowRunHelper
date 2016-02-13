using System.Collections.ObjectModel;

namespace ShadowRun_Charakter_Helper.CharController
{
    public class ControllerMultiItems<T> : CharController.ControllerMulti<T> where T : CharModel.Item, new()
    {
        public T Data { get; set; } //für Einfache-Controller

        public void ToggleActive(T obj)
        {
            //Änderung durchsetzen
            for (int i = 0; i < DataList.Count; i++)
            {
                DataList[i].Aktiv = false;
            }
            DataList[DataList.IndexOf(obj)].Aktiv = true;

            //Änderung propagieren
            T newT2 = (T)(object)obj;
            HD.Data[HD_ID].Wert = newT2.Wert;
        }




    }
}