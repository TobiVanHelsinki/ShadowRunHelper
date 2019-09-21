using ShadowRunHelperViewer.Platform;
using System;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Resources;
using Xamarin.Forms;

namespace ShadowRunHelperViewer
{
    public class L10n
    {
        const string ResourceId = "SharedCode.Ressourcen.Strings";

        static readonly Lazy<ResourceManager> ResMgr = new Lazy<ResourceManager>(() => new ResourceManager(ResourceId, IntrospectionExtensions.GetTypeInfo(typeof(SharedCode.Ressourcen.Strings)).Assembly));

        public static void SetLocale(CultureInfo ci)
        {
            DependencyService.Get<ILocalize>().SetLocale(ci);
        }

        /// <remarks>
        /// Maybe we can cache this info rather than querying every time
        /// </remarks>
        [Obsolete]
        public static string Locale()
        {
            return DependencyService.Get<ILocalize>().GetCurrentCultureInfo().ToString();
        }

        public static string Localize(string key, string comment)
        {
            //var netLanguage = Locale ();

            // Platform-specific
            Debug.WriteLine("Localize " + key);
            ILocalize locale = DependencyService.Get<ILocalize>();
            CultureInfo culture = locale.GetCurrentCultureInfo();
            string result = "";
            try
            {
                result = ResMgr.Value.GetString(key, culture);
            }
            catch (Exception)
            {
                if (System.Diagnostics.Debugger.IsAttached) System.Diagnostics.Debugger.Break();
            }

            if (result == null)
            {
                result = key; // HACK: return the key, which GETS displayed to the user
            }
            return result;
        }
    }
}
