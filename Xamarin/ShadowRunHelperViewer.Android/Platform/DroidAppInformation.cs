//Author: Tobi van Helsinki

using ShadowRunHelper;

namespace ShadowRunHelperViewer.Platform.Droid
{
    public class DroidAppInformation : IAppInformation
    {
        public static string APP_VERSION = global::Android.App.Application.Context.PackageManager.
            GetPackageInfo(global::Android.App.Application.Context.PackageName, 0).VersionName.ToString();

        public DroidAppInformation()
        {
            string[] single = APP_VERSION.Split('.');
            if (single.Length > 0 && int.TryParse(single[0], out int ma))
            {
                Version_Major = ma;
            }
            if (single.Length > 1 && int.TryParse(single[1], out int mi))
            {
                Version_Minor = mi;
            }
            if (single.Length > 2 && int.TryParse(single[2], out int b))
            {
                Version_Build = b;
            }
            if (single.Length > 3 && int.TryParse(single[3], out int r))
            {
                Version_Revision = r;
            }
        }

        public int Version_Major { get; set; }
        public int Version_Minor { get; set; }
        public int Version_Build { get; set; }
        public int Version_Revision { get; set; }
    }
}