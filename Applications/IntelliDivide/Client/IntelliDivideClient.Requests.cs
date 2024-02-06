using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Net.Http.Headers;

using HomagConnect.Base;
using HomagConnect.Base.Services;
using HomagConnect.IntelliDivide.Contracts.Common;
using HomagConnect.IntelliDivide.Contracts.Request;

using Newtonsoft.Json;

namespace HomagConnect.IntelliDivide.Client
{
    /// <summary />
    public partial class IntelliDivideClient : ServiceBase
    {
        /// <summary>
        /// Request an optimization using a structured zip file.
        /// </summary>
        /// <param name="projectFile">Structured zip file, whose format corresponds to the ImportSpecification (<seealso href="https://dev.azure.com/homag-group/FOSSProjects/_git/homag-api-gateway-client?path=/Documentation/ImportSpecification.md&_a=preview" /> format.</param>
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

        public async Task<OptimizationRequestResponse> RequestOptimizationAsync(OptimizationRequest optimizationRequest, params ImportFile[] files)
        {
            Validator.ValidateObject(optimizationRequest, new ValidationContext(optimizationRequest, null, null));

            foreach (var optimizationRequestPart in optimizationRequest.Parts.Where(p => !string.IsNullOrWhiteSpace(p.MprFileName)))
            {
                if (files.All(f => f.Name != optimizationRequestPart.MprFileName))
                {
                    throw new FileNotFoundException(optimizationRequestPart.MprFileName + " is missing!");
                }
            }

            var request = new HttpRequestMessage { Method = HttpMethod.Post };

            var uri = "api/intelliDivide/optimizations".ToLowerInvariant();
            request.RequestUri = new Uri(uri, UriKind.Relative);
            request.Headers.AcceptLanguage.Clear();
            request.Headers.AcceptLanguage.Add(new StringWithQualityHeaderValue(CultureInfo.CurrentUICulture.Name));

            using var httpContent = new MultipartFormDataContent();

            var json = JsonConvert.SerializeObject(optimizationRequest);

            httpContent.Add(new StringContent(json));

            foreach (var file in files)
            {
                HttpContent streamContent = new StreamContent(file.Stream);
                httpContent.Add(streamContent, file.Name, file.Name);
            }

            request.Content = httpContent;

            var response = await Client.SendAsync(request).ConfigureAwait(false);

            response.EnsureSuccessStatusCodeWithDetails(request);

            var result = await response.Content.ReadAsStringAsync();
            var responseObject = JsonConvert.DeserializeObject<OptimizationRequestResponse>(result);

            return responseObject ?? new OptimizationRequestResponse();
        }
    }
}