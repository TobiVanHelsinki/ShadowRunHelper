﻿

using System.Collections.Generic;

namespace ShadowRunHelper.CharModel
{
    public class Geist_Sprite : Item
    {
        string _Dienste = "";
        public string Dienste
        {
            get { return _Dienste; }
            set
            {
                if (value != this._Dienste)
                {
                    this._Dienste = value;
                    NotifyPropertyChanged();
                }
            }
        }
        bool? _Geb_Reg = false;
        public bool? Geb_Reg
        {
            get { return _Geb_Reg; }
            set
            {
                if (value != this._Geb_Reg)
                {
                    this._Geb_Reg = value;
                    NotifyPropertyChanged();
                }
            }
        }
        
        public Geist_Sprite()
        {
            this.ThingType = ThingDefs.Geist_Sprite;
        }
        
        public override Thing Copy(ref Thing target)
        {
            if (target == null)
            {
                target = new Item();
            }
            base.Copy(ref target);
            Geist_Sprite TargetS = (Geist_Sprite)target;
            TargetS.Dienste = Dienste;
            TargetS.Geb_Reg = Geb_Reg;
            return target;
        }

        public override void Reset()
        {
            base.Reset();
            Dienste = "";
            Geb_Reg = false;
        }


        public override string ToCSV(string Delimiter)
        {
            string strReturn = base.ToCSV(Delimiter);
            strReturn += Dienste;
            strReturn += Delimiter;
            strReturn += Geb_Reg;
            strReturn += Delimiter;
            return strReturn;
        }

        public override string HeaderToCSV(string Delimiter)
        {
            string strReturn = base.HeaderToCSV(Delimiter);
            strReturn += CrossPlattformHelper.GetString("Model_Geist_Sprite_Dienste/Text");
            strReturn += Delimiter;
            strReturn += CrossPlattformHelper.GetString("Model_Geist_Sprite_Geb_Reg/Text");
            strReturn += Delimiter;
            return strReturn;
        }


        public override void FromCSV(Dictionary<string, string> dic)
        {
            base.FromCSV(dic);
            foreach (var item in dic)
            {
                if (item.Key == CrossPlattformHelper.GetString("Model_Geist_Sprite_Dienste/Text"))
                {
                    this.Dienste = (item.Value);
                    continue;
                }
                if (item.Key == CrossPlattformHelper.GetString("Model_Geist_Sprite_Geb_Reg/Text"))
                {
                    this.Geb_Reg = bool.Parse(item.Value);
                    continue;
                }
            }
        }
    }
}
