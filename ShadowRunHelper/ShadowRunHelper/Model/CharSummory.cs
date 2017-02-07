using System;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace ShadowRunHelper.Model
{
    public class CharSummory : INotifyPropertyChanged
    {
        string _strFileName;
        public string strFileName
        {
            get { return _strFileName; }
            set
            {
                if (value != _strFileName)
                {
                    _strFileName = value;
                    strCharSummory = "";
                    NotifyPropertyChanged();
                }
            }
        }

        string _strCharSummory;
        public string strCharSummory
        {
            get { return _strCharSummory; }
            set
            {
                if (value != _strCharSummory)
                {
                    _strCharSummory = _strFileName.Replace('_', ' ');
                    _strCharSummory = _strCharSummory.Replace(Konstanten.DATEIENDUNG_CHAR, "");
                    NotifyPropertyChanged();
                }
            }
        }

        DateTimeOffset _tDateCreated;
        public DateTimeOffset tDateCreated
        {
            get { return _tDateCreated; }
            set
            {
                if (value != _tDateCreated)
                {
                    strCharSummory = "";
                    _tDateCreated = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string strDateCreated
        {
            get {
                return _tDateCreated.ToString(new CultureInfo("de-DE"));
                string _strDateCreated = "";
                if (_tDateCreated != null)
                {
                    _strDateCreated += " (" + _tDateCreated.Day + "." + _tDateCreated.Month + "." + _tDateCreated.Year + " " + _tDateCreated.Hour + ":" + _tDateCreated.Minute + ")";
                }
                return _strDateCreated; }
            private set
            {}
        }




        public CharSummory(string id, DateTimeOffset dateCreated)
        {
            strFileName = id;
            tDateCreated = dateCreated;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}