using ShadowRunHelper.IO;
using TLIB.IO;
using TLIB.PlatformHelper;

namespace ShadowRunHelper.Model
{
    public class AppModel : TAPPLICATION.Model.SharedAppModel<CharHolder>
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

        static async void Instance_MainObjectSaved(object sender, System.EventArgs e)
        {
            SettingsModel.I.COUNT_SAVINGS++;
            if (SettingsModel.I.BACKUP_VERSIONING)
            {
                var löschen = SettingsModel.I.FILENAME_USEDATE;
                var FileName = Instance.MainObject.MakeName(true, false);
                var Folder = new FileInfoClass(CharHolderIO.GetCurrentSavePlace(), FileName, CharHolderIO.GetCurrentSavePath()+@"\BackUp");
                try
                {
                    if (await CharHolderIO.CurrentIO.GetFileInfo(Folder, UserDecision.ThrowError) == null)
                    {
                        CharHolderIO.Save(Instance.MainObject, UserDecision.ThrowError, TAPPLICATION.IO.SaveType.Auto, Folder);
                    }
                }
                catch (System.Exception)
                {
                }

            }
        }

        public static new AppModel Instance
        {
            get
            {
                return (AppModel)instance;
            }
        }

        AppModel() : base()
        {
            PropertyChanged += (x,y)=> {
                if (y.PropertyName == nameof(MainObject) && MainObject != null) Features.Activities.GenerateCharActivityAsync(MainObject);
                if (y.PropertyName == nameof(MainObject) && MainObject == null) Features.Activities.StopCurrentCharActivity();
            };
            }
        ~AppModel() 
        {
            Features.Activities.StopCurrentCharActivity();
        }

public delegate void NavigationEventHandler(ProjectPages page, ProjectPagesOptions PageOptions);
        public event NavigationEventHandler NavigationRequested;
        public void RequestNavigation(ProjectPages p, ProjectPagesOptions po = ProjectPagesOptions.Nothing)
        {
            ModelHelper.ExecuteOnUIThreadAsync(()=>NavigationRequested?.Invoke(p, po));
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
    }
}
