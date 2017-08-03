
namespace ShadowRunHelper.CharModel
{
    public class Nachteil : Eigenschaft
    {
        public Nachteil()
        {
            ThingType = ThingDefs.Nachteil;
        }
        public override Thing Copy(Thing target = null)
        {
            if (target == null)
            {
                target = new Nachteil();
            }
            return base.Copy(target);
        }
    }
}
