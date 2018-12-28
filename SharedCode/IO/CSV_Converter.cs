using System;
using System.Collections.Generic;
using System.Linq;

namespace ShadowRunHelper.IO
{
    public static class CSV_Converter
    {
        public static string Data2CSV(char strDelimiter, char strNewLine, IEnumerable<ICSV> DataToUse )
        {
            string ret = "sep=" + strDelimiter + strNewLine;
            if (DataToUse.Count() >= 1)
            {
                ret += DataToUse.First().HeaderToCSV(strDelimiter);
            }
            ret += strNewLine;
            foreach (ICSV item in DataToUse)
            {
                ret += item.ToCSV(strDelimiter);
                ret += strNewLine;
            }
            return ret;
        }

        public static IEnumerable<T> CSV2Data<T>(char strDelimiter, char strNewLine, string strReadFile) where T : ICSV 
        {
            List<T> ret = new List<T>();
            string[] Lines = strReadFile.Split(strNewLine);
            string[] Headar = { };
            for (int i = 0; i < Lines.Length; i++) //start at 2 to overjump first lines
            {
                // key = propertie name, value = value
                string[] CSVEntries = Lines[i].Split(strDelimiter);
                if (CSVEntries.Length < 5)
                {
                    continue;
                }
                if (Headar.Length < ShadowRunHelper.CharModel.Thing.nThingPropertyCount)
                {
                    Headar = Lines[i].Split(strDelimiter);
                    continue;
                }
                Dictionary<string, string> Dic = new Dictionary<string, string>();
                int j = 0;
                foreach (var itemstring in CSVEntries)
                {
                    try
                    {
                        Dic.Add(Headar[j], itemstring);
                    }
                    catch (Exception ex)
                    {
                        TAPPLICATION.Debugging.TraceException(ex);
                        continue;
                    }
                    j++;
                }
                var newThing = Activator.CreateInstance<T>();
                ret.Add(newThing);
                newThing.FromCSV(Dic);
            }
            return ret;
        }

    }
}
