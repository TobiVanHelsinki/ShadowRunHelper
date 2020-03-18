//Author: Tobi van Helsinki

using System;
using ShadowRunHelperViewer.UI.Resources;
using TLIB;
using Xamarin.Forms;

namespace ShadowRunHelperViewer.UI.CharTemplates
{
    public partial class SharedCharTemplateResources
    {
        public SharedCharTemplateResources()
        {
            InitializeComponent();

            foreach (var (name, type, basedon, setter) in new[] {
                ("TemplateStack", typeof(StackLayout),"", new (BindableProperty, object)[] {
                    (StackLayout.SpacingProperty, 0),
                    (StackLayout.OrientationProperty, StackOrientation.Horizontal),
                    //(Layout.PaddingProperty, new Thickness(SettingsModel.I.CurrentSpacingStrategy)),
                    (Layout.PaddingProperty, new Thickness(0)),
                    (View.MarginProperty, new Thickness(0)),
                    (View.VerticalOptionsProperty, LayoutOptions.Center ),
                }),
                ("TemplateGrid", typeof(Grid),"", new (BindableProperty, object)[] {
                    (Grid.ColumnSpacingProperty, 0),
                    (Grid.RowSpacingProperty, 0),
                    //(Layout.PaddingProperty, new Thickness(SettingsModel.I.CurrentSpacingStrategy)),
                    (Layout.PaddingProperty, new Thickness(0)),
                    (View.MarginProperty, new Thickness(0)),
                    (VisualElement.BackgroundColorProperty, Color.Beige),
                    (View.VerticalOptionsProperty, LayoutOptions.Center ),
                }),
                ("SeparatorLine", typeof(BoxView),"", new (BindableProperty, object)[] {
                    (VisualElement.HeightRequestProperty, 0),
                    //(View.MarginProperty, new Thickness(-5,5,-5,0)),
                    (View.MarginProperty, new Thickness(0)),
                    (VisualElement.BackgroundColorProperty, StyleManager.ForegroundColor),
                }),
                })
            {
                try
                {
                    var style = new Style(type);
                    if (!string.IsNullOrEmpty(basedon))
                    {
                        style.BaseResourceKey = basedon;
                    }
                    foreach (var (prop, value) in setter)
                    {
                        style.Setters.Add(new Setter() { Property = prop, Value = value });
                    }
                    Add(name, style);
                }
                catch (Exception ex)
                {
                    Log.Write("Could not create resource", ex, logType: LogType.Error);
                }
            }
        }
    }
}