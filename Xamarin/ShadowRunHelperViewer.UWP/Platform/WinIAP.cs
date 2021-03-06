﻿//Author: Tobi van Helsinki

using ShadowRunHelper;
using ShadowRunHelper.IO;
using SharedCode.Resources;
using System;
using System.IO;
using System.Threading.Tasks;
using TLIB;
using Windows.Foundation.Metadata;
using Windows.Services.Store;

namespace ShadowRunHelperViewer.Platform.UWP
{
    public class WinIAP : IIAP
    {
        public async Task CheckLicence(bool force = false)
        {
            if (!ApiInformation.IsApiContractPresent("Windows.Foundation.UniversalApiContract", 4))
            {
                Constants.IAP_HIDEADS = true;
                return;
            }

            if (SettingsModel.Instance.START_COUNT % 5 == 0 || force)
            {
                try
                {
                    StoreProductQueryResult AddOns = await StoreContext.GetDefault().GetUserCollectionAsync(Constants.IAP_STORE_LIST_ADDON_TYPES);
                    Constants.IAP_HIDEADS =
                        AddOns.Products.ContainsKey(Constants.IAP_FEATUREID_ADFREE) ||
                        AddOns.Products.ContainsKey(Constants.IAP_FEATUREID_ADFREE_365);
                }
                catch (Exception ex)
                {
                    Log.Write("Could not Get IAP", ex, logType: LogType.Error);
                    Constants.IAP_HIDEADS = false;
                }
                SettingsModel.Instance.IAP_HIDEADS = Constants.IAP_HIDEADS;
            }
            else if (SettingsModel.Instance.IAP_HIDEADS)
            {
                Constants.IAP_HIDEADS = true;
            }
            if (!Constants.IAP_HIDEADS)
            {
                bool b = new FileInfo(SharedIO.CurrentIO.GetCompleteInternPath(Place.Local) + @"noads\").Exists;
                if (b)
                {
                    Constants.IAP_HIDEADS = true;
                }
                //var Info = await SharedIO.CurrentIO.GetFolderInfo(new FileInfo(Place.Local, "", SharedIO.CurrentIO.GetCompleteInternPath(Place.Local) + @"noads\"), UserDecision.ThrowError);
                //if (Info != null)
                //{
                //    Constants.IAP_HIDEADS = true;
                //}
            }
        }

        public async Task Buy(string FEATUREID)
        {
            StoreContext context = StoreContext.GetDefault();

            StorePurchaseResult result = await context.RequestPurchaseAsync(FEATUREID);

            // Capture the error message for the operation, if any.
            string extendedError = string.Empty;
            if (result.ExtendedError != null)
            {
                extendedError = result.ExtendedError.Message;
            }
            string Text = "";
            switch (result.Status)
            {
                case StorePurchaseStatus.AlreadyPurchased:
                    Text = "The user has already purchased the product.";
                    break;

                case StorePurchaseStatus.Succeeded:
                    Text = "The purchase was successful.";
                    if (FEATUREID == Constants.IAP_FEATUREID_TEE)
                    {
                        SettingsModel.I.IAP_PREMIUM_BADGE = true;
                    }
                    break;

                case StorePurchaseStatus.NotPurchased:
                    Text = "The purchase did not complete. " +
                        "The user may have cancelled the purchase. ExtendedError: " + extendedError;
                    break;

                case StorePurchaseStatus.NetworkError:
                    Text = "The purchase was unsuccessful due to a network error. " +
                        "ExtendedError: " + extendedError;
                    break;

                case StorePurchaseStatus.ServerError:
                    Text = "The purchase was unsuccessful due to a server error. " +
                        "ExtendedError: " + extendedError;
                    break;

                default:
                    Text = "The purchase was unsuccessful due to an unknown error. " +
                        "ExtendedError: " + extendedError;
                    break;
            }
            switch (result.Status)
            {
                case StorePurchaseStatus.Succeeded:
                    Log.Write(AppResources.IAP_Succeeded);
                    break;
                case StorePurchaseStatus.NotPurchased:
                    break;
                default:
                    Log.Write(AppResources.IAP_Error, logType: LogType.Error);
                    break;
            }
            await CheckLicence(true);
        }
    }
}