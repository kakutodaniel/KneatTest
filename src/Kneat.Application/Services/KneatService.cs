using Kneat.Application.Services.External;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Kneat.Application.Contracts.External;
using Kneat.Application.Views;
using Kneat.Application.Services.Interfaces;
using Kneat.Application.Services.External.Interfaces;

namespace Kneat.Application.Services
{
    public class KneatService : IKneatService
    {

        private readonly List<KeyValuePair<string, int>> conversionTable =
            new List<KeyValuePair<string, int>>() {
                new KeyValuePair<string, int>("hour", 1),
                new KeyValuePair<string, int>("day", 24),
                new KeyValuePair<string, int>("week", 168),
                new KeyValuePair<string, int>("month", 720),
                new KeyValuePair<string, int>("year", 8760),
            };


        private readonly ISwapiService _swapiService;
        private readonly ILogger<KneatService> _logger;
        private KneatView dataResult;


        public KneatService(
            ISwapiService swapiService,
            ILogger<KneatService> logger)
        {
            _swapiService = swapiService;
            _logger = logger;
        }

        /// <summary>
        /// Main method that processes the request
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<KneatView> Process(GetStarShipRequest request)
        {
            dataResult = new KneatView();

            var response = await _swapiService.GetStarShipAsync();

            Calculate(response, request.Distance);

            while (!string.IsNullOrEmpty(response.Next))
            {
                response = await _swapiService.GetStarShipAsync(response.Next);

                Calculate(response, request.Distance);
            }

            return dataResult;
        }

        /// <summary>
        /// Calculate number of stops based on inputed distance
        /// </summary>
        /// <param name="response"></param>
        /// <param name="distance"></param>
        private void Calculate(GetStarShipResponse response, long distance)
        {
            if (!response.Success)
            {
                dataResult.SetError($"error to get data - {response.ErrorMessage}");
                return;
            }

            foreach (var item in response.Results)
            {
                long.TryParse(item.MGLT, out var mglt);

                var calc = mglt * ConvertToHours(item.Consumables);

                dataResult.Items.Add(new KneatViewItem { Name = item.Name, NumberStops = calc == 0 ? "unknown" : (distance / calc).ToString() });
            }
        }

        /// <summary>
        /// extract numbers from text
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        private long ExtractNumber(string text)
        {
            string b = string.Empty;
            long val = 0;

            for (int i = 0; i < text.Length; i++)
            {
                if (char.IsDigit(text[i]))
                    b += text[i];
            }

            if (b.Length > 0)
                val = long.Parse(b);

            return val;
        }

        /// <summary>
        /// convert to hours according to period
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        private long ConvertToHours(string text)
        {
            var number = ExtractNumber(text);
            var hours = conversionTable.FirstOrDefault(x => text.ToLower().Contains(x.Key)).Value;
            return number * hours;
        }

    }
}
