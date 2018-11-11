using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShadowRunHelper
{
    public interface IAppDataPorter
    {
        Task<(List<(string, object)> set, List<(string, string)> files)> Loading { get; set; }
        Task<(List<(string, object)> set, List<(string, string)> files)> LoadAppPacket(object FileHandle);
        List<string> Forbidden { get; }

        bool InProgress { get; set; }

        Task ImportAppPacket();
    }
}