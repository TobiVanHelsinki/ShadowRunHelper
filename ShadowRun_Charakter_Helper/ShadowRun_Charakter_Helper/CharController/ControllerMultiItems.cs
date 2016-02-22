using System;
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
            HD[HD_ID].Wert = newT2.Wert;
        }

        protected void DataHasUpdatet(object sender)
        {
            Model.DictionaryCharEntry temptry = new Model.DictionaryCharEntry("", 0);
            try
            {
                temptry = HD[HD_ID];
            }
            catch (Exception)
            {
                throw new Exception("Konnte " + this.DicCD_Typ + " nicht an HD weiterleiten. - " + HD_ID + " nicht im HD.");
                // todo typ sollte automatisch gesetzt werden 
            }
            try
            {
                // for each loop over datalist, suche nach active dann daten prop
                T temp = ((T)sender);

                foreach (var item in DataList)
                {
                    if (item.Aktiv == true)
                    {
                        temptry.Bezeichner = temp.Bezeichner;
                        temptry.Wert = temp.Wert;
                      //  temptry.Typ = temp.Typ;
                        temptry.Zusatz = temp.Zusatz;
                        temptry.Notiz = temp.Notiz;
                    }
                }

            }
            catch (Exception)
            {
                throw new Exception("Konnte " + this.DicCD_Typ + " nicht an HD weiterleiten.");
            }
            HD[HD_ID] = temptry;
            System.Diagnostics.Debug.WriteLine("Data has changed, HD wurde aktualisiert" + HD_ID + " " + DicCD_Bezeichner);
        }


    }
}