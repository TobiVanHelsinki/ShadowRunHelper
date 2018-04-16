using ShadowRunHelper.CharModel;
using ShadowRunHelper.Model;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using TLIB;
using TAMARIN;

namespace ShadowRunHelper.CharController
{
    public class AttributController : Controller<Attribut>
    {
        // Variable Stuff #####################################################
        // Variable Model Stuff ###########################
        [Newtonsoft.Json.JsonIgnore] //cause sometimes an very uebel Bug
        //[Newtonsoft.Json.JsonIgnore]
        public new ObservableCollection<Attribut> Data; //cause sometimes an very uebel Bug

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

        AllListEntry MI_Konsti;
        AllListEntry MI_Geschick;
        AllListEntry MI_Reaktion;
        AllListEntry MI_Staerke;
        AllListEntry MI_Charisma;
        AllListEntry MI_Logik;
        AllListEntry MI_Intuition;
        AllListEntry MI_Willen;

        AllListEntry MI_Magie;
        AllListEntry MI_Resonanz;

        // Variable Logik Stuff ###########################
        bool m_MutexDataColectionChange = false;

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
            RefreshBezeichner();
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
            Data.CollectionChanged += Data_CollectionChanged;
            RefreshDataList();
        }

        void RefreshBezeichner()
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

        // DataList Handling ##############################
        void RefreshDataList()
        {
            RefreshBezeichner();
            Data.Clear();
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
        void Data_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (!m_MutexDataColectionChange && e.Action == NotifyCollectionChangedAction.Add && Data.Count >= 12)
            {
                m_MutexDataColectionChange = true;
                RefreshDataList();
                m_MutexDataColectionChange = false;
            }
        }

        // Implement IController ##########################
        public override IEnumerable<AllListEntry> GetElementsForThingList()
        {
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