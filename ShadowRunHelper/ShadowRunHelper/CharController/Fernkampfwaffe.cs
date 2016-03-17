using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace ShadowRunHelper.CharController
{
    public class Fernkampfwaffe : CharController.ControllerMultiItems<CharModel.Fernkampfwaffe>
    {
        public int HD_ID_P = 0;
        static private string HD_Bezeichner_P = "Präzision";

        public Fernkampfwaffe()
        {
        }

        public Fernkampfwaffe(Controller.HashDictionary hD, int hD_ID)
        {
            this.HD_ID = hD_ID;
            this.setHD(hD);
            DataList.CollectionChanged += new NotifyCollectionChangedEventHandler(DataListChanged);
        }


        new public void setHD(Controller.HashDictionary hD)
        {
            this.HD = hD;
            if (this.HD_ID == 0)
            {
                this.HD_ID = HD.getFreeKey();
            }
            else if (HD.Data.ContainsKey(HD_ID))
            {
                Error_Occured();
            }
            HD.Add(this.HD_ID, new Model.DictionaryCharEntry(HD_Bezeichner, HD_Typ, HD_Wert, HD_Zusatz, HD_Notiz));

            this.HD = hD;
            if (this.HD_ID_P == 0)
            {
                this.HD_ID_P = HD.getFreeKey();
            }
            else if (HD.Data.ContainsKey(HD_ID))
            {
                Error_Occured();
            }
            HD.Add(this.HD_ID_P, new Model.DictionaryCharEntry(HD_Bezeichner_P, HD_Typ, HD_Wert, HD_Zusatz, HD_Notiz));
        }

        protected new void Error_Occured()
        {
            //das noch beim Deck machen
            int o_nOldID = HD_ID;
            int o_nOldID_P = HD_ID_P;
            int o_nNewID = HD.getFreeMaxKey();
            int o_nNewID_P = HD.getFreeMaxKey();
            this.HD_ID = o_nNewID;
            this.HD_ID_P = o_nNewID_P;

            //todo altes speichern und benachrichtigen
            //beim speichern/laden brauchen auch multi-c eine ID
            //beim init bekommen sie zwar eine neue zugewiesen, aber die wird dann überschrieben
            // so hat schonmal jeder seine ID

            //mit einem spezialergebnis beide werte nach oben geben nach oben geben

            // dort werden die paare in einer list gesammelt

            HD.AlteHDEntrys.Add(o_nOldID, o_nNewID);
            HD.AlteHDEntrys.Add(o_nOldID_P, o_nNewID_P);

            NotifyForErrors();
        }

        private void DataListChanged(object sender, EventArgs e)
        {
            foreach (var item in DataList)
            {
                item.PropertyChanged -= new System.ComponentModel.PropertyChangedEventHandler(DataChanged);
                item.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(DataChanged);
            }
        }

        private void DataChanged(object sender, EventArgs e)
        {
            DataHasUpdatet(sender);
        }
        ~Fernkampfwaffe()
        {
            if (Data!=null){Data.PropertyChanged -= new System.ComponentModel.PropertyChangedEventHandler(DataChanged);}
        }

        new protected void DataHasUpdatet(object sender)
        {
            Model.DictionaryCharEntry temptry = new Model.DictionaryCharEntry("", 0);
            Model.DictionaryCharEntry temptry_P = new Model.DictionaryCharEntry("", 0);
            try
            {
                temptry = HD[HD_ID];
                temptry_P = HD[HD_ID_P];
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
                        temptry_P.Bezeichner = HD_Bezeichner_P;
                        temptry_P.Wert = item.PB;
                        temptry_P.Typ = HD_Typ;
                        temptry_P.Zusatz = item.Zusatz;
                        temptry_P.Notiz = item.Notiz;

                    }
                }
            }
            catch (Exception)
            {
                throw new Exception("Konnte " + this.HD_Typ + " nicht an HD weiterleiten.");
            }
            HD[HD_ID] = temptry;
            HD[HD_ID_P] = temptry_P;
            System.Diagnostics.Debug.WriteLine("Data has changed, HD wurde aktualisiert" + HD_ID + " " + HD_Bezeichner);
        }
    }
}