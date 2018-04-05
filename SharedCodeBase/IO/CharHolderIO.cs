using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ShadowRunHelper.CharModel;
using ShadowRunHelper.Model;
using Shared;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TLIB;
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
            JsonSerializerSettings settings = new JsonSerializerSettings()
            {
                Error = ErrorHandler,
                PreserveReferencesHandling = PreserveReferencesHandling.All
            };
            settings.Converters.Add(new UnknownThingConverter());
            CharHolder ReturnCharHolder;

            DateTime StartTime = DateTime.Now;
            switch (strFileVersion)
            {
                case Constants.CHARFILE_VERSION_1_3:
                    AppModel.Instance.NewNotification(StringHelper.GetString("Notification_Info_NotSupportedVersion"));
                    throw new IO_FileVersion();
                case Constants.CHARFILE_VERSION_1_5:
                    List<(string old, string @new)> replacements = new List<(string old, string @new)>
            {
                //Adept Stuff Refactoring
                ("\"CTRLAdeptenkraft_KomplexeForm\"", "\"CTRLAdeptenkraft\""),
                ("\"CTRLFoki_Widgets\"", "\"CTRLFoki\""),
                ("\"CTRLGeist_Sprite\"", "\"CTRLGeist\""),
                ("\"CTRLStroemung_Wandlung\"", "\"CTRLStroemung\""),
                ("\"CTRLTradition_Initiation\"", "\"CTRLTradition\""),
                //Linked Thing Stuff
                ("\"PoolZusammensetzung\"", "\"LinkedThings\""),
                ("\"Pool\"", "\"WertCalced\""),
                //ModelRefactoring Thing Stuff
                ("\"PB\"", "\"DK\""),
                ("\"Rueckstoss\"", "\"RK\""),
                ("\"WertZusammensetzung\"", "\"LinkedThings\""),
            };
                    fileContent = RefactorJSONString(fileContent, replacements);
                    ReturnCharHolder = JsonConvert.DeserializeObject<CharHolder>(fileContent, settings);
                    foreach (var item in ReturnCharHolder.CTRLHandlung.Data)
                    {
                        item.Wert = 0;
                    }
                    ReturnCharHolder.HasChanges = false;
                    AppModel.Instance.NewNotification(StringHelper.GetString("Notification_Info_UpgradedChar_1_5_to_1_6"));
                    break;
                case Constants.CHARFILE_VERSION_1_6:
                test:
                    ReturnCharHolder = JsonConvert.DeserializeObject<CharHolder>(fileContent, settings);
                    ReturnCharHolder.HasChanges = false;
                    break;
                default:
                    goto test;
                    throw new IO_FileVersion();
            }
            ReturnCharHolder.AfterLoad();
            DateTime StopTime = DateTime.Now;
            TimeSpan Time = StopTime - StartTime;
            return ReturnCharHolder;
        }

        static string RefactorJSONString(string Input, List<(string old, string @new)> replacements)
        {
            string Ret = Input;
            foreach (var (old, @new) in replacements)
            {
                Ret = Ret.Replace(old, @new);
            }
            return Ret;
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
                    await CopyFileToCurrentLocation(StringHelper.GetPrefix(StringHelper.PrefixType.AppPackageData) + "Assets/Example/", StringHelper.GetSimpleCountryCode(Constants.AVAILIBLE_EXAMPLE_LANGUAGES, Constants.DEFAULT_EXAMPLE_LANGUAGE)+ Constants.DATEIENDUNG_CHAR, StringHelper.GetString("ExampleChar")+Constants.DATEIENDUNG_CHAR);
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
