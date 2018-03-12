using ShadowRunHelper.Model;
using System.Collections.Generic;
using System.Linq;

namespace ShadowRunHelper.CharModel
{
    public class Fernkampfwaffe : Waffe
    {
        private double rueckstoss = 0;
        [Used_User]
        public double Rueckstoss
        {
            get { return rueckstoss; }
            set
            {
                if (value != rueckstoss)
                {
                    rueckstoss = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private string modi = "";
        [Used_User]
        public string Modi
        {
            get { return modi; }
            set
            {
                if (value != modi)
                {
                    modi = value;
                    NotifyPropertyChanged();
                }
            }
        }
//        AllListEntry _CurrentMunition;
//        [Used_User]
//        public AllListEntry CurrentMunition
//        {
//            get { return _CurrentMunition; }
//            set {
//                if (_CurrentMunition != value)
//                {
//                    void Object_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
//                    {
//                        NotifyPropertyChanged("CurrentMunition");
//                    }
//                    if (_CurrentMunition != null)
//                    {
//                        _CurrentMunition.PropertyChanged -= Object_PropertyChanged;
//                    }
//                    if (_CurrentMunition != null && _CurrentMunition.Object != null)
//                    {
//                        _CurrentMunition.Object.PropertyChanged -= Object_PropertyChanged;
//                    }
//                    _CurrentMunition = value;
//                    if (_CurrentMunition != null)
//                    {
//                        _CurrentMunition.PropertyChanged += Object_PropertyChanged;
//                    }
//                    if (_CurrentMunition != null && _CurrentMunition.Object != null)
//                    {
//                        _CurrentMunition.Object.PropertyChanged += Object_PropertyChanged;
//                    }
//                    NotifyPropertyChanged();
//                }
//}
//        }

//        public override double ValueOf(string ID = "")
//        {
//            if (CurrentMunition != null)
//            {
//                switch (ID)
//                {
//                    case null:
//                    case "":
//                    case "Wert":
//                        return Wert + CurrentMunition.Object.ValueOf(ID);
//                    case "Praezision":
//                        return Praezision + CurrentMunition.Object.ValueOf(ID);
//                    case "PB":
//                        return PB + CurrentMunition.Object.ValueOf(ID);
//                    default:
//                        break;
//                }
//            }
//            return base.ValueOf(ID);
//        }

        public static IEnumerable<ThingDefs> Filter = TypeHelper.ThingTypeProperties.Where(x=>
            x.ThingType != ThingDefs.Munition && 
            x.ThingType != ThingDefs.Implantat
        ).Select(x=>x.ThingType);

        public Fernkampfwaffe() : base()
        {
            LinkedThings.SetFilter(Filter);
        }


    }
}
