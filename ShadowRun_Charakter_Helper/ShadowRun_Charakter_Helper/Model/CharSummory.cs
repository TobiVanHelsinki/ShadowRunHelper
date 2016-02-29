using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ShadowRun_Charakter_Helper.Model
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


        public CharSummory(string id, string sum)
        {
            this.ID = id;
            this.Summory = sum;
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