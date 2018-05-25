using ShadowRunHelper.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using TLIB;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// Die Elementvorlage "Inhaltsdialogfeld" wird unter https://go.microsoft.com/fwlink/?LinkId=234238 dokumentiert.

namespace ShadowRunHelper.UI
{
    public sealed partial class CharSettingsDialog : ContentDialog
    {
        AppModel Model => AppModel.Instance;
        public CharSettingsDialog()
        {
            this.InitializeComponent();
            LoadCategoryOptions();
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }

        #region Char Settings
        public class GroupInfoList<T> : List<object>
        {
            public object Key { get; set; }
            public new IEnumerator<object> GetEnumerator()
            {
                return (System.Collections.Generic.IEnumerator<object>)base.GetEnumerator();
            }
        }
        public void LoadCategoryOptions()
        {
            if (Model?.MainObject?.Settings?.CategoryOptions == null)
            {
                return;
            }
            List<GroupInfoList<object>> DataGrouped = new List<GroupInfoList<object>>();
            var query = from opt in Model.MainObject.Settings.CategoryOptions
                        group opt by opt.Pivot into g
                        select new { GroupNr = g.Key, Items = g };

            foreach (var g in query)
            {
                GroupInfoList<object> info = new GroupInfoList<object>();
                switch (g.GroupNr)
                {
                    case 0:
                        info.Key = StringHelper.GetString("Char_View_Pivot_Aktion/Label");
                        break;
                    case 1:
                        info.Key = StringHelper.GetString("Char_View_Pivot_Item/Label");
                        break;
                    case 2:
                        info.Key = StringHelper.GetString("Char_View_Pivot_Kampf/Label");
                        break;
                    case 3:
                        info.Key = StringHelper.GetString("Char_View_Pivot_Person/Label");
                        break;
                    default:
                        break;
                }

                foreach (var item in g.Items)
                {
                    info.Add(item);
                }
                DataGrouped.Add(info);
            }
            GroupedCategoryOptions.Source = DataGrouped;
        }
        void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            Model.MainObject.Settings.ResetCategoryOptions();
        }

        #endregion
    }
}
