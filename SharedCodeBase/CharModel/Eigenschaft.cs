namespace ShadowRunHelper.CharModel
{
    public abstract class Eigenschaft : Thing
    {
        private string auswirkungen = "";
        [Used]
        public string Auswirkungen
        {
            get { return auswirkungen; }
            set
            {
                if (value != auswirkungen)
                {
                    auswirkungen = value;
                    NotifyPropertyChanged();
                }
            }
        }
    }
}
