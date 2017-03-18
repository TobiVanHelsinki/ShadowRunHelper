using System;
namespace ShadowRunHelper.Model
{
    public class AppModel : TLIB.Model.SharedAppModel<CharHolder>
    {

        public static new AppModel Instance
        {
            get
            {
                return (AppModel)TLIB.Model.SharedAppModel<CharHolder>.Instance;
            }
        }

          public CharHolder CurrentChar
        {
            get { return this.MainObject; }
            set
            {
                    this.MainObject = value;
            }
        }

        CharHolder _PreDBChar;
        public CharHolder PreDBChar
        {
            get { return this._PreDBChar; }
            set
            {
                if (value != this._PreDBChar)
                {
                    this._PreDBChar = value;
                    NotifyPropertyChanged();
                }
            }
        }


        ProjectPages CurrentPageInProgress = ProjectPages.undef;
        public void RequestedNavigation(ProjectPages ePp, object oO = null)
        {
            if (CurrentPageInProgress == ePp)
            {
                return;
            }
            CurrentPageInProgress = ePp;
            NavigationRequested?.Invoke(oO, ePp);
            CurrentPageInProgress = ProjectPages.undef;
        }

        public event EventHandler<ProjectPages> NavigationRequested;
    }
}
