using System;
using System.Collections.Generic;
using TLIB;

namespace ShadowRunHelper.Helper
{
    internal static class StringHelper
    {
        public static List<string> GetStrings(string strID)
        {
            var ret = new List<string>();
            var Counter = 1;
            string Current;
            Loop:
            try
            {
                Current = CustomManager.GetString(strID + Counter);
            }
            catch (Exception ex)
            {
                Log.Write("Could not", ex, logType: LogType.Error);
                Current = null;
            }
            if (!string.IsNullOrEmpty(Current))
            {
                ret.Add(Current);
                Counter++;
                goto Loop;
            }
            return ret;
        }

    }
}
