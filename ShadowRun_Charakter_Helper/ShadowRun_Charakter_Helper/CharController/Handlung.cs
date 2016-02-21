﻿using System;
using System.Diagnostics;

namespace ShadowRun_Charakter_Helper.CharController
{
    

    public class Handlung : CharController.ControllerSingle<CharModel.Handlung>
    {
        
        int[] templist;
        public Handlung()
        {
            DicCD_Typ = "Handlung";
        }

        public Handlung(Controller.HashDictionary hD)
        {
            DicCD_Typ = "Handlung";
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
            //in sender komplettes HD (HD.Data)
            // todo ignore if handlung
                       double temp = 0;
            try
            {
                templist = new int[this.Data.Zusammensetzung.Count];
                this.Data.Zusammensetzung.Keys.CopyTo(templist, 0);
                foreach (int i in templist)
                {
                    this.Data.Zusammensetzung[i] = ((Controller.HashDictionary)sender)[i];
                    temp += this.Data.Zusammensetzung[i].Wert;

                    // += this.Data.Zusammensetzung[i].Wert;
                }
                if (this.Data.Wert != temp)
                {
                    this.Data.Wert = temp;
                }
            }
            catch (Exception)
            {

                throw new Exception("Neu Berechnng fehlgeschlagen");
            }
            

            Debug.WriteLine("This is called when the event fires. - Handlung "+ HD_ID + " " +Data.Bezeichner);
        }
    }
}