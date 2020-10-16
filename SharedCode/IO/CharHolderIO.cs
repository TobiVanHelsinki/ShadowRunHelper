//Author: Tobi van Helsinki

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ShadowRunHelper.CharModel;
using ShadowRunHelper.Model;
using SharedCode.Resources;
using ShadowRunHelper.IO;
using TLIB;

namespace ShadowRunHelper.IO
{
#pragma warning disable CS0618 // Typ oder Element ist veraltet

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
            if (jsonObject.TryGetValue("$ref", out var isRef))
            {
                var o = serializer.Deserialize(jsonObject.CreateReader());
                return o;
            }
            var ThingTypeValue = jsonObject.GetValue(nameof(Thing.ThingType));
            var IntThingType = ThingTypeValue.Value<long>();
            var Should = TypeHelper.ThingDefToType((ThingDefs)IntThingType);
            var target = (Thing)Activator.CreateInstance(Should);
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

    internal class RemoveUnusedProps : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return typeof(Eigenschaft).IsAssignableFrom(objectType);
        }

        /// <summary>
        /// ReadJson
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="objectType"></param>
        /// <param name="existingValue"></param>
        /// <param name="serializer"></param>
        /// <returns></returns>
        /// <exception cref="JsonReaderException">Ignore.</exception>
        /// <exception cref="TargetInvocationException">Ignore.</exception>
        /// <exception cref="MethodAccessException">Ignore.</exception>
        /// <exception cref="MemberAccessException">Ignore.</exception>
        /// <exception cref="System.Runtime.InteropServices.InvalidComObjectException">Ignore.</exception>
        /// <exception cref="MissingMethodException">Ignore.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Ignore.</exception>
        /// <exception cref="TypeLoadException">Ignore.</exception>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var jsonObject = JObject.Load(reader);
            if (jsonObject.TryGetValue("$ref", out var isRef))
            {
                var o = serializer.Deserialize(jsonObject.CreateReader());
                return o;
            }
            var target = (Thing)Activator.CreateInstance(TypeHelper.ThingDefToType((ThingDefs)jsonObject.GetValue(nameof(Thing.ThingType)).Value<long>()));
            serializer.Populate(jsonObject.CreateReader(), target);

            if (target is Adeptenkraft a && !string.IsNullOrEmpty(a.Option))
            {
                if (a.Zusatz.Length == 0)
                {
                    a.Zusatz = a.Option;
                }
                else
                {
                    a.Zusatz = ", " + a.Option;
                }
                a.Option = null;
            }
            if (target is Eigenschaft e && !string.IsNullOrEmpty(e.Auswirkungen))
            {
                if (e.Zusatz.Length == 0)
                {
                    e.Zusatz = e.Auswirkungen;
                }
                else
                {
                    e.Zusatz = ", " + e.Auswirkungen;
                }
                e.Auswirkungen = null;
            }
            if (target is Implantat i && !string.IsNullOrEmpty(i.Auswirkung))
            {
                if (i.Zusatz.Length == 0)
                {
                    i.Zusatz = i.Auswirkung;
                }
                else
                {
                    i.Zusatz = ", " + i.Auswirkung;
                }
                i.Auswirkung = null;
            }
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

        /// <summary>
        /// ReadJson
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="objectType"></param>
        /// <param name="existingValue"></param>
        /// <param name="serializer"></param>
        /// <returns></returns>
        /// <exception cref="TargetInvocationException">Ignore.</exception>
        /// <exception cref="MethodAccessException">Ignore.</exception>
        /// <exception cref="MemberAccessException">Ignore.</exception>
        /// <exception cref="System.Runtime.InteropServices.InvalidComObjectException">Ignore.</exception>
        /// <exception cref="MissingMethodException">Ignore.</exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">Ignore.</exception>
        /// <exception cref="TypeLoadException">Ignore.</exception>
        /// <exception cref="JsonReaderException">Ignore.</exception>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var jsonObject = JObject.Load(reader);
            if (jsonObject.TryGetValue("$ref", out var isRef))
            {
                return serializer.Deserialize(jsonObject.CreateReader());
            }
            var ThingTypeValue = jsonObject.GetValue(nameof(Thing.ThingType));
            var IntThingType = ThingTypeValue.Value<long>();
            var Should = TypeHelper.ThingDefToType((ThingDefs)IntThingType);
            var target = (Thing)Activator.CreateInstance(Should);
            serializer.Populate(jsonObject.CreateReader(), target);

            //if (target.LinkedThings.Any())
            //{
            //    if (System.Diagnostics.Debugger.IsAttached) System.Diagnostics.Debugger.Break();
            //}
            target.Value.BaseValue = jsonObject.GetValue("Wert")?.Value<double>() ?? 0.0;
            LinkList2CalcProp(target.LinkedThings, target.Value);
            if (target is Handlung h)
            {
                h.Limit.BaseValue = jsonObject.GetValue("Grenze")?.Value<double>() ?? 0.0;
                LinkList2CalcProp(h.GrenzeZusammensetzung, h.Limit);

                h.Against.BaseValue = jsonObject.GetValue("Gegen")?.Value<double>() ?? 0.0;
                LinkList2CalcProp(h.GegenZusammensetzung, h.Against);
            }
            if (target is Waffe w)
            {
                w.DK.BaseValue = jsonObject.GetValue("DK")?.Value<double>() ?? 0.0;
                w.Precision.BaseValue = jsonObject.GetValue("Precision")?.Value<double>() ?? 0.0;
            }
            return target;
        }

        private static void LinkList2CalcProp(LinkList linkedThings, ConnectProperty calc)
        {
            calc.Connected.Clear();
            calc.Connected.AddRange(linkedThings?.Select(x => (x.PropertyID switch
            {
                "Gegen" => (x.Object as Handlung).Against,
                "Grenze" => (x.Object as Handlung).Limit,
                "DK" => (x.Object as Waffe).DK,
                "Precision" => (x.Object as Waffe).Precision,
                "Praezision" => (x.Object as Waffe).Precision,
                "RK" => (x.Object as Fernkampfwaffe).RK,
                "Reichweite" => (x.Object as Nahkampfwaffe).Reichweite,
                "Kapazitaet" => (x.Object as Panzerung).Capacity,
                "Angriff" => (x.Object as CyberDeck).Angriff,
                "Schleicher" => (x.Object as CyberDeck).Schleicher,
                "Firewall" => (x.Object as Kommlink).Firewall,
                "Datenverarbeitung" => (x.Object as Kommlink).Datenverarbeitung,
                "Sitze" => (x.Object as Vehikel).Sitze,
                "Sensor" => (x.Object as Vehikel).Sensor,
                "Rumpf" => (x.Object as Vehikel).Rumpf,
                "Pilot" => (x.Object as Vehikel).Pilot,
                "Panzerung" => (x.Object as Vehikel).Panzerung,
                "Handling" => (x.Object as Vehikel).Handling,
                "Gewicht" => (x.Object as Vehikel).Gewicht,
                "Geschwindigkeit" => (x.Object as Vehikel).Geschwindigkeit,
                "Beschleunigung" => (x.Object as Vehikel).Beschleunigung,
                _ => x.Object.Value,
            })));
            linkedThings.Clear();
            linkedThings.OnCollectionChangedCall(null);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }

