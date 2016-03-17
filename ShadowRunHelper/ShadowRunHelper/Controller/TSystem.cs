namespace ShadowRunHelper.Controller
{
    public class TSystem
    {
        public enum TResult
        {
            NO_ERROR = 0,
            NOT_IMPLEMENTED = 1,
            SYSTEM_ERROR = 2,
            IO_ERROR = 3
        }



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
