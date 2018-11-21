
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ShadowRunHelper.Xam.UI.Parts
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class IconButton : ContentView
	{
        public string Icon
        {
            get { return (string)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }
        // Using a DependencyProperty as the backing store for Icon.  This enables animation, styling, binding, etc...
        public static readonly BindableProperty IconProperty =
            BindableProperty.CreateAttached("Icon", typeof(string), typeof(IconButton), "");


        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }
        // Using a DependencyProperty as the backing store for Text.  This enables animation, styling, binding, etc...
        public static readonly BindableProperty TextProperty =
            BindableProperty.CreateAttached("Text", typeof(string), typeof(IconButton), "");


        public IconButton ()
		{
			InitializeComponent ();
        }
	}
}