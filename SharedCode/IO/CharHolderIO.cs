﻿///Author: Tobi van Helsinki

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ShadowRunHelper.CharModel;
using ShadowRunHelper.Model;
using SharedCode.Properties;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Threading.Tasks;
using TAPPLICATION;
using TAPPLICATION.IO;
using TLIB;

namespace ShadowRunHelper.IO
{
    internal class UnknownThingConverter : JsonConverter
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
            JToken ThingTypeValue = jsonObject.GetValue(nameof(Thing.ThingType));
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

    internal class Version1_7To1_8ConnectedThingsConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return typeof(Thing).IsAssignableFrom(objectType);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var jsonObject = JObject.Load(reader);
            if (jsonObject.TryGetValue("$ref", out JToken isRef))
            {
                object o = serializer.Deserialize(jsonObject.CreateReader());
                return o;
            }
            JToken ThingTypeValue = jsonObject.GetValue(nameof(Thing.ThingType));
            var IntThingType = ThingTypeValue.Value<Int64>();
            Type Should = TypeHelper.ThingDefToType((ThingDefs)IntThingType);
            Thing target = (Thing)Activator.CreateInstance(Should);
            serializer.Populate(jsonObject.CreateReader(), target);

            //if (target.LinkedThings.Any())
            //{
            //    if (System.Diagnostics.Debugger.IsAttached) System.Diagnostics.Debugger.Break();
            //}
            target.Value.BaseValue = target.Wert;
            target.Value.Connected.Clear();
            target.Value.Connected.AddRange(target.LinkedThings.Select(x => x.Object.Value)); //TODO nicht einfach nur den value nehmen, es könnte ja auch ein anderes property sein.
            //TODO Daher also erstmal alle properties, die man auswähöen sollen kann als CharCalcProp machen
            //TODO Danach anhand von x.Property (string) eine auswahl treffen.
            target.Wert = 0;
            target.LinkedThings.Clear();
            target.LinkedThings.OnCollectionChangedCall(null);
            if (target is Handlung h)
            {
                h.Limit.BaseValue = h.Gegen;
                h.Limit.Connected.Clear();
                h.Limit.Connected.AddRange(h.GegenZusammensetzung.Select(x => x.Object.Value)); //TODO siehe oben
                h.Gegen = 0;
                h.GegenZusammensetzung.Clear();
                h.GegenZusammensetzung.OnCollectionChangedCall(null);
            }
            if (target is Waffe w)
            {
                w.DK.BaseValue = jsonObject.GetValue("DK")?.Value<double>() ?? 0.0;
                w.Precision.BaseValue = jsonObject.GetValue("Precision")?.Value<double>() ?? 0.0;
            }
            return target;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }

    public class CharHolderIO : SharedIO<CharHolder>
    {
        /// <summary>
        /// ConvertWithRightVersion
        /// </summary>
        /// <param name="strAppVersion"></param>
        /// <param name="strFileVersion"></param>
        /// <param name="fileContent"></param>
        /// <returns></returns>
        /// <exception cref="ShadowRunHelper.IO_FileVersion"></exception>
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
                    Log.Write(CustomManager.GetString("Notification_Info_NotSupportedVersion"), false);
                    throw new IO_FileVersion();
                case Constants.CHARFILE_VERSION_1_5:
                    fileContent = RefactorJSONString(fileContent, new List<(string old, string @new)> {
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
                        ("\"strProperty\"", "\"PropertyID\""), });
                    ReturnCharHolder = JsonConvert.DeserializeObject<CharHolder>(fileContent, settings);
                    foreach (var item in ReturnCharHolder.CTRLHandlung.Data)
                    {
                        item.Wert = 0;
                        item.Grenze = 0;
                        item.Gegen = 0;
                    }
                    Log.Write(CustomManager.GetString("Notification_Info_UpgradedChar_1_5_to_1_6"), false);
                    break;
                case Constants.CHARFILE_VERSION_1_6:
                    ReturnCharHolder = JsonConvert.DeserializeObject<CharHolder>(fileContent, settings);
                    ReturnCharHolder.Person.Notizen = PlainTextToRtf(ReturnCharHolder.Person.Notizen);
                    Log.Write(CustomManager.GetString("Notification_Info_UpgradedChar"), false);
                    break;
                case Constants.CHARFILE_VERSION_1_7:
                    fileContent = RefactorJSONString(fileContent, new List<(string old, string @new)> {
                        ("\"Praezision\"", "\"Precision\""), });
                    settings.Converters.Add(new Version1_7To1_8ConnectedThingsConverter());
                    ReturnCharHolder = JsonConvert.DeserializeObject<CharHolder>(fileContent, settings);
                    Log.Write(CustomManager.GetString("Notification_Info_UpgradedChar"), false);
                    break;
                case Constants.CHARFILE_VERSION_1_8:
                    ReturnCharHolder = JsonConvert.DeserializeObject<CharHolder>(fileContent, settings);
                    break;
                default:
                    throw new IO_FileVersion();
            }
            ReturnCharHolder.AfterLoad();
            return ReturnCharHolder;
        }

        public static string PlainTextToRtf(string plainText)
        {
            string escapedPlainText = plainText == null ? "" : plainText.Replace(@"\", @"\\").Replace("{", @"\{").Replace("}", @"\}").Replace(@"\r}", @"\r\n}");
            string rtf =
