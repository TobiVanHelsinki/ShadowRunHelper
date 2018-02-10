namespace ShadowRunHelper.CharModel
{
    public class Connection : Thing
    {

        private double loyal = 0;
        [Used_UserAttribute]
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
        [Used_UserAttribute]
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

    
    }
}
