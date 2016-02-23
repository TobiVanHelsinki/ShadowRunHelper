using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShadowRun_Charakter_Helper.Controller
{
    class Char_Verwaltung
    {
        private ObservableCollection<Model.Char_Summory> summorys;
        public ObservableCollection<Model.Char_Summory> Summorys
        {
            get
            {
                return summorys;
            }
        }

        /// <summary>
        /// Nutzen für ohne Liste
        /// </summary>
        public Char_Verwaltung()
        {
        }
        /// <summary>
        /// Nutzen für mit Liste
        /// </summary>
        /// <param name="id"></param>
        public Char_Verwaltung(int id)
        {
            summorys = new ObservableCollection<Model.Char_Summory>();
            // List erstellen, entweder aus dem App Container oder aus dem Ordner oder beidem 
        }

        public CharHolder LadenApp(int id)
        {
           return IO.CharIO.Load_JSONChar_from_Data(id);
        }
    }
}
