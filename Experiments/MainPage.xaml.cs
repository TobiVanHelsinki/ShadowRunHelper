using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ShadowRunHelper;
using ShadowRunHelper.CharModel;
using ShadowRunHelper.Model;
using System;
using System.Collections.Generic;
using Windows.UI.Xaml.Controls;

namespace Experiments
{
    //[JsonConverter(typeof(UnknownThingConverter))]

    //class UnknownThingConverter : JsonConverter
    //{
    //    public override bool CanConvert(Type objectType)
    //    {
    //        return objectType == typeof(Thing);
    //    }

    //    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    //    {
    //        try
    //        {
    //            //http://skrift.io/articles/archive/bulletproof-interface-deserialization-in-jsonnet/
    //            var jsonObject = JObject.Load(reader);
    //            var IntThingType = jsonObject.GetValue("ThingType").Value<Int64>();
    //            Type Should = TypeHelper.ThingDefToType((ThingDefs)IntThingType);
    //            Thing target = (Thing)Activator.CreateInstance(Should);
    //            serializer.Populate(jsonObject.CreateReader(), target);
    //            return target;
    //        }
    //        catch (Exception ex)
    //        {
    //            return null;
    //        }
    //    }

    //    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    //    {
    //        throw new NotImplementedException();
    //    }
    //}

    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            //this.InitializeComponent();
            List<AllListEntry> list = new List<AllListEntry>();
            list.Add(new AllListEntry(new Handlung() { Bezeichner = "Handlung" }, "", ""));
            list.Add(new AllListEntry(new Panzerung() { Bezeichner = "Panz1", Kapazitaet = 5 }, "", ""));
            list.Add(new AllListEntry(new CyberDeck() { Bezeichner = "Deck", Angriff = 4, Datenverarbeitung = 5 }, "", ""));
            while (true)
            {
                try
                {
                    //JsonSerializerSettings serializer = new JsonSerializerSettings();
                    //serializer.ObjectCreationHandling = ObjectCreationHandling.Auto;
                    //serializer.ReferenceLoopHandling = ReferenceLoopHandling.Serialize;
                    //serializer.TypeNameHandling = TypeNameHandling.All;
                    //var conv = new UnknownThingConverter();
                    //conv.
                    //var str = JsonConvert.SerializeObject(list,conv);
                    //var result = JsonConvert.DeserializeObject<List<AllListEntry>>(str, conv);
                }
                catch (Exception ex)
                {
                }
                
            }
        }
    }
}
