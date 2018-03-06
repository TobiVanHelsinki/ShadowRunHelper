
namespace ShadowRunHelper.CharModel
{
    public class Munition : Item
    { 
        private string schadenTyp = "";
        [Used_UserAttribute]
        public string SchadenTyp
        {
            get { return schadenTyp; }
            set
            {
                if (value != schadenTyp)
                {
                    schadenTyp = value;
                    NotifyPropertyChanged();
                }
            }
        }

        double _DK = 0;
        [Used_UserAttribute]
        public double PB
        {
            get { return _DK; }
            set
            {
                if (value != _DK)
                {
                    _DK = value;
                    NotifyPropertyChanged();
                }
            }
        }

    }
}
