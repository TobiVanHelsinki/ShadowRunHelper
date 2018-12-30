using SharedCode.Strings;

namespace ShadowRunHelper
{
    public static class CustomManager
    {
        public static string GetString(string s)
        {
            return Resources.ResourceManager.GetString(s);
        }
    }
}
