using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
namespace ShadowRunHelper.Model
{


    public class CharViewModel : INotifyPropertyChanged
    {
        //internal TCharState currentState;
        private Model.CharHolder current;
        public Model.CharHolder Current
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
            current = new Model.CharHolder();
            current = null;
            //currentState = TCharState.EMPTY_CHAR;
        }

        public CharViewModel(Model.CharHolder x_current)
        {
            current = x_current;
            //currentState = TCharState.IN_USE;
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
