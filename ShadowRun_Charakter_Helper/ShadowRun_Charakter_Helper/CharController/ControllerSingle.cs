using System;
using System.Diagnostics;

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

        protected void DataHasUpdatet(object sender)
        {
            Model.DictionaryCharEntry temptry = new Model.DictionaryCharEntry("", 0);
            try
            {
                temptry = HD[HD_ID];
            }
            catch (Exception)
            {
                throw new Exception("Konnte " + this.Data.Typ + " nicht an HD weiterleiten. - " + HD_ID + " nicht im HD.");
                // todo typ sollte automatisch gesetzt werden 
            }
            try
            {
                T temp = ((T)sender);

                temptry.Bezeichner = temp.Bezeichner;
                temptry.Wert = temp.Wert;
                temptry.Typ = HD_Typ;
                temptry.Zusatz = temp.Zusatz;
                temptry.Notiz = temp.Notiz;
            }
            catch (Exception)
            {
                throw new Exception("Konnte " + this.Data.Typ + " nicht an HD weiterleiten.");
            }
            HD[HD_ID] = temptry;
            Debug.WriteLine("Data has changed, HD wurde aktualisiert" + HD_ID + " " + Data.Bezeichner);
        }
    }
}