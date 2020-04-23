//Author: Tobi van Helsinki

using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Rg.Plugins.Popup.Services;
using ShadowRunHelper;
using ShadowRunHelper.CharModel;
using ShadowRunHelper.Model;
using SharedCode.Resources;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ShadowRunHelperViewer
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ThingCopyChooser : ContentView, INotifyPropertyChanged
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
            BindingContext = this;
            Lists.ItemsSource = TypeHelper.ThingTypeProperties
                .Where(x => x.Usable)
                .Where(x => x.ThingType != ThingDefs.Attribut && x.ThingType != ThingDefs.Berechnet && x.ThingType != ThingDefs.Note);
            if (Move)
            {
                MoveText.IsVisible = true;
                CopyText.IsVisible = false;
            }
            else
            {
                MoveText.IsVisible = false;
                CopyText.IsVisible = true;
            }
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