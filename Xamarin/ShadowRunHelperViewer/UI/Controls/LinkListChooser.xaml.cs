using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using ShadowRunHelper.Model;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ShadowRunHelperViewer.UI.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LinkListChooser : PopupPage
    {
        public CharHolder MyChar { get; }

        /// <summary>
        /// Was the Choice successfull
        /// </summary>
        public bool Result { get; set; }
        public LinkListChooser(CharHolder myChar, IEnumerable<AllListEntry> preSelection)
        {
            MyChar = myChar;
            InitializeComponent();
            BindingContext = this;
            Select(preSelection);
        }

        private void Select(IEnumerable<AllListEntry> preSelection)
        {
        }

        public List<AllListEntry> Selected = new List<AllListEntry>();
        private void Cancel_Clicked(object sender, System.EventArgs e)
        {
            PopupNavigation.Instance.PopAsync(true);
        }

        private void Finish_Clicked(object sender, System.EventArgs e)
        {
            Result = true;
            Cancel_Clicked(sender, e);
        }

        bool TappingInProgess;
        private void ViewCell_Tapped(object sender, System.EventArgs e)
        {
            if (sender is ViewCell cell && cell.BindingContext is AllListEntry entry && cell.View is Layout l)
            {
                var box = l.FindByName("Selected") as CheckBox;
                //box.IsChecked = !box.IsChecked;
                //box.Focus();
                //box.RelScaleTo(1);
                box.SetValue(CheckBox.IsCheckedProperty, !box.IsChecked);
            }
        }
        private void Selected_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if (sender is CheckBox box && box.BindingContext is AllListEntry entry && box.Parent is Layout l)
            {
                //ChangeSelection(entry, l);
                (l.Parent as ViewCell).ForceUpdateSize();
            }
        }

        void ChangeSelection(AllListEntry entry, Layout panel)
        {
            if (TappingInProgess)
            {
                return;
            }
            TappingInProgess = true;
            if (Selected.Contains(entry))
            {
                panel.BackgroundColor = Color.Default;
                Selected.Remove(entry);
            }
            else
            {
                panel.BackgroundColor = Color.Accent;
                Selected.Add(entry);
            }
            panel.ForceLayout();
            panel.Focus();
            TappingInProgess = false;
        }

    }
}