using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Views;
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
            #endregion

            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            #region Forms9Patch Init
            Forms9Patch.Droid.Settings.Initialize(this);
            #endregion
            LoadApplication(new App());
        }

        public override bool OnKeyDown([GeneratedEnum] Keycode keyCode, KeyEvent e)
        {
            //TODO handle Esc here
            return base.OnKeyDown(keyCode, e);
        }

        public override void OnBackPressed()
        {
            if (Rg.Plugins.Popup.Popup.SendBackPressed(base.OnBackPressed))
            {
                //Rg.Plugins.Popup.Popup.SendBackPressed
                // Do something if there are some pages in the `PopupStack`
            }
            else
            {
                // Do something if there are not any pages in the `PopupStack`
            }
        }
    }
}