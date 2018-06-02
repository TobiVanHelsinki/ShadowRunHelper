
namespace ShadowRunHelper.CharModel
{
    public class KomplexeForm : Handlung
    {
        string _Option = "";
        [Used_UserAttribute]
        public string Option
        {
            get { return _Option; }
            set
            {
                if (value != this._Option)
                {
                    this._Option = value;
                    NotifyPropertyChanged();
                }
            }
        }
        string _Target;
        [Used_UserAttribute]
        public string Target
        {
            get { return _Target; }
            set { if (_Target != value) { _Target = value; NotifyPropertyChanged(); } }
        }
        string _Duration;
        [Used_UserAttribute]
        public string Duration
        {
            get { return _Duration; }
            set { if (_Duration != value) { _Duration = value; NotifyPropertyChanged(); } }
        }
        string _Fading;
        [Used_UserAttribute]
        public string Fading
        {
            get { return _Fading; }
            set { if (_Fading != value) { _Fading = value; NotifyPropertyChanged(); } }
        }

        public KomplexeForm() : base()
        {
        }
    }
}
