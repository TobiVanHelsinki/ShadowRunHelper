using System;
using System.Threading.Tasks;
using TAMARIN.IO;
using TAPPLICATION.IO;
using TLIB;
using Windows.Services.Store;

namespace ShadowRunHelper
{
    public static class IAP
    {
        public static async Task CheckLicence()
        {
            //TODO introduce kind of caching
            try
            {
                //Debug_TimeAnalyser.Start("IAP GetAddons");
                var AddOns = await StoreContext.GetDefault().GetUserCollectionAsync(Constants.IAP_STORE_LIST_ADDON_TYPES);
                Constants.IAP_HIDEADS =
                    AddOns.Products.ContainsKey(Constants.IAP_FEATUREID_ADFREE)
                    || AddOns.Products.ContainsKey(Constants.IAP_FEATUREID_ADFREE_365);
                //Debug_TimeAnalyser.Stop("IAP GetAddons");
            }
            catch (Exception)
            {
                Constants.IAP_HIDEADS = false;
                Model.AppModel.Instance.NewNotification("Error_LoadPurchases");
                Debug_TimeAnalyser.Stop("IAP GetAddons");
            }
            if (!Constants.IAP_HIDEADS)
            {
                //Debug_TimeAnalyser.Start("IAP NoAdsFolder");
                var Info = await SharedIO.CurrentIO.GetFolderInfo(new FileInfoClass(Place.Local, "", SharedIO.CurrentIO.GetCompleteInternPath(Place.Local) + @"noads\"));
                if (Info != null)
                {
                    Constants.IAP_HIDEADS = true;
                }
                //Debug_TimeAnalyser.Stop("IAP NoAdsFolder");
            }
        }

        internal async static Task Buy(string FEATUREID)
        {
            var context = StoreContext.GetDefault();

            StorePurchaseResult result = await context.RequestPurchaseAsync(FEATUREID);

            // Capture the error message for the operation, if any.
            string extendedError = string.Empty;
            if (result.ExtendedError != null)
            {
                extendedError = result.ExtendedError.Message;
            }
            string Text="";
            switch (result.Status)
            {
                case StorePurchaseStatus.AlreadyPurchased:
                    Text= "The user has already purchased the product.";
                    break;

                case StorePurchaseStatus.Succeeded:
                    Text = "The purchase was successful.";
                    break;

                case StorePurchaseStatus.NotPurchased:
                    Text= "The purchase did not complete. " +
                        "The user may have cancelled the purchase. ExtendedError: " + extendedError;
                    break;

                case StorePurchaseStatus.NetworkError:
                    Text= "The purchase was unsuccessful due to a network error. " +
                        "ExtendedError: " + extendedError;
                    break;

                case StorePurchaseStatus.ServerError:
                    Text= "The purchase was unsuccessful due to a server error. " +
                        "ExtendedError: " + extendedError;
                    break;

                default:
                    Text= "The purchase was unsuccessful due to an unknown error. " +
                        "ExtendedError: " + extendedError;
                    break;
            }
            //Model.AppModel.Instance.NewNotification(Text);
            switch (result.Status)
            {
                case StorePurchaseStatus.Succeeded:
                    Model.AppModel.Instance.NewNotification(StringHelper.GetString("IAP_Succeeded"));
                    break;
                case StorePurchaseStatus.NotPurchased:
                    break;
                default:
                    Model.AppModel.Instance.NewNotification(StringHelper.GetString("IAP_Error"));
                    break;
            }
        }
    }
}
