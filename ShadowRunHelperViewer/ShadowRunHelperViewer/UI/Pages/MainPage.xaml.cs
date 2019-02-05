using Rg.Plugins.Popup.Services;
using ShadowRunHelper;
using ShadowRunHelper.IO;
using ShadowRunHelper.Model;
using System;
using System.Linq;
using TAPPLICATION.IO;
using Xam.Plugin;
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
        (string, Action)[] MenuItems = new (string, Action)[] {
                        (CustomManager.GetString("UI_TxT_SaveAtCurrentPlace/Text"),null),
                        (CustomManager.GetString("UI_TxT_SaveExtern/Text"),null),
                        (CustomManager.GetString("UI_TxT_OpenFolder/Text"),null),
                        (CustomManager.GetString("UI_TxT_SubtractLifeStyleCost/Text"),null),
                        (CustomManager.GetString("UI_TxT_CharSettings/Text"),null),
                        (CustomManager.GetString("UI_TxT_Repair/Text"),null),
                        (CustomManager.GetString("UI_TxT_Unload/Text"),Unload),
                    };

        private static void Unload()
        {
        }

        void MoreMenu(object sender, System.EventArgs e)
        {
            try
            {
                PopupMenu Popup = new PopupMenu
                {
                    ItemsSource = MenuItems.Select(x => x.Item1).ToArray()
                };
                Popup.OnItemSelected += Popup_OnItemSelected;
                Popup?.ShowPopup(sender as Button);

                //https://baskren.github.io/Forms9Patch/guides/GettingStartedWindows.html
                //https://docs.microsoft.com/de-de/xamarin/xamarin-forms/app-fundamentals/navigation/pop-ups
            }
            catch (Exception)
            {
            }
        }

        private void Popup_OnItemSelected(string item)
        {
            MenuItems.FirstOrDefault(x => x.Item1 == item).Item2?.Invoke();
        }

        private void Save(object sender, System.EventArgs e)
        {

        }
    }
}
