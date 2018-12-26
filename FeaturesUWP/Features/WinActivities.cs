using ShadowRunHelper.Model;
using System;
using System.Threading.Tasks;
using TLIB;
using Windows.ApplicationModel.UserActivities;

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

            userActivity.VisualElements.DisplayText = PlatformHelper.GetString("Activity_PlayedWith") + name.Remove(name.Length - Constants.DATEIENDUNG_CHAR.Length);
            userActivity.ActivationUri = new Uri(Constants.PROTOCOL_CHAR + Char.FileInfo.Filepath + Char.FileInfo.Name);

            await userActivity.SaveAsync();

            CurrentCharActivity?.Dispose();
            CurrentCharActivity = userActivity.CreateSession();
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