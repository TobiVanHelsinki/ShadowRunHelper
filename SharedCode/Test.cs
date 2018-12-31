using SharedCode.Ressourcen;
using System.Resources;

namespace ShadowRunHelper
{
    class Test
    {
        public static void DoSmt()
        {
            ResourceManager resman = Strings.ResourceManager;
            var dyn = resman.GetString("Notification_Error_SaveFail");
            var stat = Strings.Notification_Error_SaveFail;
            var classic = Strings.Notification_Error_SaveFail;
        }
    }
}
