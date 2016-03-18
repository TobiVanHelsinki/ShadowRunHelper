using static ShadowRunHelper.Controller.TApp;

namespace ShadowRunHelper.Controller
{
    public class TSystem
    {
        
        private System.Collections.ObjectModel.ObservableCollection<Error> TError;

        public TSystem()
        {

            this.TError = new System.Collections.ObjectModel.ObservableCollection<Error>();
        }

        public TResult addError(Error newError)
        {

            return TResult.NOT_IMPLEMENTED;
        }

        public TResult removeError(Error remError)
        {

            return TResult.NOT_IMPLEMENTED;
        }


    }
}
