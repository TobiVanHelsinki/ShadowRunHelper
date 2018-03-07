using ShadowRunHelper.Model;
using System.Collections.Generic;
using System.Linq;

namespace ShadowRunHelper.CharModel
{
    public class Fernkampfwaffe : Waffe
    {
        private double rueckstoss = 0;
        [Used_UserAttribute]
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
        [Used_UserAttribute]
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
        AllListEntry _CurrentMunition;
        [Used_UserAttribute]
        public AllListEntry CurrentMunition
        {
            get { return _CurrentMunition; }
            set {
                if (_CurrentMunition != value)
                {
                    if (_CurrentMunition != null)
                    {
                        _CurrentMunition.PropertyChanged -= Object_PropertyChanged;
                    }
                    if (_CurrentMunition != null && _CurrentMunition.Object != null)
                    {
                        _CurrentMunition.Object.PropertyChanged -= Object_PropertyChanged;
                    }
                    _CurrentMunition = value;
                    if (_CurrentMunition != null)
                    {
                        _CurrentMunition.PropertyChanged += Object_PropertyChanged;
                    }
                    if (_CurrentMunition != null && _CurrentMunition.Object != null)
                    {
                        _CurrentMunition.Object.PropertyChanged += Object_PropertyChanged;
                    }
                    NotifyPropertyChanged();
                }
}
        }

        private void Object_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            NotifyPropertyChanged("CurrentMunition");
        }

        public override double GetPropertyValueOrDefault(string ID = "")
        {
            if (ID == "Wert" && CurrentMunition != null)
            {
                return Wert + CurrentMunition.Object.GetPropertyValueOrDefault(ID);
            }
            return base.GetPropertyValueOrDefault(ID);
        }

        public static IEnumerable<ThingDefs> Filter = TypeHelper.ThingTypeProperties.Where(x=>x.ThingType != ThingDefs.Munition).Select(x=>x.ThingType);
    }
}
