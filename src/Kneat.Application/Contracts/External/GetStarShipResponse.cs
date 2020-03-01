using System.Collections.Generic;

namespace Kneat.Application.Contracts.External
{
    public class GetStarShipResponse : KneatBaseResponse
    {

        public ICollection<GetStarShipResponseItem> Results { get; set; }

        public int Count { get; set; }

        public string Next { get; set; }

        public string Previous { get; set; }

    }
}
