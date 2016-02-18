namespace ShadowRun_Charakter_Helper.CharModel
{
    public class Attribut : CharModel.Model
    {
        public int wert_Mod;
        public int Wert_Mod
        {
            get { return wert_Mod; }
            set
            {
                if (value != this.wert_Mod)
                {
                    this.wert_Mod = value;
                    NotifyPropertyChanged();
                }
            }
        }


        public Attribut()
        {

        }
    }
}
