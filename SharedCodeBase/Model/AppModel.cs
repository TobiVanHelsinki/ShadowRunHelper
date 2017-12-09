﻿using System;
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

        public void TutorialChangedState(int StateNumber, bool Highlight = false)
        {
            TutorialStateChanged(StateNumber, Highlight);
        }
        public delegate void TutorialStateChangeRequestEventHandler(int StateNumber, bool Highlight);
        public event TutorialStateChangeRequestEventHandler TutorialStateChanged;
    }
}
