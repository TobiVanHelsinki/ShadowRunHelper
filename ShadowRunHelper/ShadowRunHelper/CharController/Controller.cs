using System;

namespace ShadowRunHelper.CharController
{
    public class Controller<T> where T : CharModel.Model, new()
    {
        public int HD_ID { get; set; }
        protected string HD_Bezeichner = "nichts vorhanden";
        protected string HD_Typ = "error";
        protected double HD_Wert = 0;
        protected string HD_Zusatz = "";
        protected string HD_Notiz = "";

        public Controller.HashDictionary HD;

   //     public event EventHandler OnHDError;

        /// <summary>
        /// Gibt dem Controller DAS HashDictionary \n 
        /// Sucht sich aus dem HD eine neue ID, wenn er keine hat \n 
        /// Fügt sich selbst anschließend dem HD hinzu \n 
        /// Registriert sich als Beobachter beim HD
        /// </summary>
        /// <param name="hD">HashDictionary des Chars</param>
        public void setHD(Controller.HashDictionary hD)
        {
            this.HD = hD;
            if (this.HD_ID == 0)
            {
                this.HD_ID = HD.getFreeKey();
            }
            else if (HD.Data.ContainsKey(HD_ID))
            {
                Error_Occured();
            }
            add_to_HD();
        }

        protected void Error_Occured()
        {
            int o_nOldID = HD_ID;
            int o_nNewID = HD.getFreeMaxKey();
            this.HD_ID = o_nNewID;

            HD.AlteHDEntrys.Add(o_nOldID, o_nNewID);
        }

        protected void NotifyForErrors()
        {
   //         OnHDError(EventArgs.Empty, EventArgs.Empty);
            System.Diagnostics.Debug.WriteLine("Fehler, es kam zu multiplen HD IDs");
        }

        /// <summary>
        /// Fügt sich selbst dem HD hinzu
        /// </summary>
        protected void add_to_HD()
        {
            HD.Add(this.HD_ID, new Model.DictionaryCharEntry(HD_Bezeichner, HD_Typ, HD_Wert, HD_Zusatz, HD_Notiz));
        }
        /// <summary>
        /// löscht sich aus dem HD
        /// </summary>
        public void remove_from_HD()
        {
            HD.Remove(this.HD_ID);
        }

        /// <summary>
        /// Konstruktor für neu
        /// </summary>
        public Controller()
        {
            HD_Typ = this.GetType().ToString();
            HD_Typ = Ressourcen.TypNamen.GetName_Controller(HD_Typ);
        }

        public void remove()
        {
            remove_from_HD();
        }
    }
}