///Author: Tobi van Helsinki

using System;
using System.Globalization;
using System.Resources;

namespace ShadowRunHelper
{
    public static class ResourceManagerExtension
    {
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