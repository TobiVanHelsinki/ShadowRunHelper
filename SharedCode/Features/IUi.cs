using System;
using System.Collections.Generic;
using System.Text;

namespace ShadowRunHelper
{
    public delegate void TopUiSizeChangedEventHandler(double LeftSpace, double RigthSpace);
   public interface IUi
    {
        bool IsTopUiSizeEnabled { get; set; }

        void DisplayCurrentCharName();

        event TopUiSizeChangedEventHandler TopUiSizeChanged;
        void GetTopUiSizeChanged();
        void RegisterTopUiSizeChanged(object VisualElement);
    }
}
