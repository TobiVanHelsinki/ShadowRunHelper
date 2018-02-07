using ShadowRunHelper.CharModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using TLIB_UWPFRAME.Model;

namespace ShadowRunHelper.Model
{
    public class AllListEntry : INotifyPropertyChanged
    {
        //[Newtonsoft.Json.JsonIgnore]
        //public readonly AllListEntry This;

        string _DisplayName = "";
        [Newtonsoft.Json.JsonIgnore]
        public string DisplayName
        {
            get
            {
                return _DisplayName;
            }
            set
            {
                if (value != _DisplayName && value != "" && value != null)
                {
                    _DisplayName = value;
                    NotifyPropertyChanged();
                }
            }
        }

        Thing _Object = new Thing();
        public Thing Object
        {
            get
            {
                return _Object;
            }
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
        string _PropertyID = "";
        public string PropertyID
        {
            get
            {
                return _PropertyID;
            }
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
            //This = this;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            ModelHelper.CallPropertyChangedAtDispatcher(PropertyChanged, this, propertyName);
        }

        public override string ToString()
        {
            return Object.ToString();
        }
    }
}
