using ShadowRunHelper;
using ShadowRunHelper.IO;
using ShadowRunHelper.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TLIB;
using Xamarin.Forms;

namespace ShadowRunHelperViewer
{
    public partial class MainPage : ContentPage
    {
        public AppModel AppModel => AppModel.Instance;
        public MainPage()
        {
            AppModel.MainObject = CharHolder.CreateCharWithStandardContent();
            InitializeComponent();
            BindingContext = this;
        }

        async void ChooseFile(object sender, EventArgs e)
        {
            try
            {
                AppModel.MainObject = await CharHolderIO.Load(new FileInfoClass(Place.Extern, "", ""), Constants.LST_FILETYPES_CHAR);
            }
            catch (Exception)
            {
            }
        }
    }
}
