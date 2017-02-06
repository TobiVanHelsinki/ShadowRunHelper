using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
namespace ShadowRunHelper.Model
{
    public class ViewModel : INotifyPropertyChanged
    {
        private CharHolder _currentChar;
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

        public ObservableCollection<Notification> lstNotifications;

        public ViewModel()
        {
            lstNotifications = new ObservableCollection<Notification>();
        }
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
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
