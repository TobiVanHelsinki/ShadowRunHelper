﻿using Newtonsoft.Json;
using ShadowRunHelper.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using TLIB;
using Windows.Storage;

namespace ShadowRunHelper
{
    public class WinAppDataPorter : IAppDataPorter
    {
        public bool InProgress { get; set; }
        public Task<(List<(string, object)> set, List<(string, string)> files)> Loading { get; set; }
        public async Task<(List<(string, object)> set, List<(string, string)> files)> LoadAppPacket(object FileHandle)
        {
            InProgress = true;
            var txt = await FileIO.ReadTextAsync((IStorageFile)FileHandle);
            var des = JsonConvert.DeserializeObject<(List<(string, object)>, List<(string, string)>)>(txt);
            return des;
        }

        public List<string> Forbidden { get; set; } = new List<string>() { "I", "Instance", "LastAppVersion", "LastSaveInfo", "LastPage", "ORDNERMODE", "ORDNERMODE_PFAD" };

        public async Task ImportAppPacket()
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
                    catch (Exception)
                    {
                        ErrorList.Add(item.OLD.Name);
                    }
                }
            }
            catch (Exception)
            {
            }
            // Import Chars
            try
            {
                foreach (var item in files)
                {
                    try
                    {
                        await CharHolderIO.CurrentIO.SaveFileContent(item.Item2, new FileInfo(CharHolderIO.CurrentSavePath + item.Item1));
                    }
                    catch (Exception ex)
 {
                        Log.Write("Could not save", ex, logType: LogType.Error);
                        ErrorList.Add(item.Item1);
                    }
                }
            }
            catch (Exception)
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
                Log.Write(CustomManager.GetString("AppImportErrors"), logType: LogType.Error);
            }
            else
            {
                Log.Write(CustomManager.GetString("AppImportNoErrors"), logType: LogType.Error);
            }
        }
    }
}
