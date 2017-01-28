using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using static ShadowRunHelper.Controller.TApp;
namespace ShadowRunHelper.Model
{
    public enum ThingDefs
    {
        Handlung = 1,
        Fertigkeit = 2,
        Item = 3,
        Programm = 4,
        Munition = 5,
        Implantat = 6,
        Vorteil = 7,
        Nachteil = 8,
        Connection = 9,
        Sin = 10,
        Attribut = 11,
        Nahkampfwaffe = 12,
        Fernkampfwaffe = 13,
        Kommlink = 14,
        CyberDeck = 15,
        Vehikel = 16,
        Panzerung = 17

    }

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
