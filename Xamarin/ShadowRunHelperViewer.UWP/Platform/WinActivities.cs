//Author: Tobi van Helsinki

using ShadowRunHelper;
using ShadowRunHelper.Model;
using SharedCode.Resources;
using System;
using System.Threading.Tasks;
using TLIB;
using Windows.ApplicationModel.UserActivities;
using Windows.UI.Core;

namespace ShadowRunHelperViewer.Platform.UWP
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

            userActivity.VisualElements.DisplayText = AppResources.PlayedWith + " " + name.Remove(name.Length - Constants.DATEIENDUNG_CHAR.Length);
            userActivity.ActivationUri = new Uri(Constants.PROTOCOL_CHAR + Char.FileInfo?.FullName);

            await userActivity.SaveAsync();

            CurrentCharActivity?.Dispose();
            ShadowRunHelper.Helper.PlatformHelper.ExecuteOnUIThreadAsync(() => CurrentCharActivity = userActivity.CreateSession());
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