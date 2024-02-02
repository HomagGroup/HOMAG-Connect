using System.Globalization;
using System.Net.Http.Headers;

using HomagConnect.Base;
using HomagConnect.Base.Services;
using HomagConnect.IntelliDivide.Contracts.Request;

using Newtonsoft.Json;

namespace HomagConnect.IntelliDivide.Client
{
    public partial class IntelliDivideClient : ServiceBase
    {
        public async Task<OptimizationRequestResponse?> RequestOptimizationAsync(FileInfo projectFile)
        {
            var request = new HttpRequestMessage { Method = HttpMethod.Post };

            var fileName = projectFile.Name;
            await using var stream = projectFile.OpenRead();

            var uri = "api/intelliDivide/optimizations".ToLowerInvariant();

            request.RequestUri = new Uri(uri, UriKind.Relative);
            request.Headers.AcceptLanguage.Clear();
            request.Headers.AcceptLanguage.Add(new StringWithQualityHeaderValue(CultureInfo.CurrentUICulture.Name));

            using var httpContent = new MultipartFormDataContent();

            HttpContent streamContent = new StreamContent(stream);
            httpContent.Add(streamContent, fileName, fileName);

            request.Content = httpContent;

            var response = await Client.SendAsync(request).ConfigureAwait(false);

            response.EnsureSuccessStatusCodeWithDetails(request);

            var result = await response.Content.ReadAsStringAsync();

            var optimizationRequestResponse = JsonConvert.DeserializeObject<OptimizationRequestResponse>(result);

            return optimizationRequestResponse; 
        }
    }
}