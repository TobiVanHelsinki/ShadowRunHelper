﻿using ShadowRunHelper.Model;
using System;
using System.Linq;
using TAPPLICATION.IO;
using TLIB.IO;
using TLIB.PlatformHelper;

namespace ShadowRunHelper.UI
{
    public static class SharedUIActions
    {
        public static void UI_TxT_CSV_Cat_Exportport(CharHolder CharToSave)
        {
            try
            {
                string ret = "";
                foreach (var item in CharToSave.CTRLList)
                {
                    ret += item.Data2CSV(';', '\n');
                }
                var ContentList = CharToSave.CTRLList.Select(c => (TypeHelper.ThingDefToString(c.eDataTyp, true) + Constants.DATEIENDUNG_CSV, c.Data2CSV(';', '\n')));
                SharedIO.SaveTextesToFiles(ContentList, new FileInfoClass() { Fileplace = Place.Extern, Token = "CSV_TEMP" });
            }
            catch (Exception ex)
            {
                AppModel.Instance.NewNotification(StringHelper.GetString("Notification_Error_CSVExportFail") + "2", ex);
            }
            Features.Analytics.TrackEvent("Admin_UI_TxT_CSV_Cat_Export");
        }
    }
}