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

        /// <summary>
        /// Praezision
        /// </summary>
        //double _Praezision = 0;
        //[Used_UserAttribute]
        //public double Praezision
        //{
        //    get { return _Praezision; }
        //    set
        //    {
        //        if (value != _Praezision)
        //        {
        //            _Praezision = value;
        //            NotifyPropertyChanged();
        //        }
        //    }
        //}

        //double _DK = 0;
        //[Used_UserAttribute]
        //public double DK
        //{
        //    get { return _DK; }
        //    set
        //    {
        //        if (value != _DK)
        //        {
        //            _DK = value;
        //            NotifyPropertyChanged();
        //        }
        //    }
        //}

        //double _PraezisionCalced = 0;
        //public double PraezisionCalced
        //{
        //    get { return _PraezisionCalced; }
        //    set
        //    {
        //        if (value != _PraezisionCalced)
        //        {
        //            _PraezisionCalced = value;
        //            NotifyPropertyChanged();
        //        }
        //    }
        //}

        //double _DK_Calced = 0; // DK
        //public double DKCalced
        //{
        //    get { return _DK_Calced; }
        //    set
        //    {
        //        if (value != _DK_Calced)
        //        {
        //            _DK_Calced = value;
        //            NotifyPropertyChanged();
        //        }
        //    }
        //}

        CharCalcProperty _DK;
        public CharCalcProperty DK
        {
            get { return _DK; }
            set { if (_DK != value) { _DK = value; NotifyPropertyChanged(); } }
        }

        CharCalcProperty _Precision;
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
                case "DK":
                    return DK.Value;
                default:
                    break;
            }
            return base.InternValueOf(ID);
        }
    }
}