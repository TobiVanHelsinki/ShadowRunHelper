using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace ShadowRunHelper.IO
{
    public enum Place
    {
        NotDefined = 0,
        Extern = 2,
        Roaming = 3,
        Local = 4,
        Assets = 5,
        Temp = 6
    }
    
    public interface IPlatformIO
    {
        Task<IEnumerable<ExtendetFileInfo>> GetFiles(DirectoryInfo Info, IEnumerable<string> FileTypes = null);

        #region Basic File Operations

        Task RemoveFile(FileInfo Info);
        Task<FileInfo> Rename(FileInfo SourceFile, string NewName);
        Task<FileInfo> CopyTo(FileInfo Source, FileInfo Target);
        Task MoveTo(FileInfo Source, FileInfo Target);

        #endregion
        #region Multiple File Operations

        Task MoveAllFiles(DirectoryInfo Source, DirectoryInfo Target, IEnumerable<string> FileTypes = null);
        Task CopyAllFiles(DirectoryInfo Source, DirectoryInfo Target, IEnumerable<string> FileTypes = null);


        #endregion
        #region File Content
        Task SaveFileContent(string saveChar, FileInfo Info);
        Task<string> LoadFileContent(FileInfo Info);
        #endregion
        #region Helper
        Task<DirectoryInfo> CreateFolder(DirectoryInfo Info);
        Task<bool> HasAccess(FileInfo Path);
        Task<bool> HasAccess(DirectoryInfo Path);

        #endregion
        #region Picker
        Task<DirectoryInfo> PickFolder(string Token = null);
        Task<FileInfo> PickFile(IEnumerable<string> lststrFileEndings, string Token = null);
        #endregion
        #region Other
        Task<string> GetCompleteInternPath(Place place);
        Task<bool> OpenFolder(DirectoryInfo Info);
        #endregion
    }
}
