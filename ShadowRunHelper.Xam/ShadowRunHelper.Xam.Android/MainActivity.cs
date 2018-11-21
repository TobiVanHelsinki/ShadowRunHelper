using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace ShadowRunHelper.Xam.Droid
{
    [Activity(Label = "ShadowRunHelper.Xam", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            Features.Activities = new DroidActivities();
            Features.Analytics = new DroidAnalytics();
            Features.IAP = new WinIAP();
            Features.InstanceHandling = new DroidInstanceHandling();
            Features.AppInformation = new DroidAppInformation();
            Features.AppDataPorter = new DroidAppDataPorter();

            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;
            base.OnCreate(savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());

        }
    }
}