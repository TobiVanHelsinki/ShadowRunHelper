
using System.Linq;

namespace ShadowRunHelper
{
    public class DroidAppInformation : IAppInformation
    {
        public static string APP_VERSION = global::Android.App.Application.Context.PackageManager.
            GetPackageInfo(global::Android.App.Application.Context.PackageName, 0).VersionName.ToString();

        public DroidAppInformation()
        {
            var single = APP_VERSION.Split('.');
            if (single.Length > 0 && int.TryParse(single[0], out var ma))
            {
                Version_Major = ma;
            }
            if (single.Length > 1 && int.TryParse(single[1], out var mi))
            {
                Version_Minor = mi;
            }
            if (single.Length > 2 && int.TryParse(single[2], out var b))
            {
                Version_Build = b;
            }
            if (single.Length > 3 && int.TryParse(single[3], out var r))
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
