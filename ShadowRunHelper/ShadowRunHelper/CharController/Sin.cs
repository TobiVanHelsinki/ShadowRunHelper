using System.Collections.ObjectModel;

namespace ShadowRun_Charakter_Helper.CharController
{
    public class Sin
    {
        public CharModel.Sin Data { get; set; } //für Einfache-Controller
        public Sin()
        {
            Data = new CharModel.Sin();
        }

        public Sin(ObservableCollection<CharModel.Sin> obj)
        {
        }
    }
}
