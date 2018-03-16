using ShadowRunHelper.CharModel;
using Windows.UI.Xaml.Controls;

namespace ShadowRunHelper.UI.Edit
{
    public sealed partial class Edit_Person_Detail : ContentDialog
    {
        public Person Data;

        public Edit_Person_Detail(Person data)
        {
            this.InitializeComponent();
            this.Data = data;
            try
            {
                this.GebDatePicker.Date = this.Data.BirthDate;
            }
            catch (System.Exception)
            {
                System.Diagnostics.Debug.WriteLine("Faihler");
            }

        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            ContentDialogButtonClickDeferral deferral = args.GetDeferral();
            deferral.Complete();
        }

        private void DatePicker_DateChanged(object sender, DatePickerValueChangedEventArgs e)
        {
            this.Data.BirthDate = ((DatePicker)sender).Date;
        }
    }
}
