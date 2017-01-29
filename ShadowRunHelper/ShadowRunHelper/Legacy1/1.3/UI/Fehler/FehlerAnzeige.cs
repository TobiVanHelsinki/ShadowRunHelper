using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;

namespace ShadowRunHelper1_3.UI.Fehler
{
    public class FehlerAnzeige
    {
        /// <summary>
        /// Zeigt dem User eine Fehlermeldung an
        /// </summary>
        /// <param name="Meldung"></param>
        /// <param name="Typ"></param>
        /// Typ = 0 : Genereller Fehler
        public static void showError(String Meldung, int Typ)
        {
            String Fehlertyp = "";
            switch (Typ)
            {

                default:
                    break;
            }

            switch (Typ)
            {
                case 0:
                    Fehlertyp = "Genereller Fehler";
                    break;
                default:
                    Fehlertyp = "Nicht spezifizierter Fehler";
                    break;
            }

            var messageDialog = new MessageDialog(Fehlertyp);

            messageDialog.Content = Meldung;
            messageDialog.CancelCommandIndex = 0;
            messageDialog.Commands.Add(new UICommand("OK"));

            // await Dialog.ShowAsync();
            System.Diagnostics.Debug.WriteLine(Fehlertyp + ": " + Meldung);
        }


    }
}
