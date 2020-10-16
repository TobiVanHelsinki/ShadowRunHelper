//Author: Tobi van Helsinki

namespace ShadowRunHelperViewer.UWP
{
    public sealed partial class MainPage
    {
        public MainPage()
        {
            InitializeComponent();
            LoadApplication(new ShadowRunHelperViewer.App());
        }
    }
}