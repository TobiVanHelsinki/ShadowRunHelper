using SharedCode.Ressourcen;
using System.Globalization;

namespace ShadowRunHelper
{
    public static class CustomManager
    {
        public static string GetString(string s)
        {
            if (Strings.Error_CopyFiles == null)
            {
                if (System.Diagnostics.Debugger.IsAttached) System.Diagnostics.Debugger.Break();
            }
            string ret;
            ret = Strings.ResourceManager.GetString(s, CultureInfo.CurrentCulture);
            if (ret == null)
            {
                string name2 = s.Replace("/", "_");
                ret = Strings.ResourceManager.GetString(name2, CultureInfo.CurrentCulture);
                if (ret == null)
                {
                    string name1 = s.Replace("/", ".");
                    ret = Strings.ResourceManager.GetString(name1, CultureInfo.CurrentCulture);
                    if (ret == null)
                    {
                        string name = s.Replace("/", "");
                        ret = Strings.ResourceManager.GetString(name, CultureInfo.CurrentCulture);
                        if (ret == null)
                        {
                            if (System.Diagnostics.Debugger.IsAttached) System.Diagnostics.Debugger.Break();
                        }
                    }
                }
            }
            return ret;
        }
    }
}
