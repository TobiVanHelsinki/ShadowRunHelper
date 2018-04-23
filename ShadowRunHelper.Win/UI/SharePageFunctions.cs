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
        public static void EditBox_GotFocus(object sender, RoutedEventArgs e)
        {
            (sender as TextBox).SelectAll();
        }
        public static void EditBox_ProcessKeyboardAccelerators(UIElement sender, ProcessKeyboardAcceleratorEventArgs args)
        {
            if (args.Key == Windows.System.VirtualKey.Up)
            {
                if (double.TryParse((sender as TextBox).Text, out double result))
                {
                    result++;
                    (sender as TextBox).Text = result.ToString();
                    args.Handled = true;
                }
            }
            else if (args.Key == Windows.System.VirtualKey.Down)
            {
                if (double.TryParse((sender as TextBox).Text, out double result))
                {
                    result--;
                    (sender as TextBox).Text = result.ToString();
                    args.Handled = true;
                }
            }
        }
    }
}
