using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShadowRunHelper.CharModel
{
    public class Eigenschaft : Model
    {
        private string auswirkungen = "";
        public string Auswirkungen
        {
            get { return auswirkungen; }
            set
            {
                if (value != this.auswirkungen)
                {
                    this.auswirkungen = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public Eigenschaft()
        {
            
        }

    }
}
