using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShadowRunHelper.CharModel
{
    public class Nachteil : Eigenschaft
    {
        public Nachteil()
        {
            this.ThingType = Ressourcen.TypNamen.ThingDefs.Nachteil;

        }

        public Nachteil Copy(Nachteil target = null)
        {
            if (target == null)
            {
                target = new Nachteil();
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
