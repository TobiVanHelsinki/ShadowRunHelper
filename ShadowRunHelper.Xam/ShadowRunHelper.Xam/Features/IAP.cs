using System.Threading.Tasks;

namespace ShadowRunHelper
{
    public interface IIAP
    {
        Task CheckLicence(bool force = false);

        Task Buy(string FEATUREID);
    }
}
