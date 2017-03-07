
namespace ShadowRunHelper
{
    public interface ISettings
    {
        string GetStrLastChar();
        void SetStrLastChar(string value);
        // ####################################################################
        bool GetBSaveCharOnExit();
        void SetBSaveCharOnExit(bool value);
        // ####################################################################
        bool GetBLoadCharOnStart();
        void SetBLoadCharOnStart(bool value);
        // ####################################################################
        bool GetBIsFileInProgress();
        void SetBIsFileInProgress(bool value);
        // ####################################################################
        bool GetBFolderMode();
        void SetBFolderMode(bool value);
        // ####################################################################
        string GetStrFolderModePath();
        void SetStrFolderModePath(string value);
        // ####################################################################
        bool GetBStartEditAfterAdd();
        void SetBStartEditAfterAdd(bool value);
        // ####################################################################
        bool GetBDisplayRequest();
        void SetBDisplayRequest(bool value);
    }
}
