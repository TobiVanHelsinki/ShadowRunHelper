
using Windows.UI.Xaml.Controls;

namespace Experiments
{

    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            int q = 5;
            //TestBlock.Text = Lib_STD.Class1.MyMethod();
            TLIB_UWP.Init.Do();
            TAPPLICATION_UWP.Init.Do();
            TestBlock.Text = TLIB.PlatformHelper.StringHelper.GetPrefix(TLIB.PlatformHelper.PrefixType.AppUserData);
            TestBlock2.Text = TAPPLICATION.Model.SharedSettingsModel.PlatformSettings.GetIntLocal("d", 77).ToString();
        }
    }
}
