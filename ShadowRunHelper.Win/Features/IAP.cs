﻿using System;
using System.Threading.Tasks;
using TAMARIN.IO;
using TAPPLICATION.IO;
using TLIB;
using Windows.Foundation.Metadata;
using Windows.Services.Store;

namespace ShadowRunHelper
{
    public static class IAP
    {
        public static async Task CheckLicence(bool force = false)
        {
            if (!ApiInformation.IsApiContractPresent("Windows.Foundation.UniversalApiContract",4))
            {
                Constants.IAP_HIDEADS = true;
                return;
            }

            if (SettingsModel.Instance.START_COUNT % 5 == 0 || force)
            {
                try
                {
                    var AddOns = await StoreContext.GetDefault().GetUserCollectionAsync(Constants.IAP_STORE_LIST_ADDON_TYPES);
                    Constants.IAP_HIDEADS =
                        AddOns.Products.ContainsKey(Constants.IAP_FEATUREID_ADFREE) || 
                        AddOns.Products.ContainsKey(Constants.IAP_FEATUREID_ADFREE_365);
                }
                catch (Exception)
                {
                    Constants.IAP_HIDEADS = false;
                    Model.AppModel.Instance.NewNotification("Error_LoadPurchases");
                    //Debug_TimeAnalyser.Stop("IAP GetAddons");
                }
                SettingsModel.Instance.IAP_HIDEADS = Constants.IAP_HIDEADS;
            }
            else if (SettingsModel.Instance.IAP_HIDEADS)
            {
                Constants.IAP_HIDEADS = true;
            }
            if (!Constants.IAP_HIDEADS)
            {
                var Info = await SharedIO.CurrentIO.GetFolderInfo(new FileInfoClass(Place.Local, "", SharedIO.CurrentIO.GetCompleteInternPath(Place.Local) + @"noads\"), UserDecision.ThrowError);
                if (Info != null)
                {
                    Constants.IAP_HIDEADS = true;
                }
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
            await CheckLicence(true);
        }
    }
}
