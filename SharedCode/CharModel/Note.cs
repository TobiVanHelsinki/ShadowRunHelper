///Author: Tobi van Helsinki

namespace ShadowRunHelper.CharModel
{
    public class Note : Thing
    {
        string _Text;
        [Used_UserAttribute]
        public string Text
        {
            get { return _Text; }
            set { if (_Text != value) { _Text = value; NotifyPropertyChanged(); } }
        }
    }
}