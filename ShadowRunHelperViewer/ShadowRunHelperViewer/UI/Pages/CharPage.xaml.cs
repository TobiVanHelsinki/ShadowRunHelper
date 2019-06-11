using ShadowRunHelper.Model;
using System.Threading;
using Xamarin.Forms;

namespace ShadowRunHelperViewer
{
    public partial class CharPage : ContentView
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
            }
        }
    }
}
