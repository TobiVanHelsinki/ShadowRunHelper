using PCLStorage;
using ShadowRunHelper;
using ShadowRunHelper.IO;
using ShadowRunHelper.Model;
using System;
using System.Linq;
using TAPPLICATION.IO;
using Xamarin.Forms;

namespace ShadowRunHelperViewer
{
    public partial class MainPage : ContentPage
    {
        public AppModel AppModel => AppModel.Instance;
        public MainPage()
        {
            InitializeComponent();
            BindingContext = this;
            //AppModel.MainObject = CharHolderGenerator.CreateCharWithStandardContent();
            //AppModel.MainObject = CharHolderGenerator.CreateEmtpyChar();
            AppModel.MainObject = CharHolderGenerator.TestAllCats(3);
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

        async void Settings(object sender, EventArgs e)
        {
            var rootFolder = FileSystem.Current.RoamingStorage;
            foreach (var item in (await rootFolder.GetFilesAsync()))
            {
                System.Console.WriteLine(item.Path);
            }
            var folder = await rootFolder.CreateFolderAsync("MySubFolder", CreationCollisionOption.OpenIfExists);
            var file = await folder.CreateFileAsync("answer.txt", CreationCollisionOption.ReplaceExisting);
            await file.WriteAllTextAsync("42");
        }
    }
}
