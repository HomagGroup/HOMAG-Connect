using HomagConnect.Base;
using HomagConnect.Base.Extensions;
using HomagConnect.Base.Services;
using HomagConnect.IntelliDivide.Contracts;
using HomagConnect.IntelliDivide.Contracts.Common;
using HomagConnect.IntelliDivide.Contracts.Request;
using HomagConnect.IntelliDivide.Contracts.Result;
using HomagConnect.IntelliDivide.Contracts.Statistics;

using Newtonsoft.Json;

namespace HomagConnect.IntelliDivide.Client
{
    /// <inheritdoc cref="IIntelliDivideClient" />
    public class IntelliDivideClient : ServiceBase, IIntelliDivideClient
    {
        #region Statistics

        /// <summary>
        /// Gets the statistics for the material efficiency.
        /// </summary>
        public async Task<IEnumerable<MaterialEfficiency>> GetMaterialStatisticsAsync(DateTime from, DateTime to, int take, int skip = 0)
        {
            var url = $"/api/intelliDivide/statistics/material?from={from:s}&to={to:s}&take={take}&skip={skip}";

            return await RequestEnumerable<MaterialEfficiency>(new Uri(url, UriKind.Relative));
        }

        #endregion

        #region Constructors

        /// <inheritdoc />
        public IntelliDivideClient(HttpClient client) : base(client) { }

        /// <inheritdoc />
        public IntelliDivideClient(Guid subscriptionId, string authorizationKey) : base(subscriptionId, authorizationKey) { }

        /// <inheritdoc />
        public IntelliDivideClient(Guid subscriptionId, string authorizationKey, Uri? baseUri) : base(subscriptionId, authorizationKey, baseUri) { }

        #endregion

        #region Settings (Machines, Parameters, Import templates)

       

        /// <inheritdoc />
        public async Task<IEnumerable<OptimizationImportTemplate>> GetImportTemplatesAsync(OptimizationType optimizationType, string fileExtension = "", string name = "")
        {
            var url = $"/api/intelliDivide/{optimizationType}/templates";
            var templates = await RequestEnumerable<OptimizationImportTemplate>(new Uri(url, UriKind.Relative));

            if (!string.IsNullOrEmpty(fileExtension))
            {
                templates = templates.Where(t => string.Equals(t.FileExtension.Trim(' ', '.'), fileExtension.Trim(' ', '.'), StringComparison.InvariantCultureIgnoreCase));
            }

            if (!string.IsNullOrEmpty(name))
            {
                templates = templates.Where(t => string.Equals(t.Name, name, StringComparison.InvariantCultureIgnoreCase));
            }

            return templates;
        }

        /// <inheritdoc />
        public async Task<OptimizationMachine?> GetMachineAsync(string machineName)
        {
            var machines = await GetMachinesAsync().ToListAsync();

            if (machines != null && machines.Any())
            {
                return machines.FirstOrDefault(m => string.Equals(m.Name, machineName, StringComparison.InvariantCultureIgnoreCase));
            }

            return null;
        }

        /// <inheritdoc />
        public async Task<IEnumerable<OptimizationMachine>> GetMachinesAsync()
        {
            var cuttingMachines = await GetMachinesAsync(OptimizationType.Cutting);
            var nestingMachines = await GetMachinesAsync(OptimizationType.Nesting);

            return cuttingMachines.Union(nestingMachines).OrderBy(m => m.Name);
        }

        /// <inheritdoc />
        public async Task<IEnumerable<OptimizationMachine>> GetMachinesAsync(OptimizationType optimizationType)
        {
            var url = $"/api/intelliDivide/{optimizationType}/machines";

            return await RequestEnumerable<OptimizationMachine>(new Uri(url, UriKind.Relative));
        }

        /// <inheritdoc />
        public async Task<IEnumerable<OptimizationParameter>> GetParametersAsync(OptimizationType optimizationType)
        {
            var url = $"/api/intelliDivide/{optimizationType}/parameters";
            return await RequestEnumerable<OptimizationParameter>(new Uri(url, UriKind.Relative));
        }

        #endregion

        #region Optimization Request

