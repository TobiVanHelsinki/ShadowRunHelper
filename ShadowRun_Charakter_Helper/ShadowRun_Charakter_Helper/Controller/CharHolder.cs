using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ShadowRun_Charakter_Helper.Controller
{
    public class CharHolder
    {
        public HashDictionary DicCD = new HashDictionary();
       
        public ObservableCollection<CharModel.Handlung> Handlungen { get; set; }
        public ObservableCollection<CharModel.Fertigkeit> CharData_Fertigkeiten { get; set; }
        public CharController.Panzerung PanzerungsController { get; set; }

        public CharHolder()
        {
            Handlungen = new ObservableCollection<CharModel.Handlung>();
            CharData_Fertigkeiten = new ObservableCollection<CharModel.Fertigkeit>();
            PanzerungsController = new CharController.Panzerung(DicCD);
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