@"{\rtf1\fbidis\ansi\ansicpg1252\deff0\nouicompat\deflang1031{\fonttbl{\f0\fnil Segoe UI;}}
{\colortbl ;\red0\green0\blue0;}
{\*\generator Riched20 10.0.17134}\viewkind4\uc1
\pard\tx720\cf1\f0\fs23\par
\par

";
            rtf += escapedPlainText.Replace(Environment.NewLine, @" \par ").Replace("\n", @" \par ").Replace("\r", @" \par ");
            rtf +=
@"\pard\tx720\par
}";
            return rtf;
        }

        private static string RefactorJSONString(string Input, List<(string old, string @new)> replacements)
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
            var assembly = typeof(CharHolderIO).GetTypeInfo().Assembly;
            string RessourceName;
            string TargetName;
            var Language = Constants.AVAILIBLE_EXAMPLE_LANGUAGES.Contains(CultureInfo.CurrentCulture.TwoLetterISOLanguageName) ? CultureInfo.CurrentCulture.TwoLetterISOLanguageName : Constants.DEFAULT_EXAMPLE_LANGUAGE;
            switch (chartype)
            {
                case PreSavedChar.ExampleChar:
                    RessourceName = "SharedCode.Assets.Example." + Language + Constants.DATEIENDUNG_CHAR;
                    TargetName = CustomManager.GetString("ExampleChar") + Constants.DATEIENDUNG_CHAR;
                    break;
                case PreSavedChar.PreDBChar:
                    RessourceName = "SharedCode.Assets.DB." + Language + Constants.DATEIENDUNG_CHAR;
                    TargetName = CustomManager.GetString("ExampleChar") + Constants.DATEIENDUNG_CHAR;
                    break;
                default:
                    return;
            }
            Stream resourcestream = assembly.GetManifestResourceStream(RessourceName);
            var info = new FileInfo(Path.Combine(CurrentSavePath, TargetName));
            string content;
            using (var reader = new StreamReader(resourcestream))
            {
                content = reader.ReadToEnd();
            }
            CurrentIO.SaveFileContent(content, info);
        }

        public static async Task CopyFileToCurrentLocation(string oldlocation, string oldname, string newname)
        {
            var Target = new FileInfo(CurrentSavePath + newname);
            var Source = new FileInfo(oldlocation + oldname);
            await CurrentIO.CopyTo(Target, Source);
        }
    }
}