        /// <inheritdoc />
        public async Task<OptimizationRequestResponse> RequestOptimizationAsync(OptimizationRequest optimizationRequest, params ImportFile[] files)
        {
            var partWithMissingMprFile = optimizationRequest.Parts
                .Where(p => !string.IsNullOrWhiteSpace(p.MprFileName))
                .FirstOrDefault(p => Array.TrueForAll(files, f => f.Name != p.MprFileName));

            if (partWithMissingMprFile != null)
            {
                throw new FileNotFoundException($"The mpr file '{partWithMissingMprFile.MprFileName}' referenced by part '{partWithMissingMprFile.Description}' is missing!");
            }

            var request = new HttpRequestMessage { Method = HttpMethod.Post };

            const string uri = "api/intelliDivide/optimizations";
            request.RequestUri = new Uri(uri, UriKind.Relative);

            using var httpContent = new MultipartFormDataContent();

            var json = JsonConvert.SerializeObject(optimizationRequest, SerializerSettings.Default);

            httpContent.Add(new StringContent(json), nameof(optimizationRequest));

            foreach (var file in files)
            {
                HttpContent streamContent = new StreamContent(file.Stream);
                httpContent.Add(streamContent, file.Name, file.Name);
            }

            request.Content = httpContent;

            var response = await Client.SendAsync(request);

            await response.EnsureSuccessStatusCodeWithDetailsAsync(request);

            var result = await response.Content.ReadAsStringAsync();
            var responseObject = JsonConvert.DeserializeObject<OptimizationRequestResponse>(result);

            return responseObject ?? new OptimizationRequestResponse();
        }

        /// <inheritdoc />
        public async Task<OptimizationRequestResponse> RequestOptimizationAsync(OptimizationRequestUsingTemplate optimizationRequest, params ImportFile[] files)
        {
            var request = new HttpRequestMessage { Method = HttpMethod.Post };

            const string uri = "api/intelliDivide/optimizations/RequestUsingTemplate";
            request.RequestUri = new Uri(uri, UriKind.Relative);

            using var httpContent = new MultipartFormDataContent();

            var json = JsonConvert.SerializeObject(optimizationRequest, SerializerSettings.Default);

            httpContent.Add(new StringContent(json), nameof(optimizationRequest));

            foreach (var file in files)
            {
                HttpContent streamContent = new StreamContent(file.Stream);
                httpContent.Add(streamContent, file.Name, file.Name);
            }

            request.Content = httpContent;

            var response = await Client.SendAsync(request);

            await response.EnsureSuccessStatusCodeWithDetailsAsync(request);

            var result = await response.Content.ReadAsStringAsync();
            var responseObject = JsonConvert.DeserializeObject<OptimizationRequestResponse>(result);

            return responseObject ?? new OptimizationRequestResponse();
        }

        /// <inheritdoc />
        public async Task<OptimizationRequestResponse> RequestOptimizationAsync(OptimizationRequestUsingProject optimizationRequest, FileInfo projectFile)
        {
            var request = new HttpRequestMessage { Method = HttpMethod.Post };

            var fileName = projectFile.Name;

            using var stream = projectFile.OpenRead();
            const string uri = "api/intelliDivide/optimizations/RequestUsingProject";
            request.RequestUri = new Uri(uri, UriKind.Relative);

            using var httpContent = new MultipartFormDataContent();

            var json = JsonConvert.SerializeObject(optimizationRequest, SerializerSettings.Default);

            httpContent.Add(new StringContent(json), nameof(optimizationRequest));

            HttpContent streamContent = new StreamContent(stream);
            httpContent.Add(streamContent, fileName, fileName);

            request.Content = httpContent;

            var response = await Client.SendAsync(request);

           await response.EnsureSuccessStatusCodeWithDetailsAsync(request);

            var result = await response.Content.ReadAsStringAsync();

            var responseObject = JsonConvert.DeserializeObject<OptimizationRequestResponse>(result);

            return responseObject ?? new OptimizationRequestResponse();
        }

        /// <inheritdoc />
        public async Task<Optimization> WaitForCompletionAsync(Guid optimizationId, TimeSpan maxDuration)
        {
            return await WaitForOptimizationStatusAsync(optimizationId, OptimizationStatus.Optimized, maxDuration);
        }

