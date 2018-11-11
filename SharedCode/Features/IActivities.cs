using ShadowRunHelper.Model;
using System.Threading.Tasks;

namespace ShadowRunHelper
{
    public interface IActivities
    {
        Task GenerateCharActivityAsync(CharHolder Char);
        void StopCurrentCharActivity();
    }

}
