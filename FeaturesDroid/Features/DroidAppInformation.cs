
namespace ShadowRunHelper
{
    public class DroidAppInformation : IAppInformation
    {
        public static string APP_VERSION = global::Android.App.Application.Context.PackageManager.
            GetPackageInfo(global::Android.App.Application.Context.PackageName, 0).VersionName.ToString();
        public string MyMethod()
        {
            return APP_VERSION;
        }
        public int Version_Major { get => int.Parse(MyMethod()); }
        public int Version_Minor { get => int.Parse(MyMethod()); }
        public int Version_Build { get => int.Parse(MyMethod()); }
        public int Version_Revision { get => int.Parse(MyMethod()); }
    }
}
