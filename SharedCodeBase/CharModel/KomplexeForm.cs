
namespace ShadowRunHelper.CharModel
{
    public class KomplexeForm : Thing
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
    }
}
