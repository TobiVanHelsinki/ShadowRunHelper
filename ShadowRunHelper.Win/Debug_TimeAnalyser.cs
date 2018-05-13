using System;
using System.Collections.Generic;
using System.Linq;
using TLIB;

namespace ShadowRunHelper
{
    public class Debug_TimeAnalyser
    {
        public static readonly Dictionary<string, (DateTime StartTime, DateTime StopTime)> Dict = new Dictionary<string, (DateTime StartTime, DateTime StopTime)>();

        public static void Start(string Name)
        {
            if (!Dict.ContainsKey(Name))
            {
                Dict.Add(Name, (DateTime.Now, default));
            }
        }
        public static void Stop(string Name)
        {
            Dict[Name] = (Dict[Name].StartTime, DateTime.Now);
        }
        const int Padding = 15;
        public static void Finish()
        {
            string Not = "";
            foreach (var item in Dict)
            {
                string k = item.Key.PadRight(Padding).Remove(Padding-1).PadRight(Padding);
                SystemHelper.WriteLine("{0}{1}-{2} = {3}", k, item.Value.StartTime.ToString("ss.fff"), item.Value.StopTime.ToString("ss.fff"), (item.Value.StopTime - item.Value.StartTime).ToString().Replace("00:", ""));
                Not += string.Format("{0}\t{1} - {2} =\t{3}", k, item.Value.StartTime.ToString("ss.fff"), item.Value.StopTime.ToString("ss.fff"), (item.Value.StopTime - item.Value.StartTime).ToString().Replace("00:", ""));
                Not += "\n";
            }
            if (Dict.Count != 0)
            {
                Model.AppModel.Instance.NewNotification(Not.Remove(Not.Length - 1), false);
            }
            Dict.Clear();
        }
    }
}
