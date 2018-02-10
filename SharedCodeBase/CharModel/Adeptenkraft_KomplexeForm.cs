﻿
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TLIB_UWPFRAME;

namespace ShadowRunHelper.CharModel
{
    public class Adeptenkraft_KomplexeForm : Thing
    {
        string _Option = "";
        [Used]
        public string Option
        {
            get { return _Option; }
            set
            {
                if (value != this._Option)
                {
                    this._Option = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public Adeptenkraft_KomplexeForm()
        {
            this.ThingType = ThingDefs.Adeptenkraft_KomplexeForm;
        }

        //public override Thing Copy(Thing target)
        //{
        //    base.Copy(target);
        //    Adeptenkraft_KomplexeForm TargetS = (Adeptenkraft_KomplexeForm)target;
        //    TargetS.Option = Option;
        //    return target;
        //}

        public override void Reset()
        {
            base.Reset();
            Option = "";
        }


        public override string ToCSV(string Delimiter)
        {
            string strReturn = base.ToCSV(Delimiter);
            strReturn += Option;
            strReturn += Delimiter;
            return strReturn;
        }

        public override string HeaderToCSV(string Delimiter)
        {
            string strReturn = base.HeaderToCSV(Delimiter);
            strReturn += CrossPlatformHelper.GetString("Model_Adeptenkraft_KomplexeForm_Option/Text");
            strReturn += Delimiter;
            return strReturn;
        }


        public override void FromCSV(Dictionary<string, string> dic)
        {
            base.FromCSV(dic);
            foreach (var item in dic)
            {
                if (item.Key == CrossPlatformHelper.GetString("Model_Adeptenkraft_KomplexeForm_Option/Text"))
                {
                    this.Option = (item.Value);
                    continue;
                }
            }
        }
    }
}
