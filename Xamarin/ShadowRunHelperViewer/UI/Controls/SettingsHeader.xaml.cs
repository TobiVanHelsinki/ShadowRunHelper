//Author: Tobi van Helsinki

using ShadowRunHelper;
using SharedCode.Resources;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ShadowRunHelperViewer.UI.ControlsOther
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsHeader : ContentView
    {
        public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(SettingsEntry), "HeaderText");
        public string Text
        {
            get => UiResources.ResourceManager.GetStringSafe((string)GetValue(TextProperty));
            set => SetValue(TextProperty, value);
        }

        public SettingsHeader()
        {
            InitializeComponent();
            BindingContext = this;
        }
    }
}