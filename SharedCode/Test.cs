using SharedCode.Strings;
using System.Resources;

namespace ShadowRunHelper
{
    class Test
    {
        public static void DoSmt()
        {
            ResourceManager resman = Resources.ResourceManager;
            var dyn = resman.GetString("Notification_Error_SaveFail");
            var stat = Resources.Notification_Error_SaveFail;
            var classic = Resources.Notification_Error_SaveFail;
        }
    }
}
