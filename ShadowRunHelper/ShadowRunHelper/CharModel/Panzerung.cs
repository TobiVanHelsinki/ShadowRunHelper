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
        /// <summary>
        /// für SR4 - ballisitisch wäre dann thing.wert
        /// </summary>
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
        public override double GetValue(string ID = "")
        {
            return GetValueList(ID).Find((x) => x.Key == ID).Value;
        }
        public override List<KeyValuePair<string, double>> GetValueList([CallerMemberName] string ID = "")
        {
            var res = ResourceLoader.GetForCurrentView();

            List<KeyValuePair<string, double>> lst = new List<KeyValuePair<string, double>>();
            lst.Add(new KeyValuePair<string, double>(res.GetString("Model_Thing_Wert/Text"), Wert));
            lst.Add(new KeyValuePair<string, double>(res.GetString("Model_Panzerung_Stoß/Text"), Stoß));
            lst.Add(new KeyValuePair<string, double>(res.GetString("Model_Panzerung_Kapazität/Text"), Kapazität));
            return lst;
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
