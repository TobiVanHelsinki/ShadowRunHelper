using ShadowRunHelper;
using ShadowRunHelper.IO;
using ShadowRunHelper.Model;
using System;
using TAPPLICATION.IO;
using Xamarin.Forms;

namespace ShadowRunHelperViewer
{
    public partial class MainPage : ContentPage
    {
        public AppModel AppModel => AppModel.Instance;
        public MainPage()
        {
            //AppModel.MainObject = CharHolderGenerator.CreateCharWithStandardContent();
            //AppModel.MainObject = CharHolderGenerator.CreateEmtpyChar();
            //AppModel.MainObject = CharHolderGenerator.TestAllCats(15);
            InitializeComponent();
            BindingContext = this;
        }

        private void Toggle(object sender, EventArgs e)
        {
            if (CharView.StaticOpen is bool b)
            {
                CharView.StaticOpen = !b;
            }
        }

        private void Settings(object sender, System.EventArgs e)
        {

        }

        async void Administration(object sender, EventArgs e)
        {
            try
            {
                var File = await SharedIO.CurrentIO.PickFile(Constants.LST_FILETYPES_CHAR, "NextChar");
                AppModel.Instance.MainObject = await CharHolderIO.Load(File);
            }
            catch (Exception)
            {
            }
        }

        private void CharPage(object sender, System.EventArgs e)
        {

        }

        private void MoreMenu(object sender, System.EventArgs e)
        {
            //PopupMenu menu = new PopupMenu(this, showPopupMenu);
            //menu.Inflate(Resource.Menu.popup_menu);
            //menu.Show();
        }

        private void Save(object sender, System.EventArgs e)
        {

        }
    }
}
