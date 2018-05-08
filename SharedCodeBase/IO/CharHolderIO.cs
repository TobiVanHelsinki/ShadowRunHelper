using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ShadowRunHelper.CharModel;
using ShadowRunHelper.Model;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TLIB;
using TAPPLICATION;
using TAPPLICATION.IO;
using TAMARIN.IO;

namespace ShadowRunHelper.IO
{
    class UnknownThingConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(Thing);
        }
#if DEBUG
            static int c = 0;
            static TAPPLICATION.Model.Notification n = new TAPPLICATION.Model.Notification("");
#endif

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
#if DEBUG
            if (!AppModel.Instance.lstNotifications.Contains(n))
            {
                AppModel.Instance.lstNotifications.Add(n);
            }
            n.Message = "Count Custom JSON Readings: " + c;
            c++;
#endif
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
        internal static CharHolder ConvertWithRightVersion(string strAppVersion, string strFileVersion, string fileContent)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings()
            {
                Error = ErrorHandler,
                PreserveReferencesHandling = PreserveReferencesHandling.All
            };
            settings.Converters.Add(new UnknownThingConverter());
            CharHolder ReturnCharHolder;

            switch (strFileVersion)
            {
                case Constants.CHARFILE_VERSION_1_3:
                    AppModel.Instance.NewNotification(StringHelper.GetString("Notification_Info_NotSupportedVersion"), false);
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
                //ModelRefactoring Linked List Stuff
                ("\"strProperty\"", "\"PropertyID\""),
    };
                    fileContent = RefactorJSONString(fileContent, replacements);
                    ReturnCharHolder = JsonConvert.DeserializeObject<CharHolder>(fileContent, settings);
                    foreach (var item in ReturnCharHolder.CTRLHandlung.Data)
                    {
                        item.Wert = 0;
                        item.Grenze = 0;
                        item.Gegen = 0;
                    }
                    ReturnCharHolder.HasChanges = true;
                    AppModel.Instance.NewNotification(StringHelper.GetString("Notification_Info_UpgradedChar_1_5_to_1_6"), false);
                    break;
                case Constants.CHARFILE_VERSION_1_6:
                    ReturnCharHolder = JsonConvert.DeserializeObject<CharHolder>(fileContent, settings);
                    ReturnCharHolder.HasChanges = false;
                    break;
                default:
                    throw new IO_FileVersion();
            }
            ReturnCharHolder.AfterLoad();
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
            string pap = CurrentIO.GetCompleteInternPath(Place.Assets);

            switch (chartype)
            {
                case PreSavedChar.ExampleChar:
                    await CopyFileToCurrentLocation(
                        pap + @"Assets\Example\",
                        StringHelper.GetSimpleCountryCode(Constants.AVAILIBLE_EXAMPLE_LANGUAGES, Constants.DEFAULT_EXAMPLE_LANGUAGE) + Constants.DATEIENDUNG_CHAR,
                        StringHelper.GetString("ExampleChar") + Constants.DATEIENDUNG_CHAR);
                    break;
                case PreSavedChar.PreDBChar:
                    break;
                default:
                    break;
            }
        }
        public static async Task CopyFileToCurrentLocation(string oldlocation, string oldname, string newname)
        {
            var TargetFileClass = new FileInfoClass() { Filename = newname, Filepath = GetCurrentSavePath(), Fileplace = GetCurrentSavePlace(), FolderToken = SharedConstants.ACCESSTOKEN_FOLDERMODE };
            var SourceFileClass = new FileInfoClass() { Filename = oldname, Filepath = oldlocation, Fileplace = Place.Assets };
            await CurrentIO.Copy(TargetFileClass, SourceFileClass);
        }
    }
}
