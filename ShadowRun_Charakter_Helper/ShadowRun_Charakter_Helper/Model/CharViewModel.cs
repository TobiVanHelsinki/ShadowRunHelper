using ShadowRun_Charakter_Helper.Controller;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ShadowRun_Charakter_Helper.Model
{
    public class CharViewModel : INotifyPropertyChanged
    {
        private Char defaultChar;
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
            defaultChar = new Char();
            current = new Controller.CharHolder();
            //todo entferne testdaten
            //Current.HandlungController.Add(new CharController.Handlung(Current.HD));
            //Current.HandlungController.Add(new CharController.Handlung(Current.HD));
            //Current.HandlungController.Add(new CharController.Handlung(Current.HD));
            //Current.HandlungController[0].Data.Bezeichner = "Wahrnehmung";
            //Current.HandlungController[0].Data.Wert = 15;
            //Current.HandlungController[0].Data.Typ="Handlung";
            //Current.HandlungController[1].Data.Bezeichner = "Daisy steuern";
            //Current.HandlungController[1].Data.Wert = 5;
            //Current.HandlungController[1].Data.Typ = "Handlung";
            //Current.HandlungController[1].Data.Zusatz = "+ Bodenfahrzeuge";
            //Current.HandlungController[2].Data.Bezeichner = "Verkörperung";
            //Current.HandlungController[2].Data.Wert = 105;
            //Current.HandlungController[2].Data.Zusatz = "+ Englischer Landadel + nett";
            //Current.HandlungController[2].Data.Typ = "Handlung";

            //Current.HandlungController[0].Data.Zusammensetzung.Add(6, Current.HD[6]);
            //Current.HandlungController[0].Data.Zusammensetzung.Add(4, Current.HD[4]);

            //DictionaryCharEntry temptry = new DictionaryCharEntry("", 0);
            //temptry = Current.HD[6];
            //temptry.Wert = 5;
            //Current.HD[6] = temptry;
            //temptry = new DictionaryCharEntry("", 0);
            //temptry = Current.HD[4];
            //temptry.Wert = 8;
            //Current.HD[4] = temptry;
            // testdaten ende
        }

        public CharViewModel(Char x_defaultChar, Controller.CharHolder x_current)
        {
            defaultChar = x_defaultChar;
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
