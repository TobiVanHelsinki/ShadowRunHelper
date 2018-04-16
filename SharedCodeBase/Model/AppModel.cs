using ShadowRunHelper.IO;
using TAPPLICATION.IO;

namespace ShadowRunHelper.Model
{
    public class AppModel : TAPPLICATION.Model.SharedAppModel<CharHolder>
    {
        public static AppModel Initialize()
        {
            if (instance == null)
            {
                instance = new AppModel();
            }
            CharHolderIO.MainTypeConvert = CharHolderIO.ConvertWithRightVersion;
            return Instance;
        }
        public static new AppModel Instance
        {
            get
            {
                return (AppModel)instance;
            }
        }

        AppModel() : base(){ }

        public delegate void NavigationEventHandler(object sender, ProjectPages page, ProjectPagesOptions PageOptions);
        public event NavigationEventHandler NavigationRequested;
        public void RequestNavigation(object o, ProjectPages p, ProjectPagesOptions po = ProjectPagesOptions.Nothing)
        {
            NavigationRequested?.Invoke(o, p, po);
        }

        public void TutorialChangedState(int StateNumber, bool Highlight = false)
        {
            TutorialStateChanged(StateNumber, Highlight);
        }
        public delegate void TutorialStateChangeRequestEventHandler(int StateNumber, bool Highlight);
        public event TutorialStateChangeRequestEventHandler TutorialStateChanged;


    }
}
