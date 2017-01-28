using static ShadowRunHelper.Ressourcen.TypNamen;

namespace ShadowRunHelper.CharModel
{

    public class Attribut : CharModel.Thing
    {
        //public override double GetValue(string ID = "")
        //{
        //    return Wert;
        //}
        public Attribut()
        {
            ThingType = ThingDefs.Attribut;
        }
    }
}
