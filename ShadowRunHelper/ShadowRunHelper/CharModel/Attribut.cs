
namespace ShadowRunHelper.CharModel
{

    public class Attribut : Thing
    {
        public Attribut()
        {
            ThingType = ThingDefs.Attribut;
        }


        public override double GetValue(string ID = "")
        {
            return Wert;
        }
        

        public override void Reset()
        {
            base.Reset();
        }

        
    }
}
