using ShadowRunHelper.Model;
using System;
using System.Threading.Tasks;
using TLIB;
using Windows.ApplicationModel.UserActivities;
using Windows.UI.Core;

namespace ShadowRunHelper
{
    public class WinActivities : IActivities
    {
        UserActivitySession CurrentCharActivity;
        public async Task GenerateCharActivityAsync(CharHolder Char)
        {
            if (!Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.ApplicationModel.UserActivities.UserActivityChannel"))
            {
                return;
            }
            UserActivityChannel channel = UserActivityChannel.GetDefault();
            var name = Char.MakeName(false);
            UserActivity userActivity = await channel.GetOrCreateUserActivityAsync(name);

            userActivity.VisualElements.DisplayText = CustomManager.GetString("Activity_PlayedWith") + name.Remove(name.Length - Constants.DATEIENDUNG_CHAR.Length);
            userActivity.ActivationUri = new Uri(Constants.PROTOCOL_CHAR + Char.FileInfo?.FullName);

            await userActivity.SaveAsync();

            CurrentCharActivity?.Dispose();
            TAPPLICATION.PlatformHelper.ExecuteOnUIThreadAsync(() => CurrentCharActivity = userActivity.CreateSession());
        }

        public void StopCurrentCharActivity()
        {
            if (!Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.ApplicationModel.UserActivities.UserActivityChannel"))
            {
                return;
            }
            CurrentCharActivity?.Dispose();
        }
    }
}