        /// <inheritdoc />
        public async Task<Optimization> WaitForOptimizationStatusAsync(Guid optimizationId, OptimizationStatus optimizationStatus, TimeSpan maxDuration)
        {
            var timeout = DateTime.Now + maxDuration;

            while (DateTime.Now < timeout)
            {
                var currentStatus = await GetOptimizationStatusAsync(optimizationId);

                if (currentStatus == optimizationStatus)
                {
                    return await GetOptimizationAsync(optimizationId);
                }

                if (optimizationStatus == OptimizationStatus.Started
                    && currentStatus 
                        is OptimizationStatus.Started 
                        or OptimizationStatus.Optimized 
                        or OptimizationStatus.Transferred)
                {
                    // When waiting for status Started the optimization might be already optimized or transferred.

                    return await GetOptimizationAsync(optimizationId);
                }

                if (optimizationStatus == OptimizationStatus.Optimized
                    && currentStatus 
                        is OptimizationStatus.Optimized 
                        or OptimizationStatus.Transferred)
                {
                    // When waiting for status Optimized the optimization might be already transferred.

                    return await GetOptimizationAsync(optimizationId);
                }

                if (currentStatus 
                    is OptimizationStatus.Faulted 
                    or OptimizationStatus.Canceled 
                    or OptimizationStatus.Archived)
                {
                    // It is not possible to reach another state.

                    return await GetOptimizationAsync(optimizationId);
                }

                await Task.Delay(TimeSpan.FromSeconds(1));
            }

            throw new TimeoutException();
        }

        #endregion

        #region Optimzation List, Archive, Delete, Start

        /// <inheritdoc />
        public async Task<Optimization> GetOptimizationAsync(Guid optimizationId)
        {
            var url = $"api/intelliDivide/optimizations/{optimizationId}";

            return await RequestObject<Optimization>(new Uri(url, UriKind.Relative));
        }

        /// <inheritdoc />
        public async Task<IEnumerable<Optimization>> GetOptimizationsAsync(OptimizationType optimizationType, int take, int skip = 0)
        {
            var url = $"api/intelliDivide/optimizations?optimizationType={optimizationType}&take={take}&skip={skip}";

            return await RequestEnumerable<Optimization>(new Uri(url, UriKind.Relative));
        }

        /// <inheritdoc />
        public async Task<IEnumerable<Optimization>> GetOptimizationsAsync(OptimizationType optimizationType, string orderBy, int take, int skip = 0)
        {
            var url = $"api/intelliDivide/optimizations?optimizationType={optimizationType}&take={take}&skip={skip}&orderBy={orderBy}";

            return await RequestEnumerable<Optimization>(new Uri(url, UriKind.Relative));
        }

        /// <inheritdoc />
        public async Task<IEnumerable<Optimization>> GetOptimizationsAsync(OptimizationType optimizationType, OptimizationStatus optimizationStatus, int take, int skip = 0)
        {
            var url = $"api/intelliDivide/optimizations?optimizationType={optimizationType}&state={optimizationStatus}&take={take}&skip={skip}";

            return await RequestEnumerable<Optimization>(new Uri(url, UriKind.Relative));
        }

        /// <inheritdoc />
        public async Task<IEnumerable<Optimization>> GetOptimizationsAsync(OptimizationType optimizationType, OptimizationStatus optimizationStatus, string orderBy,
            int take, int skip = 0)
        {
            var url = $"api/intelliDivide/optimizations?optimizationType={optimizationType}&state={optimizationStatus}&take={take}&skip={skip}&orderBy={orderBy}";

            return await RequestEnumerable<Optimization>(new Uri(url, UriKind.Relative));
        }

        /// <inheritdoc />
        public async Task<IEnumerable<Optimization>> GetOptimizationsAsync(int take, int skip = 0)
        {
            var url = $"api/intelliDivide/optimizations?take={take}&skip={skip}";

            return await RequestEnumerable<Optimization>(new Uri(url, UriKind.Relative));
        }

        /// <inheritdoc />
        public async Task<IEnumerable<Optimization>> GetOptimizationsAsync(string orderBy, int take, int skip = 0)
        {
            var url = $"api/intelliDivide/optimizations?take={take}&skip={skip}&orderBy={orderBy}";

            return await RequestEnumerable<Optimization>(new Uri(url, UriKind.Relative));
        }

