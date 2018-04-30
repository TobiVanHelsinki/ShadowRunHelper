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
        public new ObservableCollection<Attribut> Data;

        public Attribut Konsti;// those have to point at a sepcific list element
        public Attribut Geschick;
        public Attribut Reaktion;
        public Attribut Staerke;
        public Attribut Charisma;
        public Attribut Logik;
        public Attribut Intuition;
        public Attribut Willen;
        public Attribut Magie;
        public Attribut Resonanz;

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
            RefreshIdentifiers();

            MI_Konsti = new AllListEntry(Konsti, "");
            MI_Geschick = new AllListEntry(Geschick, "");
            MI_Reaktion = new AllListEntry(Reaktion, "");
            MI_Staerke = new AllListEntry(Staerke, "");
            MI_Charisma = new AllListEntry(Charisma, "");
            MI_Logik = new AllListEntry(Logik, "");
            MI_Intuition = new AllListEntry(Intuition, "");
            MI_Willen = new AllListEntry(Willen, "");
            MI_Magie= new AllListEntry(Magie, "");
            MI_Resonanz= new AllListEntry(Resonanz, "");

            Data = new ObservableCollection<Attribut>();
            Data.Add(Charisma);
            Data.Add(Konsti);
            Data.Add(Reaktion);
            Data.Add(Staerke);
            Data.Add(Geschick);
            Data.Add(Logik);
            Data.Add(Intuition);
            Data.Add(Willen);
            Data.Add(Magie);
            Data.Add(Resonanz);
        }

        void RefreshIdentifiers()
        {
            Konsti.Bezeichner = StringHelper.GetString("Model_Attribut_Konsti/Text");
            Geschick.Bezeichner = StringHelper.GetString("Model_Attribut_Geschick/Text");
            Reaktion.Bezeichner = StringHelper.GetString("Model_Attribut_Reaktion/Text");
            Staerke.Bezeichner = StringHelper.GetString("Model_Attribut_Staerke/Text");
            Charisma.Bezeichner = StringHelper.GetString("Model_Attribut_Charisma/Text");
            Logik.Bezeichner = StringHelper.GetString("Model_Attribut_Logik/Text");
            Intuition.Bezeichner = StringHelper.GetString("Model_Attribut_Intuition/Text");
            Willen.Bezeichner = StringHelper.GetString("Model_Attribut_Willen/Text");
            Magie.Bezeichner = StringHelper.GetString("Model_Attribut_Magie/Text");
            Resonanz.Bezeichner = StringHelper.GetString("Model_Attribut_Resonanz/Text");
        }

        // Implement IController ##########################
        public override IEnumerable<AllListEntry> GetElementsForThingList()
        {
            RefreshIdentifiers();
            var lstReturn = new List<AllListEntry>();
            lstReturn.Add(MI_Charisma);
            lstReturn.Add(MI_Geschick);
            lstReturn.Add(MI_Reaktion);
            lstReturn.Add(MI_Konsti);
            lstReturn.Add(MI_Staerke);
            lstReturn.Add(MI_Logik);
            lstReturn.Add(MI_Intuition);
            lstReturn.Add(MI_Willen);
            lstReturn.Add(MI_Magie);
            lstReturn.Add(MI_Resonanz);
            return lstReturn;
        }

        //Override cController ############################
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