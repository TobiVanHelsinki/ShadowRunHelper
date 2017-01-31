using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ShadowRunHelper.Model
{
    public class CharSummory : INotifyPropertyChanged
    {
        private string _strFileName;
        public string strFileName
        {
            get { return this._strFileName; }
            set
            {
                if (value != this._strFileName)
                {
                    this._strFileName = value;
                    strCharSummory = "";
                    NotifyPropertyChanged();
                }
            }
        }

        private string _strCharSummory;
        public string strCharSummory
        {
            get { return this._strCharSummory; }
            private set
            {
                if (value != this._strCharSummory)
                {
                    _strCharSummory = _strFileName.Replace('_', ' ');
                    _strCharSummory = _strCharSummory.Replace(Konstanten.DATEIENDUNG_CHAR, "");
                    if (_tDateCreated != null)
                    {
                        _strCharSummory += " (" + _tDateCreated.Day + "." + _tDateCreated.Month + "." + _tDateCreated.Year + " " + _tDateCreated.Hour + ":" + _tDateCreated.Minute + ")";
                    }
                    NotifyPropertyChanged();
                }
            }
        }

        private DateTimeOffset _tDateCreated;
        public DateTimeOffset tDateCreated
        {
            get { return this._tDateCreated; }
            set
            {
                if (value != this._tDateCreated)
                {
                    strCharSummory = "";
                    this._tDateCreated = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public CharSummory(string id, DateTimeOffset dateCreated)
        {
            this.strFileName = id;
            this.tDateCreated = dateCreated;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}