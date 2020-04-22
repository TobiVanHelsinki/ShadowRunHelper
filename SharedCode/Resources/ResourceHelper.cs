//Author: Tobi van Helsinki

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Resources;
using SharedCode.Resources;
using TLIB;

namespace ShadowRunHelper
{
    public static class ResourceHelper
    {
        public static List<string> GetStringsWithIncreasinIDs(string strID)
        {
            var ret = new List<string>();
            var Counter = 1;
            string Current;
        Loop:
            try
            {
                Current = AppResources.ResourceManager.GetString(strID + Counter);
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

        public static string GetStringSafe(this ResourceManager man, string name, CultureInfo culture = null)
        {
            string retval = null;
            try
            {
                if (culture is null)
                {
                    retval = man.GetString(name);
                }
                else
                {
                    retval = man.GetString(name, culture);
                }
            }
            catch (Exception)
            {
            }
            return retval is null ? name + "." + culture?.Name : retval;
        }
    }
}