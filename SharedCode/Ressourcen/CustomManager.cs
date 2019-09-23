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
            ret = ModelResources.ResourceManager.GetString(s, CultureInfo.CurrentCulture);
            if (ret == null)
            {
                ret = AppResources.ResourceManager.GetString(s, CultureInfo.CurrentCulture);
                if (ret == null)
                {
                    ret = UiResources.ResourceManager.GetString(s, CultureInfo.CurrentCulture);
                    if (ret == null)
                    {
                        s = s.Replace("/", ".").Replace("/", "").Replace("/", "_");
                        ret = Strings.ResourceManager.GetString(s, CultureInfo.CurrentCulture);
                        if (ret == null)
                        {
                            //if (System.Diagnostics.Debugger.IsAttached) System.Diagnostics.Debugger.Break();
                        }
                    }
                }
            }
            return ret;
        }
    }
}
