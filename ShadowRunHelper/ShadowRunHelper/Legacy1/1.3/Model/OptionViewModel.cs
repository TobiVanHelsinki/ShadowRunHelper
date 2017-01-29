using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ShadowRunHelper1_3
{
    public class OptionViewModel : INotifyPropertyChanged
    {
        public bool SAVE_CHAR_ON_EXIT
        {
            get { return Optionen.SAVE_CHAR_ON_EXIT; }
            set
            {
                Optionen.SAVE_CHAR_ON_EXIT = value;
                NotifyPropertyChanged();
            }
        }

        public bool LOAD_CHAR_ON_START
        {
            get { return Optionen.LOAD_CHAR_ON_START; }
            set
            {
                Optionen.LOAD_CHAR_ON_START = value;
                NotifyPropertyChanged();
            }
        }

        public bool ORDNERMODE
        {
            get { return Optionen.ORDNERMODE; }
            set
            {
                Optionen.ORDNERMODE = value;
                NotifyPropertyChanged();
            }
        }

        public string ORDNERMODE_PFAD
        {
            get { return Optionen.ORDNERMODE_PFAD; }
            set
            {
                Optionen.ORDNERMODE_PFAD = value;
                NotifyPropertyChanged();
            }
        }

        public OptionViewModel()
        {

        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
