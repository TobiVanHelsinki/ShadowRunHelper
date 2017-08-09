﻿using ShadowRunHelper.CharModel;
using ShadowRunHelper.Model;
using ShadowRunHelper.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using TLIB.Model;
using Windows.ApplicationModel.Core;
using Windows.Foundation.Metadata;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace ShadowRunHelper
{
    public sealed partial class MainPage : Page
    {
        readonly AppModel Model = AppModel.Instance;

        public MainPage()
        {
            InitializeComponent();
            Model.lstNotifications.CollectionChanged += (x, y) => ShowError();
            Model.TutorialStateChanged += TutorialStateChanged;
        }

        private void TutorialStateChanged(int StateNumber, bool Highlight)
        {
            Style StyleToBeApplied = Highlight ? Tutorial.HighlightBorderStyle_XAML : Tutorial.UnhighlightBorderStyle_XAML;
            switch (StateNumber)
            {
                case 1:
                    MainBarBorder.Style = StyleToBeApplied;
                    break;
                case 21:
                    StatusControlBorder.Style = StyleToBeApplied;
                    break;
                default:
                    //MainBarBorder.Style = Tutorial.UnhighlightBorderStyle_XAML;
                    //StatusControlBorder.Style = Tutorial.UnhighlightBorderStyle_XAML;
                    break;
            }
        }

        CyberDeck CurrentDeck;
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            Model.PropertyChanged += (x, y) => ChangeUI();
            Model.PropertyChanged += (x, y) => RefreshStatus(y);
            Model.PropertyChanged += (x, y) => GetCurrentDeck();
            GetCurrentDeck();
            ChangeUI();
            ShowError();
            Model.SetDependencies(Dispatcher);
            NavigationRequested(ProjectPages.Char);
        }

        void GetCurrentDeck()
        {
            CurrentDeck = (CyberDeck)Model.MainObject?.LinkList.Find(x => x.Object.ThingType == ThingDefs.CyberDeck).Object;

            if (CurrentDeck != null)
            {
                CurrentDeck.PropertyChanged += (x, y) => CurrentDeck_PropertyChanged(y);
                CurrentDeck_PropertyChanged();
            }
        }

        void CurrentDeck_PropertyChanged(PropertyChangedEventArgs e = null)
        {
            if (e == null
                || e.PropertyName == (((Expression<Func<double>>)(() => new CyberDeck().dSchaden)).Body as MemberExpression).Member.Name
                || e.PropertyName == (((Expression<Func<double>>)(() => new CyberDeck().dSchadenMax)).Body as MemberExpression).Member.Name)
            {
                try
                {
                    XAML_Header_Schaden_M_Text.Text = CurrentDeck.dSchaden.ToString();
                    XAML_Header_Schaden_M_Slider.Maximum = CurrentDeck.dSchadenMax;
                    XAML_Header_Schaden_M_Slider.Value = CurrentDeck.dSchaden;
                }
                catch (Exception)
                {
                }
            }
        }

        Action<ProjectPages> NavigationMethod;
        void NavigationRequested(ProjectPages e)
        {
            NavigationMethod = NavigationRequested;
            switch (e)
            {
                case ProjectPages.Char:
                    if (Model.MainObject != null)
                    {
                        MyFrame.Navigate(typeof(CharPage), NavigationMethod);
                    }
                    else
                    {
                        MyFrame.Navigate(typeof(AdministrationPage), NavigationMethod);
                    }
                    break;
                case ProjectPages.Administration:
                    MyFrame.Navigate(typeof(AdministrationPage), NavigationMethod);
                    break;
                case ProjectPages.Settings:
                    MyFrame.Navigate(typeof(SettingsPage), NavigationMethod);
                    break;
                default:
                    break;
            }
        }

        async void ShowError()
        {
            foreach (Notification item in Model.lstNotifications.Where((x) => x.bIsRead == false).OrderBy((x) => x.DateTime))
            {
                try
                {
                    var messageDialog = new MessageDialog(item.strMessage + " \n \n" + item.ThrownException.Message);
                    messageDialog.Commands.Add(new UICommand(
                        "OK"));
                    messageDialog.DefaultCommandIndex = 0;
                    await messageDialog.ShowAsync();
                }
                catch (Exception)
                {
                    continue;
                }
                item.bIsRead = true;
            }
        }

        void RefreshStatus(PropertyChangedEventArgs e = null)
        {
            Expression<Func<CharHolder>> expression = () => SharedAppModel<CharHolder>.Instance.MainObject;
            if (e == null || e.PropertyName == (expression.Body as MemberExpression).Member.Name)
            {
                StatusControl.DataContext = Model.CurrentChar;
            }
        }

        void ChangeUI(bool? bState = null)
        {
            RefreshStatus();
            if (bState == null)
            {
                bState = ((Model == null || Model.MainObject == null) ? false : true);
            }
            foreach (var item in BTNLST)
            {
                item.IsEnabled = (bool)bState;
            }
        }

        void Plus_Click(object sender, RoutedEventArgs e)
        {
            if (Model.MainObject != null)
            {
                string Controller_Name = ((string)((Button)sender).Name);

                if (Controller_Name.Contains("Karma_Gesamt"))
                {
                    Model.MainObject.Person.Karma_Gesamt++;
                }
                if (Controller_Name.Contains("Karma_Aktuell"))
                {
                    Model.MainObject.Person.Karma_Aktuell++;
                }
                if (Controller_Name.Contains("Edge_Gesamt"))
                {
                    Model.MainObject.Person.Edge_Gesamt++;
                }
                if (Controller_Name.Contains("Edge_Aktuell"))
                {
                    Model.MainObject.Person.Edge_Aktuell++;
                }
                if (Controller_Name.Contains("Initiative"))
                {
                    Model.MainObject.Person.Initiative++;
                }
                if (Controller_Name.Contains("Runs"))
                {
                    Model.MainObject.Person.Runs++;
                }
            }
        }

        void Minus_Click(object sender, RoutedEventArgs e)
        {
            if (Model.MainObject != null)
            {
                string Controller_Name = ((string)((Button)sender).Name);

                if (Controller_Name.Contains("Karma_Gesamt"))
                {
                    Model.MainObject.Person.Karma_Gesamt--;
                }
                if (Controller_Name.Contains("Karma_Aktuell"))
                {
                    Model.MainObject.Person.Karma_Aktuell--;
                }
                if (Controller_Name.Contains("Edge_Gesamt"))
                {
                    Model.MainObject.Person.Edge_Gesamt--;
                }
                if (Controller_Name.Contains("Edge_Aktuell"))
                {
                    Model.MainObject.Person.Edge_Aktuell--;
                }
                if (Controller_Name.Contains("Initiative"))
                {
                    Model.MainObject.Person.Initiative--;
                }
                if (Controller_Name.Contains("Runs"))
                {
                    Model.MainObject.Person.Runs--;
                }
            }
        }

        async void Edit_Click(object sender, RoutedEventArgs e)
        {
            String Controller_Name = ((String)((Button)sender).Name);

            if (Controller_Name.Contains("Person2"))
            {
                UI.Edit.Edit_Person2 dialog = new UI.Edit.Edit_Person2(Model.MainObject.Person);
                await dialog.ShowAsync();
            }
        }

        void XAML_Header_Schaden_M_Slider_ValueChanged(object sender, Windows.UI.Xaml.Controls.Primitives.RangeBaseValueChangedEventArgs e)
        {
            Slider XAML_Header_Schaden_M_Slider = sender as Slider;
            if (CurrentDeck == null)
            {
                return;
            }
            CurrentDeck.dSchaden = XAML_Header_Schaden_M_Slider.Value;
            //if (CurrentDeck.dSchaden <= 3)
            //{
            //    XAML_Header_Schaden_M_Slider.Foreground = new SolidColorBrush(Colors.Green);
            //}
            //else if (CurrentDeck.dSchaden <= 7)
            //{
            //    XAML_Header_Schaden_M_Slider.Foreground = new SolidColorBrush(Colors.Yellow);
            //}
            //else if (CurrentDeck.dSchaden <= 10)
            //{
            //    XAML_Header_Schaden_M_Slider.Foreground = new SolidColorBrush(Colors.Red);
            //}
            //else
            //{
            //    XAML_Header_Schaden_M_Slider.Foreground = new SolidColorBrush(Colors.Black);
            //}
        }

        void CommandBar_Loaded(object sender, RoutedEventArgs e)
        {
            if (ApiInformation.IsPropertyPresent("Windows.UI.Xaml.Controls.CommandBar", "DefaultLabelPosition"))
            {
                (sender as CommandBar).DefaultLabelPosition = CommandBarDefaultLabelPosition.Right;
            }
        }

        void Ui_Nav_Char(object sender, RoutedEventArgs e)
        {
            NavigationRequested(ProjectPages.Char);
        }

        void Ui_Nav_Admin(object sender, RoutedEventArgs e)
        {
            NavigationRequested(ProjectPages.Administration);
        }

        void Ui_Nav_Settings(object sender, RoutedEventArgs e)
        {
            NavigationRequested(ProjectPages.Settings);
        }

        List<Button> BTNLST = new List<Button>();
        void Main_Btns_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                BTNLST.Add(sender as Button);
            }
            catch (Exception)
            {
            }
            finally
            {
                ChangeUI();
            }
        }
        Slider XAML_Header_Schaden_M_Slider;
        TextBlock XAML_Header_Schaden_M_Text;
        void XAML_Header_Schaden_M_Slider_Loaded(object sender, RoutedEventArgs e)
        {
            XAML_Header_Schaden_M_Slider = sender as Slider;
            CurrentDeck_PropertyChanged();
        }

        void XAML_Header_Schaden_M_Text_Loaded(object sender, RoutedEventArgs e)
        {
            XAML_Header_Schaden_M_Text = sender as TextBlock;
            CurrentDeck_PropertyChanged();
        }

     
    }
}