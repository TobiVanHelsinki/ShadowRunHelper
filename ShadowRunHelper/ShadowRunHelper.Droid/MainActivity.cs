using Android.App;
using Android.Widget;
using Android.OS;
using ShadowRunHelper;

namespace ShadowRunHelper.Droid
{
    [Activity(Label = "ShadowRunHelper.Droid", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            Model.ViewModel ViewModel = Model.ViewModel.Instance;

            base.OnCreate(bundle);
            
            // Set our view from the "main" layout resource
            // SetContentView (Resource.Layout.Main);
        }
    }
}

