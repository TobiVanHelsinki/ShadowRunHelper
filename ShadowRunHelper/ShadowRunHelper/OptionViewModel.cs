using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ShadowRun_Charakter_Helper
{
    public class OptionViewModel : INotifyPropertyChanged
    {
        public bool Save_char_on_exit
        {
            get { return Optionen.SAVE_CHAR_ON_EXIT(); }
            set
            {
                Optionen.SAVE_CHAR_ON_EXIT(value);
                NotifyPropertyChanged();
            }
        }

        public OptionViewModel()
        {
            Save_char_on_exit = true;
        }

        public OptionViewModel(bool save_char_on_exit)
        {
            Save_char_on_exit = save_char_on_exit;
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
