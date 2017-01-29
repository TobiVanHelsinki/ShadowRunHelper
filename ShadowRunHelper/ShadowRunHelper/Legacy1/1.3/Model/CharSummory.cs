using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ShadowRunHelper1_3.Model
{
    public class CharSummory : INotifyPropertyChanged
    {
        private string iD;
        public string ID
        {
            get { return this.iD; }
            set
            {
                if (value != this.iD)
                {
                    this.iD = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private string summory;
        public string Summory
        {
            get { return this.summory; }
            set
            {
                if (value != this.summory)
                {
                    this.summory = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private DateTimeOffset dateCreated;
        public DateTimeOffset DateCreated
        {
            get { return this.dateCreated; }
            set
            {
                if (value != this.dateCreated)
                {
                    this.dateCreated = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public CharSummory(string id, string sum, DateTimeOffset dateCreated)
        {
            this.ID = id;
            this.Summory = sum;
            this.DateCreated = dateCreated;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}