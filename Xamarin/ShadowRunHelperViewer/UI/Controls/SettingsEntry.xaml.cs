//Author: Tobi van Helsinki

using System;
using ShadowRunHelper;
using ShadowRunHelperViewer.UI.Pages;
using SharedCode.Resources;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ShadowRunHelperViewer.UI.ControlsOther
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsEntry : ContentView
    {
        #region Bindings
        public static readonly BindableProperty SettingProperty = BindableProperty.Create(nameof(Setting), typeof(bool), typeof(SettingsEntry), false, propertyChanged: SettingChanged, defaultBindingMode: BindingMode.TwoWay);
        public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(SettingsEntry), "//Title", propertyChanged: TextChanged);
        public static readonly BindableProperty OnTextProperty = BindableProperty.Create(nameof(OnText), typeof(string), typeof(SettingsEntry), UiResources.OnText, propertyChanged: OnTextChanged);
        public static readonly BindableProperty OffTextProperty = BindableProperty.Create(nameof(OffText), typeof(string), typeof(SettingsEntry), UiResources.OffText, propertyChanged: OffTextChanged);
        #endregion Bindings

        #region Properties
        public bool Setting
        {
            get => (bool)GetValue(SettingProperty);
            set => SetValue(SettingProperty, value);
        }

        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        public string OnText
        {
            get => (string)GetValue(OnTextProperty);
            set => SetValue(OnTextProperty, value);
        }

        public string OffText
        {
            get => (string)GetValue(OffTextProperty);
            set => SetValue(OffTextProperty, value);
        }

        #endregion Properties

        public SettingsModel Settings => SettingsModel.I;

        #region Property Changings

        private static void SettingChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is SettingsEntry entry && newValue is bool b)
            {
                entry.LabelOnText.IsVisible = b;
                entry.LabelOffText.IsVisible = !b;
            }
        }

        private static void TextChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is SettingsEntry entry && newValue is string text)
            {
                entry.TextLabel.Text = UiResources.ResourceManager.GetStringSafe(text);
            }
        }

        private static void OnTextChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is SettingsEntry entry && newValue is string text)
            {
                entry.LabelOnText.Text = text;
            }
        }

        private static void OffTextChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is SettingsEntry entry && newValue is string text)
            {
                entry.LabelOffText.Text = text;
            }
        }
        #endregion Property Changings

        public SettingsEntry()
        {
            InitializeComponent();
            BindingContext = this;
            SettingChanged(this, null, Setting);
        }

        private void OpenTip(object sender, EventArgs e)
        {
            MainPage.Instance.DisplayAlert(UiResources.TipHeader, UiResources.ResourceManager.GetStringSafe(Text + "Tip"), AppResources.OK);
        }
    }
}