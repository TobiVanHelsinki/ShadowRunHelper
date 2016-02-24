using ShadowRun_Charakter_Helper.UI.Fehler;
using System;
using System.Diagnostics;
namespace ShadowRun_Charakter_Helper.CharController
{
    public class Handlung : CharController.ControllerSingle<CharModel.Handlung>
    {
        int[] templist;
        public Handlung()
        {
        }

        public Handlung(Controller.HashDictionary hD, int hD_ID)
        {
            this.HD_ID = hD_ID;
            this.setHD(hD);
            HD.Changed += new ShadowRun_Charakter_Helper.Controller.ChangedEventHandler(HDChanged);
            Data.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(DataChanged);
        }

        private void DataChanged(object sender, EventArgs e)
        {
            DataHasUpdatet(sender);
        }

        protected void HDChanged(object sender, EventArgs e)
        {
            // Zusammensetzung aktualisieren
            // neu berechnen
            // in sender komplettes HD (HD.Data)
            // todo ignore if handlung
            double temp = 0;
            try
            {
                templist = new int[this.Data.Zusammensetzung.Count];
                this.Data.Zusammensetzung.Keys.CopyTo(templist, 0);
                foreach (int i in templist)
                {
                    this.Data.Zusammensetzung[i] = ((Controller.HashDictionary)sender)[i];
                    // ich glaube, hier muss es i-1 sein, scheint aber zu gehen
                    temp += this.Data.Zusammensetzung[i].Wert;
                }
                if (this.Data.Wert != temp)
                {
                    this.Data.Wert = temp;
                }
            }
            catch (System.Collections.Generic.KeyNotFoundException)
            {
                FehlerAnzeige.showError("Neu Berechnng fehlgeschlagen, es konnte kein Key gefunden werden. Der Wert wird so gut es geht dargestellt.", 0);
                this.Data.Wert = temp;
            }

            Debug.WriteLine("This is called when the event fires. - Handlung " + HD_ID + " " + Data.Bezeichner);
        }
    }
}