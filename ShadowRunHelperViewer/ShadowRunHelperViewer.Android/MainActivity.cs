using Android.App;
using Android.Content.PM;
using Android.OS;
using CarouselView.FormsPlugin.Android;
using ShadowRunHelper;

namespace ShadowRunHelperViewer.Droid
{
    [Activity(Label = "ShadowRunHelperViewer", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            Init.Do();

            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            #region Init Libs
            TAPPLICATION_Droid.Init.Do();
            Rg.Plugins.Popup.Popup.Init(this, savedInstanceState);
            //CarouselViewRenderer.Init();
            #endregion

            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());
        }
    }
}