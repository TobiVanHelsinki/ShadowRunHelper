using System;
using System.Collections.ObjectModel;

namespace ShadowRun_Charakter_Helper.Models
{
    public class Attribut_ID
    {
        private int iD;
        public int ID
        {
            get { return iD; }
            set
            {
                if (value != this.iD)
                {
                    this.iD = value;
                }
            }
        }
        private int bezeichnung;
        public int Bezeichnung
        {
            get { return bezeichnung; }
            set
            {
                if (value != this.bezeichnung)
                {
                    this.bezeichnung = value;
                }
            }
        }
       
        public Attribut_ID(int v)
        {
            this.iD = v;
        }

        public Attribut_ID()
        {
        }

        internal static Attribut_ID findByID(int selected_Fähigkeit_OLD_ID, ObservableCollection<Attribut_ID> zusammensetzung_A)
        {
            for (int i = 0; i < zusammensetzung_A.Count; i++)
            {
                if (zusammensetzung_A[i].ID == selected_Fähigkeit_OLD_ID)
                {
                    return zusammensetzung_A[i];
                }

            }
            throw new MyException();
        }
    }
}