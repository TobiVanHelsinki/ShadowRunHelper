//Author: Tobi van Helsinki

using SharedCode.Ressourcen;
using System;
using System.Globalization;

namespace ShadowRunHelper
{
    public static class CustomManager
    {
        [Obsolete]
        public static string GetString(string s)
        {
            string ret;
            ret = ModelResources.ResourceManager.GetString(s, CultureInfo.CurrentCulture);
            if (ret == null)
            {
                ret = AppResources.ResourceManager.GetString(s, CultureInfo.CurrentCulture);
                if (ret == null)
                {
                    ret = UiResources.ResourceManager.GetString(s, CultureInfo.CurrentCulture);
                }
            }
            return ret;
        }
    }
}