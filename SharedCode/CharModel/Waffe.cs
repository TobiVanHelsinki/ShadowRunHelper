///Author: Tobi van Helsinki

using System.Linq;

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

        CharCalcProperty _DK;
        [Used_User]
        public CharCalcProperty DK
        {
            get { return _DK; }
            set { if (_DK != value) { _DK = value; NotifyPropertyChanged(); } }
        }

        CharCalcProperty _Precision;
        [Used_User]
        public CharCalcProperty Precision
        {
            get => _Precision;
            set { _Precision = value; NotifyPropertyChanged(); }
        }

        protected override void OnLinkedThingsChanged()
        {
            //var List = LinkedThings.Where(x => x.Object.ThingType == ThingDefs.Munition).Select(x => x.Object);
            //DKCalced = DK + List.Sum(x => x.RawValueOf("DK"));
            //PraezisionCalced = Praezision + List.Sum(x => x.RawValueOf("Praezision"));
            base.OnLinkedThingsChanged();
        }

        protected override double InternValueOf(string ID)
        {
            switch (ID)
            {
                case "Praezision":
                    return Precision.Value;
                case "Precision":
                    return Precision.Value;
                case "DK":
                    return DK.Value;
                default:
                    break;
            }
            return base.InternValueOf(ID);
        }
    }
}