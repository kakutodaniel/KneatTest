using Kneat.Application.Contracts.External;
using System.Threading.Tasks;

namespace Kneat.Application.Services.External.Interfaces
{
    public interface ISwapiService
    {

        Task<GetStarShipResponse> GetStarShipAsync(string url = null);

    }
}
