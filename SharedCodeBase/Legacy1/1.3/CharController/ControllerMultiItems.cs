using System;
using System.Collections.ObjectModel;

namespace ShadowRunHelper1_3.CharController
{
    public class ControllerMultiItems<T> : CharController.ControllerMulti<T> where T : CharModel.Item, new()
    {
        public T Data { get; set; } //fuer Einfache-Controller

        public void ToggleActive(T obj)
        {
            //aenderung durchsetzen
            for (int i = 0; i < DataList.Count; i++)
            {
                DataList[i].Aktiv = false;
            }
            DataList[DataList.IndexOf(obj)].Aktiv = true;

            //aenderung propagieren
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
                throw new Exception("Konnte " + this.HD_Typ + " nicht an HD weiterleiten. - " + HD_ID + " nicht im HD.");
            }
            try
            {
                foreach (var item in DataList)
                {
                    if (item.Aktiv == true)
                    {
                        temptry.Bezeichner = item.Bezeichner;
                        temptry.Wert = item.Wert;
                        temptry.Typ = HD_Typ;
                        temptry.Zusatz = item.Zusatz;
                        temptry.Notiz = item.Notiz;
                    }
                }
            }
            catch (Exception)
            {
                throw new Exception("Konnte " + this.HD_Typ + " nicht an HD weiterleiten.");
            }
            HD[HD_ID] = temptry;
            System.Diagnostics.Debug.WriteLine("Data has changed, HD wurde aktualisiert" + HD_ID + " " + HD_Bezeichner);
        }


    }
}