using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using ShadowRunHelper.Model;
using System.Collections.ObjectModel;

namespace ShadowRunHelper.IO
{
    internal static class CharIO
    {
        private static string Char_to_JSON(CharHolder SaveChar)
        {

            JsonSerializerSettings test = new JsonSerializerSettings();
            test.PreserveReferencesHandling = PreserveReferencesHandling.All; //war vorher objects
            test.Error = ErrorHandler;
            return JsonConvert.SerializeObject(SaveChar, test);
        }

        private static void ErrorHandler(object o, Newtonsoft.Json.Serialization.ErrorEventArgs a)
        {
            //((Newtonsoft.Json.JsonSerializer)o).
            //a.ErrorContext.Handled = true;
            //a.ErrorContext.Error.Data;
        }

        private static CharHolder String_to_Char(string fileContent)
        {
            CharHolder ReturnCharHolder;
            string strVersion = "";
            {//check if it is old version 1.3
                var Length = fileContent.IndexOf(Konstanten.STRING_APP_VERSION_NUMBER);
                if (fileContent.Substring(Length+ Konstanten.STRING_APP_VERSION_NUMBER.Length+ Konstanten.JSON_FILE_GAP, Konstanten.CHARFILE_VERSION_1_3.Length) == Konstanten.CHARFILE_VERSION_1_3)
                {
                    strVersion = Konstanten.CHARFILE_VERSION_1_3;
                }
            }
            if (strVersion.Length == 0) // get version number
            {
                var Length = fileContent.IndexOf(Konstanten.STRING_FILE_VERSION_NUMBER);
                strVersion = fileContent.Substring(Length + Konstanten.STRING_FILE_VERSION_NUMBER.Length+ Konstanten.JSON_FILE_GAP, Konstanten.STRING_CHARFILEVERSION_LENGTH);
            }
            try
            {
                switch (strVersion)
                {
                    case Konstanten.CHARFILE_VERSION_1_3:
                        ShadowRunHelper1_3.Controller.CharHolder CH1_3 = new ShadowRunHelper1_3.Controller.CharHolder();
                        CH1_3 = ShadowRunHelper1_3.IO.CharIO.JSON_to_Char(fileContent);
                        ReturnCharHolder = OldConverter.ConvertVersion1_3to1_5(CH1_3);
                        GC.Collect();
                        break;
                    case Konstanten.CHARFILE_VERSION_1_5:
                        JsonSerializerSettings test = new JsonSerializerSettings();
                        test.Error = ErrorHandler;
                        test.PreserveReferencesHandling = PreserveReferencesHandling.All;
                        ReturnCharHolder = JsonConvert.DeserializeObject<CharHolder>(fileContent, test);
                        ReturnCharHolder.RefreshThingList();
                        break;
                    default:
                        throw new Exception(ExceptionMessages.IO_DeserializeVersion_Error);
                }
            }
            catch (Exception)
            {
                throw new Exception(ExceptionMessages.IO_Deserialize_Error);
            }
            return ReturnCharHolder;
        }

        public static async Task<ObservableCollection<CharSummory>> getListofChars(StorageFolder CharFolder)
        {
            ObservableCollection<CharSummory> templist = new ObservableCollection<CharSummory>();
            try
            {
                IReadOnlyList<StorageFile> Liste = await CharFolder.GetFilesAsync();
                foreach (var item in Liste)
                {
                    if (item.FileType == Konstanten.DATEIENDUNG_CHAR)
                    {
                        Windows.Storage.FileProperties.BasicProperties basicProperties = await item.GetBasicPropertiesAsync();
                        templist.Add(new CharSummory(item.Name, basicProperties.DateModified));
                    }
                }
            }
            catch (Exception)
            {
            }
            return templist;
        }

        public static async void Speichern(CharHolder SaveChar, StorageFile file)
        {
            //Ausgewählten Char auf Plattensubsystem schreiben
            await FileIO.WriteTextAsync(file, CharIO.Char_to_JSON(SaveChar));
        }

        public static async Task<CharHolder> Laden(StorageFile file)
        {
            String inputString = "";
            try
            {
                inputString = await FileIO.ReadTextAsync(file);
                return String_to_Char(inputString);
            }
            catch (Exception)
            {
                throw new Exception("Konnte nicht aus Datei lesen und oder laden");
            }
        }

        public static async void Löschen(StorageFile toDelFile)
        {
            try
            {
                await toDelFile.DeleteAsync();
            }
            catch (Exception)
            {
                
            }
           
        }
    }
}
