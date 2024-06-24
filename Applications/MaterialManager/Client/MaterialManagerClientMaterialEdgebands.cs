using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using HomagConnect.Base.Services;
using HomagConnect.MaterialManager.Contracts.Material.Edgebands;
using HomagConnect.MaterialManager.Contracts.Material.Edgebands.Interfaces;
using HomagConnect.MaterialManager.Contracts.Request;
using HomagConnect.MaterialManager.Contracts.Statistics;

using Newtonsoft.Json;

namespace HomagConnect.MaterialManager.Client
{
    /// <summary>
    /// materialManager client for edgebands
    /// </summary>
    public class MaterialManagerClientMaterialEdgebands : ServiceBase, IMaterialManagerClientMaterialEdgebands
    {
        private const string _BaseRoute = "api/materialManager/materials/edgebands";
        private const string _BaseStatisticsRoute = "api/materialManager/statistics";
        private const string _EdgebandCode = "edgebandCode";
        private const string _IncludingDetails = "includingDetails";

        /// <inheritdoc />
        public MaterialManagerClientMaterialEdgebands(HttpClient client) : base(client) { }

        /// <inheritdoc />
        public async Task<IEnumerable<EdgebandType>> GetEdgebandTypes(int take, int skip = 0)
        {
            var url = $"{_BaseRoute}?take={take}&skip={skip}";

            return await RequestEnumerable<EdgebandType>(new Uri(url, UriKind.Relative));
        }

        /// <inheritdoc />
        public async Task<EdgebandType> GetEdgebandTypeByEdgebandCode(string edgebandCode)
        {
            var url = $"{_BaseRoute}?{_EdgebandCode}={Uri.EscapeDataString(edgebandCode)}";

            return await RequestObject<EdgebandType>(new Uri(url, UriKind.Relative));
        }

        /// <inheritdoc />
        public async Task<EdgebandTypeDetails> GetEdgebandTypeByEdgebandCodeIncludingDetails(string edgebandCode)
        {
            var url = $"{_BaseRoute}?{_EdgebandCode}={Uri.EscapeDataString(edgebandCode)}&{_IncludingDetails}=true";

            return await RequestObject<EdgebandTypeDetails>(new Uri(url, UriKind.Relative));
        }

        /// <inheritdoc />
        public async Task<IEnumerable<EdgebandType>> GetEdgebandTypesByEdgebandCodes(IEnumerable<string> edgebandCodes)
        {
            if (edgebandCodes == null)
            {
                throw new ArgumentNullException(nameof(edgebandCodes));
            }

            var codes = edgebandCodes
                .Where(m => !string.IsNullOrWhiteSpace(m))
                .Distinct()
                .OrderBy(m => m)
                .ToList();

            if (!codes.Any())
            {
                throw new ArgumentNullException(nameof(edgebandCodes), "At least one material code must be passed.");
            }

            var urls = CreateUrls(codes, _EdgebandCode);
            var boardTypes = new List<EdgebandType>();

            foreach (var url in urls)
            {
                boardTypes.AddRange(await RequestEnumerable<EdgebandType>(new Uri(url, UriKind.Relative)));
            }

            return boardTypes;
        }

        /// <inheritdoc />
        public async Task<IEnumerable<EdgebandTypeDetails>> GetEdgebandTypesByEdgebandCodesIncludingDetails(IEnumerable<string> edgebandCodes)
        {
            if (edgebandCodes == null)
            {
                throw new ArgumentNullException(nameof(edgebandCodes));
            }

            var codes = edgebandCodes
                .Where(m => !string.IsNullOrWhiteSpace(m))
                .Distinct()
                .OrderBy(m => m)
                .ToList();

            if (!codes.Any())
            {
                throw new ArgumentNullException(nameof(edgebandCodes), "At least one material code must be passed.");
            }

            var urls = CreateUrls(codes, _EdgebandCode, includingDetails: true);
            var edgebandTypeDetails = new List<EdgebandTypeDetails>();

            foreach (var url in urls)
            {
                edgebandTypeDetails.AddRange(await RequestEnumerable<EdgebandTypeDetails>(new Uri(url, UriKind.Relative)));
            }

            return edgebandTypeDetails;
        }

        /// <inheritdoc />
        public async Task<EdgebandType> CreateEdgebandType(MaterialManagerRequestEdgeBandType edgebandTypeRequest)
        {
            if (edgebandTypeRequest == null)
            {
                throw new ArgumentNullException(nameof(edgebandTypeRequest));
            }

            ValidateRequiredProperties(edgebandTypeRequest);

            var payload = JsonConvert.SerializeObject(edgebandTypeRequest);
            var content = new StringContent(payload, Encoding.UTF8, "application/json");
            var response = await PostObject(new Uri(_BaseRoute, UriKind.Relative), content);

            var responseContent = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<EdgebandType>(responseContent);

            if (result != null)
            {
                return result;
            }

            throw new Exception($"The returned object is not of type {nameof(EdgebandType)}");
        }

        /// <inheritdoc />
        public async Task<IEnumerable<EdgeInventoryHistory>> GetEdgebandTypeInventoryHistoryAsync(DateTime from, DateTime to)
        {
            List<Uri> requestUri =
            [
                new Uri(
                    $"/{_BaseStatisticsRoute}/inventory/edgebands?from={Uri.EscapeDataString(from.ToString("o", CultureInfo.InvariantCulture))}&to={Uri.EscapeDataString(to.ToString("o", CultureInfo.InvariantCulture))}",
                    UriKind.Relative)
            ];
            var ret = await RequestEnumerableAsync<EdgeInventoryHistory>(requestUri);
            return ret;
        }

        public Task<IEnumerable<EdgeInventoryHistory>> GetEdgebandTypeInventoryHistoryAsync(int daysBack)
        {
            return GetEdgebandTypeInventoryHistoryAsync(DateTime.Now.AddDays(-daysBack), DateTime.Now);
        }

        private static List<string> CreateUrls(IEnumerable<string> codes, string searchCode, string route = "",
            bool includingDetails = false)
        {
            var queryParameters = new StringBuilder("?");
            var i = 1;
            var urls = new List<string>();
            var codeList = codes.ToList();
            while (i <= codeList.Count)
            {
                queryParameters.Append($"{searchCode}={Uri.EscapeDataString(codeList[i - 1])}");
                // To reduce the size of the URL, we are going to split the request into multiple requests. Max URL length is 2048, that´s why we are using 1900 as the limit with a little bit of added buffer.
                if (queryParameters.Length + _BaseRoute.Length > QueryParametersMaxLength)
                {
                    urls.Add(includingDetails
                        ? $"{_BaseRoute}{route}{queryParameters}&{_IncludingDetails}=true"
                        : $"{_BaseRoute}{route}{queryParameters}");

                    queryParameters = new StringBuilder("?");
                }

                i++;
                if (i <= codeList.Count)
                {
                    queryParameters.Append('&');
                }
            }

            urls.Add(includingDetails
                ? $"{_BaseRoute}{route}{queryParameters}&{_IncludingDetails}=true"
                : $"{_BaseRoute}{route}{queryParameters}");

            return urls;
        }
    }
}