using Newtonsoft.Json;
using ShadowRunHelper.Model;
using Shared;
using System;
using System.Threading.Tasks;
using TLIB_UWPFRAME;
using TLIB_UWPFRAME.IO;

namespace ShadowRunHelper.IO
{
    internal class CharHolderIO : SharedIO<CharHolder>
    {
        internal static CharHolder ConvertWithRightVersion(string strFileVersion, string strAppVersion, string fileContent)
        {
            CharHolder ReturnCharHolder;
            switch (strFileVersion)
            {
                case Constants.CHARFILE_VERSION_1_3:
                    ShadowRunHelper1_3.Controller.CharHolder CH1_3 = ShadowRunHelper1_3.IO.CharIO.JSON_to_Char(fileContent);
                    ReturnCharHolder = VersionConverter.ConvertVersion1_3to1_5(CH1_3);
                    AppModel.Instance.NewNotification(CrossPlatformHelper.GetString("Notification_Info_ConvertFromPrevious"));
                    GC.Collect();
                    break;
                case Constants.CHARFILE_VERSION_1_5:
                    JsonSerializerSettings test = new JsonSerializerSettings()
                    {
                        Error = SerializationErrorHandler,
                        PreserveReferencesHandling = PreserveReferencesHandling.All
                    };
                    ReturnCharHolder = JsonConvert.DeserializeObject<CharHolder>(fileContent, test);
                    break;
                default:
                    throw new IO_FileVersion();
            }
            ReturnCharHolder.Repair();
            ReturnCharHolder.RefreshListeners();
            return ReturnCharHolder;
        }
        public enum PreSavedChar
        {
            ExampleChar = 1,
            PreDBChar = 2,
        }
        public static async Task CopyPreSavedCharToCurrentLocation(PreSavedChar chartype)
        {
            switch (chartype)
            {
                case PreSavedChar.ExampleChar:
                    await CopyFileToCurrentLocation(CrossPlatformHelper.GetPrefix(CrossPlatformHelper.PrefixType.AppPackageData) + "Assets/Example/", CrossPlatformHelper.GetSimpleCountryCode(Constants.AVAILIBLE_EXAMPLE_LANGUAGES, Constants.DEFAULT_EXAMPLE_LANGUAGE)+ Constants.DATEIENDUNG_CHAR, CrossPlatformHelper.GetString("ExampleChar")+Constants.DATEIENDUNG_CHAR);
                    break;
                case PreSavedChar.PreDBChar:
                    break;
                default:
                    break;
            }
        }
        public static async Task CopyFileToCurrentLocation(string path, string name, string newname)
        {
            var TargetFileClass = new FileInfoClass() { Filepath = GetCurrentSavePath(), Fileplace = GetCurrentSavePlace(), FolderToken = SharedConstants.ACCESSTOKEN_FOLDERMODE};
            var SourceFileClass = new FileInfoClass() { Filename = name, Filepath = path, Fileplace = Place.Assets};
            await GetIO().Copy(TargetFileClass, SourceFileClass, newname);
        }
    }
}
