using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// Die Elementvorlage "Leere Seite" ist unter http://go.microsoft.com/fwlink/?LinkId=234238 dokumentiert.

namespace ShadowRun_Charakter_Helper
{
    /// <summary>
    /// Eine leere Seite, die eigenständig verwendet werden kann oder auf die innerhalb eines Rahmens navigiert werden kann.
    /// </summary>
    public sealed partial class TestPage : Page
    {
        public TestPage()
        {
            Menu menu = new Menu();


            menu.TopMenuItems.Add(new TopMenu() { GroupName = "Basic Reports - Mobile" });

            menu.TopMenuItems[0].SubMenuItems.Add(new SubMenu() { ItemName = "Sales Reports - mobile" });

            menu.TopMenuItems.Add(new TopMenu() { GroupName = "Enhanced Reports - Mobile" });

            menu.TopMenuItems[1].SubMenuItems.Add(new SubMenu() { ItemName = "Subcategory Month - mobile" });

            menu.TopMenuItems[1].SubMenuItems.Add(new SubMenu() { ItemName = "Top Categories - mobile" });

            this.DataContext = menu;
            this.InitializeComponent();
        }



        private void StackPanel_Tapped(object sender, TappedRoutedEventArgs e)

        {

            TextBlock txtBlock = ((Windows.UI.Xaml.RoutedEventArgs)(e)).OriginalSource as TextBlock;

            if (txtBlock != null)

            {

                txtSubMenuTapped.Text = txtSubMenuTapped.Tag.ToString() + txtBlock.Text;

            }

            e.Handled = true;

        }
    }
}
