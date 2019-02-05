using ShadowRunHelper;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ShadowRunHelperViewer
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ControlCenter : ContentView
    {
        public ControlCenter()
        {
            InitializeComponent();
            try
            {
                BindingContext = this;
            }
            catch (Exception ex)
            {
                TAPPLICATION.Debugging.TraceException(ex);
            }
            MainListView.ItemsSource = new string[] {
                            CustomManager.GetString("UI_TxT_SaveAtCurrentPlace/Text"),
                            CustomManager.GetString("UI_TxT_SaveExtern/Text"),
                            CustomManager.GetString("UI_TxT_CSV_Export/Text"),
                            CustomManager.GetString("UI_TxT_OpenFolder/Text"),
                            CustomManager.GetString("UI_TxT_SubtractLifeStyleCost/Text"),
                            CustomManager.GetString("UI_TxT_CharSettings/Text"),
                            CustomManager.GetString("UI_TxT_Repair/Text"),
                            CustomManager.GetString("UI_TxT_Unload/Text"),
                        };
        }

        private void Add_Clicked(object sender, EventArgs e)
        {

        }

        private void Delete_Clicked(object sender, EventArgs e)
        {

        }

        private void Edit_Clicked(object sender, EventArgs e)
        {

        }
    }
}