using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ShadowRunHelperViewer.UI.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AdditionalValue : ContentView
	{
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }
        public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(AdditionalValue), "-", BindingMode.OneTime);

        public string ValuePath
        {
            get { return (string)GetValue(ValuePathProperty); }
            set { SetValue(ValuePathProperty, value); }
        }
        public static readonly BindableProperty ValuePathProperty = BindableProperty.Create(nameof(ValuePath), typeof(string), typeof(AdditionalValue), "-", BindingMode.OneTime);

        public AdditionalValue()
        {
            InitializeComponent();
            BindingContextChanged += AdditionalValue_BindingContextChanged;
        }

        private void AdditionalValue_BindingContextChanged(object sender, EventArgs e)
        {
            ValueDisplay.SetBinding(Label.TextProperty, ValuePath);
            NameDisplay.Text = Text;
        }
    }
}