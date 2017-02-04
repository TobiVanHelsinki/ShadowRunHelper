using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using Windows.Storage;
using ShadowRunHelper.Model;
using static ShadowRunHelper.IO.GeneralIO;
namespace ShadowRunHelper.IO
{
    enum Place
    {
        //Intern = 1,
        Extern = 2,
        Roaming = 3,
        Local = 3,
    }

    internal static class CharIO
    {
        internal async static Task<string> GetCurrentSavePath()
        {
            if (Optionen.bORDNERMODE)
            {
                if (Optionen.strORDNERMODE_PFAD == "")
                {
                    Optionen.strORDNERMODE_PFAD = (await FolderPicker()).Path;
                }
                return Optionen.strORDNERMODE_PFAD;
            }
            else
            {
                return Konstanten.CONTAINER_CHAR;
            }

        }

        internal static Place GetCurrentSavePlace()
        {
            if (Optionen.bORDNERMODE)
            {
                return Place.Extern;
            }
            else
            {
                return Place.Roaming;
            }
        }

        internal static async Task<CharHolder> LoadCharAtCurrentPlace(string strLoadChar)
        {
            StorageFile file = await GetFile(GetCurrentSavePlace(), strLoadChar, await GetCurrentSavePath());
            return await CharIO.LoadCharFromFile(file);
        }

        internal static async Task SaveCharAtCurrentPlace(CharHolder SaveChar)
        {
            if (SaveChar == null)
            {
                throw new ArgumentNullException("Char was Empty");
            }
            StorageFile Save_File = await GetFile(GetCurrentSavePlace(), SaveChar.MakeName(), await GetCurrentSavePath());
            CharIO.SaveCharToFile(SaveChar, Save_File);
        }

        internal static async Task RemoveCharAtCurrentPlace(string strDelChar)
        {
            StorageFile toDelFile = await GetFile(GetCurrentSavePlace(), strDelChar, await GetCurrentSavePath());
            Remove(toDelFile);
        }

        private static string Char_to_String(CharHolder SaveChar)
        {

            JsonSerializerSettings test = new JsonSerializerSettings();
            test.PreserveReferencesHandling = PreserveReferencesHandling.All; //war vorher objects
            test.Error = ErrorHandler;
            return JsonConvert.SerializeObject(SaveChar, test);
        }

        private static void ErrorHandler(object o, Newtonsoft.Json.Serialization.ErrorEventArgs a)
        {
            //((Newtonsoft.Json.JsonSerializer)o).
            a.ErrorContext.Handled = true;
            //todo notify user
            //a.ErrorContext.Error.Data;
        }

        private static CharHolder String_to_Char(string fileContent)
        {
            CharHolder ReturnCharHolder;
            string strAppVersion = "";
            string strFileVersion = "";
            int nAppVersionPos = fileContent.IndexOf(Konstanten.STRING_APP_VERSION_NUMBER);
            if (nAppVersionPos >=0)
            {
                strAppVersion = fileContent.Substring(nAppVersionPos + Konstanten.STRING_APP_VERSION_NUMBER.Length + Konstanten.JSON_FILE_GAP, Konstanten.CHARFILE_VERSION_1_3.Length);
            }
            int nFileVersionPos = fileContent.IndexOf(Konstanten.STRING_FILE_VERSION_NUMBER);
            if (nFileVersionPos >=0)
            {
                strFileVersion = fileContent.Substring(nFileVersionPos + Konstanten.STRING_FILE_VERSION_NUMBER.Length + Konstanten.JSON_FILE_GAP, Konstanten.STRING_CHARFILEVERSION_LENGTH);
            }
            else // old version
            {
                strFileVersion = Konstanten.CHARFILE_VERSION_1_3;
            }
            try
            {
                switch (strFileVersion)
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

        public static async void SaveCharToFile(CharHolder SaveChar, StorageFile file)
        {
            //Ausgewählten Char auf Plattensubsystem schreiben
            await FileIO.WriteTextAsync(file, CharIO.Char_to_String(SaveChar));
        }

        public static async Task<CharHolder> LoadCharFromFile(StorageFile file)
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
    }
}
