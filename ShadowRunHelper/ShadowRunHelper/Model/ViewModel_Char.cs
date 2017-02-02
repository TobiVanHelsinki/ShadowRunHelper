using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
namespace ShadowRunHelper.Model
{
    public class ViewModel_Char : INotifyPropertyChanged
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

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
