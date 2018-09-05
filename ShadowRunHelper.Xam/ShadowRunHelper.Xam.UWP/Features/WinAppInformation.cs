using Windows.ApplicationModel;

namespace ShadowRunHelper
{
    public class WinAppInformation : IAppInformation
    {
        public int Version_Major { get => Package.Current.Id.Version.Major; }
        public int Version_Minor { get => Package.Current.Id.Version.Minor; }
        public int Version_Build { get => Package.Current.Id.Version.Build; }
        public int Version_Revision { get => Package.Current.Id.Version.Revision; }
    }
}
