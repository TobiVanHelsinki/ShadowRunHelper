using ShadowRunHelper.Model;
using System;
using System.Collections.Generic;

namespace ShadowRunHelper
{
    public static class DebugOperations
    {
        public static readonly Dictionary<string, (DateTime StartTime, DateTime StopTime)> Dict = new Dictionary<string, (DateTime StartTime, DateTime StopTime)>();

        //public static void Start(string Name)
        //{
        //    if (!Dict.ContainsKey(Name))
        //    {
        //        Dict.Add(Name, (DateTime.Now, default));
        //    }
        //}
        //public static void Stop(string Name)
        //{
        //    Dict[Name] = (Dict[Name].StartTime, DateTime.Now);
        //}
        const int Padding = 15;

        public static void TraceException(
        [System.Runtime.CompilerServices.CallerMemberName] string memberName = "",
        [System.Runtime.CompilerServices.CallerFilePath] string sourceFilePath = "",
        [System.Runtime.CompilerServices.CallerLineNumber] int sourceLineNumber = 0)
        {
            AppModel.Instance.NewNotification(@"Exception from "+memberName+" at:" + sourceFilePath+":"+ sourceLineNumber);
        }
        //public static void Finish()
        //{
        //    string Not = "";
        //    foreach (var item in Dict)
        //    {
        //        string k = item.Key.PadRight(Padding).Remove(Padding-1).PadRight(Padding);
        //        System.Diagnostics.Debug.WriteLine("{0}{1}-{2} = {3}", k, item.Value.StartTime.ToString("ss.fff"), item.Value.StopTime.ToString("ss.fff"), (item.Value.StopTime - item.Value.StartTime).ToString().Replace("00:", ""));
        //        Not += string.Format("{0}\t{1} - {2} =\t{3}", k, item.Value.StartTime.ToString("ss.fff"), item.Value.StopTime.ToString("ss.fff"), (item.Value.StopTime - item.Value.StartTime).ToString().Replace("00:", ""));
        //        Not += "\n";
        //    }
        //    if (Dict.Count != 0)
        //    {
        //        Model.AppModel.Instance.NewNotification(Not.Remove(Not.Length - 1), false);
        //    }
        //    Dict.Clear();
        //}
    }
}
