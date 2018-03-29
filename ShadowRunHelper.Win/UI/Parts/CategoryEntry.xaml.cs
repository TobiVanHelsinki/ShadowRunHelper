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
            if (CurrentThing.ThingType == ThingDefs.Vorteil || CurrentThing.ThingType == ThingDefs.Nachteil)
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


        DataTemplate Default;
        DataTemplate Expanded;

        public void SetExpandedTemplate()
        {
            if (Expanded == null)
            {
                return;
            }
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

        async void Edit_LinkedThings(object sender, RoutedEventArgs e)
        {
            ThingDefs type = ((Thing)((FrameworkElement)sender).DataContext).ThingType;

            //ThingDefs type = (ThingDefs)int.Parse((sender as FrameworkElement).Tag as string);
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
                    FilterToUse = Munition.Filter;
                    break;
                case ThingDefs.Implantat:
                    FilterToUse = Implantat.Filter;
                    break;
                case ThingDefs.Vorteil:
                    FilterToUse = Eigenschaft.Filter;
                    break;
                case ThingDefs.Nachteil:
                    FilterToUse = Eigenschaft.Filter;
                    break;
                case ThingDefs.Attribut:
                    FilterToUse = Attribut.Filter;
                    break;
                case ThingDefs.Nahkampfwaffe:
                    FilterToUse = Nahkampfwaffe.Filter;
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
                    FilterToUse = Adeptenkraft.Filter;
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
                    FilterToUse = KomplexeForm.Filter;
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
            try
            {
                var dialog = new Auswahl(Model.MainObject.LinkList, ((Thing)((Button)sender).DataContext).LinkedThings, Filter: FilterToUse);
               await dialog.ShowAsync();
            }
            catch (Exception ex)
            {
            }
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
