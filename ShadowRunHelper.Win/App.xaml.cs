using Windows.ApplicationModel;
using Windows.UI.Xaml;

namespace ShadowRunHelper
{
    sealed partial class App : Application
    {
        public App()
        {
            UnhandledException += async (x, y) => { await AppHolder.App_UnhandledExceptionAsync(x, y); };

            EnteredBackground += App_EnteredBackground;
            LeavingBackground += App_LeavingBackground;
            InitializeComponent();
            AppHolder.Init();
        }

        //#region Entry-Points
        //protected override async void OnActivated(IActivatedEventArgs args)
        //{
        //    Model.RequestNavigation(ProjectPages.Char, ProjectPagesOptions.Char_Action);

        //    //Debug_TimeAnalyser.Start("Entry Protocol");
        //    if (args.Kind == ActivationKind.Protocol && args is ProtocolActivatedEventArgs uriArgs)
        //    {
        //        Settings.FORCE_LOAD_CHAR_ON_START = true;
        //        string name = uriArgs.Uri.Segments[uriArgs.Uri.Segments.Length - 1];
        //        string path = uriArgs.Uri.LocalPath.Remove(uriArgs.Uri.LocalPath.Length - name.Length);
        //        name = name.Remove(name.Length - 1);
        //        Settings.LAST_SAVE_INFO = new FileInfoClass(Place.Extern, name, path)
        //        {
        //            Token = SharedConstants.ACCESSTOKEN_FILEACTIVATED
        //        };
        //    }
        //    if (!FirstStart)
        //    {
        //        await CharLoadingHandling();
        //        rootFrame.Navigate(typeof(MainPage));

        //        Model.RequestNavigation(ProjectPages.Char, ProjectPagesOptions.Char_Action);
        //    }
        //    //Debug_TimeAnalyser.Stop("Entry Protocol");
        //}
        //protected override async void OnFileActivated(FileActivatedEventArgs args)
        //{
        //    if (args.Files[0].Name.EndsWith(".SRHChar"))
        //    {
        //        try
        //        {
        //            Windows.Storage.AccessCache.StorageApplicationPermissions.FutureAccessList.AddOrReplace(SharedConstants.ACCESSTOKEN_FILEACTIVATED, args.Files[0]);
        //        }
        //        catch (Exception ex)
        //        {
        //        }
        //        Settings.FORCE_LOAD_CHAR_ON_START = true;
        //        var info = new FileInfoClass(Place.Extern, args.Files[0].Name, args.Files[0].Path.Substring(0, args.Files[0].Path.Length - args.Files[0].Name.Length))
        //        {
        //            Token = SharedConstants.ACCESSTOKEN_FILEACTIVATED
        //        };
        //        Settings.LAST_SAVE_INFO = info;
        //        if (!FirstStart)
        //        {
        //            await CharLoadingHandling();
        //            Model.RequestNavigation(ProjectPages.Char, ProjectPagesOptions.Char_Action);
        //        }
        //    }
        //    else if (args.Files[0].Name.EndsWith(".SRHApp1"))
        //    {
        //        AppDataPorter.Loading = AppDataPorter.LoadAppPacket(args.Files[0]);
        //    }
        //    if (!FirstStart)
        //    {
        //        Model.RequestNavigation(ProjectPages.Administration, ProjectPagesOptions.Import);
        //    }
        //}
        //#endregion

        void App_LeavingBackground(object sender, LeavingBackgroundEventArgs e)
        {
            var def = e.GetDeferral();
            AppHolder.LeavingBackground();
            def.Complete();
        }


        async void App_EnteredBackground(object sender, EnteredBackgroundEventArgs e)
        {
            var def = e.GetDeferral();
            AppHolder.EnteredBackground();
            def.Complete();
        }
    }
}