#pragma warning restore CS0618 // Typ oder Element ist veraltet

    public class CharHolderIO : SharedIO<CharHolder>
    {
        /// <summary>
        /// ConvertWithRightVersion
        /// </summary>
        /// <param name="strAppVersion"></param>
        /// <param name="strFileVersion"></param>
        /// <param name="fileContent"></param>
        /// <returns></returns>
        /// <exception cref="ShadowRunHelper.IO_FileVersion">To old file version</exception>
        internal static CharHolder ConvertWithRightVersion(string strAppVersion, string strFileVersion, string fileContent)
        {
            var settings = new JsonSerializerSettings()
            {
                Error = ErrorHandler,
                PreserveReferencesHandling = PreserveReferencesHandling.All
            };
            settings.Converters.Add(new UnknownThingConverter());
            CharHolder ReturnCharHolder;

            switch (strFileVersion)
            {
                case Constants.CHARFILE_VERSION_1_3:
                    Log.Write(AppResources.Info_NotSupportedVersion, true);
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
#pragma warning disable CS0618 // Typ oder Element ist veraltet
                        item.Wert = 0;
                        item.Grenze = 0;
                        item.Gegen = 0;
#pragma warning restore CS0618 // Typ oder Element ist veraltet
                    }
                    Log.Write(AppResources.Info_UpgradedChar, true);
                    break;
                case Constants.CHARFILE_VERSION_1_6:
                    ReturnCharHolder = JsonConvert.DeserializeObject<CharHolder>(fileContent, settings);
                    ReturnCharHolder.Person.Notizen = PlainTextToRtf(ReturnCharHolder.Person.Notizen);
                    Log.Write(AppResources.Info_UpgradedChar, true);
                    break;
                case Constants.CHARFILE_VERSION_1_7:
                    fileContent = RefactorJSONString(fileContent, new List<(string old, string @new)> {
                        ("\"Praezision\"", "\"Precision\""), });
                    settings.Converters.Add(new Version1_7To1_8ConnectedThingsConverter());
                    ReturnCharHolder = JsonConvert.DeserializeObject<CharHolder>(fileContent, settings);
                    OldNoteToNewNotes(ReturnCharHolder);
                    Log.Write(AppResources.Info_UpgradedChar, true);
                    break;
                case Constants.CHARFILE_VERSION_1_8:
                    settings.Converters.Add(new RemoveUnusedProps());
                    ReturnCharHolder = JsonConvert.DeserializeObject<CharHolder>(fileContent, settings);
                    OldNoteToNewNotes(ReturnCharHolder);
                    break;
                default:
                    throw new IO_FileVersion();
            }
            ReturnCharHolder.AfterLoad();
            return ReturnCharHolder;
        }

        private static void OldNoteToNewNotes(CharHolder ReturnCharHolder)
        {
            if (!string.IsNullOrEmpty(ReturnCharHolder?.Person?.Notizen))
            {
                (ReturnCharHolder.CTRLNote.AddNewThing() as Note).Text = ReturnCharHolder.Person.Notizen;
                ReturnCharHolder.Person.Notizen = "";
            }
        }

        public static string PlainTextToRtf(string plainText)
        {
            var escapedPlainText = plainText == null ? "" : plainText.Replace(@"\", @"\\").Replace("{", @"\{").Replace("}", @"\}").Replace(@"\r}", @"\r\n}");
            var rtf =
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
            var Ret = Input;
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

        /// <summary>
        /// CopyPreSavedCharToCurrentLocation
        /// </summary>
        /// <param name="chartype"></param>
        /// <exception cref="FileLoadException"></exception>
        /// <exception cref="FileNotFoundException"></exception>
        /// <exception cref="BadImageFormatException"></exception>
        /// <exception cref="System.Security.SecurityException"></exception>
        /// <exception cref="UnauthorizedAccessException"></exception>
        /// <exception cref="PathTooLongException"></exception>
        /// <exception cref="OutOfMemoryException"></exception>
        /// <exception cref="IOException"></exception>
        public static void CopyPreSavedCharToCurrentLocation(PreSavedChar chartype)
        {
            var assembly = typeof(CharHolderIO).GetTypeInfo().Assembly;
            string RessourceName;
            string TargetName;
            var Language = Constants.AVAILIBLE_EXAMPLE_LANGUAGES.Contains(CultureInfo.CurrentCulture.TwoLetterISOLanguageName) ? CultureInfo.CurrentCulture.TwoLetterISOLanguageName : Constants.DEFAULT_EXAMPLE_LANGUAGE;
            switch (chartype)
            {
                case PreSavedChar.ExampleChar:
                    RessourceName = "SharedCode.Assets.Example." + Language + Constants.DATEIENDUNG_CHAR;
                    TargetName = AppResources.ExampleChar + Constants.DATEIENDUNG_CHAR;
                    break;
                case PreSavedChar.PreDBChar:
                    RessourceName = "SharedCode.Assets.DB." + Language + Constants.DATEIENDUNG_CHAR;
                    TargetName = AppResources.ExampleChar + Constants.DATEIENDUNG_CHAR;
                    break;
                default:
                    return;
            }
            var resourcestream = assembly.GetManifestResourceStream(RessourceName);
            var info = new FileInfo(Path.Combine(CurrentSavePath, TargetName));
            string content;
            using (var reader = new StreamReader(resourcestream))
            {
                content = reader.ReadToEnd();
            }
            CurrentIO.SaveFileContent(content, info);
        }

        /// <summary>
        /// use this method to rename files and to move files
        /// </summary>
        /// <param name="oldlocation"></param>
        /// <param name="oldname"></param>
        /// <param name="newname"></param>
        /// <returns></returns>
        /// <exception cref="System.Security.SecurityException"></exception>
        /// <exception cref="UnauthorizedAccessException"></exception>
        /// <exception cref="PathTooLongException"></exception>
        public static async Task CopyFileToCurrentLocation(string oldlocation, string oldname, string newname)
        {
            var Target = new FileInfo(CurrentSavePath + newname);
            var Source = new FileInfo(oldlocation + oldname);
            await CurrentIO.CopyTo(Target, Source);
        }
    }
}