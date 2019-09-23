using ShadowRunHelperViewer.Platform;
using System;
using System.Globalization;
using System.Resources;
using TLIB;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ShadowRunHelperViewer.Strings
{
    // You exclude the 'Extension' suffix when using in XAML
    [ContentProperty("Text")]
    public class ResourcesExtension : IMarkupExtension
    {
        readonly CultureInfo ci = null;
        readonly ResourceManager ResMgr;
        public ResourcesExtension(ResourceManager resMgr)
        {
            ResMgr = resMgr;
            if (Device.RuntimePlatform == Device.iOS || Device.RuntimePlatform == Device.Android)
            {
                ci = DependencyService.Get<ILocalize>().GetCurrentCultureInfo();
            }
            else
            {
                ci = CultureInfo.CurrentCulture;
            }
        }
        public string Text { get; set; }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if (Text == null)
                return string.Empty;
            try
            {
                var translation = ResMgr.GetString(Text, ci);

                if (translation != null)
                {
                    return translation;
                }
                else
                {
                    Log.Write(string.Format("Key '{0}' was not found in resources '{1}' for culture '{2}'.", Text, ResMgr.BaseName, ci.Name));
                }
            }
            catch (InvalidOperationException ex)
            {
                Log.Write("No Translation for " + Text, ex);
            }
            catch (MissingManifestResourceException ex)
            {
                Log.Write("No Translation for " + Text, ex);
            }
            catch (MissingSatelliteAssemblyException ex)
            {
                Log.Write("No Translation for " + Text, ex);
            }
            return Text + "." + ci?.Name;
        }

    }
}