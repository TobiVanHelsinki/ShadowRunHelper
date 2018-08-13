using System;
using ShadowRunHelper.IO;

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
            NavigationRequested?.Invoke(p, po);
        }

        public void TutorialChangedState(int StateNumber, bool Highlight = false)
        {
            TutorialStateChanged(StateNumber, Highlight);
        }
        public delegate void TutorialStateChangeRequestEventHandler(int StateNumber, bool Highlight);
        public event TutorialStateChangeRequestEventHandler TutorialStateChanged;

        bool _IsUIOperationInProgress;
        public bool IsUIOperationInProgress
        {
            get { return _IsUIOperationInProgress; }
            set { if (_IsUIOperationInProgress != value) { _IsUIOperationInProgress = value; NotifyPropertyChanged(); } }
        }
        bool _IsDisplayingTipp;
        public bool IsDisplayingTip
        {
            get { return _IsDisplayingTipp; }
            set { if (_IsDisplayingTipp != value) { _IsDisplayingTipp = value; NotifyPropertyChanged(); } }
        }

        public void ChangeProgress(bool bHow, bool DisplayTipp)
        {
            IsUIOperationInProgress = bHow;
            IsDisplayingTip = DisplayTipp & bHow;
        }
    }
}
