
namespace ShadowRunHelper.CharModel
{
    class Adeptenkraft_KomplexeForm : Thing
    {
        string _Option = "";
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
            this.ThingType = ThingDefs.Adeptenkraft_KomplexeForm;
        }
    }
}
