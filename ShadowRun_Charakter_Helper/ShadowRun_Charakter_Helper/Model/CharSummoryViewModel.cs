using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ShadowRun_Charakter_Helper.Model
{
    class CharSummoryViewModel
    {
        private ObservableCollection<Model.Char_Summory> summorys;
        public ObservableCollection<Model.Char_Summory> Summorys
        {
            get
            {
                return summorys;
            }
            set
            {
                if (value != this.summorys)
                {
                    this.summorys = value;
                    NotifySummoryChanged();
                }
            }
        }
        public CharSummoryViewModel()
        {
            summorys = new ObservableCollection<Model.Char_Summory>();

        }

        public CharSummoryViewModel(ObservableCollection<Model.Char_Summory> x_summorys)
        {
            summorys = x_summorys;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifySummoryChanged([CallerMemberName] String summoryName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(summoryName));
            }
        }

    }
}
