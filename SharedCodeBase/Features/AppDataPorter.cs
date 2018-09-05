using Newtonsoft.Json;
using ShadowRunHelper.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using TAMARIN.IO;
using TLIB;
using Windows.Storage;

namespace ShadowRunHelper
{
    internal static class AppDataPorter
    {
        internal static Task<(List<(string, object)> set, List<(string, string)> files)> Loading;
        internal static async Task<(List<(string, object)> set, List<(string, string)> files)> LoadAppPacket(IStorageItem File)
        {
            InProgress = true;
            var txt = await FileIO.ReadTextAsync((IStorageFile)File);
            var des = JsonConvert.DeserializeObject<(List<(string, object)>, List<(string, string)>)>(txt);
            return des;
        }
        static List<string> Forbidden = new List<string>() {"I","Instance", "LastAppVersion", "LastSaveInfo", "LastPage", "ORDNERMODE", "ORDNERMODE_PFAD" };

        public static bool InProgress { get; set; }

        internal static async void ImportAppPacket()
        {
            List<string> ErrorList = new List<string>();
            var (set, files) = await Loading;
            //Import Settings
            try
            {
                foreach (var item in set.Join(SettingsModel.I.GetType().GetProperties(), s=>s.Item1, s=>s.Name, (NEW, OLD)=>(NEW,OLD)))
                {
                    if (Forbidden.Contains(item.NEW.Item1))
                    {
                        continue;
                    }
                    try
                    {
                        var m = Convert.ChangeType(item.NEW.Item2, item.OLD.PropertyType);
                        item.OLD?.SetMethod.Invoke(SettingsModel.I, new object[] { m });
                    }
                    catch (Exception ex)
                    {
                        ErrorList.Add(item.OLD.Name);
                    }
                }
            }
            catch (Exception ex)
            {
            }
            // Import Chars
            try
            {
                foreach (var item in files)
                {
                    try
                    {
                        await CharHolderIO.CurrentIO.SaveFileContent(item.Item2, new FileInfoClass(CharHolderIO.GetCurrentSavePlace(), item.Item1, CharHolderIO.GetCurrentSavePath()), UserDecision.AskUser);
                    }
                    catch (Exception)
                    {
                        ErrorList.Add(item.Item1);
                    }
                }
            }
            catch (Exception ex)
            {
            }
            InProgress = false;
            if (ErrorList.Count != 0)
            {
                string Errors = "";
                foreach (var item in ErrorList)
                {
                    Errors += item + "\n";
                }
                Model.AppModel.Instance.NewNotification(StringHelper.GetString("AppImportErrors") + "\n\n" + Errors, false);
            }
            else
            {
                Model.AppModel.Instance.NewNotification(StringHelper.GetString("AppImportNoErrors"), false);

            }
        }
    }
}
