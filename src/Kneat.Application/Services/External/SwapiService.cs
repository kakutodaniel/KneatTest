using Kneat.Application.Contracts.External;
using Kneat.Application.Services.External.Interfaces;
using Kneat.Application.Settings.External;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Kneat.Application.Services.External
{
    public class SwapiService : BaseService, ISwapiService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<SwapiService> _logger;
        private readonly SwapiSettings _swapiSettings;


        public SwapiService(
            HttpClient httpClient,
            ILogger<SwapiService> logger,
            SwapiSettings swapiSettings) : base(httpClient, logger)
        {
            _httpClient = httpClient;
            _logger = logger;
            _swapiSettings = swapiSettings;
        }

        /// <summary>
        /// Get starships from API
        /// </summary>
        /// <param name="url">direct url to next pages</param>
        /// <returns>Starships found</returns>
        public async Task<GetStarShipResponse> GetStarShipAsync(string url = null)
        {
            //get resource name from settings file
            var resource = _swapiSettings.StarShipsResource;

            if (!string.IsNullOrEmpty(url))
            {
                var uri = new Uri(url);
                resource += $"/{uri.Query}";
            }

            //log into file
            _logger.LogInformation($"fetching starships ...[resource: /{resource}]");

            var response = new GetStarShipResponse();

            try
            {
                response = await GetAsync<GetStarShipResponse>(resource, _swapiSettings.Timeout);
                _logger.LogInformation($"{response.Count} starships found");

            }
            catch (Exception ex)
            {
                response.SetError($"{ex.Message}");
            }

            return response;

        }
        
    }
}
