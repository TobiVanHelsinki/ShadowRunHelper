
namespace TLIB.Settings
{
    public interface IPlatformSettings
    {
        object GetRoaming(string place);
        object GetLocal(string place);
        void SetRoaming(string place, object value);
        void SetLocal(string place, object value);
        void PrepareSettingsSavePlace();
        void RemoveAllSettings();
    }
}
