using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ShadowRun_Charakter_Helper.CharModel
{
    public class Model
    {
        public int dicCD_ID;
        public int DicCD_ID
        {
            get { return dicCD_ID; }
            set
            {
                if (value != this.dicCD_ID)
                {
                    this.dicCD_ID = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public String bezeichner;
        public String Bezeichner
        {
            get { return bezeichner; }
            set
            {
                if (value != this.bezeichner)
                {
                    this.bezeichner = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public String Typ { get; set; }
        public int Wert { get; set; }
        public String Zusatz { get; set; }
        public String Notiz { get; set; }

        public Model()
        {

        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

    }


}
