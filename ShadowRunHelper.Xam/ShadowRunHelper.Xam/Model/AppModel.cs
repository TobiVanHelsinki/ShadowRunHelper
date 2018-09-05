using System;
using System.Collections.Generic;
using ShadowRunHelper.IO;
using TAMARIN.IO;

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
            Instance.MainObjectSaved += (x, y) => { SettingsModel.I.COUNT_SAVINGS++; };

            return Instance;
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
                if (y.PropertyName == nameof(MainObject) && MainObject != null) Activities.GenerateCharActivityAsync(MainObject);
                if (y.PropertyName == nameof(MainObject) && MainObject == null) Activities.StopCurrentCharActivity();
            };
            }
        ~AppModel() 
        {
            Activities.StopCurrentCharActivity();
        }

public delegate void NavigationEventHandler(ProjectPages page, ProjectPagesOptions PageOptions);
        public event NavigationEventHandler NavigationRequested;
        public void RequestNavigation(ProjectPages p, ProjectPagesOptions po = ProjectPagesOptions.Nothing)
        {
            DispatcherHelper.ExecuteOnUIThreadAsync(()=>NavigationRequested?.Invoke(p, po));
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
