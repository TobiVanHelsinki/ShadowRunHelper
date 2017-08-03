
namespace ShadowRunHelper.CharModel
{
    public class Munition : Item
    {
        public Munition()
        {
            ThingType = ThingDefs.Munition;
        }
        public override Thing Copy(Thing target = null)
        {
            if (target == null)
            {
                target = new Munition();
            }
            return base.Copy(target);
        }
    }
}
