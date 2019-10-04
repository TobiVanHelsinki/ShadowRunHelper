using ShadowRunHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ShadowRunHelperViewer.UI.ControlsOther
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsView : ContentView
    {
        public SettingsModel Settings => SettingsModel.Instance;
        public SettingsView()
        {
            BindingContext = this;
            InitializeComponent();
        }
    }
}