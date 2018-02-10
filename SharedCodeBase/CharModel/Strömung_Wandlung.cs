using System.Collections.Generic;
using TLIB_UWPFRAME;

namespace ShadowRunHelper.CharModel
{
    public class Stroemung_Wandlung : Thing
    {
        // Bezeichner ist Stroemung
        // Wert ist Wandlungsgrad
       
        string _Paragon = "";
        [Used]
        public string Paragon
        {
            get { return _Paragon; }
            set
            {
                if (value != this._Paragon)
                {
                    this._Paragon = value;
                    NotifyPropertyChanged();
                }
            }
        }
       
        string _Echos = "";
        [Used]
        public string Echos
        {
            get { return _Echos; }
            set
            {
                if (value != this._Echos)
                {
                    this._Echos = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public Stroemung_Wandlung()
        {
            this.ThingType = ThingDefs.Stroemung_Wandlung;
        }


        public override string ToCSV(string Delimiter)
        {
            string strReturn = base.ToCSV(Delimiter);
            strReturn += Paragon;
            strReturn += Delimiter;
            strReturn += Echos;
            strReturn += Delimiter;
            return strReturn;
        }

        public override string HeaderToCSV(string Delimiter)
        {
            string strReturn = base.HeaderToCSV(Delimiter);
            strReturn += CrossPlatformHelper.GetString("Model_Stroemung_Wandlung_Paragon/Text");
            strReturn += Delimiter;
            strReturn += CrossPlatformHelper.GetString("Model_Stroemung_Wandlung_Echos/Text");
            strReturn += Delimiter;
            return strReturn;
        }

        public override void FromCSV(Dictionary<string, string> dic)
        {
            base.FromCSV(dic);
            foreach (var item in dic)
            {
                if (item.Key == CrossPlatformHelper.GetString("Model_Stroemung_Wandlung_Paragon/Text"))
                {
                    this.Paragon = (item.Value);
                    continue;
                }
                if (item.Key == CrossPlatformHelper.GetString("Model_Stroemung_Wandlung_Echos/Text"))
                {
                    this.Echos = (item.Value);
                    continue;
                }
            }
        }
    }
}
