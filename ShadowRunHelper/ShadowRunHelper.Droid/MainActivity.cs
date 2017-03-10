using Android.App;
using Android.Widget;
using Android.OS;
using ShadowRunHelper;

namespace ShadowRunHelper.Droid
{
    [Activity(Label = "ShadowRunHelper.Droid", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        Model.AppModel AppModel = Model.AppModel.Instance;

        protected override void OnCreate(Bundle bundle)
        {

            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
        }
    }
}

