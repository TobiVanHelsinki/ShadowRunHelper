using System.Collections.ObjectModel;

namespace ShadowRunHelper1_3.CharController
{
    public class Connection
    {
        public CharModel.Connection Data { get; set; } //fuer Einfache-Controller

        public Connection()
        {
            Data = new CharModel.Connection();
        }

        public Connection(ObservableCollection<CharModel.Connection> obj)
        {
        }
    }
}
