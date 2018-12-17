using System;
using ShadowRunHelper;
using ShadowRunHelper.CharController;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ShadowRunHelperViewer
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class GController : ContentView
	{
        public IController Controller
        {
            get { return (IController)GetValue(ControllerProperty); }
            set { SetValue(ControllerProperty, value); }
        }

        private void OnControllerChanged()
        {
            if (Controller != null)
            {
                Headline.Text = TypeHelper.ThingDefToString(Controller.eDataTyp, true);
                Headline.Text = "temp test cat";
                BindingContext = Controller;
            }
        }

        // Using a DependencyProperty as the backing store for Controller.  This enables animation, styling, binding, etc...
        public static readonly BindableProperty ControllerProperty =
            BindableProperty.CreateAttached(nameof(Controller), typeof(IController), typeof(CategoryView), null, BindingMode.OneWay, propertyChanged: (a,b,c)=> meth(a,b,c));

        private static void meth(BindableObject bindable, object oldValue, object newValue)
        {
            if (oldValue != null && newValue == null && bindable != null)
            {
                (bindable as GController).Controller = newValue as IController;
            }
            (bindable as GController)?.OnControllerChanged();
        }

        public GController ()
		{
			InitializeComponent ();
		}

        private void Items_BindingContextChanged(object sender, EventArgs e)
        {

        }
    }
}