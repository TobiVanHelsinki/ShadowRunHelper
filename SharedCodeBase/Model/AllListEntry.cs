using ShadowRunHelper.CharModel;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using TAPPLICATION.Model;
using TLIB;

namespace ShadowRunHelper.Model
{
    public class AllListEntry : INotifyPropertyChanged
    {
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
        Thing _Object /*= new Thing()*/;
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
        [Obsolete("Just for compatibility with versions before 1.7 ", true)]
        public string strProperty
        {
            get { return PropertyID; }
            set { PropertyID = value; }
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
        void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            ModelHelper.CallPropertyChangedAtDispatcher(PropertyChanged, this, propertyName);
        }

        public override string ToString()
        {
            return Object + PropertyID;
        }
    }
}
