using TLIB.PlatformHelper;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;


namespace ShadowRunHelper.Win.UI
{
    public sealed partial class SettingElement : UserControl
    {
        string ja = StringHelper.GetString("UI_Optionen_Toggle/OnContent");
        string nein = StringHelper.GetString("UI_Optionen_Toggle/OffContent");
        public SettingElement()
        {
            this.InitializeComponent();
        }

        private void DisableToolTip()
        {
            ToolTip.Visibility = Visibility.Collapsed;
            ToolTipPanelSign.Visibility = Visibility.Collapsed;
        }

        public string Text
        {
            get { return GetValue(TextProperty) as string; }
            set { SetValue(TextProperty, value); }
        }
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(SettingElement), new PropertyMetadata(0));

        public string Tip
        {
            get { var s = GetValue(TipProperty); if (string.IsNullOrEmpty(s as string)) { DisableToolTip();  return "---"; } else { return s as string; } }
            set { if (string.IsNullOrEmpty(value)) { DisableToolTip(); } else { SetValue(TipProperty, value); } }
        }
        public static readonly DependencyProperty TipProperty =
            DependencyProperty.Register("Tip", typeof(string), typeof(SettingElement), new PropertyMetadata(0));

        public string OnText
        {
            get { var s = GetValue(OnTextProperty); if (string.IsNullOrEmpty(s as string)) { return ja; } else { return s as string; } }
            set { if (string.IsNullOrEmpty(value)) { SetValue(OnTextProperty, ja); } else { SetValue(OnTextProperty, value); } }
        }

        public static readonly DependencyProperty OnTextProperty =
            DependencyProperty.Register("OnText", typeof(string), typeof(SettingElement), new PropertyMetadata(0));

        public string OffText
        {
            get { var s = GetValue(OffTextProperty); if (string.IsNullOrEmpty(s as string)) { return nein; } else { return s as string; } }
            set { if (string.IsNullOrEmpty(value)) { SetValue(OffTextProperty, nein); } else { SetValue(OffTextProperty, value); } }
        }

        public static readonly DependencyProperty OffTextProperty =
            DependencyProperty.Register("OffText", typeof(string), typeof(SettingElement), new PropertyMetadata(0));

        public bool Setting
        {
            get { return (bool)GetValue(SettingProperty); }
            set { SetValue(SettingProperty, value); }
        }

        public static readonly DependencyProperty SettingProperty =
            DependencyProperty.Register("Setting", typeof(bool), typeof(SettingElement), new PropertyMetadata(0));

    }
}
