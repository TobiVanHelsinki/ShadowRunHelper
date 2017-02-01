using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
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
                if (value != this.loyal)
                {
                    this.loyal = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public Connection()
        {
            this.ThingType = ThingDefs.Connection;
        }

        public Connection Copy(Connection target = null)
        {
            if (target == null)
            {
                target = new Connection();
            }
            base.Copy(target);
            target.Loyal = Loyal;
            //target.Alias = Alias;
            return target;
        }

        public override void Reset()
        {
            //Alias = "";
            Loyal = 0;
            base.Reset();
        }

        public override string ToCSV(string Delimiter)
        {
            string strReturn = base.ToCSV(Delimiter);
            strReturn += Loyal;
            strReturn += Delimiter;
            //strReturn += Einfluss;
            //strReturn += Delimiter;
            return strReturn;
        }
        public override string HeaderToCSV(string Delimiter)
        {
            var res = ResourceLoader.GetForCurrentView();
            string strReturn = base.HeaderToCSV(Delimiter);
            strReturn += res.GetString("Model_Connection_Loyal/Text");
            strReturn += Delimiter;
            //strReturn += res.GetString("Model_Connection_Einfluss/Text");
            //strReturn += Delimiter;
            return strReturn;
        }


    }
}
