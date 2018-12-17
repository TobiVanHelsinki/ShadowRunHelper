using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ShadowRunHelperViewer
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CategoryView : ContentView
	{
		public CategoryView ()
		{
			InitializeComponent ();
		}

        private void GoBackClicked(object sender, EventArgs e)
        {

        }
    }
}