
namespace ShadowRunHelper.CharModel
{
    public class Berechnet : Thing
    {
        public Berechnet()
        {
            ThingType = ThingDefs.Berechnet;
        }

        public override Thing Copy(Thing target = null)
        {
            if (target == null)
            {
                target = new Berechnet();
            }
            return base.Copy(target);
        }
    }
}
