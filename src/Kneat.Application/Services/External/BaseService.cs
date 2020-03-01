using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Kneat.Application.Services.External
{
    public abstract class BaseService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger _logger;

        protected BaseService(
           HttpClient httpClient,
           ILogger logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        /// <summary>
        /// generic method to get async
        /// </summary>
        /// <typeparam name="T">concrete class</typeparam>
        /// <param name="resource">uri resource from request</param>
        /// <param name="timeout">informed timeout optional in milliseconds</param>
        /// <returns>concrete class</returns>
        public async Task<T> GetAsync<T>(string resource, int timeout = int.MaxValue)
        {
            //create an id to fetch further informations into log file if throw error
            var id = Guid.NewGuid().ToString().Substring(0, 8);
            var cts = new CancellationTokenSource(timeout);
            HttpResponseMessage httpResult = null;

            try
            {
                httpResult = await _httpClient.GetAsync(resource, cts.Token);
            }
            catch (OperationCanceledException ex)
            {
                var err = $"timeout! {timeout} milliseconds - baseAddress: {_httpClient.BaseAddress} - resource: {resource} - id: {id}";
                _logger.LogError(ex, err);
                throw new Exception(err);
            }
            catch (Exception ex)
            {
                var err = $"unexpected error - baseAddress: {_httpClient.BaseAddress} - resource: {resource} - id: {id}";
                _logger.LogError(ex, err);
                throw new Exception(err);
            }
            finally
            {
                cts = null;
            }

            if (httpResult == null)
            {
                var err = $"no data - baseAddress: {_httpClient.BaseAddress} - resource: {resource}";
                _logger.LogError(err);
                throw new Exception(err);
            }

            var statusCodeResult = (int)httpResult.StatusCode;

            if (!httpResult.IsSuccessStatusCode)
            {
                var err = $"unsuccessful status code - baseAddress: {_httpClient.BaseAddress} - resource: {resource} - statuscode: {statusCodeResult}";
                _logger.LogError(err);
                throw new Exception(err);
            }

            return JsonConvert.DeserializeObject<T>(await httpResult.Content.ReadAsStringAsync());
        }

    }
}
