using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

using HomagConnect.Base;
using HomagConnect.Base.Services;
using HomagConnect.MaterialManager.Contracts.Processing.Optimization;

using Newtonsoft.Json;

namespace HomagConnect.MaterialManager.Client;

public class MaterialManagerClientProcessingOptimization : ServiceBase
{
    public MaterialManagerClientProcessingOptimization(HttpClient client) : base(client) { }

    public async Task<IDictionary<string, MaximumBookHeight>?> GetMaximumBookHeights(params string[] materialCodes)
    {
        const string url = $"/api//materialManager/processing/optimization/bookheight";

        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri(url, UriKind.Relative)
        };

        request.Headers.AcceptLanguage.Clear();
        request.Headers.AcceptLanguage.Add(new StringWithQualityHeaderValue(CultureInfo.CurrentUICulture.Name));

        var json = JsonConvert.SerializeObject(materialCodes);
        request.Content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await Client.SendAsync(request).ConfigureAwait(false);
        await response.EnsureSuccessStatusCodeWithDetailsAsync(request);

        var result = await response.Content.ReadAsStringAsync();
        var data = JsonConvert.DeserializeObject<IDictionary<string, MaximumBookHeight>>(result, SerializerSettings.Default);

        return data;
    }
}