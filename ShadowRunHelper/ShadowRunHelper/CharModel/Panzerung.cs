using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Resources;

namespace ShadowRunHelper.CharModel
{
    public class Panzerung : Item
    {
        private double stoß = 0;
        public double Stoß
        {
            get { return stoß; }
            set
            {
                if (value != this.stoß)
                {
                    this.stoß = value;
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

        public Panzerung()
        {
            this.ThingType = ThingDefs.Panzerung;

        }

        public override string ToCSV(string Delimiter)
        {
            string strReturn = base.ToCSV(Delimiter);
            strReturn += Stoß;
            strReturn += Delimiter;
            strReturn += Kapazität;
            strReturn += Delimiter;
            return strReturn;
        }

        public override string HeaderToCSV(string Delimiter)
        {
            var res = ResourceLoader.GetForCurrentView();
            string strReturn = base.HeaderToCSV(Delimiter);
            strReturn += res.GetString("Model_Panzerung_Stoß/Text");
            strReturn += Delimiter;
            strReturn += res.GetString("Model_Panzerung_Kapazität/Text");
            strReturn += Delimiter;
            return strReturn;
        }
        public override void FromCSV(Dictionary<string, string> dic)
        {
            var res = ResourceLoader.GetForCurrentView();
            base.FromCSV(dic);
            foreach (var item in dic)
            {
                if (item.Key == res.GetString("Model_Panzerung_Stoß/Text"))
                {
                    Stoß = double.Parse(item.Value);
                    continue;
                }
                if (item.Key == res.GetString("Model_Panzerung_Kapazität/Text"))
                {
                    Kapazität = double.Parse(item.Value);
                    continue;
                }
            }
        }
    }
}
