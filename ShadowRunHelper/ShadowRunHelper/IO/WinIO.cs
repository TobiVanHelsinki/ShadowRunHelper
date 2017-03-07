using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ShadowRunHelper.Model;
using Windows.Storage;
using Windows.Storage.Pickers;

namespace ShadowRunHelper.IO
{

    internal class WinIO : IGeneralIO
    {
        // ##############################
        public async void Save(string saveChar, Place ePlace, string strSaveName = "", string strSavePath = "")
        {
            StorageFile x = await GetFile(ePlace, strSaveName, strSavePath);
            await FileIO.WriteTextAsync(x, saveChar);
        }

        public async void Remove(string delChar, Place ePlace, string strSaveName = "", string strSavePath = "")
        {
            StorageFile x = await GetFile(ePlace, strSaveName, strSavePath);
            await x.DeleteAsync();
        }

        public async Task<string> Load(Place ePlace, string strSaveName = "", string strSavePath = "", List<string> FileTypes = null, UserDecision eUD = UserDecision.AskUser)
        {
            StorageFile x = await GetFile(ePlace, strSaveName, strSavePath, FileTypes, eUD);
            if (x == null)
            {
                x = await FilePicker();
            }
            return await FileIO.ReadTextAsync(x);
        }

        // ##############################
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
        /// <summary>
        /// Extern:
        /// Action depends on the string parameters:
        /// Path and Name are provided correctly -> File is returned
        /// Path is provided incorrectly or is null -> Try to create Folder, then ask User for Folderinput
        /// Name is provided incorrectly or is null -> Try to create File, then ask User for Fileinput
        /// Path and Name are provided incorrectly or null -> User shall input File
        /// 
        /// </summary>
        /// <param name="ePlace"></param>
        /// <param name="strFileName"></param>
        /// <param name="strPath"></param>
        /// <param name="FileTypes"></param>
        /// <returns></returns>
        internal async static Task<StorageFile> GetFile(Place ePlace, string strFileName = null, string strPath = null, List<string> FileTypes = null, UserDecision eUser = UserDecision.AskUser)
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
                        catch (Exception ex)
                        {
                            throw new Exception("GeneralIO.GetFile.Intern.GetFolder:" + strPath, ex);
                        }
                    }
                    try//to get file
                    {
                        return await Folder.CreateFileAsync(strFileName, CreationCollisionOption.OpenIfExists);
                    }
                    catch (Exception ex1)
                    {
                        throw new Exception("GeneralIO.GetFile.Intern.GetFile:" + strFileName, ex1);
                    }
                case Place.Extern:
                    try // to get it
                    {
                        if (strPath == null && strFileName == null) // both is empty, that means we need a folder and file -> both can be handeld with one action -> the filepicker
                        {
                            throw new Exception();
                        }
                        Folder = await GetFolder(Place.Extern, strPath); //get or create or get from user - folder
                        return await Folder.CreateFileAsync(strFileName, CreationCollisionOption.OpenIfExists); // get or create file
                    }
                    catch (Exception)
                    {
                        if (eUser == UserDecision.AskUser)
                        {
                            return await FilePicker(FileTypes); // get from user
                        }
                        else
                        {
                            throw;
                        }
                    }
            }
            throw new Exception("GeneralIO.GetFile.Wrong Enum Type:" + ePlace);
        }
        public static async Task<StorageFile> FilePicker(List<string> lststrFileEndings = null)
        {
            if (lststrFileEndings == null)
            {
                lststrFileEndings = new List<string>(new string[] {Konstanten.DATEIENDUNG_CHAR,Konstanten.DATEIENDUNG_CSV});
            }
            try 
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
            catch (Exception ex)
            {
                throw new ArgumentException("GeneralIO.FilePicker", ex);
            }
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
        internal async static Task<StorageFolder> GetFolder(Place ePlace, string strPath = null, UserDecision eUser = UserDecision.AskUser)
        {
            switch (ePlace)
            {
                case Place.Roaming:
                    try
                    {
                        return await ApplicationData.Current.RoamingFolder.CreateFolderAsync(strPath, CreationCollisionOption.OpenIfExists);
                    }
                    catch (Exception ex)
                    {
                        throw new ArgumentException("GetFolder.Roaming", ex);
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
                            return await UpFolder.CreateFolderAsync(strPath.Substring(LastPos, strPath.Length), CreationCollisionOption.OpenIfExists);
                        }
                        catch (Exception)
                        {
                            if (eUser == UserDecision.AskUser)
                            {
                                return await FolderPicker();
                            }
                            else
                            {
                                throw;
                            }
                        }
                    }
            }
            throw new ArgumentException("Wrong Enum Type:" + ePlace);
        }
        /// <summary>
        /// Throws things
        /// </summary>
        /// <param name="strSuggestedStartLocation"></param>
        /// <returns></returns>
        public static async Task<StorageFolder> FolderPicker(List<string> lststrFileEndings = null)
        {

            try
            {
                var folderPicker = new Windows.Storage.Pickers.FolderPicker();
                folderPicker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.ComputerFolder;

                if (lststrFileEndings == null)
                {
                    lststrFileEndings = new List<string>(new string[] { Konstanten.DATEIENDUNG_CHAR, Konstanten.DATEIENDUNG_CSV });
                }
                foreach (var item in lststrFileEndings)
                {
                    folderPicker.FileTypeFilter.Add(item);
                }

                StorageFolder Folder = null;
                Folder = await folderPicker.PickSingleFolderAsync();
                Windows.Storage.AccessCache.StorageApplicationPermissions.
                    FutureAccessList.AddOrReplace("PickedFolderToken", Folder);
                return Folder;

            }
            catch (Exception ex)
            {
                throw new ArgumentException("GeneralIO.Folderpicker", ex);
            }
        }

    }
}
