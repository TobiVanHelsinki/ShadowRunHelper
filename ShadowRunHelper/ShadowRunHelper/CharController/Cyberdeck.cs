using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace ShadowRunHelper.CharController
{
    public class CyberDeck : CharController.ControllerMultiItems<CharModel.CyberDeck>
    {
        public int HD_ID_A;
        public int HD_ID_S;
        public int HD_ID_F;
        public int HD_ID_D;
        static private string HD_Bezeichner_A = "Angriff";
        static private string HD_Bezeichner_S = "Schleicher";
        static private string HD_Bezeichner_F = "Firewall";
        static private string HD_Bezeichner_D = "Datenverarbeitung";

        public CyberDeck()
        {
        }

        public CyberDeck(Controller.HashDictionary hD, int hD_ID)
        {
            this.HD_ID = hD_ID;
            this.setHD(hD);
            DataList.CollectionChanged += new NotifyCollectionChangedEventHandler(DataListChanged);
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
            if (this.HD_ID_A == 0)
            {
                this.HD_ID_A = HD.getFreeKey();
            }
            else if (HD.Data.ContainsKey(HD_ID))
            {
                Error_Occured();
            }
            HD.Add(this.HD_ID_A, new Model.DictionaryCharEntry(HD_Bezeichner_A, HD_Typ, HD_Wert, HD_Zusatz, HD_Notiz));
            if (this.HD_ID_S == 0)
            {
                this.HD_ID_S = HD.getFreeKey();
            }
            else if (HD.Data.ContainsKey(HD_ID))
            {
                Error_Occured();
            }
            HD.Add(this.HD_ID_S, new Model.DictionaryCharEntry(HD_Bezeichner_S, HD_Typ, HD_Wert, HD_Zusatz, HD_Notiz));
            if (this.HD_ID_F == 0)
            {
                this.HD_ID_F = HD.getFreeKey();
            }
            else if (HD.Data.ContainsKey(HD_ID))
            {
                Error_Occured();
            }
            HD.Add(this.HD_ID_F, new Model.DictionaryCharEntry(HD_Bezeichner_F, HD_Typ, HD_Wert, HD_Zusatz, HD_Notiz));

            if (this.HD_ID_D == 0)
            {
                this.HD_ID_D = HD.getFreeKey();
            }
            else if (HD.Data.ContainsKey(HD_ID))
            {
                Error_Occured();
            }
            HD.Add(this.HD_ID_D, new Model.DictionaryCharEntry(HD_Bezeichner_D, HD_Typ, HD_Wert, HD_Zusatz, HD_Notiz));
        }

        new protected void  DataHasUpdatet(object sender)
        {
            Model.DictionaryCharEntry temptry = new Model.DictionaryCharEntry("", 0);
            Model.DictionaryCharEntry temptry_A = new Model.DictionaryCharEntry("", 0);
            Model.DictionaryCharEntry temptry_S = new Model.DictionaryCharEntry("", 0);
            Model.DictionaryCharEntry temptry_F = new Model.DictionaryCharEntry("", 0);
            Model.DictionaryCharEntry temptry_D = new Model.DictionaryCharEntry("", 0);
            try
            {
                temptry = HD[HD_ID];
                temptry_A = HD[HD_ID_A];
                temptry_S = HD[HD_ID_S];
                temptry_F = HD[HD_ID_F];
                temptry_D = HD[HD_ID_D];
            }
            catch (Exception)
            {
                throw new Exception("Konnte " + this.HD_Typ + " nicht an HD weiterleiten. - " + HD_ID + " nicht im HD.");
            }
            try
            {
                foreach (var item in DataList)
                {
                    if (item.Aktiv==true)
                    {
                        temptry.Bezeichner = item.Bezeichner;
                        temptry.Wert = item.Wert;
                        temptry.Typ = HD_Typ;
                        temptry.Zusatz = item.Zusatz;
                        temptry.Notiz = item.Notiz;
                        temptry_A.Bezeichner = HD_Bezeichner_A;
                        temptry_A.Wert = item.Angriff;
                        temptry_A.Typ = HD_Typ;
                        temptry_A.Zusatz = item.Zusatz;
                        temptry_A.Notiz = item.Notiz;
                        temptry_S.Bezeichner = HD_Bezeichner_S;
                        temptry_S.Wert = item.Schleicher;
                        temptry_S.Typ = HD_Typ;
                        temptry_S.Zusatz = item.Zusatz;
                        temptry_S.Notiz = item.Notiz;
                        temptry_F.Bezeichner = HD_Bezeichner_F;
                        temptry_F.Wert = item.Firewall;
                        temptry_F.Typ = HD_Typ;
                        temptry_F.Zusatz = item.Zusatz;
                        temptry_F.Notiz = item.Notiz;
                        temptry_D.Bezeichner = HD_Bezeichner_D;
                        temptry_D.Wert = item.Datenverarbeitung;
                        temptry_D.Typ = HD_Typ;
                        temptry_D.Zusatz = item.Zusatz;
                        temptry_D.Notiz = item.Notiz;
                    }
                }
            }
            catch (Exception)
            {
                throw new Exception("Konnte " + this.HD_Typ + " nicht an HD weiterleiten.");
            }
            HD[HD_ID] = temptry;
            HD[HD_ID_A] = temptry_A;
            HD[HD_ID_S] = temptry_S;
            HD[HD_ID_F] = temptry_F;
            HD[HD_ID_D] = temptry_D;
            System.Diagnostics.Debug.WriteLine("Data has changed, HD wurde aktualisiert" + HD_ID + " " + HD_Bezeichner);
        }

        ~CyberDeck()
        {
            if (Data!=null){Data.PropertyChanged -= new System.ComponentModel.PropertyChangedEventHandler(DataChanged);}
        }
    }
}
