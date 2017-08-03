
namespace ShadowRunHelper.CharModel
{
    public class Attribut : Thing
    {
        public Attribut()
        {
            ThingType = ThingDefs.Attribut;
        }

        public override Thing Copy(Thing target = null)
        {
            if (target == null)
            {
                target = new Attribut();
            }
            return base.Copy(target);
        }
    }
}
