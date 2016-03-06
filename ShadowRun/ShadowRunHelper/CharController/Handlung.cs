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
        /// <summary>
        /// Wenn das HD geändert wurde, ändert sich auch die Zusammensetzung, diese Änderung wird hier an das kleine HD in der Handlung propagiert und gleichzeitig die NeuBerechung der Werte ausgeführt
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void HDChanged(object sender, EventArgs e)
        {
            
            double temp = 0;
            try
            {
                templist = new int[this.Data.Zusammensetzung.Count];
                this.Data.Zusammensetzung.Keys.CopyTo(templist, 0);
                foreach (int i in templist)
                {
                    if (((Controller.HashDictionary)sender)[i].Typ != "Handlung")
                    {
                        // ignore if handlung
                        this.Data.Zusammensetzung[i] = ((Controller.HashDictionary)sender)[i];
                    }
                    temp += this.Data.Zusammensetzung[i].Wert;
                }
                if (this.Data.Wert != temp)
                {
                    this.Data.Wert = temp;
                }
                temp = 0;
                templist = new int[this.Data.GrenzeZusammensetzung.Count];
                this.Data.GrenzeZusammensetzung.Keys.CopyTo(templist, 0);
                foreach (int i in templist)
                {
                    if (((Controller.HashDictionary)sender)[i].Typ!="Handlung")
                    {
                        // ignore if handlung
                        this.Data.GrenzeZusammensetzung[i] = ((Controller.HashDictionary)sender)[i];
                    }
                    temp += this.Data.GrenzeZusammensetzung[i].Wert;
                }
                if (this.Data.Grenze != temp)
                {
                    this.Data.Grenze = temp;
                }
                temp = 0;
            }
            catch (System.Collections.Generic.KeyNotFoundException)
            {
                FehlerAnzeige.showError("Neu Berechnng fehlgeschlagen, es konnte kein Key gefunden werden. Der Wert wird so gut es geht dargestellt.", 0);
                this.Data.Wert = temp;
            }

            Debug.WriteLine("This is called when the event fires. - Handlung " + HD_ID + " " + Data.Bezeichner);
        }


        ~Handlung()
        {
            if (Data!=null){Data.PropertyChanged -= new System.ComponentModel.PropertyChangedEventHandler(DataChanged);}

        }

    }
}