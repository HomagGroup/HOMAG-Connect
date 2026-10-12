using HomagConnect.Base.Client;
using HomagConnect.Base.Contracts;
using HomagConnect.Base.Contracts.Enumerations;
using HomagConnect.MmrMobile.Contracts;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace HomagConnect.MmrMobile.Client
{
    public class MmrMobileClient : ClientBase, IMmrMobileClient
    {
        #region Constructors

        /// <inheritdoc />
        public MmrMobileClient(HttpClient client) : base(client) { }

        /// <inheritdoc />
        public MmrMobileClient(Guid subscriptionOrPartnerId, string authorizationKey) : base(subscriptionOrPartnerId, authorizationKey) { }

        /// <inheritdoc />
        public MmrMobileClient(Guid subscriptionOrPartnerId, string authorizationKey, Uri baseUri) : base(subscriptionOrPartnerId, authorizationKey, baseUri) { }

        #endregion

        #region machinedata

        /// <summary>
        /// Get all machines, the customer has access to.
        /// </summary>
        /// <returns></returns>
        public Task<IEnumerable<MmrMachine>> GetMachines(OutputFormat format = OutputFormat.Default, CultureInfo culture = null)
        {
            return GetMachinesCore(format, culture);
        }

        private async Task<IEnumerable<MmrMachine>> GetMachinesCore(OutputFormat format, CultureInfo culture)
        {
            const string url = "/api/machinedata/machines";

            var request = CreateGetRequest(url, culture);
            var response = await Client.SendAsync(request).ConfigureAwait(false);
            await response.EnsureSuccessStatusCodeWithDetailsAsync(request).ConfigureAwait(false);

            var result = await response.Content.ReadAsStringAsync();
            return DeserializeCollection<MmrMachine>(result, format, culture);
        }

        /// <summary>
        /// Retrieve all available nodes of a machine.
        /// </summary>
        /// <param name="machineNumber">The machine number.</param>
        /// <param name="format"></param>
        /// <param name="culture"></param>
        /// <returns>list of nodenames</returns>
        public Task<MmrNodeList> GetNodesOfMachine(string machineNumber, OutputFormat format = OutputFormat.Default, CultureInfo culture = null)
        {
            return GetNodesOfMachineCore(machineNumber, format, culture);
        }

        private async Task<MmrNodeList> GetNodesOfMachineCore(string machineNumber, OutputFormat format, CultureInfo culture)
        {
            var url = $"/api/machinedata/machines/{machineNumber}/nodes";

            var request = CreateGetRequest(url, culture);
            var response = await Client.SendAsync(request).ConfigureAwait(false);
            await response.EnsureSuccessStatusCodeWithDetailsAsync(request).ConfigureAwait(false);

            var result = await response.Content.ReadAsStringAsync();
            var data = DeserializeObject<MmrNodeList>(result, format, culture);

            return data ?? new MmrNodeList { MachineNumber = machineNumber };
        }

        /// <summary>
        /// Retrieve the current value of one machine node or node subtree.
        /// </summary>
        /// <param name="machineNumber">The machine number.</param>
        /// <param name="node">The node key or node subtree.</param>
        /// <param name="format"></param>
        /// <param name="culture"></param>
        /// <returns>list of values applying to the filtered nodenames of the machine</returns>
        public Task<MmrNodeData> GetCurrentValuesFromMachine(string machineNumber, string node, OutputFormat format = OutputFormat.Default, CultureInfo culture = null)
        {
            return GetCurrentValuesFromMachineCore(machineNumber, node, format, culture);
        }

        private async Task<MmrNodeData> GetCurrentValuesFromMachineCore(string machineNumber, string node, OutputFormat format, CultureInfo culture)
        {
            var url = $"/api/machinedata/machines/{machineNumber}/nodes/{node}";

            var request = CreateGetRequest(url, culture);
            var response = await Client.SendAsync(request).ConfigureAwait(false);
            await response.EnsureSuccessStatusCodeWithDetailsAsync(request).ConfigureAwait(false);

            var result = await response.Content.ReadAsStringAsync();
            var data = DeserializeObject<MmrNodeData>(result, format, culture);

            return data ?? new MmrNodeData { MachineNumber = machineNumber };
        }

        /// <summary>
        /// Retrieve the value of one machine node or node subtree at a specific point in time.
        /// </summary>
        /// <param name="machineNumber">The machine number.</param>
        /// <param name="node">The node key or node subtree.</param>
        /// <param name="timestamp">The point in time to query.</param>
        /// <param name="format"></param>
        /// <param name="culture"></param>
        /// <returns>list of values applying to the filtered nodenames of the machine</returns>
        public Task<MmrNodeData> GetPointInTimeValuesFromMachine(string machineNumber, string node, DateTime timestamp, OutputFormat format = OutputFormat.Default, CultureInfo culture = null)
        {
            return GetPointInTimeValuesFromMachineCore(machineNumber, node, timestamp, format, culture);
        }

        private async Task<MmrNodeData> GetPointInTimeValuesFromMachineCore(string machineNumber, string node, DateTime timestamp, OutputFormat format, CultureInfo culture)
        {
            var url = $"/api/machinedata/machines/{machineNumber}/nodes/{node}?timestamp={Uri.EscapeDataString(timestamp.ToString("o", CultureInfo.InvariantCulture))}";

            var request = CreateGetRequest(url, culture);
            var response = await Client.SendAsync(request).ConfigureAwait(false);
            await response.EnsureSuccessStatusCodeWithDetailsAsync(request).ConfigureAwait(false);

            var result = await response.Content.ReadAsStringAsync();
            var data = DeserializeObject<MmrNodeData>(result, format, culture);

            return data ?? new MmrNodeData { MachineNumber = machineNumber };
        }

        /// <summary>
        /// Retrieve the values of one machine node or node subtree for a time span.
        /// </summary>
        /// <param name="machineNumber">The machine number.</param>
        /// <param name="node">The node key or node subtree.</param>
        /// <param name="from">Inclusive start timestamp.</param>
        /// <param name="to">Inclusive end timestamp.</param>
        /// <param name="take">optional, how many rows to take</param>
        /// <param name="skip">optional, how many rows should be skipped before taking them into account</param>
        /// <param name="format"></param>
        /// <param name="culture"></param>
        /// <returns>list of values applying to the filtered nodenames of the machine</returns>
        public Task<MmrNodeData> GetTimeSeriesFromMachine(string machineNumber, string node, DateTime from, DateTime to, int take, int skip = 0, OutputFormat format = OutputFormat.Default, CultureInfo culture = null)
        {
            return GetTimeSeriesFromMachineCore(machineNumber, node, from, to, null, take, skip, format, culture);
        }

        /// <summary>
        /// Retrieve the values of one machine node or node subtree for a relative lookback window.
        /// </summary>
        /// <param name="machineNumber">The machine number.</param>
        /// <param name="node">The node key or node subtree.</param>
        /// <param name="daysBack">Optional relative lookback window in days.</param>
        /// <param name="take">optional, how many rows to take</param>
        /// <param name="skip">optional, how many rows should be skipped before taking them into account</param>
        /// <param name="format"></param>
        /// <param name="culture"></param>
        /// <returns>list of values applying to the filtered nodenames of the machine</returns>
        public Task<MmrNodeData> GetTimeSeriesFromMachine(string machineNumber, string node, int daysBack, int take, int skip = 0, OutputFormat format = OutputFormat.Default, CultureInfo culture = null)
        {
            return GetTimeSeriesFromMachineCore(machineNumber, node, null, null, daysBack, take, skip, format, culture);
        }

        private async Task<MmrNodeData> GetTimeSeriesFromMachineCore(string machineNumber, string node, DateTime? from, DateTime? to, int? daysBack, int take, int skip, OutputFormat format, CultureInfo culture)
        {
            var url = $"/api/machinedata/machines/{machineNumber}/nodes/{node}/history";
            var parameters = GetParameters(from, to, daysBack, null, null, null, null, null, null, null, take, skip, null);

            var request = CreateGetRequest(url + parameters, culture);
            var response = await Client.SendAsync(request).ConfigureAwait(false);
            await response.EnsureSuccessStatusCodeWithDetailsAsync(request).ConfigureAwait(false);

            var result = await response.Content.ReadAsStringAsync();
            var data = DeserializeObject<MmrNodeData>(result, format, culture);

            return data ?? new MmrNodeData { MachineNumber = machineNumber };
        }


        /// <inheritdoc />
        public async Task UploadProductionProtocol(Stream file, string machine)
        {
            if (file == null)
                throw new ArgumentNullException(nameof(file));
            if (machine == null)
                throw new ArgumentNullException(nameof(machine));
            var request = new HttpRequestMessage { Method = HttpMethod.Post };

            const string uri = "/api/machinedata/upload/productionProtocol/saw";
            request.RequestUri = new Uri(uri, UriKind.Relative);
            request.Headers.Add("MachineId",machine);

            request.Content = new StreamContent(file);

            var response = await Client.SendAsync(request);

            await response.EnsureSuccessStatusCodeWithDetailsAsync(request);

        }

        #endregion

        #region eventdatadata of a machine

        /// <inheritdoc />
        public Task<IEnumerable<AlertEvent>> GetAlertEventsFromMachine(string machineNumber, DateTime from, DateTime to, int take, int skip = 0, OutputFormat format = OutputFormat.Default, CultureInfo culture = null)
        {
            return GetAlertEventsFromMachineCore(machineNumber, from, to, null, take, skip, format, culture);
        }

        /// <inheritdoc />
        public Task<IEnumerable<AlertEvent>> GetAlertEventsFromMachine(string machineNumber, int daysBack, int take, int skip = 0, OutputFormat format = OutputFormat.Default, CultureInfo culture = null)
        {
            return GetAlertEventsFromMachineCore(machineNumber, null, null, daysBack, take, skip, format, culture);
        }

        private async Task<IEnumerable<AlertEvent>> GetAlertEventsFromMachineCore(string machineNumber, DateTime? from, DateTime? to, int? daysBack, int take, int skip, OutputFormat format, CultureInfo culture)
        {
            var url = $"/api/machinedata/machines/{machineNumber}/alerts/history";
            var parameters = GetParameters(from, to, daysBack, null, null, null, null, null, null, null, take, skip, null);

            var request = CreateGetRequest(url + parameters, culture);
            var response = await Client.SendAsync(request).ConfigureAwait(false);
            await response.EnsureSuccessStatusCodeWithDetailsAsync(request).ConfigureAwait(false);

            var result = await response.Content.ReadAsStringAsync();
            return DeserializeCollection<AlertEvent>(result, format, culture);
        }

        /// <inheritdoc />
        public Task<IEnumerable<AlertEvent>> GetRecentAlertEvents(string machineNumber, int daysBack, int take, int skip = 0, OutputFormat format = OutputFormat.Default, CultureInfo culture = null)
        {
            return GetRecentAlertEventsCore(machineNumber, daysBack, take, skip, format, culture);
        }

        /// <inheritdoc />
        public Task<IEnumerable<AlertEvent>> GetRecentAlertEvents(string machineNumber, DateTime from, DateTime to, int take, int skip = 0, OutputFormat format = OutputFormat.Default, CultureInfo culture = null)
        {
            return GetAlertEventsFromMachine(machineNumber, from, to, take, skip, format, culture);
        }

        private async Task<IEnumerable<AlertEvent>> GetRecentAlertEventsCore(string machineNumber, int daysBack, int take, int skip, OutputFormat format, CultureInfo culture)
        {
            var url = $"/api/machinedata/machines/{machineNumber}/alerts/history";
            var parameters = GetParameters(null, null, daysBack, null, null, null, null, null, null, null, take, skip, null);

            var request = CreateGetRequest(url + parameters, culture);
            var response = await Client.SendAsync(request).ConfigureAwait(false);
            await response.EnsureSuccessStatusCodeWithDetailsAsync(request).ConfigureAwait(false);

            var result = await response.Content.ReadAsStringAsync();
            return DeserializeCollection<AlertEvent>(result, format, culture);
        }
        #endregion

        #region mmr states and counters
        /// <summary>
        /// get all machines, the customer has access to
        /// </summary>
        /// <returns></returns>
        public Task<IEnumerable<MmrMachine>> GetMmrMachines()
        {
            return GetMmrMachinesCore(JsonFormat.Default, null);
        }

        private async Task<IEnumerable<MmrMachine>> GetMmrMachinesCore(JsonFormat format, CultureInfo culture)
        {
            const string url = "/api/mmr-mobile/machines";

            var request = CreateGetRequest(url, culture);
            var response = await Client.SendAsync(request).ConfigureAwait(false);
            await response.EnsureSuccessStatusCodeWithDetailsAsync(request).ConfigureAwait(false);

            var result = await response.Content.ReadAsStringAsync();
            return DeserializeCollection<MmrMachine>(result, format, culture);
        }

        /// <summary>
        /// get one machine information
        /// </summary>
        /// <param name="machineNumber"></param>
        /// <param name="format"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public Task<MmrMachine> GetMmrMachine(string machineNumber, JsonFormat format = JsonFormat.Default, CultureInfo culture = null)
        {
            return GetMmrMachineCore(machineNumber, format, culture);
        }

        private async Task<MmrMachine> GetMmrMachineCore(string machineNumber, JsonFormat format, CultureInfo culture)
        {
            var url = $"/api/mmr-mobile/machines/{machineNumber}";

            var request = CreateGetRequest(url, culture);
            var response = await Client.SendAsync(request).ConfigureAwait(false);
            await response.EnsureSuccessStatusCodeWithDetailsAsync(request).ConfigureAwait(false);

            var result = await response.Content.ReadAsStringAsync();
            var data = DeserializeObject<MmrMachine>(result, format, culture);

            return data;
        }



        /// <inheritdoc />
        public Task<IEnumerable<MachineState>> GetStateData(DateTime? from = null, DateTime? to = null,
            string machineNumber = null, string instanceId = null,
            string machineType = null, string stateId = null, string detailedStateId = null, Granularity? granularity = null,
            int? take = null, int skip = 0, OutputFormat format = OutputFormat.Default, CultureInfo culture = null)
        {
            return GetStateDataCore(from, to, machineNumber, instanceId, machineType, stateId, detailedStateId, granularity, take, skip, format, culture);
        }

        private async Task<IEnumerable<MachineState>> GetStateDataCore(DateTime? from, DateTime? to,
            string machineNumber, string instanceId, string machineType, string stateId, string detailedStateId, Granularity? granularity,
            int? take, int skip, OutputFormat format, CultureInfo culture)
        {
            const string url = "/api/mmr-mobile/states";
            string parameters = GetParameters(from, to, null, machineNumber, instanceId, machineType, stateId, detailedStateId, null, granularity, take, skip, null);
            var request = CreateGetRequest(url + parameters, culture);

            var response = await Client.SendAsync(request).ConfigureAwait(false);
            await response.EnsureSuccessStatusCodeWithDetailsAsync(request).ConfigureAwait(false);

            var result = await response.Content.ReadAsStringAsync();
            return DeserializeCollection<MachineState>(result, format, culture);
        }

        /// <inheritdoc />
        public Task<IEnumerable<MachineCounter>> GetCounterData(DateTime? from = null, DateTime? to = null,
            string machineNumber = null, string instanceId = null, string machineType = null, string counterId = null, Granularity? granularity = null,
            int? take = null, int skip = 0, OutputFormat format = OutputFormat.Default, CultureInfo culture = null)
        {
            return GetCounterDataCore(from, to, machineNumber, instanceId, machineType, counterId, granularity, take, skip, format, culture);
        }

        private async Task<IEnumerable<MachineCounter>> GetCounterDataCore(DateTime? from, DateTime? to,
            string machineNumber, string instanceId, string machineType, string counterId, Granularity? granularity,
            int? take, int skip, OutputFormat format, CultureInfo culture)
        {
            const string url = "/api/mmr-mobile/counters";
            var parameters = GetParameters(from, to, null, machineNumber, instanceId, machineType, null, null, counterId, granularity, take, skip, null);
            var request = CreateGetRequest(url + parameters, culture);

            var response = await Client.SendAsync(request).ConfigureAwait(false);
            await response.EnsureSuccessStatusCodeWithDetailsAsync(request).ConfigureAwait(false);

            var result = await response.Content.ReadAsStringAsync();
            return DeserializeCollection<MachineCounter>(result, format, culture);
        }

        private static string GetParameters(DateTime? from, DateTime? to, int? daysBack, string machineNumber, string instanceId, string machineType,
            string stateId, string detailedStateId, string counterId, Granularity? granularity, int? take, int skip, int? defaultTakeLimit)
        {
            string parameters = string.Empty;
            string separator = "?";

            if (from != null)
            {
                parameters += separator;
                separator = "&";
                parameters += $"from={Uri.EscapeDataString(from.Value.ToString("o", CultureInfo.InvariantCulture))}";
            }

            if (to != null)
            {
                parameters += separator;
                separator = "&";
                parameters += $"to={Uri.EscapeDataString(to.Value.ToString("o", CultureInfo.InvariantCulture))}";
            }

            if (daysBack != null)
            {
                parameters += separator;
                separator = "&";
                parameters += $"daysBack={daysBack.Value}";
            }

            if (take != null)
            {
                parameters += separator;
                separator = "&";
                parameters += $"take={take.Value}";
            }
            else if (defaultTakeLimit != null)
            {
                parameters += separator;
                separator = "&";
                parameters += $"take={defaultTakeLimit.Value}";
            }

            if (skip > 0)
            {
                parameters += separator;
                separator = "&";
                parameters += $"skip={skip}";
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
        private static HttpRequestMessage CreateGetRequest(string url, CultureInfo culture)
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(url, UriKind.Relative)
            };

            request.Headers.AcceptLanguage.Clear();
            request.Headers.AcceptLanguage.Add(new StringWithQualityHeaderValue((culture ?? CultureInfo.CurrentUICulture).Name));
            return request;
        }

        private static T DeserializeObject<T>(string result, JsonFormat format, CultureInfo culture)
        {
            if (format == JsonFormat.Localized && culture != null)
            {
                return JsonConvert.DeserializeObject<T>(result, SerializerSettings.Localized(culture));
            }

            return JsonConvert.DeserializeObject<T>(result, SerializerSettings.Default);
        }

        private static T DeserializeObject<T>(string result, OutputFormat format, CultureInfo culture)
        {
            if (format == OutputFormat.Localized && culture != null)
            {
                return JsonConvert.DeserializeObject<T>(result, SerializerSettings.Localized(culture));
            }

            return JsonConvert.DeserializeObject<T>(result, SerializerSettings.Default);
        }

        private static IEnumerable<T> DeserializeCollection<T>(string result, JsonFormat format, CultureInfo culture)
        {
            var data = DeserializeObject<IEnumerable<T>>(result, format, culture);
            return data ?? Array.Empty<T>();
        }

        private static IEnumerable<T> DeserializeCollection<T>(string result, OutputFormat format, CultureInfo culture)
        {
            var data = DeserializeObject<IEnumerable<T>>(result, format, culture);
            return data ?? Array.Empty<T>();
        }

        #endregion
    }
}