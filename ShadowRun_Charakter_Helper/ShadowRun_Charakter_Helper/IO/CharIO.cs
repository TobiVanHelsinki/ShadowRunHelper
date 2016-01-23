using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace ShadowRun_Charakter_Helper.IO
{
    class CharIO
    {
        static String Chars_Container_Name = "Char_Store";

        private static string Save_Char_to_JSON(Models.Char SaveChar)
        {
            return JsonConvert.SerializeObject(SaveChar);
        }

        public static void Save_JSONChar_to_Data(Models.Char SaveChar)
        {


            Windows.Storage.ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            Windows.Storage.ApplicationDataContainer container = localSettings.CreateContainer(Chars_Container_Name, Windows.Storage.ApplicationDataCreateDisposition.Always);

            localSettings.Containers[Chars_Container_Name].Values[SaveChar.ID_Char + ""] = Save_Char_to_JSON(SaveChar);
        }

        public static async void Save_JSONChar_to_IO(Models.Char SaveChar)
        {

            //Ordner Auswähler vorbereiten
            var folderPicker = new Windows.Storage.Pickers.FolderPicker();
            folderPicker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.ComputerFolder;
            folderPicker.FileTypeFilter.Add(".SRWin");
            //Ordner Auswähler rufen
            Windows.Storage.StorageFolder folder = await folderPicker.PickSingleFolderAsync();
            if (folder != null)
            {
                // Application now has read/write access to all contents in the picked folder
                // (including other sub-folder contents)
                Windows.Storage.AccessCache.StorageApplicationPermissions.
                FutureAccessList.AddOrReplace("PickedFolderToken", folder);



                //Dateiname und Datei vorbereiten
                String filename = SaveChar.Alias + "_" + SaveChar.Karma_Gesamt + "_Karma_" + SaveChar.Runs + "_Runs_" + DateTime.Now.Year + "_" + DateTime.Now.Month + "_" + DateTime.Now.Day + "_" + DateTime.Now.Hour + "_" + DateTime.Now.Minute + "_" + DateTime.Now.Second + ".SRWin";
                StorageFile Save_File = await folder.CreateFileAsync(filename, CreationCollisionOption.ReplaceExisting);
                Save_File = await folder.GetFileAsync(filename);


                //Ausgewählten Char auf schreiben
                await FileIO.WriteTextAsync(Save_File, CharIO.Save_Char_to_JSON(SaveChar));
            }


        }

        private static Models.Char Load_Char_from_JSON(string fileContent)
        {
            return JsonConvert.DeserializeObject<Models.Char>(fileContent);
        }

        public static Models.Char Load_JSONChar_from_Data(int LoadID)
        {
            Windows.Storage.ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            return Load_Char_from_JSON((string)localSettings.Containers[Chars_Container_Name].Values[LoadID + ""]);

        }

        public static async Task<Models.Char> Load_JSONChar_from_IO()
        {
            String inputString = "";
            try
            {
                inputString = await Load_JSONChar_from_IO_Async_Part();
                return Load_Char_from_JSON(inputString);
            }
            catch (Exception)
            {
                return new Models.Char();
            }
        }

        private static async Task<String> Load_JSONChar_from_IO_Async_Part()
        {
            var picker = new Windows.Storage.Pickers.FileOpenPicker();
            picker.ViewMode = Windows.Storage.Pickers.PickerViewMode.List;
            picker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.ComputerFolder;
            picker.FileTypeFilter.Add(".SRWin");

            Windows.Storage.StorageFile file = await picker.PickSingleFileAsync();
            if (file != null)
            {
                return await FileIO.ReadTextAsync(file);
            }
            return "";
           
        }

    }
}
