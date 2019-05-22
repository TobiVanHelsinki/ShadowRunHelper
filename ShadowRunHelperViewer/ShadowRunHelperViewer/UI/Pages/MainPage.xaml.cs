using PCLStorage;
using ShadowRunHelper.Model;
using System.Linq;
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

        async void Button_Clicked(object sender, System.EventArgs e)
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
