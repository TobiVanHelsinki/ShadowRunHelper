using Newtonsoft.Json;
using ShadowRunHelper.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using TAMARIN.IO;
using Windows.Storage;

namespace ShadowRunHelper
{
    internal static class AppDataPorter
    {
        internal static Task<(List<(string, object)> set, List<(string, string)> files)> Loading;
        internal static async Task<(List<(string, object)> set, List<(string, string)> files)> LoadAppPacket(IStorageItem File)
        {
            var txt = await FileIO.ReadTextAsync((IStorageFile)File);
            var des = JsonConvert.DeserializeObject<(List<(string, object)>, List<(string, string)>)>(txt);
            return des;
        }

        internal static async void ImportAppPacket()
        {
            var Content = await Loading;
            //Import Settings
            foreach (var item in Content.set.Join(SettingsModel.I.GetType().GetProperties(), s=>s.Item1, s=>s.Name, (NEW, OLD)=>(NEW,OLD)))
            {
                item.OLD.SetValue(SettingsModel.I, item.NEW.Item2);
            }
            // Import Chars
            foreach (var item in Content.files)
            {
                await CharHolderIO.CurrentIO.SaveFileContent(item.Item2, new FileInfoClass(CharHolderIO.GetCurrentSavePlace(), item.Item1 , CharHolderIO.GetCurrentSavePath()), UserDecision.AskUser);
            }
        }
    }
}
