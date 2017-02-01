﻿using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
namespace ShadowRunHelper.Model
{


    public class CharViewModel : INotifyPropertyChanged
    {
        internal TCharState currentState;
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
            currentState = TCharState.EMPTY_CHAR;
            current = new Model.CharHolder();
        }

        public CharViewModel(Model.CharHolder x_current)
        {
            currentState = TCharState.IN_USE;
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
