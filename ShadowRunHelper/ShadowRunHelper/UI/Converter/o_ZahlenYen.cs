using System;
using Windows.UI.Xaml.Data;

namespace ShadowRunHelper.UI.Converter
{
    // Custom class implements the IValueConverter interface. 
    public class o_ZahlenYen : IValueConverter
    {
        #region IValueConverter Members 
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            string returnvalue = "";
            int j = 1;
            for (int i = (value.ToString()).Length-1; i >= 0 ; i--)
            {
                returnvalue += (value.ToString())[i];
                if (j%3==0 && i>0)
                {
                    returnvalue += ".";//todo lokalisiern
                }
                j++;
            }
            char[] c = returnvalue.ToCharArray();
            Array.Reverse(c);
            returnvalue = new string(c);
            return returnvalue  + " ¥";
        }
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            double retvalue = 0;
            return retvalue;
        }
        #endregion
    }
}
