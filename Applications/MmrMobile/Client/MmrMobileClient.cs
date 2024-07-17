using HomagConnect.Base;
using HomagConnect.Base.Services;
using HomagConnect.MmrMobile.Contracts;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace HomagConnect.MmrMobile.Client
{
    public class MmrMobileClient : ServiceBase, IMmrMobileClient
    {
        #region machinedata
        /// <summary>
        /// Constructor. Provide subscription and HttpClient
        /// </summary>
        /// <param name="client"></param>
        /// <param name="subscriptionId"></param>
        public MmrMobileClient(HttpClient client) : base(client) { }

        /// <summary>
        /// get all machines, the customer has access to
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<MmrMachine>> GetMachines()
        {
            const string url = "/api/mmr-mobile/machinedata/machines";

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(url, UriKind.Relative)
            };
            request.Headers.AcceptLanguage.Clear();
            request.Headers.AcceptLanguage.Add(new StringWithQualityHeaderValue(CultureInfo.CurrentUICulture.Name));

            HttpResponseMessage response = await Client.SendAsync(request).ConfigureAwait(false);
            await response.EnsureSuccessStatusCodeWithDetailsAsync(request).ConfigureAwait(false);

            var result = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<IEnumerable<MmrMachine>>(result, SerializerSettings.Default);

            return data ?? Array.Empty<MmrMachine>();
        }

        /// <summary>
        /// retrieve for a given machinenumber a list of valid nodes with data available
        /// </summary>
        /// <param name="machineNumber"></param>
        /// <returns>list of nodenames</returns>
        public async Task<MmrNodeList> GetNodesOfMachine(string machineNumber)
        {
            var url = $"/api/mmr-mobile/machinedata/machines/{machineNumber}/nodes";

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(url, UriKind.Relative)
            };
            request.Headers.AcceptLanguage.Clear();
            request.Headers.AcceptLanguage.Add(new StringWithQualityHeaderValue(CultureInfo.CurrentUICulture.Name));

            var response = await Client.SendAsync(request).ConfigureAwait(false);
            await response.EnsureSuccessStatusCodeWithDetailsAsync(request).ConfigureAwait(false);

            var result = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<MmrNodeList>(result, SerializerSettings.Default);

            return data ?? new MmrNodeList { MachineNumber = machineNumber };
        }

        /// <summary>
        /// retrieve for a given machinenumber one specific value (current value)
        /// </summary>
        /// <param name="machineNumber"></param>
        /// <param name="node">Can ba a comma separated list of values. Each value is taken as a prefix, 
        /// so all nodes beginning with the entry are selected"</param>
        /// <returns>list of values applying to the filtered nodenames of the machine</returns>
        public async Task<MmrNodeData> GetCurrentValuesFromMachine(string machineNumber, string node)
        {
            var url = $"/api/mmr-mobile/machinedata/machines/{machineNumber}/nodes/{node}";

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(url, UriKind.Relative)
            };
            request.Headers.AcceptLanguage.Clear();
            request.Headers.AcceptLanguage.Add(new StringWithQualityHeaderValue(CultureInfo.CurrentUICulture.Name));

            var response = await Client.SendAsync(request).ConfigureAwait(false);
            await response.EnsureSuccessStatusCodeWithDetailsAsync(request).ConfigureAwait(false);

            var result = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<MmrNodeData>(result, SerializerSettings.Default);

            return data ?? new MmrNodeData { MachineNumber = machineNumber };
        }

        /// <summary>
        /// retrieve for a given machinenumber one specific value (current value)
        /// </summary>
        /// <param name="machineNumber"></param>
        /// <param name="node">Can ba a comma separated list of values. Each value is taken as a prefix, 
        /// so all nodes beginning with the entry are selected"</param>
        /// <param name="timestamp" ></param>
        /// <returns>list of values applying to the filtered nodenames of the machine</returns>
        public async Task<MmrNodeData> GetPointInTimeValuesFromMachine(string machineNumber, string node, DateTime timestamp)
        {
            string url = $"/api/mmr-mobile/machinedata/machines/{machineNumber}/nodes/{node}?timestamp={Uri.EscapeDataString(timestamp.ToString("o", CultureInfo.InvariantCulture))}";

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(url, UriKind.Relative)
            };
            request.Headers.AcceptLanguage.Clear();
            request.Headers.AcceptLanguage.Add(new StringWithQualityHeaderValue(CultureInfo.CurrentUICulture.Name));

            var response = await Client.SendAsync(request).ConfigureAwait(false);
            await response.EnsureSuccessStatusCodeWithDetailsAsync(request).ConfigureAwait(false);

            var result = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<MmrNodeData>(result, SerializerSettings.Default);

            return data ?? new MmrNodeData { MachineNumber = machineNumber };
        }

        /// <summary>
        /// retrieve for a given machinenumber one specific value (current value)
        /// </summary>
        /// <param name="machineNumber"></param>
        /// <param name="node">Can ba a comma separated list of values. Each value is taken as a prefix, 
        /// so all nodes beginning with the entry are selected"</param>
        /// <param name="from">mandatory : timestamp, from when the data are retrieved (included)</param>
        /// <param name="to">mandatory : timestamp, up to when the data are retrieved </param>
        /// <param name="take">optional, how many rows to take</param>
        /// <param name="skip">optional, how many rows should be skipped before taking them into account</param>
        /// <returns>list of values applying to the filtered nodenames of the machine</returns>
        public async Task<MmrNodeData> GetTimeSeriesFromMachine(string machineNumber, string node, DateTime from, DateTime to, int take, int skip = 0)
        {
            string url = $"/api/mmr-mobile/machinedata/machines/{machineNumber}/nodes/{node}/history?from={Uri.EscapeDataString(from.ToString("o", CultureInfo.InvariantCulture))}&to={Uri.EscapeDataString(to.ToString("o", CultureInfo.InvariantCulture))}&take={take}&skip={skip}";

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(url, UriKind.Relative)
            };
            request.Headers.AcceptLanguage.Clear();
            request.Headers.AcceptLanguage.Add(new StringWithQualityHeaderValue(CultureInfo.CurrentUICulture.Name));

            var response = await Client.SendAsync(request).ConfigureAwait(false);
            await response.EnsureSuccessStatusCodeWithDetailsAsync(request).ConfigureAwait(false);

            var result = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<MmrNodeData>(result, SerializerSettings.Default);

            return data ?? new MmrNodeData { MachineNumber = machineNumber };
        }
        #endregion

        #region eventdatadata of a machine

        /// <inheritdoc />
        public async Task<IEnumerable<AlertEvent>> GetAlertEventsFromMachine(string machineNumber, DateTime from, DateTime to, int take, int skip = 0)
        {
            var url = $"/api/mmr-mobile/machinedata/machines/{machineNumber}/alerts/history" +
                      $"?from={Uri.EscapeDataString(from.ToString("o", CultureInfo.InvariantCulture))}" +
                      $"&to={Uri.EscapeDataString(to.ToString("o", CultureInfo.InvariantCulture))}" +
                      $"&take={take}&skip={skip}";

            return await RequestEnumerable<AlertEvent>(new Uri(url, UriKind.Relative));
        }

        /// <inheritdoc />
        public async Task<IEnumerable<AlertEvent>> GetRecentAlertEvents(string machineNumber, int daysBack, int take, int skip = 0)
        {
            var url = $"/api/mmr-mobile/machinedata/machines/{machineNumber}/alerts/history" +
                      $"?daysBack={daysBack}" +
                      $"&take={take}&skip={skip}";

            return await RequestEnumerable<AlertEvent>(new Uri(url, UriKind.Relative));
        }
        #endregion

        #region mmr states and counters
        /// <inheritdoc />
        public async Task<IEnumerable<MachineState>> GetStateData(DateTime? from = null, DateTime? to = null,
            string machineNumber = null, string instanceId = null,
            string machineType = null, string stateId = null, string detailedStateId = null, Granularity? granularity = null)
        {
            const string url = "/api/mmr/states";
            string parameters = GetParameters(from, to, machineNumber, instanceId, machineType, stateId, detailedStateId, null, granularity);
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(url + parameters, UriKind.Relative)
            };

            var response = await Client.SendAsync(request).ConfigureAwait(false);
            await response.EnsureSuccessStatusCodeWithDetailsAsync(request).ConfigureAwait(false);

            var result = await response.Content.ReadAsStringAsync();
            var machineStateResponse = JsonConvert.DeserializeObject<IEnumerable<MachineState>>(result, SerializerSettings.Default);
            return machineStateResponse;
        }

        /// <inheritdoc />
        public async Task<IEnumerable<MachineCounter>> GetCounterData(DateTime? from = null, DateTime? to = null,
            string machineNumber = null, string instanceId = null, string machineType = null, string counterId = null, Granularity? granularity = null)
        {
            const string url = "/api/mmr/counters";
            var parameters = GetParameters(from, to, machineNumber, instanceId, machineType, null, null, counterId, granularity);
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(url + parameters, UriKind.Relative)
            };

            var response = await Client.SendAsync(request).ConfigureAwait(false);
            await response.EnsureSuccessStatusCodeWithDetailsAsync(request).ConfigureAwait(false);

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
        #endregion
    }
}