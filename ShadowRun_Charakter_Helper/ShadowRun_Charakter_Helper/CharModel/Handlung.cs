using System;
using System.Collections.Generic;

namespace ShadowRun_Charakter_Helper.CharModel
{
    public class Handlung : CharModel.Model
    {

        private List<int> zusammensetzung;
        public List<int> Zusammensetzung
        {
            get { return zusammensetzung; }
            set
            {
                if (value != this.zusammensetzung)
                {
                    this.zusammensetzung = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private int wert_Mod;
        public int Wert_Mod
        {
            get { return wert_Mod; }
            set
            {
                if (value != this.wert_Mod)
                {
                    this.wert_Mod = value;
                    NotifyPropertyChanged();
                }
            }
        }
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
            Zusammensetzung = new List<int>();
            Wert_Mod = 0;
            Grenze = "";
        }
    }
}
