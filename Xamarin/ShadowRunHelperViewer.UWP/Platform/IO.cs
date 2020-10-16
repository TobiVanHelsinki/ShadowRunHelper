//Author: Tobi van Helsinki
using ShadowRunHelper.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TLIB;
using Windows.Storage;
using Windows.Storage.AccessCache;
using Windows.Storage.Pickers;
using Windows.System;

namespace ShadowRunHelperViewer.Platform.UWP
{
    public static class CustomFileInfoExtension
    {
        public static ExtendetFileInfo ToFileInfo(this StorageFile d)
        {
            Windows.Foundation.IAsyncOperation<Windows.Storage.FileProperties.BasicProperties> T = d.GetBasicPropertiesAsync();
            T.AsTask().Wait();
            Windows.Storage.FileProperties.BasicProperties att = T.GetResults();
            return new ExtendetFileInfo(d.Path, att.DateModified.DateTime, (long)att.Size);
        }
    }

    public class IO : StandardIO, IPlatformIO
    {
        public override async Task<IEnumerable<ExtendetFileInfo>> GetFiles(DirectoryInfo Info, IEnumerable<string> FileTypes = null)
        {
            StorageFolder folderhandle = await GetFolder(Info);
            return (await folderhandle.GetFilesAsync()).Where(x => FileTypes != null ? FileTypes.Contains(x.FileType) : true).Select(x => x.ToFileInfo());
        }

        #region Basic File Operations

        public new async Task RemoveFile(FileInfo Info)
        {
            StorageFile filehandle = await GetFile(Info);
            await filehandle.DeleteAsync();
        }

        public override async Task<FileInfo> Rename(FileInfo Source, string NewName)
        {
            StorageFile filehandle = await GetFile(Source);
            await filehandle.RenameAsync(NewName);
            return new FileInfo(filehandle.Path);
        }

        public override async Task<FileInfo> CopyTo(FileInfo Source, FileInfo Target)
        {
            StorageFile sourcehandle = await GetFile(Source);
            StorageFolder targethandle = await GetFolder(Target.Directory);
            StorageFile newfile = await sourcehandle.CopyAsync(targethandle, Target.Name, NameCollisionOption.GenerateUniqueName);
            return new FileInfo(newfile.Path);
        }

        public override async Task MoveTo(FileInfo Source, FileInfo Target)
        {
            StorageFile sourcehandle = await GetFile(Source);
            StorageFolder targethandle = await GetFolder(Target.Directory);
            await sourcehandle.MoveAsync(targethandle, Target.Name, NameCollisionOption.GenerateUniqueName);
        }

        #endregion Basic File Operations

        #region File Content

        public override async Task SaveFileContent(string saveChar, FileInfo Info)
        {
            StorageFile filehandle = await GetOrCreateFile(Info);
            await FileIO.WriteTextAsync(filehandle, saveChar);
        }

        public override async Task<string> LoadFileContent(FileInfo Info)
        {
            StorageFile filehandle = await GetFile(Info);
            return await FileIO.ReadTextAsync(filehandle);
        }

        #endregion File Content

        #region Helper

        private async Task<StorageFile> GetFile(FileInfo Info)
        {
            return await StorageFile.GetFileFromPathAsync(Info.FullName);
        }

        private async Task<StorageFile> GetOrCreateFile(FileInfo Info)
        {
            try
            {
                return await GetFile(Info);
            }
            catch (Exception)
            {
                StorageFolder folderhandle = await StorageFolder.GetFolderFromPathAsync(Info.Directory.FullName);
                return await folderhandle.CreateFileAsync(Info.Name, CreationCollisionOption.OpenIfExists);
            }
        }

        private async Task<StorageFolder> GetFolder(DirectoryInfo Info)
        {
            try
            {
                return await StorageFolder.GetFolderFromPathAsync(Info.FullName);
            }
            catch (Exception ex)
            {
                Log.Write("Could not GetFolder", ex, logType: LogType.Error);
                throw;
            }
        }

