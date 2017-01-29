
namespace ShadowRunHelper.CharModel
{

    public class Attribut : Thing
    {
        //public override double GetValue(string ID = "")
        //{
        //    return Wert;
        //}
        public Attribut()
        {
            ThingType = ThingDefs.Attribut;
        }


        public override double GetValue(string ID = "")
        {
            return Wert;
        }

        public Attribut Copy(Attribut target = null)
        {
            if (target == null)
            {
                target = new Attribut();
            }
            base.Copy(target);
            return target;
        }

        public new void Reset()
        {
            base.Reset();
        }


    }
}
