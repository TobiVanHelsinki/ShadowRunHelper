namespace ShadowRunHelper.CharModel
{
    public abstract class Eigenschaft : Thing
    {
        //public override double GetValue(string ID = "")
        //{
        //    return Wert;
        //}
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
            this.ThingType = ThingDefs.Eigenschaft;
        }

        private Eigenschaft Copy(Eigenschaft target = null)
        {
            if (target == null)
            {
                throw new System.ArgumentNullException();
                //target = new Eigenschaft();
            }
            base.Copy(target);
            target.Auswirkungen = Auswirkungen;
            return target;
        }

        public new void Reset()
        {
            Auswirkungen = "";
            base.Reset();
        }
    }
}
