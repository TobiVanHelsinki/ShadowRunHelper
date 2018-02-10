namespace ShadowRunHelper.CharModel
{
    public class Programm : Item
    {
        private string optionen = "";
        [Used]
        public string Optionen
        {
            get { return optionen; }
            set
            {
                if (value != this.optionen)
                {
                    this.optionen = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public Programm()
        {
            ThingType = ThingDefs.Programm;
        }
    }
}
