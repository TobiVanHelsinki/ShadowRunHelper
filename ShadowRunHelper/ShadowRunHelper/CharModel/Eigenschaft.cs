namespace ShadowRunHelper.CharModel
{
    public class Eigenschaft : Thing
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
            this.ThingType = Ressourcen.TypNamen.ThingDefs.Eigenschaft;
        }

        public Eigenschaft Copy(Eigenschaft target = null)
        {
            if (target == null)
            {
                target = new Eigenschaft();
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
