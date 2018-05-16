using Newtonsoft.Json;
using ShadowRunHelper.IO;
using System.Collections.Generic;
using TAMARIN.IO;
using TAPPLICATION.IO;
using Windows.UI.Xaml.Controls;

namespace ShadowRunHelper.Win.UI
{
    public sealed partial class ClassicDialog : ContentDialog
    {
        readonly string NewLink = " https://github.com/TobiVanHelsinki/ShadowRunHelper/tree/Classic";
        public ClassicDialog()
        {
            InitializeComponent();
        }

        private async void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            var set = SettingsModel.I.ExportAllSettings();
            var files = new List<string>();
            var List = await CharHolderIO.CurrentIO.GetListofFiles(new FileInfoClass() { Fileplace = CharHolderIO.GetCurrentSavePlace(), Filepath = CharHolderIO.GetCurrentSavePath(), FolderToken = Constants.ACCESSTOKEN_FOLDERMODE }, UserDecision.ThrowError, Constants.LST_FILETYPES_CHAR);
            foreach (var item in List)
            {
                var content = await CharHolderIO.CurrentIO.LoadFileContent(item, null, UserDecision.ThrowError);
                files.Add(content.strFileContent);
            }
            string ser = JsonConvert.SerializeObject((set, files));
            await CharHolderIO.CurrentIO.SaveFileContent(ser, new FileInfoClass(Place.Extern, "ShadowRunHelperAppContent.SRHApp1","" ), UserDecision.AskUser);
        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }
    }
}
