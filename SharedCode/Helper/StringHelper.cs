using System;
using System.Collections.Generic;

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
                Current = TLIB.PlatformHelper.GetString(strID + Counter);
            }
            catch (Exception)
 { TAPPLICATION.Debugging.TraceException();
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
