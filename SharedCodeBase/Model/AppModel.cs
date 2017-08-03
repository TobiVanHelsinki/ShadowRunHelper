using System;
using Windows.UI.Core;

namespace ShadowRunHelper.Model
{
    public class AppModel : TLIB.Model.SharedAppModel<CharHolder>
    {
        public static AppModel Initialize()
        {
            if (instance == null)
            {
                instance = new AppModel();
            }
            return Instance;
        }
        public static new AppModel Instance
        {
            get
            {
                return (AppModel)instance;
            }
        }

        public CharHolder CurrentChar
        {
            get { return MainObject; }
            set
            {
                MainObject = value;
            }
        }

        public void SetDependencies(CoreDispatcher dispatcher)
        {
            Dispatcher = dispatcher;
        }
        /// <summary>
        /// for future multithreading
        /// </summary>
        /// <param name="dispatcher"></param>
        public CoreDispatcher Dispatcher;

        public void TutorialChangedState(int StateNumber, bool Highlight = false)
        {
            TutorialStateChanged(StateNumber, Highlight);
        }
        public delegate void TutorialStateChangeRequestEventHandler(int StateNumber, bool Highlight);
        public event TutorialStateChangeRequestEventHandler TutorialStateChanged;
    }
}
