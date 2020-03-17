//Author: Tobi van Helsinki

using System;
using System.IO;
using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using ShadowRunHelper;
using ShadowRunHelperViewer.UI.Pages;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ShadowRunHelperViewer.Droid
{
    [Activity(Label = "ShadowRunHelperViewer", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            #region Init Libs
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(LocalConstants.SyncFusion_LICENSEKEY);
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;
            TAPPLICATION_Xamarin.Init.Do();
            Rg.Plugins.Popup.Popup.Init(this, savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState); // add this line to your code, it may also be called: bundle
            Init.Do();
            #endregion Init Libs

            //Xamarin.Forms.Forms.SetFlags("CollectionView_Experimental");
            //Xamarin.Forms.Forms.SetFlags("SwipeView_Experimental");
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            SetAccentColor();
            LoadApplication(new App());
            //DispalyFolderPaths();
        }

        /// <summary>
        /// Displays all folderpaths that have a name
        /// </summary>
        private static void DisplayFolderPaths()
        {
            for (int i = 0; i < 60; i++)
            {
                try
                {
                    System.Diagnostics.Debug.WriteLine((System.Environment.SpecialFolder)i + ":" + System.Environment.GetFolderPath((System.Environment.SpecialFolder)i));
                }
                catch (Exception)
                {
                }
            }
            System.Diagnostics.Debug.WriteLine("ExternalStorageDirectory:" + global::Android.OS.Environment.ExternalStorageDirectory.AbsolutePath);
            System.Diagnostics.Debug.WriteLine("ExternalStorageState:" + global::Android.OS.Environment.ExternalStorageState);
            System.Diagnostics.Debug.WriteLine("RoamingStorage:" + PCLStorage.FileSystem.Current.RoamingStorage?.Path);
            System.Diagnostics.Debug.WriteLine("LocalStorage:" + PCLStorage.FileSystem.Current.LocalStorage?.Path);
            System.Diagnostics.Debug.WriteLine("AppDataDirectory:" + FileSystem.AppDataDirectory);
            System.Diagnostics.Debug.WriteLine("CacheDirectory:" + FileSystem.CacheDirectory);
        }

        /// <summary>
        /// Called when [back pressed]. Instead of ending, the app is pushed in the back. This is,
        /// to prevent an unintende behaivior. This was: The app get's two times initialized when
        /// back button was pressed. this caused errors at SyncFusionsNavigationDrawer and at some
        /// events, that got multiple times fires
        /// </summary>
        public override void OnBackPressed()
        {
            base.OnBackPressed();
            if (MainPage.Instance.ShallExit)
            {
                MainPage.Instance.ShallExit = false;
                MoveTaskToBack(true);
            }
        }

        private void SetAccentColor()
        {
            // get the accent color from your theme
            var themeAccentColor = new TypedValue();
            Theme.ResolveAttribute(Resource.Attribute.colorAccent, themeAccentColor, true);
            var droidAccentColor = new Color(themeAccentColor.Data);

            // set Xamarin Color.Accent to match the theme's accent color
            try
            {
                var accentColorProp = typeof(Xamarin.Forms.Color).GetProperty(nameof(Xamarin.Forms.Color.Accent), System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static);
                var xamarinAccentColor = new Xamarin.Forms.Color(droidAccentColor.R / 255.0, droidAccentColor.G / 255.0, droidAccentColor.B / 255.0, droidAccentColor.A / 255.0);
                xamarinAccentColor = new Color(21.0 / 255.0, 161.0 / 255.0, 21.0 / 255.0, 1); // a nice green
                accentColorProp.SetValue(null, xamarinAccentColor, System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static, null, null, System.Globalization.CultureInfo.CurrentCulture);
            }
            catch (Exception)
            {
            }
        }

        public override bool OnKeyDown([GeneratedEnum] Keycode keyCode, KeyEvent e)
        {
            if (keyCode == Keycode.Escape)
            {
                return UI.Pages.MainPage.Instance.OnKeyDown("ESC");
            }
            return base.OnKeyDown(keyCode, e);
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}