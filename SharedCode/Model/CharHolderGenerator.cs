//Author: Tobi van Helsinki

using ShadowRunHelper.CharModel;
using SharedCode.Resources;
using System;
using System.IO;
using System.Linq;
using ShadowRunHelper.IO;

namespace ShadowRunHelper.Model
{
    public static class CharHolderGenerator
    {
        public static CharHolder TestAllCats(int count = 1)
        {
            var testChar = new CharHolder();
            try
            {
                testChar.FileInfo = new FileInfo(Path.Combine(SharedIO.CurrentSavePath, "CompleteTestChar" + Constants.DATEIENDUNG_CHAR));
            }
            catch (Exception)
            {
            }

            var c = 0;
            foreach (var item in TypeHelper.ThingTypeProperties.Where(x => x.Usable))
            {
                for (int i = 0; i < count; i++)
                {
                    try
                    {
                        var newThing = testChar.Add(item.ThingType);
                        newThing.Bezeichner = "Test " + i;
                        newThing.Notiz = @"Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet. Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet.";
                        newThing.Typ = item.Type.Name;
                        newThing.Value.BaseValue = c++;
                        newThing.Zusatz = TLIB.StaticRandom.Next(0, 2) == 0 ? "-1" : "+1";
                    }
                    catch (System.Exception)
                    {
                        continue;
                    }
                }
            }
            testChar.CTRLItem[0].Value.BaseValue = 123;
            testChar.CTRLHandlung[0].Bezeichner = "Handlung mit Cons";
            testChar.CTRLHandlung[0].Value.BaseValue = 55;
            testChar.CTRLHandlung[0].Value.Connected.Add(testChar.CTRLItem[0].Value);
            testChar.AfterLoad();
            return testChar;
        }

        public static CharHolder CreateEmtpyChar()
        {
            var ret = new CharHolder();
            ret.AfterLoad();
            try
            {
                ret.FileInfo = new FileInfo(ShadowRunHelper.IO.SharedIO.CurrentSavePath + ret.MakeName(false));
            }
            catch (Exception)
            {
            }
            return ret;
        }