        private async Task<StorageFolder> GetOrCreateFolder(DirectoryInfo Info)
        {
            try
            {
                return await GetFolder(Info);
            }
            catch (Exception)
            {
                StorageFolder folderhandle = await StorageFolder.GetFolderFromPathAsync(Info.Parent.FullName);
                return await folderhandle.CreateFolderAsync(Info.Name, CreationCollisionOption.OpenIfExists);
            }
        }

        public async Task<DirectoryInfo> CreateFolder(DirectoryInfo Info)
        {
            return new DirectoryInfo((await GetOrCreateFolder(Info)).Path);
        }

        public async Task<bool> HasAccess(FileInfo Path)
        {
            try
            {
                return Path.Exists;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> HasAccess(DirectoryInfo Path)
        {
            try
            {
                StorageFolder folderhandle = await StorageFolder.GetFolderFromPathAsync(Path.FullName);
                return Path.Exists;
            }
            catch (Exception)
            {
                return false;
            }
        }
        #endregion Helper

        #region Picker

        public async Task<FileInfo> PickFile(IEnumerable<string> lststrFileEndings, string Token = null)
        {
            FileOpenPicker openPicker = new FileOpenPicker()
            {
                SuggestedStartLocation = PickerLocationId.ComputerFolder
            };
            foreach (string item in lststrFileEndings)
            {
                openPicker.FileTypeFilter.Add(item);
            }

            StorageFile file = await openPicker.PickSingleFileAsync();
            if (file == null)
            {
                throw new IsOKException();
            }
            AddToFutureRequestList(Token, file);
            return new FileInfo(file.Path);
        }

        private static void AddToFutureRequestList(string Token, IStorageItem item)
        {
            if (!string.IsNullOrEmpty(Token))
            {
                try
                {
                    StorageApplicationPermissions.FutureAccessList.AddOrReplace(Token, item);
                }
                catch (Exception ex)
                {
                    if (System.Diagnostics.Debugger.IsAttached)
                    {
                        System.Diagnostics.Debugger.Break();
                    }

                    Log.Write("Could not AddToFutureRequestList: " + Token, ex, logType: LogType.Error);
                }
            }
        }

        /// <summary>
        /// Throws things
        /// </summary>
        /// <param name="strSuggestedStartLocation"></param>
        /// <returns></returns>
        public async Task<DirectoryInfo> PickFolder(string Token = null)
        {
            FolderPicker folderPicker = new FolderPicker()
            {
                SuggestedStartLocation = PickerLocationId.ComputerFolder,
                ViewMode = PickerViewMode.List
            };
            folderPicker.FileTypeFilter.Add(".");
            StorageFolder dir = await folderPicker.PickSingleFolderAsync();
            if (dir == null)
            {
                throw new IsOKException();
            }
            AddToFutureRequestList(Token, dir);
            return new DirectoryInfo(dir.Path);
        }

        #endregion Picker

        #region Other

        public async Task<string> GetCompleteInternPath(Place place)
        {
            switch (place)
            {
                case Place.Temp:
                    return ApplicationData.Current.TemporaryFolder.Path + Path.DirectorySeparatorChar;
                case Place.Local:
                    return ApplicationData.Current.LocalFolder.Path + Path.DirectorySeparatorChar;
                case Place.Roaming:
                    return ApplicationData.Current.RoamingFolder.Path + Path.DirectorySeparatorChar;
                case Place.Assets:
                    return Windows.ApplicationModel.Package.Current.InstalledLocation.Path + Path.DirectorySeparatorChar;
                default:
                    throw new NotImplementedException();
            }
        }

        public async Task<bool> OpenFolder(DirectoryInfo Info)
        {
            return await Launcher.LaunchFolderAsync(await StorageFolder.GetFolderFromPathAsync(Info.FullName));
        }
        #endregion Other
    }
}