using System;
using System.Collections.Generic;
using System.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

namespace ShadowRunHelper.UI
{
    public class SharePageFunctions
    {
        public static void EditBox_SelectAll(object sender, RoutedEventArgs e)
        {
            (sender as TextBox).SelectAll();
        }
        public static void EditBox_UpDownKeys(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Up)
            {
                if (double.TryParse((sender as TextBox).Text, out double result))
                {
                    result++;
                    (sender as TextBox).Text = result.ToString();
                    e.Handled = true;
                }
            }
            else if (e.Key == Windows.System.VirtualKey.Down)
            {
                if (double.TryParse((sender as TextBox).Text, out double result))
                {
                    result--;
                    (sender as TextBox).Text = result.ToString();
                    e.Handled = true;
                }
            }
        }
    }
}
