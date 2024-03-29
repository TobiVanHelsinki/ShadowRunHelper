﻿//Author: Tobi van Helsinki


using ShadowRunHelper.CharModel;
using ShadowRunHelper.Helper;
using ShadowRunHelper.IO;
using System;
using System.IO;
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

        private static void Instance_MainObjectSaved(object sender, IMainType e)
        {
            Log.Write("MainObjectSaved");
            SettingsModel.I.COUNT_SAVINGS++;
            if (SettingsModel.I.BACKUP_VERSIONING)
            {
                string FileName = (e as CharHolder)?.MakeName(true);
                try
                {
                    DirectoryInfo BackUpFolder = new DirectoryInfo(Path.Combine(SharedIO.CurrentSavePath, "BackUp"));
                    SharedIO.CurrentIO.CreateFolder(BackUpFolder).Wait();
                    FileInfo BackUpFile = new FileInfo(Path.Combine(BackUpFolder.FullName, FileName));
                    SharedIO.Save(e, BackUpFile).Wait();
                }
                catch (Exception ex)
                {
                    Log.Write("Could not save BackUpChar", ex, logType: LogType.Error);
                }
            }
        }

        public static new AppModel Instance => instance as AppModel;

        private AppModel() : base()
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
                        Features.Activities?.GenerateCharActivityAsync(MainObject);
                    }
                    else
                    {
                        Features.Activities?.StopCurrentCharActivity();
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
            System.Diagnostics.Debug.WriteLine("                           AppModelRequest");
            PlatformHelper.ExecuteOnUIThreadAsync(() => NavigationRequested?.Invoke(p, po));
        }

        [Obsolete]
        public void TutorialChangedState(int StateNumber, bool Highlight = false)
        {
            TutorialStateChanged(StateNumber, Highlight);
        }

        public delegate void TutorialStateChangeRequestEventHandler(int StateNumber, bool Highlight);
        public event TutorialStateChangeRequestEventHandler TutorialStateChanged;

        public bool IsCharInProgress => CharInProgress != null;

        private FileInfo _CharInProgress; // TODO new Datastructure is needed
        public FileInfo CharInProgress
        {
            get => _CharInProgress;
            set { if (_CharInProgress != value) { _CharInProgress = value; NotifyPropertyChanged(nameof(IsCharInProgress)); NotifyPropertyChanged(); } }
        }

        private Thing _PendingScrollEntry;
        public Thing PendingScrollEntry
        {
            get => _PendingScrollEntry;
            set { if (_PendingScrollEntry != value) { _PendingScrollEntry = value; NotifyPropertyChanged(); } }
        }

        public bool IsFileActivated { get; set; }
    }
}