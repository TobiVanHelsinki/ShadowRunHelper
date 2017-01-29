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
    class CharIO
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

        private static CharHolder JSON_to_Char(string fileContent)
        {
            CharHolder tempChar;
            try
            {
                JsonSerializerSettings test = new JsonSerializerSettings();
                test.Error = ErrorHandler;
                test.PreserveReferencesHandling = PreserveReferencesHandling.All;
                tempChar = JsonConvert.DeserializeObject<CharHolder>(fileContent, test);
                tempChar.RefreshThingList();
            }
            catch (Exception)
            {
                tempChar = new CharHolder();
                throw new Exception(ExceptionMessages.IO_Deserialize_Error);
            }
            return tempChar;
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
                        templist.Add(new CharSummory(item.Name, "", basicProperties.DateModified));
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
                return JSON_to_Char(inputString);
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
