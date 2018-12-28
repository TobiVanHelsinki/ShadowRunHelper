using ShadowRunHelper.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TLIB;

namespace ShadowRunHelper
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
            var (set, files) = await Loading;
            //Import Settings
            try
            {
                foreach (var item in set.Join(SettingsModel.I.GetType().GetProperties(), s => s.Item1, s => s.Name, (NEW, OLD) => (NEW, OLD)))
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
                        TAPPLICATION.Debugging.TraceException(ex);
                        ErrorList.Add(item.OLD.Name);
                    }
                }
            }
            catch (Exception ex)
            {
                TAPPLICATION.Debugging.TraceException(ex);
            }
            // Import Chars
            try
            {
                foreach (var item in files)
                {
                    try
                    {
                //TODO
                        await CharHolderIO.CurrentIO.SaveFileContent(item.Item2, new FileInfo(CharHolderIO.GetCurrentSavePath() + item.Item1));
                    }
                    catch (Exception ex)
                    {
                        TAPPLICATION.Debugging.TraceException(ex);
                        ErrorList.Add(item.Item1);
                    }
                }
            }
            catch (Exception ex)
            {
                TAPPLICATION.Debugging.TraceException(ex);
            }
            InProgress = false;
            if (ErrorList.Count != 0)
            {
                string Errors = "";
                foreach (var item in ErrorList)
                {
                    Errors += item + "\n";
                }
                Model.AppModel.Instance?.NewNotification(PlatformHelper.GetString("AppImportErrors") + "\n\n" + Errors, false);
            }
            else
            {
                Model.AppModel.Instance?.NewNotification(PlatformHelper.GetString("AppImportNoErrors"), false);
            }
        }
    }
}
