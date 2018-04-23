using System;
using System.Threading.Tasks;
using Windows.ApplicationModel.Store;
using Windows.Services.Store;

namespace ShadowRunHelper
{
    public static class IAP
    {
        public static void CheckLicence()
        {
            var licenseInformation = CurrentAppSimulator.LicenseInformation;
            try
            {
                Constants.IAP_HIDEADS =
                    licenseInformation.ProductLicenses[Constants.IAP_FEATUREID_ADFREE].IsActive
                    || licenseInformation.ProductLicenses[Constants.IAP_FEATUREID_ADFREE_365].IsActive;
            }
            catch (Exception)
            {
                Constants.IAP_HIDEADS = false;
            }
        }

        internal async static Task BuyOLD(string FEATURENAME)
        {
            LicenseInformation licenseInformation = CurrentAppSimulator.LicenseInformation;
            if (!licenseInformation.ProductLicenses[FEATURENAME].IsActive)
            {
                try
                {
                    // The customer doesn't own this feature, so
                    // show the purchase dialog.
                    await CurrentAppSimulator.RequestProductPurchaseAsync(FEATURENAME);

                    //Check the license state to determine if the in-app purchase was successful.
                }
                catch (Exception)
                {
                    // The in-app purchase was not completed because
                    // an error occurred.
                }
            }
            else
            {
                // The customer already owns this feature.
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
                    Text= "The purchase was successful.";
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
            Model.AppModel.Instance.NewNotification(Text);
        }
    }
}
