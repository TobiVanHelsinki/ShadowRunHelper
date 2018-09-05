namespace ShadowRunHelper
{
    public interface IAppInformation
    {
        int Version_Major { get; }
        int Version_Minor { get; }
        int Version_Build { get; }
        int Version_Revision { get; }

    }
}