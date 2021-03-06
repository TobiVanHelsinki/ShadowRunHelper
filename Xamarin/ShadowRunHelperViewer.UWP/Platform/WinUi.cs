﻿//Author: Tobi van Helsinki

using ShadowRunHelper;
using ShadowRunHelper.Model;
using TLIB;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;

namespace ShadowRunHelperViewer.Platform.UWP
{
    internal class WinUi : IUi
    {
        bool _IsTopUiSizeEnabled;
        public bool IsCustomTitleBarEnabled
        {
            get { return _IsTopUiSizeEnabled; }
            set { if (_IsTopUiSizeEnabled != value) { _IsTopUiSizeEnabled = value; TitleBar_LayoutMetricsChanged(null, null); } }
        }

        public event CustomTitleBarChangesEventHandler CustomTitleBarChanges;

        public void DisplayCurrentCharName()
        {
            var appView = ApplicationView.GetForCurrentView();
            appView.Title = AppModel.Instance?.MainObject?.Person?.Alias ?? Package.Current.DisplayName;
        }

        public void TriggerCustomTitleBarChanges()
        {
            TitleBar_LayoutMetricsChanged(null, null);
        }

        bool Registered;

        private void RegisterVisualChangedEventHandlersIfNeccesary()
        {
            if (Registered)
            {
                return;
            }
            CoreApplication.GetCurrentView().TitleBar.LayoutMetricsChanged += TitleBar_LayoutMetricsChanged;
            CoreApplication.GetCurrentView().TitleBar.IsVisibleChanged += TitleBar_LayoutMetricsChanged;
            Registered = true;
        }

        public void SetCustomTitleBar(object VisualElement)
        {
            RegisterVisualChangedEventHandlersIfNeccesary();
            Log.Write("Change SetTitleBar to: " + VisualElement?.ToString() + " " + (VisualElement as FrameworkElement)?.Name);
            Window.Current.SetTitleBar(VisualElement as UIElement);
        }

        private void TitleBar_LayoutMetricsChanged(CoreApplicationViewTitleBar sender, object args)
        {
            try
            {
                var AppTitlebar = ApplicationView.GetForCurrentView().TitleBar;
                AppTitlebar.ButtonBackgroundColor = Windows.UI.Colors.Transparent;
                AppTitlebar.ButtonInactiveBackgroundColor = Windows.UI.Colors.Transparent;

                var currentTitlebar = sender ?? CoreApplication.GetCurrentView().TitleBar;
                //Log.Write($"IsTopUiSizeEnabled={IsCustomTitleBarEnabled}({currentTitlebar.SystemOverlayLeftInset},{currentTitlebar.SystemOverlayRightInset})");
                currentTitlebar.ExtendViewIntoTitleBar = IsCustomTitleBarEnabled;
                if (!IsCustomTitleBarEnabled)
                {
                    CustomTitleBarChanges?.Invoke(0, 0, currentTitlebar.Height);
                }
                else
                {
                    CustomTitleBarChanges?.Invoke(currentTitlebar.SystemOverlayLeftInset, currentTitlebar.SystemOverlayRightInset, currentTitlebar.Height);
                }
            }
            catch (System.Exception)
            {
            }
        }
    }
}