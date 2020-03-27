//Author: Tobi van Helsinki

using ShadowRunHelper;
using ShadowRunHelper.CharModel;
using ShadowRunHelperViewer.UI.CharTemplates;
using Xamarin.Forms;

namespace ShadowRunHelperViewer
{
    internal class ThingDataTemplateSelector : DataTemplateSelector
    {
        private readonly DataTemplate SeparatorTemplate;
        private readonly DataTemplate ThingTemplate;
        private readonly DataTemplate HandlungTemplate;
        private readonly DataTemplate NoteTemplate;
        private readonly DataTemplate SuperNaturalSkillsTemplate;
        private readonly DataTemplate EigenschaftTemplate;
        private readonly DataTemplate ItemTemplate;
        private readonly DataTemplate VehikelTemplate;
        private readonly DataTemplate KommlinkTemplate;
        private readonly DataTemplate CyberDeckTemplate;
        private readonly DataTemplate ProgrammTemplate;
        private readonly DataTemplate FernkampfwaffeTemplate;
        private readonly DataTemplate NahkampfwaffeTemplate;
        private readonly DataTemplate PanzerungTemplate;
        private readonly DataTemplate MunitionTemplate;
        private readonly DataTemplate HelperTemplate;
        private readonly DataTemplate ConnectionTemplate;
        private readonly DataTemplate InitiationTemplate;
        private readonly DataTemplate WandlungTemplate;

        public ThingDataTemplateSelector()
        {
            SeparatorTemplate = new DataTemplate(typeof(SeparatorTemplate));
            ThingTemplate = new DataTemplate(typeof(ThingTemplate));

            HandlungTemplate = new DataTemplate(typeof(HandlungTemplate));
            NoteTemplate = new DataTemplate(typeof(NoteTemplate));
            SuperNaturalSkillsTemplate = new DataTemplate(typeof(SuperNaturalSkillsTemplate));
            EigenschaftTemplate = new DataTemplate(typeof(EigenschaftTemplate));
            ItemTemplate = new DataTemplate(typeof(ItemTemplate));
            VehikelTemplate = new DataTemplate(typeof(VehikelTemplate));
            KommlinkTemplate = new DataTemplate(typeof(KommlinkTemplate));
            CyberDeckTemplate = new DataTemplate(typeof(CyberDeckTemplate));
            ProgrammTemplate = new DataTemplate(typeof(ProgrammTemplate));
            FernkampfwaffeTemplate = new DataTemplate(typeof(FernkampfwaffeTemplate));
            NahkampfwaffeTemplate = new DataTemplate(typeof(NahkampfwaffeTemplate));
            PanzerungTemplate = new DataTemplate(typeof(PanzerungTemplate));
            MunitionTemplate = new DataTemplate(typeof(MunitionTemplate));
            HelperTemplate = new DataTemplate(typeof(HelperTemplate));
            ConnectionTemplate = new DataTemplate(typeof(ConnectionTemplate));
            InitiationTemplate = new DataTemplate(typeof(InitiationTemplate));
            WandlungTemplate = new DataTemplate(typeof(WandlungTemplate));
        }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            if (item is Thing t)
            {
                if (t.IsSeperator)
                {
                    return SeparatorTemplate;
                }
                else
                {
                    return t.ThingType switch
                    {
                        ThingDefs.Handlung => HandlungTemplate,
                        ThingDefs.Note => NoteTemplate,
                        ThingDefs.Zaubersprueche => SuperNaturalSkillsTemplate,
                        ThingDefs.KomplexeForm => SuperNaturalSkillsTemplate,
                        ThingDefs.Eigenschaft => EigenschaftTemplate,
                        ThingDefs.Vorteil => EigenschaftTemplate,
                        ThingDefs.Nachteil => EigenschaftTemplate,
                        ThingDefs.Item => ItemTemplate,
                        ThingDefs.Implantat => ItemTemplate,
                        ThingDefs.Vehikel => VehikelTemplate,
                        ThingDefs.Kommlink => KommlinkTemplate,
                        ThingDefs.CyberDeck => CyberDeckTemplate,
                        ThingDefs.Programm => ProgrammTemplate,
                        ThingDefs.Fernkampfwaffe => FernkampfwaffeTemplate,
                        ThingDefs.Nahkampfwaffe => NahkampfwaffeTemplate,
                        ThingDefs.Panzerung => PanzerungTemplate,
                        ThingDefs.Munition => MunitionTemplate,
                        ThingDefs.Geist => HelperTemplate,
                        ThingDefs.Sprite => HelperTemplate,
                        ThingDefs.Connection => ConnectionTemplate,
                        ThingDefs.Initiation => InitiationTemplate,
                        ThingDefs.Wandlung => WandlungTemplate,
                        _ => ThingTemplate,
                    };
                }
            }
            else
            {
                return null;
            }
        }
    }
}