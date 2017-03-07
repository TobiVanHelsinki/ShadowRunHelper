using System.Collections.Generic;
using Windows.ApplicationModel.Resources;

namespace ShadowRunHelper.CharModel
{
    public class Connection : Thing
    {

        private double loyal = 0;
        public double Loyal
        {
            get { return loyal; }
            set
            {
                if (value != loyal)
                {
                    loyal = value;
                    NotifyPropertyChanged();
                }
            }
        }


        private double _einfluss = 0;
        public double Einfluss
        {
            get { return _einfluss; }
            set
            {
                if (value != _einfluss)
                {
                    _einfluss = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public Connection()
        {
            ThingType = ThingDefs.Connection;
        }

        public Thing Copy(Thing target = null)
        {
            if (target == null)
            {
                target = new Connection();
            }
            base.Copy(ref target);
            ((Connection)target).Loyal = Loyal;
            ((Connection)target).Einfluss = Einfluss;
            return target;
        }

        public override void Reset()
        {
            Loyal = 0;
            Einfluss = 0;
            base.Reset();
        }

        public override string ToCSV(string Delimiter)
        {
            string strReturn = base.ToCSV(Delimiter);
            strReturn += Loyal;
            strReturn += Delimiter;
            strReturn += Einfluss;
            strReturn += Delimiter;
            return strReturn;
        }
        public override string HeaderToCSV(string Delimiter)
        {
            var res = ResourceLoader.GetForCurrentView();
            string strReturn = base.HeaderToCSV(Delimiter);
            strReturn += res.GetString("Model_Connection_Loyal/Text");
            strReturn += Delimiter;
            strReturn += res.GetString("Model_Connection_Einfluss/Text");
            strReturn += Delimiter;
            return strReturn;
        }

        public override void FromCSV(Dictionary<string, string> dic)
        {
            var res = ResourceLoader.GetForCurrentView();
            base.FromCSV(dic);
            foreach (var item in dic)
            {
                if (item.Key == res.GetString("Model_Connection_Loyal/Text"))
                {
                    Loyal = double.Parse(item.Value);
                    continue;
                }
                if (item.Key == res.GetString("Model_Connection_Einfluss/Text"))
                {
                    Einfluss = double.Parse(item.Value);
                    continue;
                }
            }
        }

    }
}
