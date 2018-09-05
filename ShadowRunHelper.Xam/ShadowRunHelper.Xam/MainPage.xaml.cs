using ShadowRunHelper.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ShadowRunHelper.Xam
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            CharHolder NewChar = CharHolder.CreateCharWithStandardContent();

            Console.Text = "ThingList.Count: " + NewChar.ThingList.Count;
        }
    }
}
