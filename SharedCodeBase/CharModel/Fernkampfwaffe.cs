using System.Collections.Generic;
using TLIB_UWPFRAME;

namespace ShadowRunHelper.CharModel
{
    public class Fernkampfwaffe : Waffe
    {
        private double rueckstoss = 0;
        [Used]
        public double Rueckstoss
        {
            get { return rueckstoss; }
            set
            {
                if (value != rueckstoss)
                {
                    rueckstoss = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private string modi = "";
        [Used]
        public string Modi
        {
            get { return modi; }
            set
            {
                if (value != modi)
                {
                    modi = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public Fernkampfwaffe()
        {

            ThingType = ThingDefs.Fernkampfwaffe;
        }
    }
}
