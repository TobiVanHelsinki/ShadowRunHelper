using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using ShadowRunHelper.Model;
using System.Collections.Generic;

namespace ShadowRunHelper.IO
{
    enum Place
    {
        Extern = 2,
        Roaming = 3,
        Local = 3,
    }

    enum SaveType
    {
        Unknown = 0,
        Manually = 1,
        Auto = 2,
        Emergency = 3
    }

    internal static class CharIO
    {
        //#####################################################################
        private static void SerializationErrorHandler(object o, Newtonsoft.Json.Serialization.ErrorEventArgs a)
        {
#if DEBUG
            //if (System.Diagnostics.Debugger.IsAttached) System.Diagnostics.Debugger.Break();
#endif
            AppModel.Instance.lstNotifications.Add(new Notification(
                CrossPlattformHelper.GetString("Notification_Error_Loader_Error1/Text") +
                "ErrorContextData: " + a.ErrorContext.Error.Data +
                "CurrentObject: " + a.CurrentObject +
                "OriginalObject: " + a.ErrorContext.OriginalObject
#if __ANDROID__
#else
                + "Path: " + a.ErrorContext.Path
#endif
                ));
            a.ErrorContext.Handled = true;
        }
        private static string Char_to_String(CharHolder SaveChar)
        {

            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.MissingMemberHandling = MissingMemberHandling.Ignore;
            //settings.NullValueHandling = NullValueHandling.Include; 
            settings.PreserveReferencesHandling = PreserveReferencesHandling.All; //war vorher objects
            settings.Error = SerializationErrorHandler;
#if __ANDROID__
            throw new NotImplementedException();
            //return JsonConvert.SerializeObject(SaveChar, null, settings);
#else
            return JsonConvert.SerializeObject(SaveChar, settings);
#endif
        }

        //#####################################################################
        private static void DeserializationErrorHandler(object o, Newtonsoft.Json.Serialization.ErrorEventArgs a)
        {
#if DEBUG
            //if (System.Diagnostics.Debugger.IsAttached) System.Diagnostics.Debugger.Break();
#endif
            AppModel.Instance.lstNotifications.Add(new Notification(
                CrossPlattformHelper.GetString("Notification_Error_Loader_Error1/Text") +
                "ErrorContextData: " + a.ErrorContext.Error.Data +
                "CurrentObject: " + a.CurrentObject +
                "OriginalObject: " + a.ErrorContext.OriginalObject
#if __ANDROID__
#else
                + "Path: " + a.ErrorContext.Path
#endif
                ));
            a.ErrorContext.Handled = true;
        }
        private static CharHolder String_to_Char(string fileContent)
        {
            CharHolder ReturnCharHolder;
            string strAppVersion = "";
            string strFileVersion = "";
            int nAppVersionPos = fileContent.IndexOf(Konstanten.STRING_APP_VERSION_NUMBER);
            if (nAppVersionPos >= 0)
            {
                strAppVersion = fileContent.Substring(nAppVersionPos + Konstanten.STRING_APP_VERSION_NUMBER.Length + Konstanten.JSON_FILE_GAP, Konstanten.CHARFILE_VERSION_1_3.Length);
            }
            int nFileVersionPos = fileContent.IndexOf(Konstanten.STRING_FILE_VERSION_NUMBER);
            if (nFileVersionPos >= 0)
            {
                strFileVersion = fileContent.Substring(nFileVersionPos + Konstanten.STRING_FILE_VERSION_NUMBER.Length + Konstanten.JSON_FILE_GAP, Konstanten.STRING_CHARFILEVERSION_LENGTH);
            }
            else // old version
            {
                strFileVersion = Konstanten.CHARFILE_VERSION_1_3;
            }
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
                    test.Error = SerializationErrorHandler;
                    test.PreserveReferencesHandling = PreserveReferencesHandling.All;
                    ReturnCharHolder = JsonConvert.DeserializeObject<CharHolder>(fileContent, test);
                    ReturnCharHolder.RefreshThingList();
                    break;
                default:
                    throw new Exception(ExceptionMessages.IO_DeserializeVersion_Error);
            }
            return ReturnCharHolder;
        }

        //#####################################################################
        internal static string GetCurrentSavePath()
        {
            if (AppSettings.Instance.ORDNERMODE)
            {
                return AppSettings.Instance.ORDNERMODE_PFAD;
            }
            else
            {
                return Konstanten.CONTAINER_CHAR;
            }

        }

        internal static Place GetCurrentSavePlace()
        {
            if (AppSettings.Instance.ORDNERMODE)
            {
                return Place.Extern;
            }
            else
            {
                return Place.Roaming;
            }
        }

        private static IGeneralIO GetIO()
        {
            return
#if __ANDROID__
                new DroidIO();
#else
                new WinIO();
#endif
        }

        //#####################################################################

        internal static async Task<CharHolder> LoadCharAtCurrentPlace(string strLoadChar)
        {
            return await LoadChar(GetCurrentSavePlace(), strLoadChar, GetCurrentSavePath());
        }

        internal static async Task<CharHolder> LoadCharAtCurrentPlaceAndThrow(string strLoadChar)
        {
            return await LoadChar(GetCurrentSavePlace(), strLoadChar, GetCurrentSavePath(), null, IO.UserDecision.ThrowError);
        }

        internal static async Task SaveCharAtCurrentPlace(CharHolder SaveChar, SaveType eSaveType = SaveType.Unknown)
        {
            await Task.Factory.StartNew(() => 
                CharIO.SaveChar(SaveChar, GetCurrentSavePlace(), SaveChar.MakeName(), GetCurrentSavePath())
            );
        }

        internal static void RemoveCharAtCurrentPlace(string strDelChar)
        {
            GetIO().Remove(strDelChar, GetCurrentSavePlace(), strDelChar, GetCurrentSavePath());
        }
        //#####################################################################

        internal static async void SaveChar(CharHolder SaveChar, Place ePlace, string strSaveName = "", string strSavePath = "", UserDecision eUD = UserDecision.AskUser, SaveType eSaveType = SaveType.Unknown)
        {
            if (SaveChar == null)
            {
                throw new ArgumentNullException("Char was Empty");
            }
            string strAdditionalName = "";
            switch (eSaveType)
            {
                case SaveType.Unknown:

                    break;
                case SaveType.Manually:
                    break;
                case SaveType.Auto:
                    break;
                case SaveType.Emergency:
                    strAdditionalName = "EmergencySave_";
                    break;
                default:
                    break;
            }
            await Task.Factory.StartNew(() =>
                GetIO().Save(Char_to_String(SaveChar), Place.Extern, strAdditionalName + strSaveName, strSavePath)
            );
        }

        internal static async Task<CharHolder> LoadChar(Place ePlace, string strSaveName = "", string strSavePath = "", List<string> FileTypes = null, UserDecision eUD = UserDecision.AskUser)
        {
            return String_to_Char(await GetIO().Load(GetCurrentSavePlace(), strSaveName, strSavePath, FileTypes, eUD));
        }
    }
}
