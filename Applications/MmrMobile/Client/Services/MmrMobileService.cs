using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

using HomagConnect.Base;
using HomagConnect.Base.Services;
using HomagConnect.MmrMobile.Client.Interfaces;
using HomagConnect.MmrMobile.Client.Models;

using Newtonsoft.Json;

namespace HomagConnect.MmrMobile.Client.Services
{
    public class MmrMobileService : ServiceBase, IMmrMobileService
    {
        protected HttpClient HttpClient;

        public MmrMobileService(HttpClient httpClient) : base(httpClient)
        {
            HttpClient = httpClient;
        }

        /// <inheritdoc />
        public async Task<IEnumerable<MachineState>> GetStateData(DateTime? from = null, DateTime? to = null,
            string machineNumber = null, string instanceId = null,
            string machineType = null, string stateId = null, string detailedStateId = null, Granularity? granularity= null)
        {
            var url = $"/api/mmr/states";
            var parameters = GetParameters(from, to, machineNumber, instanceId, machineType, stateId, detailedStateId, null, granularity);
            var request = new HttpRequestMessage { Method = HttpMethod.Get };
            request.RequestUri = new Uri(url + parameters, UriKind.Relative);
            request.Headers.AcceptLanguage.Clear();
            request.Headers.AcceptLanguage.Add(new StringWithQualityHeaderValue(CultureInfo.CurrentUICulture.Name));

            var response = await HttpClient.SendAsync(request).ConfigureAwait(false);
            response.EnsureSuccessStatusCodeWithDetails(request);

            var result = await response.Content.ReadAsStringAsync();
            var machineStateResponse = JsonConvert.DeserializeObject<IEnumerable<MachineState>>(result, SerializerSettings.Default);
            return machineStateResponse;
        }

        /// <inheritdoc />
        public async Task<IEnumerable<MachineCounter>> GetCounterData(DateTime? from = null, DateTime? to = null,
            string machineNumber = null, string instanceId = null, string machineType = null, string counterId = null, Granularity? granularity = null)
        {
            var url = $"/api/mmr/counters";
            var parameters = GetParameters(from, to, machineNumber, instanceId, machineType, null, null, counterId, granularity);
            var request = new HttpRequestMessage { Method = HttpMethod.Get };
            request.RequestUri = new Uri(url + parameters, UriKind.Relative);
            request.Headers.AcceptLanguage.Clear();
            request.Headers.AcceptLanguage.Add(new StringWithQualityHeaderValue(CultureInfo.CurrentUICulture.Name));

            var response = await HttpClient.SendAsync(request).ConfigureAwait(false);
            response.EnsureSuccessStatusCodeWithDetails(request);

            var result = await response.Content.ReadAsStringAsync();
            var machineCounterResponse = JsonConvert.DeserializeObject<IEnumerable<MachineCounter>>(result, SerializerSettings.Default);
            return machineCounterResponse;
        }

        private string GetParameters(DateTime? from, DateTime? to, string machineNumber, string instanceId, string machineType,
            string stateId, string detailedStateId, string counterId, Granularity? granularity)
        {
            string parameters = string.Empty;

            if (from != null)
            {
                parameters += string.IsNullOrEmpty(parameters) ? "?" : "&";
                parameters += $"from={from:s}";
            }

            if (to != null)
            {
                parameters += string.IsNullOrEmpty(parameters) ? "?" : "&";
                parameters += $"to={to:s}";
            }

            if (!string.IsNullOrEmpty(machineNumber))
            {
                parameters += string.IsNullOrEmpty(parameters) ? "?" : "&";
                parameters += $"machineNumber={Uri.EscapeDataString(machineNumber)}";
            }

            if (!string.IsNullOrEmpty(instanceId))
            {
                parameters += string.IsNullOrEmpty(parameters) ? "?" : "&";
                parameters += $"instanceId={Uri.EscapeDataString(instanceId)}";
            }

            if (!string.IsNullOrEmpty(machineType))
            {
                parameters += string.IsNullOrEmpty(parameters) ? "?" : "&";
                parameters += $"machineType={Uri.EscapeDataString(machineType)}";
            }

            if (!string.IsNullOrEmpty(stateId))
            {
                parameters += string.IsNullOrEmpty(parameters) ? "?" : "&";
                parameters += $"stateId={Uri.EscapeDataString(stateId)}";
            }

            if (!string.IsNullOrEmpty(detailedStateId))
            {
                parameters += string.IsNullOrEmpty(parameters) ? "?" : "&";
                parameters += $"detailedStateId={Uri.EscapeDataString(detailedStateId)}";
            }

            if (!string.IsNullOrEmpty(counterId))
            {
                parameters += string.IsNullOrEmpty(parameters) ? "?" : "&";
                parameters += $"counterId={Uri.EscapeDataString(counterId)}";
            }

            if (granularity != null)
            {
                parameters += string.IsNullOrEmpty(parameters) ? "?" : "&";
                parameters += $"granularity={Uri.EscapeDataString(granularity.Value.ToString())}";
            }

            return parameters;
        }
    }
}