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

        /// <summary>
        /// Praezision
        /// </summary>
        double _Praezision = 0;
        [Used_UserAttribute]
        public double Praezision
        {
            get { return _Praezision; }
            set
            {
                if (value != _Praezision)
                {
                    _Praezision = value;
                    NotifyPropertyChanged();
                }
            }
        }

        double _DK = 0; // DK
        [Used_UserAttribute]
        public double PB
        {
            get { return _DK; }
            set
            {
                if (value != _DK)
                {
                    _DK = value;
                    NotifyPropertyChanged();
                }
            }
        }

        double _PraezisionCalced = 0;
        public double PraezisionCalced
        {
            get { return _PraezisionCalced; }
            set
            {
                if (value != _PraezisionCalced)
                {
                    _PraezisionCalced = value;
                    NotifyPropertyChanged();
                }
            }
        }

        double _PB_Calced = 0; // DK
        public double PBCalced
        {
            get { return _PB_Calced; }
            set
            {
                if (value != _PB_Calced)
                {
                    _PB_Calced = value;
                    NotifyPropertyChanged();
                }
            }
        }

        protected override void OnLinkedThingsChanged()
        {
            var List = LinkedThings.Where(x=>x.Object.ThingType == ThingDefs.Munition).Select(x => x.Object);
            PBCalced = PB + List.Sum(x=>x.RawValueOf("PB"));
            PraezisionCalced = Praezision + List.Sum(x=>x.RawValueOf("Praezision"));
            base.OnLinkedThingsChanged();
        }
        protected override double InternValueOf(string ID)
        {
            switch (ID)
            {
                case "Praezision":
                    return PraezisionCalced;
                case "PB":
                    return PBCalced;
                default:
                    break;
            }
            return base.InternValueOf(ID);
        }

    }
}
