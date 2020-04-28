//Author: Tobi van Helsinki

using Newtonsoft.Json;

namespace ShadowRunHelper.CharModel
{
    public class Item : Thing
    {
        [JsonIgnore]
        public bool? State
        {
            get => Aktiv == true ? true : (Besitz == true ? null : (bool?)false);
            set
            {
                if (value != State)
                {
                    if (value == true)
                    {
                        Aktiv = true;
                        Besitz = true;
                    }
                    else if (value == null)
                    {
                        Aktiv = false;
                        Besitz = true;
                    }
                    else
                    {
                        Aktiv = false;
                        Besitz = false;
                    }
                    NotifyPropertyChanged();
                }
            }
        }

        private bool? besitz = true;
        [Used_UserAttribute]
        public bool? Besitz
        {
            get => besitz;
            set
            {
                if (value != besitz)
                {
                    besitz = value;
                    NotifyPropertyChanged();
                    NotifyPropertyChanged(nameof(State));
                }
            }
        }

        private bool? aktiv = false;
        [Used_UserAttribute]
        public bool? Aktiv
        {
            get => aktiv;
            set
            {
                if (value != aktiv)
                {
                    aktiv = value;
                    if (aktiv == true)
                    {
                        Besitz = true;
                    }
                    RefreshCharProperties();
                    NotifyPropertyChanged();
                    NotifyPropertyChanged(nameof(State));
                }
            }
        }

        private double anzahl = 1;
        [Used_UserAttribute]
        public double Anzahl
        {
            get => anzahl;
            set
            {
                if (value != anzahl)
                {
                    anzahl = value;
                    NotifyPropertyChanged();
                }
            }
        }

        string _Modifications;
        [Used_User]
        public string Modifications
        {
            get { return _Modifications ?? ""; }
            set
            {
                if (_Modifications != value)
                {
                    _Modifications = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public Item() => RefreshCharProperties();

        private void RefreshCharProperties()
        {
            foreach (var item in GetConnects())
            {
                item.Active = aktiv == true;
            }
        }
    }
}