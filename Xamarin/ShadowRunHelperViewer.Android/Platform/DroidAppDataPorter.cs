//Author: Tobi van Helsinki

using ShadowRunHelper;
using ShadowRunHelper.IO;
using SharedCode.Resources;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TLIB;

namespace ShadowRunHelperViewer.Platform.Android
{
    public class DroidAppDataPorter : IAppDataPorter
    {
        public bool InProgress { get; set; }
        public Task<(List<(string, object)> set, List<(string, string)> files)> Loading { get; set; }

        public async Task<(List<(string, object)> set, List<(string, string)> files)> LoadAppPacket(object FileHandle)
        {
            return (new List<(string, object)>(), new List<(string, string)>());
        }

        public List<string> Forbidden { get; set; } = new List<string>() { "I", "Instance", "LastAppVersion", "LastSaveInfo", "LastPage", "ORDNERMODE", "ORDNERMODE_PFAD" };

        public async Task ImportAppPacket()
        {
            List<string> ErrorList = new List<string>();
            (List<(string, object)> set, List<(string, string)> files) = await Loading;
            //Import Settings
            try
            {
                foreach (((string, object) NEW, System.Reflection.PropertyInfo OLD) item in set.Join(SettingsModel.I.GetType().GetProperties(), s => s.Item1, s => s.Name, (NEW, OLD) => (NEW, OLD)))
                {
                    if (Forbidden.Contains(item.NEW.Item1))
                    {
                        continue;
                    }
                    try
                    {
                        object m = Convert.ChangeType(item.NEW.Item2, item.OLD.PropertyType);
                        item.OLD?.SetMethod.Invoke(SettingsModel.I, new object[] { m });
                    }
                    catch (Exception ex)
                    {
                        Log.Write("Could not", ex, logType: LogType.Error);
                        ErrorList.Add(item.OLD.Name);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Write("Could not", ex, logType: LogType.Error);
            }
            // Import Chars
            try
            {
                foreach ((string, string) item in files)
                {
                    try
                    {
                        //TODO
                        await SharedIO.CurrentIO.SaveFileContent(item.Item2, new FileInfo(Path.Combine(SharedIO.CurrentSavePath, item.Item1)));
                    }
                    catch (Exception ex)
                    {
                        Log.Write("Could not", ex, logType: LogType.Error);
                        ErrorList.Add(item.Item1);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Write("Could not", ex, logType: LogType.Error);
            }
            InProgress = false;
            if (ErrorList.Count != 0)
            {
                string Errors = "";
                foreach (string item in ErrorList)
                {
                    Errors += item + "\n";
                }
                Log.Write(AppResources.AppImportErrors + "\n\n" + Errors, false);
            }
            else
            {
                Log.Write(AppResources.AppImportNoErrors, false);
            }
        }
    }
}