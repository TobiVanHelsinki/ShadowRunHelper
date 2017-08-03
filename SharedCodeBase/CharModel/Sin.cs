
namespace ShadowRunHelper.CharModel
{
    public class Sin : Thing
    {
        public Sin()
        {
            ThingType = ThingDefs.Sin;
        }

        public override Thing Copy(Thing target = null)
        {
            if (target == null)
            {
                target = new Sin();
            }
            return base.Copy(target);
        }
    }
}
