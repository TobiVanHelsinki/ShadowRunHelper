using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ShadowRun_Charakter_Helper.Controller
{
    public class CharHolder
    {
        public HashDictionary HD = new HashDictionary();

        public ObservableCollection<CharController.Fertigkeit> FertigkeitenController { get; set; }
        public ObservableCollection<CharController.Handlung> HandlungsController { get; set; }
        public CharController.Panzerung PanzerungsController { get; set; }

        // für "neu"
        public CharHolder()
        {
            FertigkeitenController = new ObservableCollection<CharController.Fertigkeit>();
            FertigkeitenController.Add(new CharController.Fertigkeit());
            FertigkeitenController.Add(new CharController.Fertigkeit(new CharModel.Fertigkeit()));
            FertigkeitenController[0].setHD(HD);
            FertigkeitenController[0].Data.Bezeichner = "Testen";
            HandlungsController = new ObservableCollection<CharController.Handlung>();
            PanzerungsController = new CharController.Panzerung();
            PanzerungsController.setHD(HD);
        }

        // für "laden"
        public CharHolder(
            ObservableCollection<CharController.Fertigkeit> F, 
            ObservableCollection<CharController.Handlung> H, 
            CharController.Panzerung P)
        {
            FertigkeitenController = F;
            HandlungsController = H;
            PanzerungsController = P; // new CharController.Panzerung(HD);
            PanzerungsController.setHD(HD);
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