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

        public void Activate(CharHolder myChar)
        {
            if (Content is Grid G)
            {
                G.Children.Add(new GCharHolder(myChar) { Margin = 0, Padding = 0, BackgroundColor = Color.Transparent });
                AppModel.AddMainObject(myChar);
            }
        }

        /// <summary>
        /// return true if handled
        /// </summary>
        /// <returns></returns>
        internal bool OnBackButtonPressed()
        {
            if (Content is Grid g && g.Children.Count > 0  && g.Children[0] is GCharHolder currentchar)
            {
                currentchar.MenuOpen = true;
                return true;
            }
            return false;
        }

        public void Dispose()
        {
            if (Content is Grid G && G.Children.Count > 0 && G.Children[0] is GCharHolder gChar)
            {
                AppModel.RemoveMainObject(gChar.MyChar);
            }
        }
    }
}
