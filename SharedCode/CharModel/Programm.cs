namespace ShadowRunHelper.CharModel
{
    public class Programm : Item
    {
        private string optionen = "";
        [Used_UserAttribute]
        //TODO in Item.Mods ändern
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
    }
}
