using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
namespace ShadowRunHelper.Model
{


    public class CharViewModel : INotifyPropertyChanged
    {
        internal TCharState currentState;
        private Model.CharHolder _CurrentChar;
        public Model.CharHolder CurrentChar
        {
            get { return this._CurrentChar; }
            set
            {
                if (value != this._CurrentChar)
                {
                    this._CurrentChar = value;
                    NotifyPropertyChanged();
                }
            }
        }

   
        public CharViewModel()
        {
            currentState = TCharState.EMPTY;
            _CurrentChar = new Model.CharHolder();
        }

        public CharViewModel(Model.CharHolder x_current)
        {
            currentState = TCharState.IN_USE;
            _CurrentChar = x_current;
        }



        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        
        
    }
}
