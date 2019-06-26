using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ShadowRunHelperViewer.UI.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsControl : ContentView
    {
        public static readonly BindableProperty TextProperty = BindableProperty.Create("Text", typeof(string), typeof(SettingsControl), "");
        public static readonly BindableProperty SettingProperty = BindableProperty.Create("Setting", typeof(bool), typeof(SettingsControl), false, BindingMode.TwoWay, propertyChanged: (s, o, n) => (s as SettingsControl).TextChanged(o, n));

        void TextChanged(object oldValue, object newValue)
        {

        }


        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public bool Setting
        {
            get { return (bool)GetValue(SettingProperty); }
            set { SetValue(SettingProperty, value); }
        }

        public SettingsControl()
        {
            InitializeComponent();
            //BindingContext = this;
        }
    }
}