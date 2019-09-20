using ShadowRunHelper;
using Xamarin.Forms.Platform.UWP;

namespace ShadowRunHelperViewer.UWP
{
    public sealed partial class MainPage
    {
        public MainPage()
        {
            InitializeComponent();
            var application = new ShadowRunHelperViewer.App();
            LoadApplication(application);
            try
            {
                var visualElement = (application.MainPage as UI.Pages.MainPage).ContentPlaceBackPublic.GetOrCreateRenderer().ContainerElement;
                Features.Ui.RegisterTopUiSizeChanged(visualElement);
                Features.Ui.IsTopUiSizeEnabled = true;
            }
            catch (System.Exception)
            {
                Features.Ui.IsTopUiSizeEnabled = false;
            }
        }
    }
}
