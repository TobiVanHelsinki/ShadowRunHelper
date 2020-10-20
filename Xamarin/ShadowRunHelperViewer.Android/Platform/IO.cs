//Author: Tobi van Helsinki

using Android.App;
using Android.Content;
using ShadowRunHelper.IO;
using ShadowRunHelperViewer.Droid;
using System.IO;
using System.Threading.Tasks;

namespace ShadowRunHelperViewer.Platform.Droid
{
    public class IO : Xam.IO, IPlatformIO
    {
        /// <summary>
        /// PickFolder
        /// </summary>
        /// <param name="Token"></param>
        /// <returns></returns>
        /// <exception cref="ActivityNotFoundException"></exception>
        public override async Task<DirectoryInfo> PickFolder(string Token = null)
        {
            if (!(MainActivity.ActivityContext is Activity activity))
            {
                activity = Xamarin.Forms.Forms.Context as Activity;
            }
            activity.Intent = new Intent();
            activity.Intent.SetAction(Intent.ActionOpenDocumentTree);
            if (!int.TryParse(Token, out int token))
            {
                token = 1;
            }
            activity.StartActivityForResult(activity.Intent, token);
            return null;
        }

        public override async Task<bool> HasAccess(DirectoryInfo Path)
        {
            return true; // todo ...
        }
    }
}