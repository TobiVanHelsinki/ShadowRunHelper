//Author: Tobi van Helsinki

using ShadowRunHelperViewer.Platform.Xam;
using System;
using System.Globalization;
using System.Resources;
using TLIB;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ShadowRunHelperViewer.Strings
{
    /// <summary>
    /// This Class acts for an base class for any type of dot net resources. it provides an
    /// Markupextension for usage in xamarin forms projects
    /// </summary>
    // You exclude the 'Extension' suffix when using in XAML
    [ContentProperty("Text")]
    public abstract class ResourcesExtension : IMarkupExtension
    {
        private readonly CultureInfo ci = null;
        private readonly ResourceManager ResMgr;

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
        public virtual string Text { get; set; }

        /// <summary>
        /// returns a string from the resource file or null
        /// </summary>
        /// <returns></returns>
        public virtual string ProvideString()
        {
            try
            {
                string translation = ResMgr.GetString(Text, ci);

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
            return null;
        }

        /// <summary>
        /// allways retuns a string that may be present in the resource file or consits of the Text
        /// value and the current language
        /// </summary>
        /// <param name="serviceProvider">can be null</param>
        /// <returns></returns>
        public virtual object ProvideValue(IServiceProvider serviceProvider)
        {
            if (Text == null)
            {
                return string.Empty;
            }

            return ProvideString() ?? Text + "." + ci?.Name;
        }
    }
}