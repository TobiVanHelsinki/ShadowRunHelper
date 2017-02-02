using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Pickers;

namespace ShadowRunHelper.IO
{

    internal static class GeneralIO
    {

        public static async Task<List<StorageFile>> GetListofFiles(StorageFolder CharFolder, List<string> FileTypes)
        {
            List<StorageFile> templist = new List<StorageFile>();
            try
            {
                IReadOnlyList<StorageFile> Liste = await CharFolder.GetFilesAsync();
                foreach (var item in Liste)
                {
                    if (FileTypes.Contains(item.FileType))
                    {
                        Windows.Storage.FileProperties.BasicProperties basicProperties = await item.GetBasicPropertiesAsync();
                        templist.Add(item);
                    }
                }
                return templist;
            }
            catch (Exception)
            {
                throw new ArgumentException();
            }
        }


        internal async static Task<StorageFile> GetFile(Place ePlace, string strPath, string strFileName, CreationCollisionOption eCreateOptions = CreationCollisionOption.OpenIfExists)
        {
            switch (ePlace)
            {
                case Place.Roaming:
                    StorageFolder Folder = null;
                    try // to get sub folder
                    {
                        Folder = await ApplicationData.Current.RoamingFolder.GetFolderAsync(strPath);
                    }
                    catch (System.IO.FileNotFoundException)
                    {
                        try // to create subfolder //TODO make recursive
                        {
                            Folder = await ApplicationData.Current.RoamingFolder.CreateFolderAsync(strPath);
                        }
                        catch (System.IO.FileNotFoundException)
                        {
                            throw new ArgumentException("Invalid Characters:" + strPath);
                        }
                        catch (System.UnauthorizedAccessException)
                        {
                            throw new ArgumentException("UnauthorizedAccessException:" + strPath);
                        }
                    }
                    try//to get file
                    {
                        return await Folder.GetFileAsync(strFileName);
                    }
                    catch (System.UnauthorizedAccessException)
                    {
                        throw new ArgumentException("UnauthorizedAccessException:" + strPath);
                    }
                    catch (System.ArgumentException)
                    {
                        throw new ArgumentException("Wrong Url:" + strPath);
                    }
                case Place.Extern:
                    try // to get it
                    {
                        return await StorageFile.GetFileFromPathAsync(strPath);
                    }
                    catch (Exception)
                    {
                        try // to create it
                        {
                            StorageFolder UpFolder = await StorageFolder.GetFolderFromPathAsync(strPath);
                            return await UpFolder.CreateFileAsync(strFileName, eCreateOptions);
                        }
                        catch (Exception)
                        {
                            try // to get from user
                            {
                                Folder = await FolderPicker(new List<string>(new string[] { Konstanten.DATEIENDUNG_CHAR }));
                                return await Folder.CreateFileAsync(strFileName, eCreateOptions);
                            }
                            catch (Exception)
                            {
                                throw new ArgumentException("Could Not Get Folder From Picker");
                            }
                        }
                    }
            }
            throw new ArgumentException("Wrong Enum Type:" + ePlace);
        }

        internal static async Task<StorageFile> FilePicker(List<string> lststrFileEndings)
        {
            FileOpenPicker openPicker = new FileOpenPicker();
            openPicker.SuggestedStartLocation = PickerLocationId.ComputerFolder;
            foreach (var item in lststrFileEndings)
            {
                openPicker.FileTypeFilter.Add(item);
            }

            StorageFile file = await openPicker.PickSingleFileAsync();
            Windows.Storage.AccessCache.StorageApplicationPermissions.
    FutureAccessList.AddOrReplace("PickedFolderToken", file);
            return file;
        }
        /// <summary>
        /// Returns a Folder. 
        /// Intern Folder: Param Path has to be a either "Local" or "Roaming"
        /// Extern Folder: Param Path has to be a valid Path. If it's not: first attempt is to create this file Then the Folder Picker will be displayed.
        /// </summary>
        /// <param name="ePlace"></param>
        /// <param name="eCreateOptions"></param>
        /// <param name="strPath"></param>
        /// <returns></returns>
        /// <throws>ArgumentException</throws>
        internal async static Task<StorageFolder> GetFolder(Place ePlace, string strPath, CreationCollisionOption eCreateOptions = CreationCollisionOption.OpenIfExists)
        {
            switch (ePlace)
            {
                case Place.Roaming:
                    try
                    {
                        return await ApplicationData.Current.RoamingFolder.GetFolderAsync(strPath);
                    }
                    catch (System.IO.FileNotFoundException)
                    {
                        try
                        {
                            return await ApplicationData.Current.RoamingFolder.CreateFolderAsync(strPath, eCreateOptions);
                        }
                        catch (System.IO.FileNotFoundException)
                        {
                            throw new ArgumentException("Invalid Characters:" + strPath);
                        }
                        catch (System.UnauthorizedAccessException)
                        {
                            throw new ArgumentException("UnauthorizedAccessException:" + strPath);
                        }

                    }
                    catch (System.UnauthorizedAccessException)
                    {
                        throw new ArgumentException("UnauthorizedAccessException:" + strPath);
                    }
                    catch (System.ArgumentException)
                    {
                        throw new ArgumentException("Wrong Url:" + strPath);
                    }
                case Place.Extern:
                    try // to get it
                    {
                        return await StorageFolder.GetFolderFromPathAsync(strPath);
                    }
                    catch (Exception)
                    {
                        try // to create it
                        {
                            int LastPos = strPath.LastIndexOf("//");
                            StorageFolder UpFolder = await StorageFolder.GetFolderFromPathAsync(strPath.Substring(1, LastPos));
                            return await UpFolder.CreateFolderAsync(strPath.Substring(LastPos, strPath.Length), eCreateOptions);
                        }
                        catch (Exception)
                        {
                            try // to get from user
                            {
                                return await FolderPicker();
                            }
                            catch (Exception)
                            {
                                throw new ArgumentException("Could Not Get Folder From Picker");
                            }
                        }
                    }
            }
            throw new ArgumentException("Wrong Enum Type:" + ePlace);
        }
        internal async static void Write(StorageFile File, string content)
        {
            await FileIO.WriteTextAsync(File,content);
        }
        internal async static void Remove(StorageFile File)
        {
            await File.DeleteAsync();
        }

        /// <summary>
        /// Throws things
        /// </summary>
        /// <param name="strSuggestedStartLocation"></param>
        /// <returns></returns>
        internal static async Task<StorageFolder> FolderPicker(List<string> lststrFileEndings = null)
        {
            var folderPicker = new Windows.Storage.Pickers.FolderPicker();
            folderPicker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.ComputerFolder;

            if (lststrFileEndings != null)
            {
                foreach (var item in lststrFileEndings)
                {
                    folderPicker.FileTypeFilter.Add(item);
                }
            }
            StorageFolder CharFolder = null;
            CharFolder = await folderPicker.PickSingleFolderAsync();
            Windows.Storage.AccessCache.StorageApplicationPermissions.
                FutureAccessList.AddOrReplace("PickedFolderToken", CharFolder);
            return CharFolder;
        }
    }
}