        /// <inheritdoc />
        public async Task<OptimizationStatus> GetOptimizationStatusAsync(Guid optimizationId)
        {
            var url = $"api/intelliDivide/optimizations/{optimizationId}/state";

            return await RequestObject<OptimizationStatus>(new Uri(url, UriKind.Relative));
        }

        /// <inheritdoc />
        public async Task ArchiveOptimizationAsync(Guid optimizationId)
        {
            var url = $"api/intelliDivide/optimizations/{optimizationId}/archive";

            await PostObject(new Uri(url, UriKind.Relative));
        }

        /// <inheritdoc />
        public async Task DeleteOptimizationAsync(Guid optimizationId)
        {
            var url = $"api/intelliDivide/optimizations/{optimizationId}";

            await DeleteObject(new Uri(url, UriKind.Relative));
        }

        /// <inheritdoc />
        public async Task StartOptimizationAsync(Guid optimizationId)
        {
            var url = $"api/intelliDivide/optimizations/{optimizationId}/optimize";

            await PostObject(new Uri(url, UriKind.Relative));
        }

        #endregion

        #region Solutions

        /// <inheritdoc />
        public async Task<SolutionDetails> GetSolutionDetailsAsync(Guid optimizationId, Guid solutionId)
        {
            var url = $"/api/intelliDivide/optimizations/{optimizationId}/solutions/{solutionId}";

            var solution = await RequestObject<SolutionDetails>(new Uri(url, UriKind.Relative));

            return solution;
        }

        /// <inheritdoc />
        public async Task<IEnumerable<Solution>> GetSolutionsAsync(Guid optimizationId)
        {
            var url = $"/api/intelliDivide/optimizations/{optimizationId}/solutions";

            return await RequestEnumerable<Solution>(new Uri(url, UriKind.Relative));
        }

        /// <inheritdoc />
        public async Task SendSolutionAsync(Guid optimizationId, Guid solutionId)
        {
            await Task.Run(() => throw new NotSupportedException());

            throw new NotSupportedException();
        }

        /// <inheritdoc />
        public async Task DownloadSolutionExportAsync(Solution solution, SolutionExportType exportType, DirectoryInfo targetDirectory)
        {

            var optimization = await GetOptimizationAsync(solution.OptimizationId);

            var fileExtension = exportType switch
            {
                SolutionExportType.Saw => ".saw",
                SolutionExportType.Pdf => ".pdf",
                SolutionExportType.Ptx => ".ptx",
                SolutionExportType.MaterialDemand => ".xlsx",
                _ => throw new NotSupportedException($"Export type {exportType} is not supported.")
            };

            var fileInfo = new FileInfo(Path.Combine(targetDirectory.FullName, Uri.EscapeDataString(optimization.Name) + fileExtension));

            await DownloadSolutionExportAsync(solution.OptimizationId, solution.Id, exportType, fileInfo);

            if (!fileInfo.Exists)
            {
                throw new FileNotFoundException(fileInfo.FullName);
            }
        }

        /// <inheritdoc />
        public async Task DownloadSolutionExportAsync(Guid optimizationId, Guid solutionId, SolutionExportType exportTye, FileInfo fileInfo)
        {
            var url = $"/api/intelliDivide/optimizations/{optimizationId}/solutions/{solutionId}/exports/{exportTye}";

            var data = await RequestRawData(new Uri(url, UriKind.Relative));

            if (data == null || data.Length == 0)
            {
                throw new FileNotFoundException();
            }

            // ReSharper disable once MethodHasAsyncOverload
            File.WriteAllBytes(fileInfo.FullName, data);
        }

        /// <inheritdoc />
        public async Task<IEnumerable<SolutionPart>> GetSolutionProducedParts(Guid optimizationId, Guid solutionId)
        {
            var url = $"/api/intelliDivide/optimizations/{optimizationId}/solutions/{solutionId}/parts";

            return await RequestEnumerable<SolutionPart>(new Uri(url, UriKind.Relative));
        }

        /// <inheritdoc />
        public async Task<IEnumerable<SolutionPattern>> GetSolutionPatterns(Guid optimizationId, Guid solutionId)
        {
            var url = $"/api/intelliDivide/optimizations/{optimizationId}/solutions/{solutionId}/patterns";

            return await RequestEnumerable<SolutionPattern>(new Uri(url, UriKind.Relative));
        }

        #endregion
    }
}