using System.Collections.ObjectModel;

namespace ShadowRun_Charakter_Helper.CharController
{
    public class Connection
    {
        public CharModel.Connection Data { get; set; } //für Einfache-Controller

        public Connection()
        {
            Data = new CharModel.Connection();
        }

        public Connection(ObservableCollection<CharModel.Connection> obj)
        {
        }
    }
}
