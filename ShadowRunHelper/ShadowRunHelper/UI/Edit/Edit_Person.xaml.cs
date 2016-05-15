using Windows.UI.Xaml.Controls;

namespace ShadowRunHelper.UI.Edit
{
    public sealed partial class Edit_Person : ContentDialog
    {
        public CharModel.Person Data;
        public Controller.HashDictionary HD;

        public Edit_Person(CharModel.Person data, Controller.HashDictionary hd)
        {
            this.InitializeComponent();
            this.Data = data;
            this.HD = hd;
            //System.DateTimeOffset GebDatePickerNew = new System.DateTimeOffset();
            //GebDatePickerNew.DateTime = new System.DateTimeOffset(this.Data.Geburtsdatum2);
            //GebDatePickerNew.Date = 
            try
            {
                this.GebDatePicker.Date = this.Data.GeburtsdatumDateTimeOffset;
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
            this.Data.GeburtsdatumDateTimeOffset = ((DatePicker)sender).Date;
        }
    }
}
