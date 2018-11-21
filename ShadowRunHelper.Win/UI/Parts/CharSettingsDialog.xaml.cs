using ShadowRunHelper.Model;
using System.Collections.Generic;
using System.Linq;
using TLIB;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

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

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Hide();
        }
        void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            Model.MainObject.Settings.ResetCategoryOptions();
        }


        #region Char Settings
        class GroupInfoList<T> : List<object>
        {
            public object Key { get; set; }
            public new IEnumerator<object> GetEnumerator()
            {
                return (System.Collections.Generic.IEnumerator<object>)base.GetEnumerator();
            }
        }
        void LoadCategoryOptions()
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
                        info.Key = PlatformHelper.GetString("Char_View_Pivot_Aktion/Label");
                        break;
                    case 1:
                        info.Key = PlatformHelper.GetString("Char_View_Pivot_Item/Label");
                        break;
                    case 2:
                        info.Key = PlatformHelper.GetString("Char_View_Pivot_Kampf/Label");
                        break;
                    case 3:
                        info.Key = PlatformHelper.GetString("Char_View_Pivot_Person/Label");
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
        #endregion
    }
}
