using ShadowRunHelper.CharModel;
using ShadowRunHelper.Model;
using ShadowRunHelper.UI;
using ShadowRunHelper.UI.Edit;
using System;
using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace ShadowRunHelper.Win.UI
{
    public sealed partial class CategoryEntry : UserControl
    {
        #region Variables
        readonly AppModel Model = AppModel.Instance;
        #endregion
        public CategoryEntry()
        {
            this.InitializeComponent();
            this.DataContextChanged += CategoryEntry_DataContextChanged;
        }

        private void CategoryEntry_DataContextChanged(FrameworkElement sender, DataContextChangedEventArgs args)
        {
            if (CurrentThing?.ThingType == ThingDefs.Attribut)
            {

            }
            GetStuff();
            DefaultTemplate();
        }

        public Thing CurrentThing => DataContext as Thing;

        //public Thing CurrentThing
        //{
        //    get { return (Thing)GetValue(CurrentThingProperty); }
        //    set { SetValue(CurrentThingProperty, value); OnThingChanged(); }
        //}
        //// Using a DependencyProperty as the backing store for CurrentThing.  This enables animation, styling, binding, etc...
        //public static readonly DependencyProperty CurrentThingProperty =
        //    DependencyProperty.Register("CurrentThing", typeof(Thing), typeof(CategoryEntry), new PropertyMetadata(0));

        DataTemplate Default;
        DataTemplate Expanded;
        private void GetStuff()
        {
            var Name = TypeHelper.ThingDefToString(CurrentThing.ThingType, false);
            Default = (DataTemplate)Resources[Name + "Item"];
            Expanded = (DataTemplate)Resources[Name + "ItemX"];
        }

        public void ExpandedTemplate()
        {
            EntryItem.ContentTemplate = Expanded;
            if (CurrentThing.ThingType == ThingDefs.Handlung)
            {
                if (!SettingsModel.I.TutorialHandlungShown)
                {
#pragma warning disable CS4014
                    new Tutorial(30, 31).ShowAsync();
#pragma warning restore CS4014
                    SettingsModel.I.TutorialHandlungShown = true;
                }
            }
        }
        internal void DefaultTemplate()
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

        async void HandlungEditZusDialog_Click(object sender, RoutedEventArgs e)
        {
            Auswahl dialog = new Auswahl(Model.MainObject.LinkList, ((Handlung)((Button)sender).DataContext).WertZusammensetzung, Filter: Handlung.Filter);
            await dialog.ShowAsync();

        }

        async void HandlungEditGrenzeZusDialog_Click(object sender, RoutedEventArgs e)
        {
            Auswahl dialog = new Auswahl(Model.MainObject.LinkList, ((Handlung)((Button)sender).DataContext).GrenzeZusammensetzung, Filter: Handlung.Filter);
            var ergebnis = await dialog.ShowAsync();
        }

        async void HandlungEditGegenZusDialog_Click(object sender, RoutedEventArgs e)
        {
            Auswahl dialog = new Auswahl(Model.MainObject.LinkList, ((Handlung)((Button)sender).DataContext).GegenZusammensetzung, Filter: Handlung.Filter);
            await dialog.ShowAsync();
        }

        async void FertigkeitenZusammensetzungBearbeiten(object sender, RoutedEventArgs e)
        {
            Auswahl dialog = new Auswahl(Model.MainObject.LinkList, ((Fertigkeit)((Button)sender).DataContext).PoolZusammensetzung, Filter: Fertigkeit.Filter);
            await dialog.ShowAsync();
        }

        async void Edit_LinkedThings(object sender, RoutedEventArgs e)
        {
            ThingDefs type = (ThingDefs)int.Parse((sender as FrameworkElement).Tag as string);
            IEnumerable<ThingDefs> FilterToUse = null;
            switch (type)
            {
                case ThingDefs.Handlung:
                    FilterToUse = Handlung.Filter;
                    break;
                case ThingDefs.Fertigkeit:
                    FilterToUse = Fertigkeit.Filter;
                    break;
                case ThingDefs.Item:
                    break;
                case ThingDefs.Programm:
                    break;
                case ThingDefs.Munition:
                    break;
                case ThingDefs.Implantat:
                    break;
                case ThingDefs.Vorteil:
                    break;
                case ThingDefs.Nachteil:
                    break;
                case ThingDefs.Attribut:
                    FilterToUse = Attribut.Filter;
                    break;
                case ThingDefs.Nahkampfwaffe:
                    break;
                case ThingDefs.Fernkampfwaffe:
                    FilterToUse = Fernkampfwaffe.Filter;
                    break;
                case ThingDefs.Kommlink:
                    break;
                case ThingDefs.CyberDeck:
                    break;
                case ThingDefs.Vehikel:
                    break;
                case ThingDefs.Panzerung:
                    FilterToUse = Panzerung.Filter;
                    break;
                case ThingDefs.Eigenschaft:
                    break;
                case ThingDefs.Adeptenkraft:
                    break;
                case ThingDefs.Geist:
                    break;
                case ThingDefs.Foki:
                    break;
                case ThingDefs.Stroemung:
                    break;
                case ThingDefs.Tradition:
                    break;
                case ThingDefs.Zaubersprueche:
                    break;
                case ThingDefs.Berechnet:
                    break;
                case ThingDefs.KomplexeForm:
                    break;
                case ThingDefs.Sprite:
                    break;
                case ThingDefs.Widgets:
                    break;
                case ThingDefs.Wandlung:
                    break;
                case ThingDefs.Initiation:
                    break;
                default:
                    break;
            }
            var dialog = new Auswahl(Model.MainObject.LinkList, ((Thing)((Button)sender).DataContext).LinkedThings, Filter: FilterToUse);
            await dialog.ShowAsync();
        }
        //async void MunitionBearbeiten(object sender, RoutedEventArgs e)
        //{
        //    var TemList = new ObservableCollection<AllListEntry> ();
        //    Auswahl dialog = new Auswahl(Model.MainObject.LinkList, TemList, false, Fernkampfwaffe.Filter);
        //    await dialog.ShowAsync();
        //    ((Fernkampfwaffe)((Button)sender).DataContext).CurrentMunition = TemList.FirstOrDefault();
        //}
        #endregion
    }
}
