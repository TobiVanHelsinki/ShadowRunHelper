namespace ShadowRunHelper.CharModel
{
    public class Eigenschaft : Thing
    {
        private string auswirkungen = "";
        public string Auswirkungen
        {
            get { return auswirkungen; }
            set
            {
                if (value != this.auswirkungen)
                {
                    this.auswirkungen = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public Eigenschaft()
        {
            
        }

    }
}
