﻿using ShadowRunHelper.IO;
using TAPPLICATION.Model;
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
                    var T = CharHolderIO.CurrentIO.GetFileInfo(BackUpFile, UserDecision.ThrowError);
                    T.Wait();
                    if (T.Result == null)
                    {
                        System.Diagnostics.Debug.WriteLine("SaveBackUp " + e.ToString());
                        T = CharHolderIO.Save(e, UserDecision.ThrowError, BackUpFile);
                        T.Wait();
                    }
                }
                catch (System.Exception ex)
                {
                    if (System.Diagnostics.Debugger.IsAttached) System.Diagnostics.Debugger.Break();
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
