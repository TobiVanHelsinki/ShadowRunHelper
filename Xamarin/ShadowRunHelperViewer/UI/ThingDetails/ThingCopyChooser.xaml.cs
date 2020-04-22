
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using ShadowRunHelper;
using ShadowRunHelper.CharModel;
using ShadowRunHelper.Model;
using ShadowRunHelperViewer.UI.Resources;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ShadowRunHelperViewer
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ThingCopyChooser : PopupPage, INotifyPropertyChanged
    {
        #region NotifyPropertyChanged
        public new event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion NotifyPropertyChanged

        public Thing MyThing { get; set; }
        public CharHolder MyChar { get; set; }

        private readonly bool Move;

        public ThingCopyChooser(Thing mything, CharHolder mychar, bool move)
        {
            MyThing = mything;
            MyChar = mychar;
            Move = move;
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            BindingContext = this;
            base.OnAppearing();
            Lists.ItemsSource = TypeHelper.ThingTypeProperties;
        }

        private void PopupPage_SizeChanged(object sender, EventArgs e)
        {
            (MainFrame.WidthRequest, MainFrame.HeightRequest) = Common.MaximumDimensions(Width, Height);
        }

        private void ViewCell_Tapped(object sender, EventArgs e)
        {
            if (sender is ViewCell vc && vc.BindingContext is ThingTypeProperty ttp)
            {
                MyChar.PrepareToMoveOrCopy(MyThing);
                if (Move)
                {
                    MyChar.MovePreparedItems(ttp.ThingType);
                }
                else
                {
                    MyChar.CopyPreparedItems(ttp.ThingType);
                }
                PopupNavigation.Instance.PopAsync();
            }
        }
    }
}