///Author: Tobi van Helsinki

using ShadowRunHelper.CharModel;
using System;
using System.IO;
using System.Linq;

namespace ShadowRunHelper.Model
{
    public static class CharHolderGenerator
    {
        public static CharHolder TestAllCats(int count = 1)
        {
            var CH = new CharHolder();
            var c = 0;
            foreach (var item in TypeHelper.ThingTypeProperties.Where(x => x.Usable))
            {
                for (int i = 0; i < count; i++)
                {
                    try
                    {
                        var newThing = CH.Add(item.ThingType);
                        newThing.Bezeichner = "Test";
                        newThing.Notiz = @"Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet. Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet.";
                        newThing.Typ = item.Type.Name;
                        newThing.Wert = c++;
                        newThing.Zusatz = TLIB.StaticRandom.Next(0, 2) == 0 ? "-1" : "+1";
                    }
                    catch (System.Exception)
                    {
                        continue;
                    }
                }
            }
            CH.CTRLItem[0].Value.BaseValue = 123;
            CH.CTRLHandlung[0].Bezeichner = "Handlung mit Cons";
            CH.CTRLHandlung[0].Value.BaseValue = 55;
            CH.CTRLHandlung[0].Value.Connected.Add(CH.CTRLItem[0].Value);
            CH.AfterLoad();
            return CH;
        }

        public static CharHolder CreateEmtpyChar()
        {
            var ret = new CharHolder();
            ret.AfterLoad();
            try
            {
                ret.FileInfo = new FileInfo(TAPPLICATION.IO.SharedIO.CurrentSavePath + ret.MakeName(false));
            }
            catch (Exception)
            {
            }
            return ret;
        }

        public static CharHolder CreateCharWithStandardContent()
        {
            var ret = new CharHolder();
            var item = new Handlung
            {
                Bezeichner = CustomManager.GetString("Content_Selbstbeherrschung")
            };
            item.LinkedThings.Add(ret.CTRLAttribut.MI_Charisma);
            item.LinkedThings.Add(ret.CTRLAttribut.MI_Willen);
            ret.Add(item);
            item = new Handlung
            {
                Bezeichner = CustomManager.GetString("Content_Menschenkenntnis")
            };
            item.LinkedThings.Add(ret.CTRLAttribut.MI_Intuition);
            item.LinkedThings.Add(ret.CTRLAttribut.MI_Charisma);
            ret.Add(item);
            item = new Handlung
            {
                Bezeichner = CustomManager.GetString("Content_Erinnerung")
            };
            item.LinkedThings.Add(ret.CTRLAttribut.MI_Logik);
            item.LinkedThings.Add(ret.CTRLAttribut.MI_Willen);
            ret.Add(item);
            item = new Handlung
            {
                Bezeichner = CustomManager.GetString("Content_Schadenswiderstand")
            };
            item.LinkedThings.Add(ret.CTRLAttribut.MI_Konsti);
            //item.LinkedThings.Add(ret.CTRLPanzerung.ActiveItem.Value);
            ret.Add(item);
            ret.AfterLoad();
            ret.HasChanges = true;
            try
            {
                ret.FileInfo = new FileInfo(IO.CharHolderIO.CurrentSavePath + ret.MakeName(false));
            }
            catch (Exception ex)
            {
                if (System.Diagnostics.Debugger.IsAttached) System.Diagnostics.Debugger.Break();
                TLIB.Log.Write("Error setting newchar saveplace", ex);
            }
            return ret;
        }
    }
}