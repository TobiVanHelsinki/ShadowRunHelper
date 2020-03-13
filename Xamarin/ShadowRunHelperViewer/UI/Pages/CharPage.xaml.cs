///Author: Tobi van Helsinki

using ShadowRunHelper.Model;
using System;
using System.Threading;
using Xamarin.Forms;

namespace ShadowRunHelperViewer
{
    public partial class CharPage : ContentView, IDisposable
    {
        public AppModel AppModel => AppModel.Instance;

        public CharPage()
        {
            InitializeComponent();
            BindingContext = this;
            Thread.Sleep(50); //Enable Waiting INdicator
        }

        public virtual void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool t)
        {
            if (Content is Grid g && g.Children.Count > 0 && g.Children[0] is IDisposable currentchar)
            {
                currentchar.Dispose();
            }
        }

        public void Activate(CharHolder myChar)
        {
            if (Content is Grid G)
            {
                G.Children.Add(new GCharHolder(myChar) { Margin = 0, Padding = 0, BackgroundColor = Color.Transparent });
            }
        }

        /// <summary>
        /// return true if handled
        /// </summary>
        /// <returns></returns>
        internal bool OnBackButtonPressed()
        {
            if (Content is Grid g && g.Children.Count > 0 && g.Children[0] is GCharHolder currentchar)
            {
                return currentchar.OnBackButtonPressed();
            }
            return false;
        }
    }
}