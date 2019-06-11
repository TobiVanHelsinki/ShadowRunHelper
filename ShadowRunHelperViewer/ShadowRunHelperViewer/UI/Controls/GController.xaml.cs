﻿using Rg.Plugins.Popup.Services;
using ShadowRunHelper;
using ShadowRunHelper.CharController;
using ShadowRunHelper.CharModel;
using ShadowRunHelper.Model;
using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Xam.Plugin;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ShadowRunHelperViewer
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class GController : ContentView, INotifyPropertyChanged
	{
        #region NotifyPropertyChanged
		public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
        public CharHolder MyChar { get; set; }
        #region Hold IController
        IController _Controller;
        public IController Controller
        {
            get { return _Controller; }
            set { if (_Controller != value) { _Controller = value; NotifyPropertyChanged(); OnControllerChanged(); } }
        }
        private void GController_BindingContextChanged(object sender, EventArgs e)
        {
            Controller = BindingContext as IController;
        }

        private void OnControllerChanged()
        {
            if (Controller != null)
            {
                string key = TypeHelper.ThingDefToString(Controller.eDataTyp, false);
                Resources.TryGetValue(key, out object CustomTemplate);
                if (CustomTemplate is DataTemplate DT)
                {
                    Items.ItemTemplate = DT;
                }

                Resources.TryGetValue(key+"_H", out CustomTemplate);
                if (CustomTemplate is ControlTemplate HL)
                {
                    Items_H.ControlTemplate = HL;
                }

                Setting = MyChar.Settings.CategoryOptions.FirstOrDefault(x => x.ThingType == Controller.eDataTyp);
                IsVisible = Setting.Visibility;
                Headline.Text = TypeHelper.ThingDefToString(Controller.eDataTyp, true);
            }
            else
            {
                IsVisible = false;
            }
        }
        #endregion
      
        public CategoryOption Setting { get; private set; }

        public GController(CharHolder myChar)
        {
            MyChar = myChar;
            InitializeComponent();
            OnControllerChanged();
            BindingContextChanged += GController_BindingContextChanged;
        }

        async void Items_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem is Thing t)
            {
                await PopupNavigation.Instance.PushAsync(new DetailsPage(t, false));
                Items.SelectedItem = null;
            }
        }

        private void Add(object sender, EventArgs e)
        {
            MyChar.Add(Controller.eDataTyp);
        }

        (string, Action)[] MenuItems = new (string, Action)[] {
                        (CustomManager.GetString("UI_TxT_Cat_AddSep/Text"),null),
                        (CustomManager.GetString("UI_TxT_Cat_UncheckAll/Text"),null),
                        (CustomManager.GetString("UI_TxT_Cat_Order_ABC/Text"),null),
                        (CustomManager.GetString("UI_TxT_Cat_Order_Type/Text"),null),
                        (CustomManager.GetString("UI_TxT_Cat_Order_Save/Text"),null),
                        (CustomManager.GetString("UI_TxT_Cat_Order_Orig/Text"),null),
                        (CustomManager.GetString("UI_TxT_CSV_Cat_Export/Text"),null),
                        (CustomManager.GetString("UI_TxT_CSV_Cat_Export_Selected/Text"),null),
                        (CustomManager.GetString("UI_TxT_CSV_Cat_Import/Text"),null),
                        (CustomManager.GetString("UI_TxT_Cat_Truncate/Text"),null),
                    };

        private void Options(object sender, EventArgs e)
        {
            try
            {
                PopupMenu Popup = new PopupMenu
                {
                    ItemsSource = MenuItems.Select(x => x.Item1).ToArray()
                };
                Popup.OnItemSelected += Popup_OnItemSelected;
                Popup?.ShowPopup(sender as Button);
            }
            catch (Exception)
            {
            }
        }

        private void Popup_OnItemSelected(string item)
        {
            MenuItems.FirstOrDefault(x => x.Item1 == item).Item2?.Invoke();
        }


        private void Add_Clicked(object sender, EventArgs e)
        {

        }

        private void Delete_Clicked(object sender, EventArgs e)
        {

        }

        async void Edit_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (sender is BindableObject v && v.BindingContext is Thing t)
                {
                    await PopupNavigation.Instance.PushAsync(new DetailsPage(t, true));
                }
            }
            catch (Exception)
            {
            }
        }
    }

}