using ShadowRunHelper.CharModel;
using ShadowRunHelper.IO;
using TAPPLICATION.Model;
using TLIB;

namespace ShadowRunHelper.Model
{
    public class AppModel : SharedAppModel<CharHolder>
    {
        public static AppModel Initialize()
        {
            if (instance == null)
            {
                instance = new AppModel();
            }
            CharHolderIO.MainTypeConvert = CharHolderIO.ConvertWithRightVersion;
            Instance.MainObjectSaved += Instance_MainObjectSaved;
            return Instance;
        }

        static void Instance_MainObjectSaved(object sender, IMainType e)
        {
            System.Diagnostics.Debug.WriteLine("Instance_MainObjectSaved");
            SettingsModel.I.COUNT_SAVINGS++;
            if (SettingsModel.I.BACKUP_VERSIONING)
            {
                var FileName = (e as CharHolder)?.MakeName(true);
                string BackUpFolderName = @"\BackUp";
                var BackUpFile = new FileInfoClass(CharHolderIO.GetCurrentSavePlace(), FileName, CharHolderIO.GetCurrentSavePath()+BackUpFolderName);
                try
                {
                    //var T = CharHolderIO.CurrentIO.GetFileInfo(BackUpFile, UserDecision.ThrowError);
                    //T.Wait();
                    if (/*T.Result == null*/ BackUpFile.SystemFileInfo?.Exists != true)
                    {
                        System.Diagnostics.Debug.WriteLine("SaveBackUp " + e.ToString());
                        var T = CharHolderIO.Save(e, UserDecision.ThrowError, BackUpFile);
                        T.Wait();
                    }
                }
                catch (System.Exception)
                {
                    if (System.Diagnostics.Debugger.IsAttached) System.Diagnostics.Debugger.Break();
                }

            }
        }

        public static new AppModel Instance
        {
            get
            {
                return instance as AppModel;
            }
        }

        AppModel() : base()
        {
            PropertyChanged += AppModel_PropertyChanged;
        }

        private void AppModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(MainObject):
                    if (MainObject != null)
                    {
                        Features.Activities.GenerateCharActivityAsync(MainObject);
                    }
                    else
                    {
                        Features.Activities.StopCurrentCharActivity();
                    }
                    break;
                default:
                    break;
            }
        }

        ~AppModel() 
        {
            Features.Activities.StopCurrentCharActivity();
        }

        public delegate void NavigationEventHandler(ProjectPages page, ProjectPagesOptions PageOptions);
        public event NavigationEventHandler NavigationRequested;
        public void RequestNavigation(ProjectPages p, ProjectPagesOptions po = ProjectPagesOptions.Nothing)
        {
            PlatformHelper.ExecuteOnUIThreadAsync(()=>NavigationRequested?.Invoke(p, po));
        }

        public void TutorialChangedState(int StateNumber, bool Highlight = false)
        {
            TutorialStateChanged(StateNumber, Highlight);
        }
        public delegate void TutorialStateChangeRequestEventHandler(int StateNumber, bool Highlight);
        public event TutorialStateChangeRequestEventHandler TutorialStateChanged;

        public bool IsCharInProgress
        {
            get { return CharInProgress != null; }
        }
        FileInfoClass _CharInProgress;
        public FileInfoClass CharInProgress
        {
            get { return _CharInProgress; }
            set { if (_CharInProgress != value) { _CharInProgress = value; NotifyPropertyChanged(nameof(IsCharInProgress)); NotifyPropertyChanged(); } }
        }
        Thing _PendingScrollEntry;
        public Thing PendingScrollEntry
        {
            get { return _PendingScrollEntry; }
            set { if (_PendingScrollEntry != value) { _PendingScrollEntry = value; NotifyPropertyChanged(); } }
        }


    }
}
