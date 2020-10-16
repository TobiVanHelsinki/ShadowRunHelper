//Author: Tobi van Helsinki

using ShadowRunHelper.CharModel;
using ShadowRunHelper.Helper;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ShadowRunHelper.Model
{
    public class AllListEntry : INotifyPropertyChanged
    {
        private string _DisplayName = "";
        [Newtonsoft.Json.JsonIgnore]
        public string DisplayName
        {
            get => _DisplayName;
            set
            {
                if (value != _DisplayName && value != "" && value != null)
                {
                    _DisplayName = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private Thing _Object /*= new Thing()*/;
        public Thing Object
        {
            get => _Object;
            set
            {
                if (_Object != value && value != null)
                {
                    _Object = value;
                    NotifyPropertyChanged();
                }
                else
                {
                }
            }
        }

        private string _PropertyID = "";
        public string PropertyID
        {
            get => _PropertyID;
            set
            {
                if (value != _PropertyID && value != null)
                {
                    _PropertyID = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public AllListEntry(Thing o, string newDisplayName = "", string newPropID = "") : this()
        {
            Object = o;
            PropertyID = newPropID;
            DisplayName = newDisplayName;
        }

        public AllListEntry()
        {
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PlatformHelper.CallPropertyChanged(PropertyChanged, this, propertyName);
        }

        public override string ToString()
        {
            return Object + PropertyID;
        }
    }
}