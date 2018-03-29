using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ShadowRunHelper.CharModel;
using ShadowRunHelper.Model;
using Shared;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TLIB_UWPFRAME;
using TLIB_UWPFRAME.IO;

namespace ShadowRunHelper.IO
{
    class UnknownThingConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(Thing);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            //http://skrift.io/articles/archive/bulletproof-interface-deserialization-in-jsonnet/
            var jsonObject = JObject.Load(reader);
            if (jsonObject.TryGetValue("$ref", out JToken isRef))
            {
                object o = serializer.Deserialize(jsonObject.CreateReader());
                return o;
            }
            JToken ThingTypeValue = jsonObject.GetValue("ThingType");
            var IntThingType = ThingTypeValue.Value<Int64>();
            Type Should = TypeHelper.ThingDefToType((ThingDefs)IntThingType);
            Thing target = (Thing)Activator.CreateInstance(Should);
            serializer.Populate(jsonObject.CreateReader(), target);
#if DEBUG
            if (target == null && System.Diagnostics.Debugger.IsAttached)
            {
                System.Diagnostics.Debugger.Break();
            }
#endif
            return target;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }

    internal class CharHolderIO : SharedIO<CharHolder>
    {
        internal static CharHolder ConvertWithRightVersion(string strFileVersion, string strAppVersion, string fileContent)
        {
            DateTime StartTime = DateTime.Now;
            CharHolder ReturnCharHolder;
            switch (strFileVersion)
            {
                case Constants.CHARFILE_VERSION_1_3:
                    AppModel.Instance.NewNotification(CrossPlatformHelper.GetString("Notification_Info_NotSupportedVersion"));
                    throw new IO_FileVersion();
                case Constants.CHARFILE_VERSION_1_5:
                    JsonSerializerSettings settings = new JsonSerializerSettings()
                    {
                        Error = ErrorHandler,
                        PreserveReferencesHandling = PreserveReferencesHandling.All
                    };
                    settings.Converters.Add(new UnknownThingConverter());
                    ReturnCharHolder = JsonConvert.DeserializeObject<CharHolder>(fileContent, settings);
                    ReturnCharHolder.HasChanges = false;
                    break;
                default:
                    throw new IO_FileVersion();
            }
            ReturnCharHolder.AfterLoad();
            DateTime StopTime = DateTime.Now;
            TimeSpan Time = StopTime - StartTime;
            return ReturnCharHolder;
        }

        static string RefactorJSONString(string Input)
        {
            List<(string old, string @new)> replacements = new List<(string old, string @new)>
            {
                //("\"CTRLAdeptenkraft_KomplexeForm\"", "\"CTRLAdeptenkraft\"")
            };
            foreach (var (old, @new) in replacements)
            {
                Input.Replace(old, @new);
            }
            return Input;
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
