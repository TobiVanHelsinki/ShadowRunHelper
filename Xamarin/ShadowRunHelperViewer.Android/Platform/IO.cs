//Author: Tobi van Helsinki
using Android.Content;
using ShadowRunHelper.IO;
using System.IO;
using System.Threading.Tasks;
using Android.Content;

using Xamarin.Forms.Platform.Android;

namespace ShadowRunHelperViewer.Platform.Droid
{
    public class IO : Xam.IO, IPlatformIO
    {
        public static object message;

        /// <summary>
        /// PickFolder
        /// </summary>
        /// <param name="Token"></param>
        /// <returns></returns>
        /// <exception cref="Java.Lang.IllegalMonitorStateException"></exception>
        /// <exception cref="Java.Lang.InterruptedException"></exception>
        /// <exception cref="ActivityNotFoundException"></exception>
        public override async Task<DirectoryInfo> PickFolder(string Token = null)
        {
            var a = Android.App.Application.Context;
            //Android.App.Activity activity = a as Android.App.Activity;
            Xamarin.Forms.Forms.Context.StartActivity();
            MainApplication.ActivityContext;
            Android.App.Activity activity = a.GetActivity();
            activity.Intent = new Intent();
            activity.Intent.SetAction(Intent.ActionOpenDocumentTree);
            int REQUEST_CODE_OPEN_DIRECTORY = 1;
            //DirectoryInfo result = null;
            //MessagingCenter.Subscribe<Activity>(this, REQUEST_CODE_OPEN_DIRECTORY.ToString(), (sender) =>
            //{
            //    if (System.Diagnostics.Debugger.IsAttached) System.Diagnostics.Debugger.Break();
            //    //result = new DirectoryInfo(args.ToUri(IntentUriType.Scheme));
            //});
            //MessagingCenter.Subscribe<Activity, Intent>(this, REQUEST_CODE_OPEN_DIRECTORY.ToString(), (sender, args) =>
            //{
            //    if (System.Diagnostics.Debugger.IsAttached) System.Diagnostics.Debugger.Break();
            //    result = new DirectoryInfo(args.ToUri(IntentUriType.Scheme));
            //});
            activity.StartActivityForResult(activity.Intent, REQUEST_CODE_OPEN_DIRECTORY);
            //activity.StartLockTask();

            //activity.OnActivityResult += (object sender, ActivityResultEventArgs e) =>
            //{
            //    FolderPath = e.Intent.Data;
            //    string DummyPath = FolderPath.Path;
            //    OriginalPath = DummyPath.Split(':')[1];
            //    RealPath = "/storage/emulated/0/" + OriginalPath;
            //    MainPage.Unzip(FolderPath.ToString());
            //};
            //while (message is null)
            {
                //Thread.Sleep(500);
            }
            //if (System.Diagnostics.Debugger.IsAttached) System.Diagnostics.Debugger.Break();
            return new DirectoryInfo("");
        }
    }
}