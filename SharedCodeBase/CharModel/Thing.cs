﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using TLIB_UWPFRAME;
using TLIB_UWPFRAME.Model;

namespace ShadowRunHelper.CharModel
{

    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public sealed class UsedAttribute : Attribute
    {
        public UsedAttribute() { }
    }
    public class Thing : INotifyPropertyChanged
    {
        public const uint nThingPropertyCount = 5;
        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            ModelHelper.CallPropertyChangedAtDispatcher(PropertyChanged, this, propertyName);
        }
        public Thing()
        {
            ThingType = ThingDefs.Undef;
        }
        ThingDefs thingType = 0;
        [Newtonsoft.Json.JsonIgnore]
        public ThingDefs ThingType
        {
            get { return thingType; }
            protected set
            {
                if (value != thingType)
                {
                    thingType = value;
                    NotifyPropertyChanged();
                }
            }
        }
        string bezeichner ="";
        [Used]
        public string Bezeichner
        {
            get => bezeichner;
            set
            {
                if (value != bezeichner)
                {
                    bezeichner = value;
                    NotifyPropertyChanged();
                }
            }
        }
        string typ = "";
        [Used]
        public string Typ
        {
            get => typ;
            set
            {
                if (value != typ)
                {
                    typ = value;
                    NotifyPropertyChanged();
                }
            }
        }
        double wert = 0;
        [Used]
        public double Wert
        {
            get { return wert; }
            set
            {
                if (value != wert)
                {
                    wert = value;
                    NotifyPropertyChanged();
                }
            }
        }
        string zusatz = "";
        [Used]
        public string Zusatz
        {
            get { return zusatz; }
            set
            {
                if (value != zusatz)
                {
                    zusatz = value;
                    NotifyPropertyChanged();
                }
            }
        }
        string notiz = "";
        [Used]
        public string Notiz
        {
            get { return notiz; }
            set
            {
                if (value != notiz)
                {
                    notiz = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public double GetValue(string ID = "")
        {
            if (ID == "")
            {
                return Wert;
            }
            try
            {
#if DEBUG
                Type t = this.GetType();
                var pinfo = t.GetProperty(ID);
                object value = pinfo.GetValue(this);
                return double.Parse(value.ToString());
#else 
                return double.Parse(this.GetType().GetProperty(ID).GetValue(this).ToString());
#endif
            }
            catch (Exception)
            {
                return Wert;
            }
        }

        public virtual Thing Copy(Thing target = null)
        {
            //TODO
            var list = this.PropertyChanged?.GetInvocationList();
            if (target == null)
            {
                target = (Thing)Activator.CreateInstance(this.GetType());
            }
            target.Bezeichner = Bezeichner;
            target.Notiz = Notiz;
            //target.Ordnung = Ordnung;
            //target.ThingType= ThingType;
            target.Typ= Typ;
            target.Wert = Wert;
            target.Zusatz = Zusatz;
            return target;
        }
        public virtual void Reset()
        {
            foreach (var item in GetType().GetProperties().Where(p => p.CustomAttributes.Any(c => c.AttributeType == typeof(UsedAttribute))))
            {
                item.SetValue(this, default);
            }
            //Bezeichner = "";
            //Notiz = "";
            //Ordnung = 0;
            //ThingType = 0;
            //Typ = "";
            //Wert = 0;
            //Zusatz = "";
        }

        public virtual string ToCSV(string Delimiter)
        {
            string strReturn = "";
            strReturn += Bezeichner;
            strReturn += Delimiter;
            strReturn += Notiz;
            //strReturn += Delimiter;
            //strReturn += Ordnung;
            //strReturn += Delimiter;
            //strReturn += ThingType;
            strReturn += Delimiter;
            strReturn += Typ;
            strReturn += Delimiter;
            strReturn += Wert;
            strReturn += Delimiter;
            strReturn += Zusatz;
            strReturn += Delimiter;
            return strReturn;
        }
        public virtual string HeaderToCSV(string Delimiter)
        {
            string strReturn = "";
            strReturn += CrossPlatformHelper.GetString("Model_Thing_Bezeichner/Text");
            strReturn += Delimiter;
            strReturn += CrossPlatformHelper.GetString("Model_Thing_Notiz/Text");
            //strReturn += Delimiter;
            //strReturn += Ordnung;
            //strReturn += Delimiter;
            //strReturn += CrossPlatformHelper.GetString("Model_Thing_ThingTyp/Text");
            strReturn += Delimiter;
            strReturn += CrossPlatformHelper.GetString("Model_Thing_Typ/Text");
            strReturn += Delimiter;
            strReturn += CrossPlatformHelper.GetString("Model_Thing_Wert/Text");
            strReturn += Delimiter;
            strReturn += CrossPlatformHelper.GetString("Model_Thing_Zusatz/Text");
            strReturn += Delimiter;
            return strReturn;
        }
        public virtual void FromCSV(Dictionary<string, string> dic)
        {
            foreach (var item in dic)
            {
                if (item.Key == CrossPlatformHelper.GetString("Model_Thing_Bezeichner/Text"))
                {
                    Bezeichner = item.Value;
                    continue;
                }
                if (item.Key == CrossPlatformHelper.GetString("Model_Thing_Notiz/Text"))
                {
                    Notiz = item.Value;
                    continue;
                }
                if (item.Key == CrossPlatformHelper.GetString("Model_Thing_Typ/Text"))
                {
                    Typ = item.Value;
                    continue;
                }
                if (item.Key == CrossPlatformHelper.GetString("Model_Thing_Wert/Text"))
                {
                    Wert= double.Parse(item.Value);
                    continue;
                }
                if (item.Key == CrossPlatformHelper.GetString("Model_Thing_Zusatz/Text"))
                {
                    Zusatz = item.Value;
                    continue;
                }

            }
        }
        public override string ToString()
        {
            string sep = ", ";
            return bezeichner + sep + wert + sep + Zusatz;
        }

        /// <summary>
        /// Determines, if the Both Things have same Name und ThingType, so that they can be seen as "the same"
        /// </summary>
        /// <param name="t1"></param>
        /// <param name="t2"></param>
        /// <returns></returns>
        public static bool HasSameIdentifiers(Thing t1, Thing t2)
        {
            return t1.ThingType == t2.ThingType ? t1.Bezeichner == t2.Bezeichner ? true : false : false;
        }

        /// <summary>
        /// Checks, if this object has Similarities to the parameter, will be overridden by others
        /// </summary>
        /// <param name="i_t"></param>
        /// <returns> 1    -> type   correct, name correct </returns>
        /// <returns> >0.5 -> type   correct, name somehow</returns>
        /// <returns> 0.5  -> type incorrect, name correct</returns>
        /// <returns> <0.5  -> type incorrect, name somehow</returns>
        /// <returns> <0.5 -> type   correct, name incorrect</returns>
        /// <returns> 0    -> type incorrect, name incorrect</returns>
        public virtual float SimilaritiesTo(string text)
        {

            float retval = 0;
            if (TypenHelper.ThingDefToString(ThingType, false).ToLower().Contains(text))
            {
                retval += 0.4f;
            }
            if (this.ToString().ToLower().Contains(text.ToLower()))
            {
                retval += 0.4f;
            }
            if (this.ToString().ToLower() == (text.ToLower()))
            {
                retval += 0.1f;
            }
            return retval;
        }
    }
}
