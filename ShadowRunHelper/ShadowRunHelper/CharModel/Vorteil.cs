using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShadowRunHelper.CharModel
{
    public class Vorteil : Eigenschaft
    {
        public Vorteil()
        {
            this.ThingType = ThingDefs.Vorteil;

        }

        public Vorteil Copy(Vorteil target = null)
        {
            if (target == null)
            {
                target = new Vorteil();
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
