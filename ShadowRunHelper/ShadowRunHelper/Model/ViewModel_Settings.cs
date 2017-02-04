using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ShadowRunHelper
{
    public class ViewModel_Settings : INotifyPropertyChanged
    {
        public bool SAVE_CHAR_ON_EXIT
        {
            get { return Optionen.bSaveCharOnExit; }
            set
            {
                Optionen.bSaveCharOnExit = value;
                NotifyPropertyChanged();
            }
        }

        public bool LOAD_CHAR_ON_START
        {
            get { return Optionen.bLoadCharOnStart; }
            set
            {
                Optionen.bLoadCharOnStart = value;
                NotifyPropertyChanged();
            }
        }

        public bool ORDNERMODE
        {
            get { return Optionen.bORDNERMODE; }
            set
            {
                Optionen.bORDNERMODE = value;
                NotifyPropertyChanged();
            }
        }

        public string ORDNERMODE_PFAD
        {
            get { return Optionen.strORDNERMODE_PFAD; }
            set
            {
                Optionen.strORDNERMODE_PFAD = value;
                NotifyPropertyChanged();
            }
        }
        public bool bStartEditAfterAdd
        {
            get { return Optionen.bStartEditAfterAdd; }
            set
            {
                Optionen.bStartEditAfterAdd = value;
                NotifyPropertyChanged();
            }
        }

        

        public ViewModel_Settings()
        {

        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
