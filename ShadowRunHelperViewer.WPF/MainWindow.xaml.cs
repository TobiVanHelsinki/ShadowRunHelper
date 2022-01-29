using System;
using Syncfusion.ListView.XForms;
using Syncfusion.ListView.XForms.Control;
using Syncfusion.ListView.XForms.Control.Helpers;
using Syncfusion.ListView.XForms.Helpers;
using Syncfusion.SfBusyIndicator.XForms;
using Syncfusion.SfNavigationDrawer.XForms;
using Syncfusion.Win32;
using Syncfusion.XForms.Buttons;
using Syncfusion.XForms;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Xamarin.Forms;
using Xamarin.Forms.Platform.WPF;
using Syncfusion.ListView.XForms.WPF;
using Syncfusion.XForms.WPF.Border;
using Syncfusion.XForms.WPF.Buttons;

namespace ShadowRunHelperViewer.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : FormsApplicationPage
    {
        private readonly List<Assembly> assembliesToInclude = new List<Assembly>();
        public MainWindow()
        {
            InitializeComponent();

            #region Init Libs
            assembliesToInclude.Add(typeof(dotMorten.Xamarin.Forms.AutoSuggestBox).GetTypeInfo().Assembly);
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(LocalConstants.SyncFusion_LICENSEKEY);
            //assembliesToInclude.Add(typeof(SfBusyIndicatorRenderer).GetTypeInfo().Assembly);
            assembliesToInclude.Add(typeof(SfListViewRenderer).GetTypeInfo().Assembly);
            assembliesToInclude.Add(typeof(SfBorderRenderer).GetTypeInfo().Assembly);
            assembliesToInclude.Add(typeof(SfButtonRenderer).GetTypeInfo().Assembly);
            //assembliesToInclude.Add(typeof(SfPopupLayoutRenderer).GetTypeInfo().Assembly);
            //assembliesToInclude.Add(typeof(SfNavigationDrawerRenderer).GetTypeInfo().Assembly);
            //assembliesToInclude.Add(typeof(SfPopupLayoutRenderer).GetTypeInfo().Assembly);
            assembliesToInclude.Add(typeof(Rg.Plugins.Popup.Popup).GetTypeInfo().Assembly);
            SfListViewRenderer.Init();
            //SfPopupLayoutRenderer.Init();

            ShadowRunHelperViewer.Platform.Xam.Init.Do();
            //ShadowRunHelperViewer.Platform.UWP.Init.Do();

            Rg.Plugins.Popup.Popup.Init();
            #endregion Init Libs

            Forms.Init(assembliesToInclude);
            LoadApplication(new ShadowRunHelperViewer.App());
        }
    }
}
