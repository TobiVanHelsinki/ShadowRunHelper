
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Windows.ApplicationModel.Resources;

namespace ShadowRunHelper.CharModel
{
    public class Adeptenkraft_KomplexeForm : Thing
    {
        string _Option = "";
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
        //public override double GetValue([CallerMemberName] string ID = "")
        //{
        //    return base.GetValue(ID);
        //}
        public override Thing Copy(ref Thing target)
        {
            if (target == null)
            {
                target = new Item();
            }
            base.Copy(ref target);
            Adeptenkraft_KomplexeForm TargetS = (Adeptenkraft_KomplexeForm)target;
            TargetS.Option = Option;
            return target;
        }

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
            //System.Resources.ResourceReader
            var res = ResourceLoader.GetForCurrentView();
            string strReturn = base.HeaderToCSV(Delimiter);
            strReturn += res.GetString("Model_Adeptenkraft_KomplexeForm_Option/Text");
            strReturn += Delimiter;
            return strReturn;
        }


        public override void FromCSV(Dictionary<string, string> dic)
        {
            var res = ResourceLoader.GetForCurrentView();
            base.FromCSV(dic);
            foreach (var item in dic)
            {
                if (item.Key == res.GetString("Model_Adeptenkraft_KomplexeForm_Option/Text"))
                {
                    this.Option = (item.Value);
                    continue;
                }
            }
        }
    }
}
