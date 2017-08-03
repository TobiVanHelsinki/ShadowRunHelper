
namespace ShadowRunHelper.CharModel
{
    public class Foki_Widgets : Item
    {
        public Foki_Widgets()
        {
            ThingType = ThingDefs.Foki_Widgets;
        }
        public override Thing Copy(Thing target = null)
        {
            if (target == null)
            {
                target = new Foki_Widgets();
            }
            return base.Copy(target);
        }
    }
}
