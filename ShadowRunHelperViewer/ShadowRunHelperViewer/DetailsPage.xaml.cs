using ShadowRunHelper;
using ShadowRunHelper.CharModel;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ShadowRunHelperViewer
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetailsPage : Rg.Plugins.Popup.Pages.PopupPage
    {
        public Thing MyThing { get; set; }
        public DetailsPage(Thing thing)
        {
            MyThing = thing;
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            string key = TypeHelper.ThingDefToString(MyThing.ThingType, false);
            Resources.TryGetValue(key, out object CustomTemplate);
            try
            {
                MainContent.ControlTemplate = CustomTemplate as ControlTemplate;
            }
            catch (System.Exception ex)
            {
                TAPPLICATION.Debugging.TraceException(ex);
            }
            BindingContext = this;
            base.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }
    }
}