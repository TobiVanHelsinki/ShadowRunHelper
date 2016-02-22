using System;
using System.Collections.Generic;

namespace ShadowRun_Charakter_Helper.CharModel
{
    public class Handlung : CharModel.Model
    {

        private Dictionary<int, ShadowRun_Charakter_Helper.Model.DictionaryCharEntry> zusammensetzung;
        public Dictionary<int, ShadowRun_Charakter_Helper.Model.DictionaryCharEntry> Zusammensetzung
        {
            get { return zusammensetzung;
                // todo return zusammensetzung ohne handlungen
            }
            set
            {
                if (value != this.zusammensetzung)
                {
                        this.zusammensetzung = value;
                        NotifyPropertyChanged();
                }
            }
        }

        private Dictionary<int, ShadowRun_Charakter_Helper.Model.DictionaryCharEntry> grenzeZusammensetzung;
        public Dictionary<int, ShadowRun_Charakter_Helper.Model.DictionaryCharEntry> GrenzeZusammensetzung
        {
            get
            {
                return grenzeZusammensetzung;
                // todo return zusammensetzung ohne handlungen
            }
            set
            {
                if (value != this.grenzeZusammensetzung)
                {
                    this.grenzeZusammensetzung = value;
                    NotifyPropertyChanged();
                }
            }
        }

        //Todo darf in der zus liste nicht eigene HD Id beinhalten, sonst vermutlich crash
        private String grenze;
        public String Grenze
        {
            get { return grenze; }
            set
            {
                if (value != this.grenze)
                {
                    this.grenze = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public Handlung(int dicCD_ID)
        {

        }

        public Handlung()
        {
            //todo alles auf null setzen schon im Kontruktor
            Zusammensetzung = new Dictionary<int, ShadowRun_Charakter_Helper.Model.DictionaryCharEntry>();
            GrenzeZusammensetzung = new Dictionary<int, ShadowRun_Charakter_Helper.Model.DictionaryCharEntry>();
            Grenze = "";
        }
    }
}
