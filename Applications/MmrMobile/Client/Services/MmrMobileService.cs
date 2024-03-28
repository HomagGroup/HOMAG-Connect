using System;
using System.Collections.Generic;
using System.Net.Http;
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
            const string url = "/api/mmr/states";
            string parameters = GetParameters(from, to, machineNumber, instanceId, machineType, stateId, detailedStateId, null, granularity);
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(url + parameters, UriKind.Relative)
            };

            var response = await HttpClient.SendAsync(request).ConfigureAwait(false);
            await response.EnsureSuccessStatusCodeWithDetailsAsync(request);

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
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(url + parameters, UriKind.Relative)
            };

            var response = await HttpClient.SendAsync(request).ConfigureAwait(false);
            await response.EnsureSuccessStatusCodeWithDetailsAsync(request);

            var result = await response.Content.ReadAsStringAsync();
            var machineCounterResponse = JsonConvert.DeserializeObject<IEnumerable<MachineCounter>>(result, SerializerSettings.Default);
            return machineCounterResponse;
        }

        private static string GetParameters(DateTime? from, DateTime? to, string machineNumber, string instanceId, string machineType,
            string stateId, string detailedStateId, string counterId, Granularity? granularity)
        {
            string parameters = string.Empty;
            string separator = "?";

            if (from != null)
            {
                parameters += separator;
                separator = "&";
                parameters += $"from={from:s}";
            }

            if (to != null)
            {
                parameters += separator;
                separator = "&";
                parameters += $"to={to:s}";
            }

            if (!string.IsNullOrEmpty(machineNumber))
            {
                parameters += separator;
                separator = "&";
                parameters += $"machineNumber={Uri.EscapeDataString(machineNumber)}";
            }

            if (!string.IsNullOrEmpty(instanceId))
            {
                parameters += separator;
                separator = "&";
                parameters += $"instanceId={Uri.EscapeDataString(instanceId)}";
            }

            if (!string.IsNullOrEmpty(machineType))
            {
                parameters += separator;
                separator = "&";
                parameters += $"machineType={Uri.EscapeDataString(machineType)}";
            }

            if (!string.IsNullOrEmpty(stateId))
            {
                parameters += separator;
                separator = "&";
                parameters += $"stateId={Uri.EscapeDataString(stateId)}";
            }

            if (!string.IsNullOrEmpty(detailedStateId))
            {
                parameters += separator;
                separator = "&";
                parameters += $"detailedStateId={Uri.EscapeDataString(detailedStateId)}";
            }

            if (!string.IsNullOrEmpty(counterId))
            {
                parameters += separator;
                separator = "&";
                parameters += $"counterId={Uri.EscapeDataString(counterId)}";
            }

            if (granularity != null)
            {
                parameters += separator;
                parameters += $"granularity={Uri.EscapeDataString(granularity.Value.ToString())}";
            }

            return parameters;
        }
    }
}