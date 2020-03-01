using Kneat.Application.Contracts.External;
using Kneat.Application.Views;
using System.Threading.Tasks;

namespace Kneat.Application.Services.Interfaces
{
    public interface IKneatService
    {

        Task<KneatView> Process(GetStarShipRequest request);

    }
}
