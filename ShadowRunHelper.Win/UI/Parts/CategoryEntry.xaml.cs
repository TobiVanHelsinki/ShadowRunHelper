using ShadowRunHelper.CharModel;
using ShadowRunHelper.Model;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace ShadowRunHelper.UI
{
    public sealed partial class CategoryEntry : UserControl
    {
        readonly AppModel Model = AppModel.Instance;
        public Thing CurrentThing => DataContext as Thing;
        public CategoryEntry()
        {
            InitializeComponent();
            DataContextChanged += (x,y) => Initialize();
        }
        bool Initialized;
        private void Initialize()
        {
            if (Initialized || CurrentThing == null)
            {
                return;
            }
            CurrentThing.PropertyChanged += CurrentThing_PropertyChanged;
            if (CurrentThing.IsSeperator)
            {
                Default = Seperator;
                Expanded = SeperatorX;
            }
            else if (CurrentThing.ThingType == ThingDefs.Vorteil || CurrentThing.ThingType == ThingDefs.Nachteil)
            {
                Default = EigenschaftItem;
                Expanded = EigenschaftItemX;
            }
            else
            {
                var Name = TypeHelper.ThingTypeProperties.Find(x => x.ThingType == CurrentThing.ThingType).DisplayName;
                Resources.TryGetValue(Name + "Item", out object val);
                Default = (DataTemplate)val;
                Resources.TryGetValue(Name + "ItemX", out val);
                Expanded = (DataTemplate)val;
            }
            SetDefaultTemplate();
            Initialized = true;
        }

        private void CurrentThing_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (CurrentThing.IsSeperator)
            {
                Default = Seperator;
                Expanded = Seperator;
                SetDefaultTemplate();
            }
        }

        DataTemplate Default;
        DataTemplate Expanded;

        public void SetExpandedTemplate()
        {
            if (Expanded == null)
            {
                return;
            }
            EntryItem.ContentTemplate = Expanded;
        }
        internal void SetDefaultTemplate()
        {
            EntryItem.ContentTemplate = Default;
        }

        #region Model-UI-Stuff

        async void Edit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                await new EditThingDialog(((Thing)((Button)sender).DataContext)).ShowAsync();
            }
            catch (Exception)
            {
            }
        }
        async void Edit_Attribut(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            try
            {
                await new EditThingDialog(((Thing)((Grid)sender).DataContext)).ShowAsync();
            }
            catch (Exception)
            {
            }
        }
        void Del_Click(object sender, RoutedEventArgs e)
        {
            if ((Thing)((Button)sender).DataContext != null)
            {
                Model.MainObject.Remove((Thing)((Button)sender).DataContext);
            }
        }

        #endregion
    }
}