        public static CharHolder CreateCharWithStandardContent()
        {
            var ret = new CharHolder();

            #region STD Content
            {
                ret.CTRLAttribut.Konsti.Value.BaseValue = 1;
                ret.CTRLAttribut.Geschick.Value.BaseValue = 1;
                ret.CTRLAttribut.Reaktion.Value.BaseValue = 1;
                ret.CTRLAttribut.Staerke.Value.BaseValue = 1;
                ret.CTRLAttribut.Charisma.Value.BaseValue = 1;
                ret.CTRLAttribut.Logik.Value.BaseValue = 1;
                ret.CTRLAttribut.Intuition.Value.BaseValue = 1;
                ret.CTRLAttribut.Willen.Value.BaseValue = 1;
            }
            {
                var item = new Item
                {
                    Bezeichner = ModelResources.Content_SmartLink,
                };
                item.Value.BaseValue = 1;
                item.Aktiv = false;
                item.Besitz = true;
                ret.Add(item);
                item = new Item
                {
                    Bezeichner = ModelResources.Content_SmartLinkPrecision,
                };
                item.Value.BaseValue = 2;
                item.Aktiv = true;
                item.Besitz = true;
                ret.Add(item);
            }
            {
                var item = new Fertigkeit
                {
                    Bezeichner = ModelResources.Content_Wahrnehmung,
                };
                item.Value.Connected.Add(ret.CTRLAttribut.Intuition.Value);
                ret.Add(item);
                item = new Fertigkeit
                {
                    Bezeichner = ModelResources.Content_Schleichen,
                };
                item.Value.Connected.Add(ret.CTRLAttribut.Intuition.Value);
                ret.Add(item);
                item = new Fertigkeit
                {
                    Bezeichner = ModelResources.Content_Akrobatik,
                };
                item.Value.Connected.Add(ret.CTRLAttribut.Geschick.Value);
                ret.Add(item);

                item = new Fertigkeit
                {
                    Bezeichner = ModelResources.Content_ErsteHilfe,
                };
                item.Value.Connected.Add(ret.CTRLAttribut.Logik.Value);
                ret.Add(item);
                item = new Fertigkeit
                {
                    Bezeichner = ModelResources.Content_Bodenfahrzeuge,
                };
                item.Value.Connected.Add(ret.CTRLAttribut.Geschick.Value);
                ret.Add(item);
                item = new Fertigkeit
                {
                    Bezeichner = ModelResources.Content_FeuerwaffeFertigkeit,
                };
                item.Value.Connected.Add(ret.CTRLAttribut.Geschick.Value);
                ret.Add(item);
                item = new Fertigkeit
                {
                    Bezeichner = ModelResources.Content_Nahkampf,
                };
                item.Value.Connected.Add(ret.CTRLAttribut.Geschick.Value);
                ret.Add(item);
                item = new Fertigkeit
                {
                    Typ = ModelResources.Content_Typ_Elektronik,
                    Bezeichner = ModelResources.Content_Computer,
                };
                item.Value.Connected.Add(ret.CTRLAttribut.Logik.Value);
                ret.Add(item);
                item = new Fertigkeit
                {
                    Typ = ModelResources.Content_Typ_Sprache,
                    Bezeichner = ModelResources.Content_Muttersprache,
                };
                item.Value.BaseValue = 99;
                ret.Add(item);
            }
            {
                var item = new Handlung
                {
                    Typ = ModelResources.Content_Typ_Physisch,
                    Bezeichner = ModelResources.Content_Ini,
                };
                item.Value.Connected.Add(ret.CTRLAttribut.Reaktion.Value);
                item.Value.Connected.Add(ret.CTRLAttribut.Intuition.Value);
                ret.Add(item);
                item = new Handlung
                {
                    Typ = ModelResources.Content_Typ_Physisch,
                    Bezeichner = ModelResources.Content_FeuerwaffeHandlung,
                };
                item.Value.Connected.Add(ret.CTRLFertigkeit.Data.FirstOrDefault(x => x.Bezeichner == ModelResources.Content_FeuerwaffeFertigkeit).Value);
                item.Value.Connected.Add(ret.CTRLAttribut.Geschick.Value);
                item.Limit.Connected.Add(ret.CTRLFernkampfwaffe.ActiveItem.Precision);
                item.Value.Connected.Add(ret.CTRLItem.Data.FirstOrDefault(x => x.Bezeichner == ModelResources.Content_SmartLink).Value);
                item.Limit.Connected.Add(ret.CTRLItem.Data.FirstOrDefault(x => x.Bezeichner == ModelResources.Content_SmartLinkPrecision).Value);
                ret.Add(item);
                item = new Handlung
                {
                    Typ = ModelResources.Content_Typ_Physisch,
                    Bezeichner = ModelResources.Content_Ausweichen,
                };
                item.Value.Connected.Add(ret.CTRLAttribut.Reaktion.Value);
                item.Value.Connected.Add(ret.CTRLAttribut.Intuition.Value);
                ret.Add(item);
                item = new Handlung
                {
                    Typ = ModelResources.Content_Typ_Physisch,
                    Bezeichner = ModelResources.Content_Schadenswiderstand,
                };
                item.Value.Connected.Add(ret.CTRLAttribut.Konsti.Value);
                item.Value.Connected.Add(ret.CTRLPanzerung.ActiveItem.Value);
                ret.Add(item);
                item = new Handlung
                {
                    Typ = ModelResources.Content_Typ_Physisch,
                    Bezeichner = ModelResources.Content_Widerstand,
                    Notiz = ModelResources.Content_Widerstand_Note,
                };
                item.Value.Connected.Add(ret.CTRLAttribut.Konsti.Value);
                item.Value.Connected.Add(ret.CTRLAttribut.Willen.Value);
                ret.Add(item);

                item = new Handlung
                {
                    Typ = ModelResources.Content_Typ_Mental,
                    Bezeichner = ModelResources.Content_Selbstbeherrschung,
                };
                item.Value.Connected.Add(ret.CTRLAttribut.Charisma.Value);
                item.Value.Connected.Add(ret.CTRLAttribut.Willen.Value);
                ret.Add(item);
                item = new Handlung
                {
                    Typ = ModelResources.Content_Typ_Mental,
                    Bezeichner = ModelResources.Content_Menschenkenntnis,
                };
                item.Value.Connected.Add(ret.CTRLAttribut.Intuition.Value);
                item.Value.Connected.Add(ret.CTRLAttribut.Charisma.Value);
                ret.Add(item);
                item = new Handlung
                {
                    Typ = ModelResources.Content_Typ_Mental,
                    Bezeichner = ModelResources.Content_Erinnerung,
                };
                item.Value.Connected.Add(ret.CTRLAttribut.Logik.Value);
                item.Value.Connected.Add(ret.CTRLAttribut.Willen.Value);
                ret.Add(item);

                item = new Handlung
                {
                    Typ = ModelResources.Content_Typ_Matrix,
                    Bezeichner = ModelResources.Content_Matrixsuche,
                };
                item.Value.Connected.Add(ret.CTRLFertigkeit.Data.FirstOrDefault(x => x.Bezeichner == ModelResources.Content_Computer).Value);
                item.Value.Connected.Add(ret.CTRLAttribut.Intuition.Value);
                ret.Add(item);
            }
            #endregion STD Content

            ret.AfterLoad();
            ret.HasChanges = true;
            try
            {
                ret.FileInfo = new FileInfo(SharedIO.CurrentSavePath + ret.MakeName(false));
            }
            catch (Exception ex)
            {
                TLIB.Log.Write("Error setting newchar saveplace", ex);
            }
            return ret;
        }
    }
}