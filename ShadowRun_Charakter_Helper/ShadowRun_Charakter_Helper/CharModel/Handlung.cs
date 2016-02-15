using System;
using System.Collections.Generic;

namespace ShadowRun_Charakter_Helper.CharModel
{
    public class Handlung : CharModel.Model
    {

        public List<int> zusammensetzung;
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
        public int wert_Mod;
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
        public String grenze;
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

        }
    }
}
