///Author: Tobi van Helsinki

using ShadowRunHelper.CharModel;
using System.Collections.ObjectModel;

namespace ShadowRunHelper.CharController
{
    [ShadowRunHelperController(SupportsEdit = false)]
    public class AttributController : Controller<Attribut>
    {
        #region Properties
        public Attribut Konsti { get; set; } = new Attribut();// those have to point at a sepcific list element
        public Attribut Geschick { get; set; } = new Attribut();
        public Attribut Reaktion { get; set; } = new Attribut();
        public Attribut Staerke { get; set; } = new Attribut();
        public Attribut Charisma { get; set; } = new Attribut();
        public Attribut Logik { get; set; } = new Attribut();
        public Attribut Intuition { get; set; } = new Attribut();
        public Attribut Willen { get; set; } = new Attribut();
        public Attribut Magie { get; set; } = new Attribut();
        public Attribut Resonanz { get; set; } = new Attribut();
        #endregion Properties

        private ObservableCollection<Attribut> MyData;

        #region Override Controller
        public override ObservableCollection<Attribut> Data { get => MyData; protected set => MyData = value; }

        #endregion Override Controller

        public AttributController()
        {
            RefreshIdentifiers(this);
            Data = new ObservableCollection<Attribut>
            {
                Charisma,
                Konsti,
                Reaktion,
                Staerke,
                Geschick,
                Logik,
                Intuition,
                Willen,
                Magie,
                Resonanz
            };
        }
    }
}