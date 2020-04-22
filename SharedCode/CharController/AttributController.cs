
using ShadowRunHelper.CharModel;

namespace ShadowRunHelper.CharController
{
    [ShadowRunHelperController(SupportsEdit = false)]
    public class AttributController : OwnDataController<Attribut>
    {
        public Attribut Konsti { get; set; } = new Attribut();
        public Attribut Geschick { get; set; } = new Attribut();
        public Attribut Reaktion { get; set; } = new Attribut();
        public Attribut Staerke { get; set; } = new Attribut();
        public Attribut Charisma { get; set; } = new Attribut();
        public Attribut Logik { get; set; } = new Attribut();
        public Attribut Intuition { get; set; } = new Attribut();
        public Attribut Willen { get; set; } = new Attribut();
        public Attribut Magie { get; set; } = new Attribut();
        public Attribut Resonanz { get; set; } = new Attribut();
    }
}