using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Resources;

namespace ShadowRunHelper.CharModel
{
    public class Implantat : Item
    {
        private double essenz = 0;
        public double Essenz
        {
            get { return essenz; }
            set
            {
                if (value != this.essenz)
                {
                    this.essenz = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private double kapazität = 0;
        public double Kapazität
        {
            get { return kapazität; }
            set
            {
                if (value != this.kapazität)
                {
                    this.kapazität = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public Implantat()
        {
            this.ThingType = ThingDefs.Implantat;
        }

        public Implantat Copy(ref Implantat target)
        {
            if (target == null)
            {
                target = new Implantat();
            }
            base.Copy((Thing)target);
            target.Essenz = Essenz;
            target.Kapazität = Kapazität;
            return target;
        }

        public override void Reset()
        {
            base.Reset();
            Kapazität = 0;
            Essenz = 0;
        }

        public override string ToCSV(string Delimiter)
        {
            string strReturn = base.ToCSV(Delimiter);
            strReturn += Essenz;
            strReturn += Delimiter;
            strReturn += Kapazität;
            strReturn += Delimiter;
            return strReturn;
        }

        public override string HeaderToCSV(string Delimiter)
        {
            var res = ResourceLoader.GetForCurrentView();
            string strReturn = base.HeaderToCSV(Delimiter);
            strReturn += res.GetString("Model_Implantat_Essenz/Text");
            strReturn += Delimiter;
            strReturn += res.GetString("Model_Implantat_Kapazität/Text");
            strReturn += Delimiter;
            return strReturn;
        }

    }
}
