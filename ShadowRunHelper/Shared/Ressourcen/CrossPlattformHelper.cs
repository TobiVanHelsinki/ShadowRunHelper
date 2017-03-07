#if __ANDROID__
#else 
#endif

namespace ShadowRunHelper
{
    static class CrossPlattformHelper
    {
        public static string GetString(string strID)
        {
            string strReturn = "";
#if __ANDROID__
            strReturn = "NotImplemented";
#else 
            strReturn = Windows.ApplicationModel.Resources.ResourceLoader.GetForViewIndependentUse().GetString(strID);
#endif
            return strReturn;
        }
    }
}
