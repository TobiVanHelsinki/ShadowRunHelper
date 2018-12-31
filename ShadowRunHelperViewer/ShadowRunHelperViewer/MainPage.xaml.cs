using ShadowRunHelper.Model;
using Xamarin.Forms;

namespace ShadowRunHelperViewer
{
    public partial class MainPage : ContentPage
    {
        public AppModel AppModel => AppModel.Instance;
        public MainPage()
        {
            //AppModel.MainObject = CharHolder.CreateCharWithStandardContent();
            AppModel.MainObject = CharHolderTest.TestAllCats(2);
            InitializeComponent();
            BindingContext = this;
        }
    }
}
