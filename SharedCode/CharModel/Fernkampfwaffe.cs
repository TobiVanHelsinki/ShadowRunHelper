
namespace ShadowRunHelper.CharModel
{
    public class Fernkampfwaffe : Waffe
    {
        private double _RK = 0;
        [Used_User]
        public double RK //RK
        {
            get => _RK;
            set
            {
                if (value != _RK)
                {
                    _RK = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private string modi = "";
        [Used_User]
        public string Modi
        {
            get => modi;
            set
            {
                if (value != modi)
                {
                    modi = value;
                    NotifyPropertyChanged();
                }
            }
        }
    }
}