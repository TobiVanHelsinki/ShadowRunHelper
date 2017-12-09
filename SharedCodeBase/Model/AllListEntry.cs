using ShadowRunHelper.CharModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using TLIB.Model;

namespace ShadowRunHelper.Model
{
    public class AllListEntry : INotifyPropertyChanged
    {
        [Newtonsoft.Json.JsonIgnore]
        public readonly AllListEntry This;

        string _strPropertyName = "";
        [Newtonsoft.Json.JsonIgnore]
        public string strPropertyName
        {
            get
            {
                return _strPropertyName;
            }
            set
            {
                if (value != _strPropertyName && value != "" && value != null)
                {
                    _strPropertyName = value;
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
        string _strProperty = "";
        public string strProperty
        {
            get
            {
                return _strProperty;
            }
            set
            {
                if (value != _strProperty && value != null)
                {
                    _strProperty = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public AllListEntry(Thing o, string strPropName = "", string strPropID = "") : this()
        {
            Object = o;
            strProperty = strPropID;
            strPropertyName = strPropName;
        }
        public AllListEntry()
        {
            This = this;
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
