///Author: Tobi van Helsinki

using ShadowRunHelper.CharModel;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace ShadowRunHelper.CharController
{
    [ShadowRunHelperController(SupportsEdit = false)]
    public class BerechnetController : Controller<Berechnet>
    {
        #region Properties
        public Berechnet Essenz { get; set; } = new Berechnet();

        public Berechnet Limit_K { get; set; } = new Berechnet();
        public Berechnet Limit_G { get; set; } = new Berechnet();
        public Berechnet Limit_S { get; set; } = new Berechnet();
        public Berechnet Laufen { get; set; } = new Berechnet();
        public Berechnet Rennen { get; set; } = new Berechnet();
        public Berechnet Tragen { get; set; } = new Berechnet();
        public Berechnet MaxDamageK { get; set; } = new Berechnet();
        public Berechnet MaxDamageG { get; set; } = new Berechnet();
        #endregion Properties

        #region References
        private AttributController AttributeRef;
        private Person Person;
        private ObservableCollection<Implantat> lstImplantateRef;
        #endregion References

        private ObservableCollection<Berechnet> MyData;

        #region Override Controller
        public override ObservableCollection<Berechnet> Data { get => MyData; protected set => MyData = value; }

        #endregion Override Controller

        public BerechnetController()
        {
            RefreshIdentifiers(this);
            Data = new ObservableCollection<Berechnet>
            {
                Essenz,
                Limit_K,
                Limit_G,
                Limit_S,
                Laufen,
                Rennen,
                Tragen
            };
        }

        public void SetDependencies(Person p, ObservableCollection<Implantat> i, AttributController a)
        {
            AttributeRef = a;
            Person = p;
            lstImplantateRef = i;

            Essenz.Value.PropertyChanged += (x, y) => { if (y.PropertyName == nameof(Essenz.Value.BaseValue) || y.PropertyName == nameof(Essenz.Value.Value)) { RefreshLimitS(); } };
            Person.PropertyChanged += (x, y) => { if (y.PropertyName == nameof(Person.Essenz)) { RefreshEssenz(); } };
            Person.PropertyChanged += (x, y) => { if (y.PropertyName == nameof(Person.Schaden_G_max_mod)) { RefreshLimitSchadenG(); } };
            Person.PropertyChanged += (x, y) => { if (y.PropertyName == nameof(Person.Schaden_K_max_mod)) { RefreshLimitSchadenK(); } };
            lstImplantateRef.CollectionChanged += (x, y) => RegisterEssenzRefreshers();

            AttributeRef.Geschick.Value.PropertyChanged += (x, y) => { RefreshLaufen(); RefreshRennen(); };
            AttributeRef.Konsti.Value.PropertyChanged += (x, y) => { RefreshLimitK(); RefreshLimitSchadenK(); };
            AttributeRef.Reaktion.Value.PropertyChanged += (x, y) => RefreshLimitK();
            AttributeRef.Staerke.Value.PropertyChanged += (x, y) => { RefreshTragen(); RefreshLimitK(); };
            AttributeRef.Charisma.Value.PropertyChanged += (x, y) => RefreshLimitS();
            AttributeRef.Logik.Value.PropertyChanged += (x, y) => RefreshLimitG();
            AttributeRef.Intuition.Value.PropertyChanged += (x, y) => RefreshLimitG();
            AttributeRef.Willen.Value.PropertyChanged += (x, y) => { RefreshLimitS(); RefreshLimitG(); RefreshLimitSchadenG(); };

            RefreshEssenz();
            RefreshLimitS();
            RefreshLimitG();
            RefreshLimitK();
            RefreshLimitSchadenG();
            RefreshLimitSchadenK();
            RefreshLaufen();
            RefreshRennen();
            RefreshTragen();
        }

        private void RegisterEssenzRefreshers()
        {
            void MyEventHandler(object x, PropertyChangedEventArgs y) => RefreshEssenz();
            foreach (var item in lstImplantateRef)
            {
                item.PropertyChanged += MyEventHandler;
            }
            RefreshEssenz();
        }

        #region Refresh Methods

        protected void RefreshEssenz()
        {
            Essenz.Value.BaseValue = 6;
            Essenz.Value.BaseValue += Person.Essenz;
            Essenz.Value.BaseValue -= lstImplantateRef.Where(x => x.Besitz == true).Sum(x => x.Essenz);
        }

        protected void RefreshLimitSchadenG()
        {
            Person.Schaden_G_max = 8 + Math.Ceiling(AttributeRef.Willen.Value.Value / 2) + Person.Schaden_G_max_mod;
            MaxDamageG.Value.BaseValue = Person.Schaden_G_max;
        }

        protected void RefreshLimitSchadenK()
        {
            Person.Schaden_K_max = 8 + Math.Ceiling(AttributeRef.Konsti.Value.Value / 2) + Person.Schaden_K_max_mod;
            MaxDamageK.Value.BaseValue = Person.Schaden_K_max;
        }

        //Physical Limit: (STR x2 + BOD + REA) / 3
        protected void RefreshLimitK()
        {
            Limit_K.Value.BaseValue = Math.Ceiling((AttributeRef.Staerke.Value.Value * 2 + AttributeRef.Konsti.Value.Value + AttributeRef.Reaktion.Value.Value) / 3);
        }

        //Mental Limit: (LOG x2 + INT +WIL) / 3

        protected void RefreshLimitG()
        {
            Limit_G.Value.BaseValue = Math.Ceiling((AttributeRef.Logik.Value.Value * 2 + AttributeRef.Intuition.Value.Value + AttributeRef.Willen.Value.Value) / 3);
        }
        //Social Limit: (CHA x2 + WIL + Essence) /3

        protected void RefreshLimitS()
        {
            Limit_S.Value.BaseValue = Math.Ceiling((AttributeRef.Charisma.Value.Value * 2 + AttributeRef.Willen.Value.Value + Essenz.Value.Value) / 3);
        }

        protected void RefreshLaufen()
        {
            Laufen.Value.BaseValue = AttributeRef.Geschick.Value.Value * 2;
        }

        protected void RefreshRennen()
        {
            Rennen.Value.BaseValue = AttributeRef.Geschick.Value.Value * 4;
        }

        protected void RefreshTragen()
        {
            Tragen.Value.BaseValue = AttributeRef.Staerke.Value.Value * 10; //5 und 15 auch mgl TODO
        }
        #endregion Refresh Methods
    }
}