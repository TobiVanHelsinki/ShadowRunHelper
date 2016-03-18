using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using static ShadowRunHelper.Controller.TApp;
namespace ShadowRunHelper.Model
{
    public class CharViewModel : INotifyPropertyChanged
    {
        public TCharState currentState;
        private Controller.CharHolder current;
        public Controller.CharHolder Current
        {
            get { return this.current; }
            set
            {
                if (value != this.current)
                {
                    this.current = value;
                    NotifyPropertyChanged();
                }
            }
        }

   
        public CharViewModel()
        {
            currentState = TCharState.EMPTY_CHAR;
            current = new Controller.CharHolder();
        }

        public CharViewModel(Controller.CharHolder x_current)
        {
            currentState = TCharState.NEW_CHAR;
            current = x_current;
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
