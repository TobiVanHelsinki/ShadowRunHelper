namespace ShadowRunHelper
{
    public delegate void CustomTitleBarChangesEventHandler(double LeftSpace, double RigthSpace, double Heigth);
    public interface IUi
    {
        bool IsCustomTitleBarEnabled { get; set; }

        void DisplayCurrentCharName();

        event CustomTitleBarChangesEventHandler CustomTitleBarChanges;
        void TriggerCustomTitleBarChanges();
        void SetCustomTitleBar(object VisualElement);
    }
}
