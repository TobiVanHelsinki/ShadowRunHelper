using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace ShadowRunHelperViewer.Platform
{
    public interface IFormsInteractions
    {
        object GetRenderer(VisualElement source);
    }
}
