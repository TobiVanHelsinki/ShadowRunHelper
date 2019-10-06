using ShadowRunHelper.Model;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TAPPLICATION.IO;
using TLIB;

namespace ShadowRunHelper.UI
{
    public static class SharedUIActions
    {
        public static async Task UI_TxT_CSV_Cat_Exportport(CharHolder CharToSave)
        {
            try
            {
                string ret = "";
                foreach (var item in CharToSave.CTRLList)
                {
                    ret += item.Data2CSV(';', '\n');
                }
                var ContentList = CharToSave.CTRLList.Select(c => (TypeHelper.ThingDefToString(c.eDataTyp, true) + Constants.DATEIENDUNG_CSV, c.Data2CSV(';', '\n')));
                var folder = await SharedIO.CurrentIO.PickFolder();
                SharedIO.SaveTextesToFiles(ContentList, folder);
            }
            catch (Exception ex)
            {
                Log.Write(CustomManager.GetString("Notification_Error_CSVExportFail") + "2", ex);
            }
            Features.Analytics.TrackEvent("Admin_UI_TxT_CSV_Cat_Export");
        }
    }
}
