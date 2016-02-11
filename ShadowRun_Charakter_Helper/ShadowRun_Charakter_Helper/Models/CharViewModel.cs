using ShadowRun_Charakter_Helper.Controller;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ShadowRun_Charakter_Helper.Models
{
    public class CharViewModel : INotifyPropertyChanged
    {
        private Char defaultChar = new Char();
        public Char DefaultChar
        {
            get { return this.defaultChar; }
            set
            {
                if (value != this.defaultChar)
                {
                    this.defaultChar = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private Controller.CharHolder current = new Controller.CharHolder();
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
