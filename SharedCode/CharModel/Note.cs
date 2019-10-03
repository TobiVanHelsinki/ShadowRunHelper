namespace ShadowRunHelper.CharModel
{
    public class Note : Thing
    {
        [Used_UserAttribute]
        string _Text;
        public string Text
        {
            get { return _Text; }
            set { if (_Text != value) { _Text = value; NotifyPropertyChanged(); } }
        }

        protected override bool UseForCalculation()
        {
            return false;
        }
    }
}
