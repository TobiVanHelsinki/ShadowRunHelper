﻿using ShadowRunHelper.CharModel;
using Windows.UI.Xaml.Controls;

namespace ShadowRunHelper.UI.Edit
{
    public sealed partial class Edit_Person : ContentDialog
    {
        public Person Data;

        public Edit_Person(Person data)
        {
            this.InitializeComponent();
            this.Data = data;
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
