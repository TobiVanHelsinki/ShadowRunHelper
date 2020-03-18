//Author: Tobi van Helsinki

using ShadowRunHelper;
using ShadowRunHelper.CharModel;
using ShadowRunHelperViewer.UI.CharTemplates;
using Xamarin.Forms;

namespace ShadowRunHelperViewer
{
    internal class ThingDataTempalteSelector : DataTemplateSelector
    {
        private readonly DataTemplate SeparatorTemplate;
        private readonly DataTemplate ThingTemplate;
        private readonly DataTemplate HandlungTemplate;

        public ThingDataTempalteSelector()
        {
            SeparatorTemplate = new DataTemplate(typeof(SeparatorTemplate));
            ThingTemplate = new DataTemplate(typeof(ThingTemplate));

            HandlungTemplate = new DataTemplate(typeof(HandlungTemplate));
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