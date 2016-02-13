using System.Collections.ObjectModel;

namespace ShadowRun_Charakter_Helper.CharController
{
    public class Panzerung : CharController.Controller<CharModel.Panzerung> 
    {

        public Panzerung(Controller.HashDictionary dicCD)
        {
            DicCD = dicCD;
            DataList = new ObservableCollection<CharModel.Panzerung>();
            Dic_ID = DicCD.getFreeKey();

            DicCD.Data.Add(Dic_ID, new Models.DictionaryCharEntry("Panzerungscontroller", "Panzerung", 0, "", ""));
        }

        public void ToggleActive(CharModel.Panzerung obj)
        {
            //Änderung durchsetzen
            for (int i = 0; i < DataList.Count; i++)
            {
                DataList[i].Aktiv = false;
            }
            DataList[DataList.IndexOf(obj)].Aktiv = true;
            
            //Änderung propagieren
            CharModel.Panzerung newT2 = (CharModel.Panzerung)(object)obj;
            DicCD.Data[Dic_ID].Wert = newT2.Wert;


        }

    }
}
