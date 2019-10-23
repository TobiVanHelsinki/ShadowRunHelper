///Author: Tobi van Helsinki

namespace ShadowRunHelper.CharModel
{
    public abstract class Waffe : Item
    {
        string _SchadenTyp = "";
        [Used_UserAttribute]
        public string SchadenTyp
        {
            get { return _SchadenTyp; }
            set
            {
                if (value != _SchadenTyp)
                {
                    _SchadenTyp = value;
                    NotifyPropertyChanged();
                }
            }
        }

        ConnectProperty _DK;
        [Used_User]
        public ConnectProperty DK
        {
            get { return _DK; }
            set { if (_DK != value) { _DK = value; NotifyPropertyChanged(); } }
        }

        ConnectProperty _Precision;
        [Used_User]
        public ConnectProperty Precision
        {
            get => _Precision;
            set { _Precision = value; NotifyPropertyChanged(); }
        }

        //protected override double InternValueOf(string ID)
        //{
        //    switch (ID)
        //    {
        //        case "Praezision":
        //            return Precision.Value;
        //        case "Precision":
        //            return Precision.Value;
        //        case "DK":
        //            return DK.Value;
        //        default:
        //            break;
        //    }
        //    return base.InternValueOf(ID);
        //}
    }
}