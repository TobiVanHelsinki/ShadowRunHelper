using Newtonsoft.Json;
using ShadowRunHelper.CharModel;
using ShadowRunHelper.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using TLIB;

namespace ShadowRunHelper.CharController
{
    public class AttributController : Controller<Attribut>
    {
        [JsonIgnore]
        public new ObservableCollection<Attribut> Data { get; protected set; }

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

        [JsonIgnore]
        public AllListEntry MI_Konsti { get; set; }
        [JsonIgnore]
        public AllListEntry MI_Geschick { get; set; }
        [JsonIgnore]
        public AllListEntry MI_Reaktion { get; set; }
        [JsonIgnore]
        public AllListEntry MI_Staerke { get; set; }
        [JsonIgnore]
        public AllListEntry MI_Charisma { get; set; }
        [JsonIgnore]
        public AllListEntry MI_Logik { get; set; }
        [JsonIgnore]
        public AllListEntry MI_Intuition { get; set; }
        [JsonIgnore]
        public AllListEntry MI_Willen { get; set; }

        [JsonIgnore]
        public AllListEntry MI_Magie { get; set; }
        [JsonIgnore]
        public AllListEntry MI_Resonanz { get; set; }

        // Start Stuff ########################################################
        public AttributController()
        {
            Konsti = new Attribut();
            Geschick = new Attribut();
            Reaktion = new Attribut();
            Staerke = new Attribut();
            Charisma = new Attribut();
            Logik = new Attribut();
            Intuition = new Attribut();
            Willen = new Attribut();
            Magie = new Attribut();
            Resonanz = new Attribut();
            RefreshIdentifiers(this);

            MI_Konsti = new AllListEntry(Konsti);
            MI_Geschick = new AllListEntry(Geschick);
            MI_Reaktion = new AllListEntry(Reaktion);
            MI_Staerke = new AllListEntry(Staerke);
            MI_Charisma = new AllListEntry(Charisma);
            MI_Logik = new AllListEntry(Logik);
            MI_Intuition = new AllListEntry(Intuition);
            MI_Willen = new AllListEntry(Willen);
            MI_Magie= new AllListEntry(Magie);
            MI_Resonanz= new AllListEntry(Resonanz);

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

        // Implement IController ##########################
        public override IEnumerable<AllListEntry> GetElementsForThingList()
        {
            RefreshIdentifiers(this);
            var lstReturn = new List<AllListEntry>
            {
                MI_Charisma,
                MI_Geschick,
                MI_Reaktion,
                MI_Konsti,
                MI_Staerke,
                MI_Logik,
                MI_Intuition,
                MI_Willen,
                MI_Magie,
                MI_Resonanz
            };
            return lstReturn;
        }

        //Override cController ############################

        public override IEnumerable<Thing> GetElements()
        {
            var lstReturn = new List<Thing>
            {
                Charisma,
                Geschick,
                Reaktion,
                Konsti,
                Staerke,
                Logik,
                Intuition,
                Willen,
                Magie,
                Resonanz
            };
            return lstReturn;
        }

        public override Thing AddNewThing()
        {
            throw new NotSupportedException();
        }

        public override void RemoveThing(Thing tRem)
        {
            throw new NotSupportedException();
        }
    }
}