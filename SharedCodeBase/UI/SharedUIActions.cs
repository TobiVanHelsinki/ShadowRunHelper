using Microsoft.AppCenter.Analytics;
using ShadowRunHelper.Model;
using System;
using System.Linq;
using TAMARIN.IO;
using TAPPLICATION.IO;
using TLIB;

namespace ShadowRunHelper.UI
{
    public static class SharedUIActions
    {
        public static void UI_TxT_CSV_Cat_Exportport(CharHolder CharToSave)
        {
            try
            {
                string ret = "";
                foreach (var item in CharToSave.lstCTRL)
                {
                    ret += item.Data2CSV(';', '\n');
                }
                var ContentList = CharToSave.lstCTRL.Select(c => (TypeHelper.ThingDefToString(c.eDataTyp, true) + Constants.DATEIENDUNG_CSV, c.Data2CSV(';', '\n')));
                SharedIO.SaveTextesToFiles(ContentList, new FileInfoClass() { Fileplace = Place.Extern, FolderToken = "CSV_TEMP" });
            }
            catch (Exception ex)
            {
                AppModel.Instance.NewNotification(StringHelper.GetString("Notification_Error_CSVExportFail") + "2", ex);
            }
            Analytics.TrackEvent("Admin_UI_TxT_CSV_Cat_Export");
        }
    }
}
