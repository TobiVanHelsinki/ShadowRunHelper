using System;
using System.Linq;
using System.Text.RegularExpressions;
using Windows.UI.Xaml.Data;

namespace ShadowRunHelper.UI.Converter
{
    // Custom class implements the IValueConverter interface. 
    public class io_ZahlenDouble : IValueConverter
    {
        #region IValueConverter Members 
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return value.ToString();
        }
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            string strOrigin = (value as string);
            if (strOrigin == null)
            {
                return 0;
            }
            string strTemp = "";
            double dRetVal = 0;
            strOrigin += "+";
            foreach (char item in strOrigin)
            {
                //filter out letters or special chars
                if (char.IsNumber(item)
                        || char.IsDigit(item)
                        || char.IsSeparator(item)
                        || char.IsPunctuation(item)
                        || item == '-' 
                        || item == '+'
                        )
                {
                    if (item == '-' || item == '+')
                    {
                        try
                        {
                            dRetVal += Double.Parse(strTemp);
                        }
                        catch (Exception) { }
                        strTemp = "";
                    }
                    strTemp += item;
                }

            }
            return dRetVal;
        }
        #endregion
    }
}
