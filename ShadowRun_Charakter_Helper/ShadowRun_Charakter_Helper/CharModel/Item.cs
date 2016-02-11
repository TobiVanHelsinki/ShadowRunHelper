using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ShadowRun_Charakter_Helper.CharModel
{
    public class Item : CharModel.Model
    {
        public int besitz;
        public int Besitz
        {
            get { return besitz; }
            set
            {
                if (value != this.besitz)
                {
                    this.besitz = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public int aktiv;
        public int Aktiv
        {
            get { return aktiv; }
            set
            {
                if (value != this.aktiv)
                {
                    this.aktiv = value;
                    NotifyPropertyChanged();
                }
            }
        }
    }
}
