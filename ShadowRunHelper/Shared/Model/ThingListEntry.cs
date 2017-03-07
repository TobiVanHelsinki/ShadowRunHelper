using ShadowRunHelper.CharModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ShadowRunHelper.Model
{
    public class ThingListEntry : INotifyPropertyChanged
    {
        [Newtonsoft.Json.JsonIgnore]
        public readonly ThingListEntry This;

        Thing _Object;
        public Thing Object
        {
            get
            {
                return _Object;
            }
            set
            {
                if (value != null)
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
                if (value != _strProperty)
                {
                    _strProperty = value;
                    NotifyPropertyChanged();
                }
            }
        }
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

        public ThingListEntry(Thing o, string strPropName = "", string strPropID = "")
        {
            if (o == null)
            {
                Object = new Thing();
            }
            else
            {
                Object = o;
            }
            if (strPropID == null)
            {
                strProperty = "";
            }
            else
            {
                strProperty = strPropID;
            }
            if (strPropName == null)
            {
                strPropertyName = "";
            }
            else
            {
                strPropertyName = strPropName;
            }
            This = this;
        }
        public ThingListEntry()
        {
            Object = new Thing();
            strProperty = "";
            strPropertyName = "";
            This = this;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
