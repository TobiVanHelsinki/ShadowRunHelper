using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
namespace ShadowRunHelper.Model
{
    public sealed class ViewModel : INotifyPropertyChanged
    {
        static readonly ViewModel instance = new ViewModel();

        ViewModel()
        {
            lstNotifications = new ObservableCollection<Notification>();
        }

        public static ViewModel Instance
            {
                get
                {
                    return instance;
                }
            }
        CharHolder _currentChar;
        public CharHolder CurrentChar
        {
            get { return this._currentChar; }
            set
            {
                if (value != this._currentChar)
                {
                    this._currentChar = value;
                    NotifyPropertyChanged();
                }
            }
        }

        CharHolder _PreDBChar;
        public CharHolder PreDBChar
        {
            get { return this._PreDBChar; }
            set
            {
                if (value != this._PreDBChar)
                {
                    this._PreDBChar = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public ObservableCollection<Notification> lstNotifications;
        
        ProjectPages CurrentPageInProgress = ProjectPages.undef;
        public void RequestedNavigation(ProjectPages ePp, object oO = null)
        {
            if (CurrentPageInProgress == ePp)
            {
                return;
            }
            CurrentPageInProgress = ePp;
            NavigationRequested?.Invoke(oO, ePp);
            CurrentPageInProgress = ProjectPages.undef;
        }

        public event EventHandler<ProjectPages> NavigationRequested;

        public event PropertyChangedEventHandler PropertyChanged;
        void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
