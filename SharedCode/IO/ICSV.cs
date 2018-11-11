using System;
using System.Collections.Generic;

namespace ShadowRunHelper.IO
{
    public interface ICSV
    {
        string ToCSV(char strDelimiter);
        string HeaderToCSV(char strDelimiter);
        void FromCSV(Dictionary<string, string> dic);
    }
}
