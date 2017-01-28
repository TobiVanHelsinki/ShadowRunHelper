using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ShadowRunHelper.CharModel
{
    public class Kommlink : CharModel.Item
    {
        private double programmanzahl = 0;
        public double Programmanzahl
        {
            get { return programmanzahl; }
            set
            {
                if (value != this.programmanzahl)
                {
                    this.programmanzahl = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private double firewall = 0;
        public double Firewall
        {
            get { return firewall; }
            set
            {
                if (value != this.firewall)
                {
                    this.firewall = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private double datenverarbeitung = 0;
        public double Datenverarbeitung
        {
            get { return datenverarbeitung; }
            set
            {
                if (value != this.datenverarbeitung)
                {
                    this.datenverarbeitung = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public Kommlink()
        {
            this.ThingType = Ressourcen.TypNamen.ThingDefs.Kommlink;

        }

        public Kommlink Copy(Kommlink target = null)
        {
            if (target == null)
            {
                target = new Kommlink();
            }
            base.Copy(target);
            target.Programmanzahl = Programmanzahl;
            target.Firewall = Firewall;
            target.Datenverarbeitung = Datenverarbeitung;
            return target;
        }

        public void Reset()
        {
            base.Reset();
            Programmanzahl = 0;
            Firewall = 0;
            Datenverarbeitung = 0;
        }
    }
}