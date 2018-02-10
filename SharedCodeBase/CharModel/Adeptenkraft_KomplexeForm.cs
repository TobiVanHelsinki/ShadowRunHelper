
namespace ShadowRunHelper.CharModel
{
    public class Adeptenkraft_KomplexeForm : Thing
    {
        string _Option = "";
        [Used]
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

        public Adeptenkraft_KomplexeForm()
        {
            ThingType = ThingDefs.Adeptenkraft_KomplexeForm;
        }
    }
}
