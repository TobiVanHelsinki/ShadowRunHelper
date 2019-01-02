using ShadowRunHelper.Model;
using Xamarin.Forms;

namespace ShadowRunHelperViewer
{
    public partial class MainPage : ContentPage
    {
        public AppModel AppModel => AppModel.Instance;
        public MainPage()
        {
            //AppModel.MainObject = CharHolderGenerator.CreateCharWithStandardContent();
            AppModel.MainObject = CharHolderGenerator.CreateEmtpyChar();
            //AppModel.MainObject = CharHolderGenerator.TestAllCats(15);
            InitializeComponent();
            BindingContext = this;
        }
    }
}
