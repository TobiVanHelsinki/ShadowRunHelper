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
        public CharHolder CurrentChar { get; set; }
        public MainPage()
        {
            InitializeComponent();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            CurrentChar = CharHolder.CreateCharWithStandardContent();


        }

        private void Ui_Nav_Char(object sender, EventArgs e)
        {

        }

        private void Ui_Nav_Admin(object sender, EventArgs e)
        {

        }

        private void Ui_Nav_Settings(object sender, EventArgs e)
        {

        }

        private void CreateDebugChar(object sender, EventArgs e)
        {

        }
    }
}
