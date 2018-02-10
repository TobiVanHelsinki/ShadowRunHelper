using System.Collections.Generic;
using TLIB_UWPFRAME;

namespace ShadowRunHelper.CharModel
{
    public class Connection : Thing
    {

        private double loyal = 0;
        [Used]
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
        [Used]
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
    }
}
