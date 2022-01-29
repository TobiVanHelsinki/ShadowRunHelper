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
            DataContextChanged += (x,y) => Initialize();
        }
        bool Initialized;
        private void Initialize()
        {
            if (Initialized || CurrentThing == null)
            {
                return;
            }
            InitializeComponent();
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
                Default = val as DataTemplate;
                Resources.TryGetValue(Name + "ItemX", out val);
                Expanded = val as DataTemplate;
            }
            SetDefaultTemplate();
            Initialized = true;
        }

        void CurrentThing_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (CurrentThing?.IsSeperator == true)
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
        public void SetDefaultTemplate()
        {
            EntryItem.ContentTemplate = Default;
        }

        #region Model-UI-Stuff

        async void Edit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                await new EditThingDialog(((Thing)((FrameworkElement)sender).DataContext)).ShowAsync();
            }
            catch (Exception ex)
 { TAPPLICATION.Debugging.TraceException(ex);
            }
        }
        async void Edit_Attribut(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            try
            {
                await new EditThingDialog(((Thing)((FrameworkElement)sender).DataContext)).ShowAsync();
            }
            catch (Exception ex)
 { TAPPLICATION.Debugging.TraceException(ex);
            }
        }
        void Del_Click(object sender, RoutedEventArgs e)
        {
            if ((Thing)((FrameworkElement)sender).DataContext != null)
            {
                Model.MainObject.Remove((Thing)((FrameworkElement)sender).DataContext);
            }
        }
        private void Fav_Click(object sender, RoutedEventArgs e)
        {
            ((sender as FrameworkElement).DataContext as Thing).IsFavorite =! ((sender as FrameworkElement).DataContext as Thing).IsFavorite;
        }

        private void Copy_Click(object sender, RoutedEventArgs e)
        {
            Model.MainObject.ClearPreparedItems();
            Model.MainObject.PrepareToMoveOrCopy((sender as FrameworkElement).DataContext as Thing);
            Model.MainObject.IsItemsMove = false;
        }

        private void Cut_Click(object sender, RoutedEventArgs e)
        {
            Model.MainObject.ClearPreparedItems();
            Model.MainObject.PrepareToMoveOrCopy((sender as FrameworkElement).DataContext as Thing);
            Model.MainObject.IsItemsMove = true;
        }

        private void Paste_Click(object sender, RoutedEventArgs e)
        {
            if (Model.MainObject.IsItemsMove == true)
            {
                Model.MainObject.MovePreparedItems(CurrentThing.ThingType);
            }
            else if (Model.MainObject.IsItemsMove == false)
            {
                Model.MainObject.CopyPreparedItems(CurrentThing.ThingType);
            }
        }

        private void Share_Click(object sender, RoutedEventArgs e)
        {

        }
        
        #endregion
    }
}
