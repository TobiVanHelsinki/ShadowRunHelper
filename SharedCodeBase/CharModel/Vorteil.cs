
namespace ShadowRunHelper.CharModel
{
    public class Vorteil : Eigenschaft
    {
        public Vorteil()
        {
            ThingType = ThingDefs.Vorteil;

        }
        public override Thing Copy(Thing target = null)
        {
            if (target == null)
            {
                target = new Vorteil();
            }
            return base.Copy(target);
        }
    }
}
