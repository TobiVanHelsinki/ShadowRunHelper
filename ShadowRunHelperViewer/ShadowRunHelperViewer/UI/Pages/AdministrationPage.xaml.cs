using ShadowRunHelper;
using ShadowRunHelper.IO;
using System;
using TAPPLICATION.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ShadowRunHelperViewer.UI.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AdministrationPage : ContentView
    {
        public AdministrationPage()
        {
            InitializeComponent();
        }

        async void LoadChar()
        {
            try
            {
                var File = await SharedIO.CurrentIO.PickFile(Constants.LST_FILETYPES_CHAR, "NextChar");
                var Char = await CharHolderIO.Load(File);
                if (Application.Current.MainPage is MainPage MP)
                {
                    MP.NavigatoToSingleInstanceOf<CharPage>(true, (x) => x.Activate(Char));
                }
            }
            catch (Exception)
            {
            }
        }


    }
}