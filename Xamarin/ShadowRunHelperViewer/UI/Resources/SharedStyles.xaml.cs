//Author: Tobi van Helsinki

using ShadowRunHelperViewer.UI.Resources;

namespace ShadowRunHelperViewer
{
    public partial class SharedStyles
    {
        public SharedStyles()
        {
            foreach (var item in typeof(StyleManager).GetProperties())
            {
                try
                {
                    Add(item.Name, item.GetValue(null));
                }
                catch (System.Exception)
                {
                }
            }

            InitializeComponent();
        }
    }
}