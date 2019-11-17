//Author: Tobi van Helsinki

///Author: Tobi van Helsinki

using Newtonsoft.Json;
using System.Collections.Generic;

namespace ShadowRunHelper.CharModel
{
    public class Fertigkeit : Thing
    {
        [JsonIgnore]
        public override IEnumerable<ThingDefs> Filter => StaticFilter;

        static readonly IEnumerable<ThingDefs> StaticFilter = new List<ThingDefs>()
            {
                ThingDefs.Handlung, ThingDefs.Connection, ThingDefs.Sin
            };

        public Fertigkeit() : base()
        {
        }
    }
}