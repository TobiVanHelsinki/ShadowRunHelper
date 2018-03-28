using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// Die Elementvorlage "Benutzersteuerelement" wird unter https://go.microsoft.com/fwlink/?LinkId=234236 dokumentiert.

namespace ShadowRunHelper.Win.UI
{
    public sealed partial class CategoryEntry : UserControl
    {
        public CategoryEntry()
        {
            this.InitializeComponent();
            EntryItem.ContentTemplate = ItemItem;
        }

        public void Expand()
        {
            EntryItem.ContentTemplate = ItemItemX;

        }

        internal void Shrink()
        {
            EntryItem.ContentTemplate = ItemItem;

        }
        public void ChangeTemplate()
        {
            switch ((int)ThingDefs.Handlung)
            {
                case (int)ThingDefs.Handlung:
                //                    NewTemplate = HandlungItem;
                //                    NewTemplateX = HandlungItemX;
                //                    if (!SettingsModel.I.TutorialHandlungShown)
                //                    {
                //#pragma warning disable CS4014
                //                        new Tutorial(30, 31).ShowAsync();
                //#pragma warning restore CS4014
                //                        SettingsModel.I.TutorialHandlungShown = true;
                //                    }
                //                    break;
                //                case (int)ThingDefs.Fertigkeit:
                //                    NewTemplate = FertigkeitItem;
                //                    NewTemplateX = FertigkeitItemX;
                //                    break;
                //                case (int)ThingDefs.Item:
                //                    NewTemplate = ItemItem;
                //                    NewTemplateX = ItemItemX;
                //                    break;
                //                case (int)ThingDefs.Programm:
                //                    NewTemplate = ProgrammItem;
                //                    NewTemplateX = ProgrammItemX;
                //                    break;
                //                case (int)ThingDefs.Munition:
                //                    NewTemplate = MunitionItem;
                //                    NewTemplateX = MunitionItemX;
                //                    break;
                //                case (int)ThingDefs.Implantat:
                //                    NewTemplate = ImplantatItem;
                //                    NewTemplateX = ImplantatItemX;
                //                    break;
                //                case (int)ThingDefs.Vorteil:
                //                    NewTemplate = EigenschaftItem;
                //                    NewTemplateX = EigenschaftItemX;
                //                    break;
                //                case (int)ThingDefs.Nachteil:
                //                    NewTemplate = EigenschaftItem;
                //                    NewTemplateX = EigenschaftItemX;
                //                    break;
                //                case (int)ThingDefs.Connection:
                //                    NewTemplate = ConnectionItem;
                //                    NewTemplateX = ConnectionItemX;
                //                    break;
                //                case (int)ThingDefs.Sin:
                //                    NewTemplate = SinItem;
                //                    NewTemplateX = SinItemX;
                //                    break;
                //                case (int)ThingDefs.Nahkampfwaffe:
                //                    NewTemplate = NahkampfwaffeItem;
                //                    NewTemplateX = NahkampfwaffeItemX;
                //                    break;
                //                case (int)ThingDefs.Fernkampfwaffe:
                //                    NewTemplate = FernkampfwaffeItem;
                //                    NewTemplateX = FernkampfwaffeItemX;
                //                    break;
                //                case (int)ThingDefs.Kommlink:
                //                    NewTemplate = KommlinkItem;
                //                    NewTemplateX = KommlinkItemX;
                //                    break;
                //                case (int)ThingDefs.CyberDeck:
                //                    NewTemplate = CyberDeckItem;
                //                    NewTemplateX = CyberDeckItemX;
                //                    break;
                //                case (int)ThingDefs.Vehikel:
                //                    NewTemplate = VehikelItem;
                //                    NewTemplateX = VehikelItemX;
                //                    break;
                //                case (int)ThingDefs.Panzerung:
                //                    NewTemplate = PanzerungItem;
                //                    NewTemplateX = PanzerungItemX;
                //                    break;
                //                case (int)ThingDefs.Adeptenkraft_KomplexeForm:
                //                    NewTemplate = Adeptenkraft_KomplexeFormItem;
                //                    NewTemplateX = Adeptenkraft_KomplexeFormItemX;
                //                    break;
                //                case (int)ThingDefs.Geist_Sprite:
                //                    NewTemplate = Geist_SpriteItem;
                //                    NewTemplateX = Geist_SpriteItemX;
                //                    break;
                //                case (int)ThingDefs.Foki_Widgets:
                //                    NewTemplate = Foki_WidgetsItem;
                //                    NewTemplateX = Foki_WidgetsItemX;
                //                    break;
                //                case (int)ThingDefs.Stroemung_Wandlung:
                //                    NewTemplate = Stroemung_WandlungItem;
                //                    NewTemplateX = Stroemung_WandlungItemX;
                //                    break;
                //                case (int)ThingDefs.Tradition_Initiation:
                //                    NewTemplate = Tradition_InitiationItem;
                //                    NewTemplateX = Tradition_InitiationItemX;
                //                    break;
                //                case (int)ThingDefs.Zaubersprueche:
                //                    NewTemplate = ZauberspruecheItem;
                //                    NewTemplateX = ZauberspruecheItemX;
                //                    break;
                //                case (int)ThingDefs.KomplexeForm:
                //                    NewTemplate = KomplexeFormItem;
                //                    NewTemplateX = KomplexeFormItemX;
                //                    break;
                //                case (int)ThingDefs.Sprite:
                //                    NewTemplate = SpriteItem;
                //                    NewTemplateX = SpriteItemX;
                //                    break;
                //                case (int)ThingDefs.Widgets:
                //                    NewTemplate = WidgetsItem;
                //                    NewTemplateX = WidgetsItemX;
                //                    break;
                //                case (int)ThingDefs.Wandlung:
                //                    NewTemplate = WandlungItem;
                //                    NewTemplateX = WandlungItemX;
                //                    break;
                //                case (int)ThingDefs.Initiation:
                //                    NewTemplate = InitiationItem;
                //                    NewTemplateX = InitiationItemX;
                //                    break;

                default:
                    return;
            }

        }
        private void Grid_Entry_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void Del_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {

        }

    }
}
