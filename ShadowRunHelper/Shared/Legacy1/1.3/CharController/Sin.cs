using System.Collections.ObjectModel;

namespace ShadowRunHelper1_3.CharController
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
