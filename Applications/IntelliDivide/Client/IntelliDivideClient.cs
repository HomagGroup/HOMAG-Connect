using HomagConnect.Base;
using HomagConnect.Base.DataModel;
using HomagConnect.Base.Extensions;
using HomagConnect.Base.Services;
using HomagConnect.IntelliDivide.Contracts;
using HomagConnect.IntelliDivide.Contracts.Common;
using HomagConnect.IntelliDivide.Contracts.Request;
using HomagConnect.IntelliDivide.Contracts.Result;
using HomagConnect.IntelliDivide.Contracts.Statistics;
using HomagConnect.MaterialManager.Contracts.Material.Boards;
using HomagConnect.MaterialManager.Contracts.Material.Boards.Enumerations;
using HomagConnect.MaterialManager.Contracts.Material.Edgebands;
using Newtonsoft.Json;
using System.Globalization;
using System.Net.Http.Headers;
using System.Security.Authentication;

namespace HomagConnect.IntelliDivide.Client
{
    /// <inheritdoc cref="IIntelliDivideClient" />
    public class IntelliDivideClient : ServiceBase, IIntelliDivideClient
    {
        #region Statistics

        /// <inheritdoc />
        public async Task<IEnumerable<MaterialEfficiency>?> GetMaterialStatistics(int daysBack, int take, int skip = 0)
        {
            var url = $"/api/intelliDivide/statistics/material?daysBack={daysBack}&take={take}&skip={skip}";

            return await RequestEnumerable<MaterialEfficiency>(new Uri(url, UriKind.Relative));
        }

        /// <inheritdoc />
        public async Task<IEnumerable<MaterialEfficiency>?> GetMaterialStatistics(DateTime from, DateTime to, int take, int skip = 0)
        {
            var url = $"/api/intelliDivide/statistics/material?from={from:s}&to={to:s}&take={take}&skip={skip}";

            return await RequestEnumerable<MaterialEfficiency>(new Uri(url, UriKind.Relative));
        }

        /// <inheritdoc />
        public async Task<IEnumerable<EdgebandUsage>?> GetEdgebandStatistics(int daysBack, int take, int skip = 0)
        {
            var url = $"/api/intelliDivide/statistics/edgeband?daysBack={daysBack}&take={take}&skip={skip}";

            return await RequestEnumerable<EdgebandUsage>(new Uri(url, UriKind.Relative));
        }

        /// <inheritdoc />
        public async Task<IEnumerable<EdgebandUsage>?> GetEdgebandStatistics(DateTime from, DateTime to, int take, int skip = 0)
        {
            var url = $"/api/intelliDivide/statistics/edgeband?from={from:s}&to={to:s}&take={take}&skip={skip}";

            return await RequestEnumerable<EdgebandUsage>(new Uri(url, UriKind.Relative));
        }

        /// <inheritdoc />
        public Task<IEnumerable<PartSizesByMaterialStatistic>> GetPartSizesByMaterialStatistics(IEnumerable<string> materialCodes, int daysBack)
        {
            return GetPartSizesByMaterialStatistics(materialCodes, DateTime.Now.AddDays(-daysBack), DateTime.Now);
        }

        /// <inheritdoc />
        public Task<IEnumerable<PartSizesByMaterialStatistic>> GetPartSizesByMaterialStatistics(IEnumerable<string> materialCodes, DateTime from, DateTime to)
        {
            if (materialCodes == null)
            {
                throw new ArgumentNullException(nameof(materialCodes));
            }

            var validMaterialCodes = materialCodes
                .Select(m => m.Trim())
                .Where(m => !string.IsNullOrWhiteSpace(m))
                .Distinct()
                .OrderBy(m => m).ToList();

            if (!validMaterialCodes.Any())
            {
                throw new ArgumentNullException(nameof(materialCodes), "At least one board code code must be passed.");
            }

            return GetPartSizesByMaterialStatisticsInternal(validMaterialCodes, from, to);
        }

        private async Task<IEnumerable<PartSizesByMaterialStatistic>> GetPartSizesByMaterialStatisticsInternal(IEnumerable<string> materialCodes, DateTime from, DateTime to)
        {
            var uris = materialCodes
                .Select(materialCode => $"&materialCode={Uri.EscapeDataString(materialCode)}")
                .Join(QueryParametersMaxLength)
                .Select(c => $"/api/intelliDivide/statistics/partSizesByMaterial?from={from:s}&to={to:s}" + c)
                .Select(c => new Uri(c, UriKind.Relative));

            return await RequestEnumerableAsync<PartSizesByMaterialStatistic>(uris);
        }

        #endregion

        #region Constructors

        /// <inheritdoc />
        public IntelliDivideClient(HttpClient client) : base(client) { }

        /// <inheritdoc />
        public IntelliDivideClient(Guid subscriptionOrPartnerId, string authorizationKey) : base(subscriptionOrPartnerId, authorizationKey) { }

        /// <inheritdoc />
        public IntelliDivideClient(Guid subscriptionOrPartnerId, string authorizationKey, Uri? baseUri) : base(subscriptionOrPartnerId, authorizationKey, baseUri) { }

        #endregion

        #region Settings (Machines, Parameters, Import templates)

        /// <inheritdoc />
        public async Task<IEnumerable<OptimizationImportTemplate>?> GetImportTemplates(OptimizationType optimizationType, string fileExtension = "", string name = "")
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
        public async Task<OptimizationMachine?> GetMachine(string machineName)
        {
            var machines = await GetMachines().ToListAsync();

            if (machines != null && machines.Any())
            {
                return machines.FirstOrDefault(m => string.Equals(m.Name, machineName, StringComparison.InvariantCultureIgnoreCase));
            }

            return null;
        }

        /// <inheritdoc />
        public async Task<IEnumerable<OptimizationMachine>> GetMachines()
        {
            var optimizationMachines = new List<OptimizationMachine>();

            try
            {
                var cuttingMachines = await GetMachines(OptimizationType.Cutting);

                if (cuttingMachines != null)
                {
                    optimizationMachines.AddRange(cuttingMachines);
                }
            }
            catch (AuthenticationException)
            {
                
            }

            try
            {
                var nestingMachines = await GetMachines(OptimizationType.Nesting);

                if (nestingMachines != null)
                {
                    optimizationMachines.AddRange(nestingMachines);
                }
            }
            catch (AuthenticationException)
            {

            }

            return optimizationMachines.OrderBy(m => m.Name);
        }

        /// <inheritdoc />
        public async Task<IEnumerable<OptimizationMachine>?> GetMachines(OptimizationType optimizationType)
        {
            var url = $"/api/intelliDivide/{optimizationType}/machines";

            return await RequestEnumerable<OptimizationMachine>(new Uri(url, UriKind.Relative));
        }

        /// <inheritdoc />
        public async Task<IEnumerable<OptimizationParameter>?> GetParameters(OptimizationType optimizationType)
        {
            var url = $"/api/intelliDivide/{optimizationType}/parameters";
            return await RequestEnumerable<OptimizationParameter>(new Uri(url, UriKind.Relative));
        }

        #endregion

        #region Materials

        /// <inheritdoc />
        public async Task<IEnumerable<BoardType>?> GetBoardTypes(int take, int skip = 0)
        {
            if (skip < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(skip), "The skip parameter must not be negative.");
            }

            if (take < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(take), "The take parameter must be greater than zero.");
            }

            var url = $"api/intelliDivide/materials/boards?take={take}&skip={skip}";
            return await RequestEnumerable<BoardType>(new Uri(url, UriKind.Relative));
        }

        /// <inheritdoc />
        public async Task<IDictionary<BoardMaterialCategory, string>> GetBoardMaterialCategoryDisplayNames()
        {
            return await GetBoardMaterialCategoryDisplayNames(CultureInfo.CurrentUICulture);
        }

        /// <inheritdoc />
        public async Task<IDictionary<BoardMaterialCategory, string>> GetBoardMaterialCategoryDisplayNames(CultureInfo cultureInfo)
        {
            string url = $"/api/localizations/enums/{nameof(BoardMaterialCategory)}/{cultureInfo.TwoLetterISOLanguageName}";

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(url, UriKind.Relative)
            };

            request.Headers.AcceptLanguage.Clear();
            request.Headers.AcceptLanguage.Add(new StringWithQualityHeaderValue(cultureInfo.Name));

            var response = await Client.SendAsync(request).ConfigureAwait(false);
            await response.EnsureSuccessStatusCodeWithDetailsAsync(request).ConfigureAwait(false);

            var result = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<IDictionary<BoardMaterialCategory, string>>(result, SerializerSettings.Default);

            return data ?? new Dictionary<BoardMaterialCategory, string>();
        }

        /// <inheritdoc />
        public async Task<IEnumerable<EdgebandType>?> GetEdgebandTypes(int take, int skip = 0)
        {
            if (skip < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(skip), "The skip parameter must not be negative.");
            }

            if (take < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(take), "The take parameter must be greater than zero.");
            }

            var url = $"api/intelliDivide/materials/edgebands?take={take}&skip={skip}";
            return await RequestEnumerable<EdgebandType>(new Uri(url, UriKind.Relative));
        }

        #endregion

        #region Optimization Request

        /// <inheritdoc />
        public async Task<OptimizationRequestResponse> RequestOptimization(OptimizationRequest optimizationRequest, IEnumerable<ImportFile> files)
        {
            return await RequestOptimization(optimizationRequest, files.ToArray());
        }

        /// <inheritdoc />
        public async Task<OptimizationRequestResponse> RequestOptimization(OptimizationRequest optimizationRequest, params ImportFile[] files)
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
            var responseObject = JsonConvert.DeserializeObject<OptimizationRequestResponse>(result, SerializerSettings.Default);

            return responseObject ?? new OptimizationRequestResponse();
        }

        /// <inheritdoc />
        public async Task<OptimizationRequestResponse> RequestOptimization(OptimizationRequestUsingTemplate optimizationRequest, params ImportFile[] files)
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
            var responseObject = JsonConvert.DeserializeObject<OptimizationRequestResponse>(result, SerializerSettings.Default);

            return responseObject ?? new OptimizationRequestResponse();
        }

        /// <inheritdoc />
        public async Task<OptimizationRequestResponse> RequestOptimization(OptimizationRequestUsingProject optimizationRequest, FileInfo projectFile)
        {
            var request = new HttpRequestMessage { Method = HttpMethod.Post };

            if (projectFile == null)
            {
                throw new ArgumentNullException(nameof(projectFile));
            }

            if (!projectFile.Exists)
            {
                throw new FileNotFoundException($"The project file '{projectFile.FullName}' does not exist.");
            }

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

            var responseObject = JsonConvert.DeserializeObject<OptimizationRequestResponse>(result, SerializerSettings.Default);

            return responseObject ?? new OptimizationRequestResponse();
        }

        /// <inheritdoc />
        public async Task<Optimization?> WaitForCompletion(Guid optimizationId, TimeSpan maxDuration)
        {
            return await WaitForOptimizationStatus(optimizationId, OptimizationStatus.Optimized, maxDuration);
        }

        /// <inheritdoc />
        public async Task<Optimization?> WaitForOptimizationStatus(Guid optimizationId, OptimizationStatus optimizationStatus, TimeSpan maxDuration)
        {
            var timeout = DateTime.Now + maxDuration;

            while (DateTime.Now < timeout)
            {
                var currentStatus = await GetOptimizationStatus(optimizationId);

                if (currentStatus == optimizationStatus)
                {
                    return await GetOptimization(optimizationId);
                }
                
                if (optimizationStatus == OptimizationStatus.Started
                    && currentStatus
                        is OptimizationStatus.Started
                        or OptimizationStatus.Optimized
                        or OptimizationStatus.Transferred)
                {
                    // When waiting for status Started the optimization might be already optimized or transferred.

                    return await GetOptimization(optimizationId);
                }

                if (optimizationStatus == OptimizationStatus.Optimized
                    && currentStatus
                        is OptimizationStatus.Optimized
                        or OptimizationStatus.Transferred)
                {
                    // When waiting for status Optimized the optimization might be already transferred.

                    return await GetOptimization(optimizationId);
                }

                if (currentStatus
                    is OptimizationStatus.Faulted
                    or OptimizationStatus.Canceled
                    or OptimizationStatus.Archived)
                {
                    // It is not possible to reach another state.

                    throw new InvalidOperationException($"The optimization status is '{currentStatus}'. The target status '{optimizationStatus}' can no longer be reached.");
                }

                await Task.Delay(TimeSpan.FromSeconds(1));
            }

            throw new TimeoutException();
        }

        /// <inheritdoc />
        public async Task<IEnumerable<DataModelProperty>> GetPartProperties()
        {
            var request = new HttpRequestMessage { Method = HttpMethod.Get };

            const string uri = "api/intelliDivide/optimizations/GetPartProperties";
            request.RequestUri = new Uri(uri, UriKind.Relative);
            var response = await Client.SendAsync(request);

            await response.EnsureSuccessStatusCodeWithDetailsAsync(request);

            var result = await response.Content.ReadAsStringAsync();

            var responseObject = JsonConvert.DeserializeObject<IEnumerable<DataModelProperty>>(result, SerializerSettings.Default);

            return responseObject ?? new List<DataModelProperty>(0);
        }

        #endregion

        #region Optimzation List, Archive, Delete, Start

        /// <inheritdoc />
        public async Task<Optimization?> GetOptimization(Guid optimizationId)
        {
            if (optimizationId == Guid.Empty)
            {
                throw new ArgumentException("The optimization id must not be empty.", nameof(optimizationId));
            }

            var url = $"api/intelliDivide/optimizations/{optimizationId}";

            return await RequestObject<Optimization>(new Uri(url, UriKind.Relative));
        }

        /// <inheritdoc />
        public async Task<IEnumerable<Optimization>?> GetOptimizations(OptimizationType optimizationType, int take, int skip = 0)
        {
            var url = $"api/intelliDivide/optimizations?optimizationType={optimizationType}&take={take}&skip={skip}";

            return await RequestEnumerable<Optimization>(new Uri(url, UriKind.Relative));
        }

        /// <inheritdoc />
        public async Task<IEnumerable<Optimization>?> GetOptimizations(OptimizationType optimizationType, string orderBy, int take, int skip = 0)
        {
            var url = $"api/intelliDivide/optimizations?optimizationType={optimizationType}&take={take}&skip={skip}&orderBy={orderBy}";

            return await RequestEnumerable<Optimization>(new Uri(url, UriKind.Relative));
        }

        /// <inheritdoc />
        public async Task<IEnumerable<Optimization>?> GetOptimizations(OptimizationType optimizationType, OptimizationStatus optimizationStatus, int take, int skip = 0)
        {
            var url = $"api/intelliDivide/optimizations?optimizationType={optimizationType}&state={optimizationStatus}&take={take}&skip={skip}";

            return await RequestEnumerable<Optimization>(new Uri(url, UriKind.Relative));
        }

        /// <inheritdoc />
        public async Task<IEnumerable<Optimization>?> GetOptimizations(OptimizationType optimizationType, OptimizationStatus optimizationStatus, string orderBy, string machine, int take, int skip = 0)
        {
            var url = $"api/intelliDivide/optimizations?optimizationType={optimizationType}&state={optimizationStatus}&machine={Uri.EscapeDataString(machine)}&take={take}&skip={skip}";

            return await RequestEnumerable<Optimization>(new Uri(url, UriKind.Relative));
        }

        /// <inheritdoc />
        public async Task<IEnumerable<Optimization>?> GetOptimizations(OptimizationType optimizationType, OptimizationStatus optimizationStatus, string orderBy,
            int take, int skip = 0)
        {
            var url = $"api/intelliDivide/optimizations?optimizationType={optimizationType}&state={optimizationStatus}&take={take}&skip={skip}&orderBy={orderBy}";

            return await RequestEnumerable<Optimization>(new Uri(url, UriKind.Relative));
        }

        /// <inheritdoc />
        public async Task<IEnumerable<Optimization>?> GetOptimizations(int take, int skip = 0)
        {
            var url = $"api/intelliDivide/optimizations?take={take}&skip={skip}";

            return await RequestEnumerable<Optimization>(new Uri(url, UriKind.Relative));
        }

        /// <inheritdoc />
        public async Task<IEnumerable<Optimization>?> GetOptimizations(string orderBy, int take, int skip = 0)
        {
            var url = $"api/intelliDivide/optimizations?take={take}&skip={skip}&orderBy={orderBy}";

            return await RequestEnumerable<Optimization>(new Uri(url, UriKind.Relative));
        }

        /// <inheritdoc />
        public async Task<OptimizationStatus> GetOptimizationStatus(Guid optimizationId)
        {
            var url = $"api/intelliDivide/optimizations/{optimizationId}/state";

            return await RequestObject<OptimizationStatus>(new Uri(url, UriKind.Relative));
        }

        /// <inheritdoc />
        public async Task ArchiveOptimization(Guid optimizationId)
        {
            var url = $"api/intelliDivide/optimizations/{optimizationId}/archive";

            await PostObject(new Uri(url, UriKind.Relative));
        }

        /// <inheritdoc />
        public async Task DeleteOptimization(Guid optimizationId)
        {
            var url = $"api/intelliDivide/optimizations/{optimizationId}";

            await DeleteObject(new Uri(url, UriKind.Relative));
        }

        /// <inheritdoc />
        public async Task StartOptimization(Guid optimizationId)
        {
            var url = $"api/intelliDivide/optimizations/{optimizationId}/optimize";

            await PostObject(new Uri(url, UriKind.Relative));
        }

        #endregion

        #region Solutions

        /// <inheritdoc />
        public async Task<SolutionDetails?> GetSolutionDetails(Guid optimizationId, Guid solutionId)
        {
            var url = $"/api/intelliDivide/optimizations/{optimizationId}/solutions/{solutionId}";

            var solution = await RequestObject<SolutionDetails>(new Uri(url, UriKind.Relative));

            return solution;
        }

        /// <inheritdoc />
        public async Task<IEnumerable<Solution>?> GetSolutions(Guid optimizationId)
        {
            var url = $"/api/intelliDivide/optimizations/{optimizationId}/solutions";

            return await RequestEnumerable<Solution>(new Uri(url, UriKind.Relative));
        }

        /// <inheritdoc />
        public async Task SendSolution(Guid optimizationId, Guid solutionId)
        {
            var url = $"/api/intelliDivide/optimizations/{optimizationId}/solutions/{solutionId}/send";

            await PostObject(new Uri(url, UriKind.Relative));
        }

        /// <inheritdoc />
        public async Task DownloadSolutionExport(Solution solution, SolutionExportType exportType, DirectoryInfo targetDirectory)
        {
            var optimization = await GetOptimization(solution.OptimizationId);

            var fileExtension = exportType switch
            {
                SolutionExportType.Saw => ".saw",
                SolutionExportType.Pdf => ".pdf",
                SolutionExportType.Ptx => ".ptx",
                SolutionExportType.ZIP => ".zip",
                SolutionExportType.MaterialDemand => ".xlsx",
                _ => throw new NotSupportedException($"Export type {exportType} is not supported.")
            };

            var fileInfo = new FileInfo(Path.Combine(targetDirectory.FullName, Uri.EscapeDataString(optimization.Name) + fileExtension));

            await DownloadSolutionExport(solution.OptimizationId, solution.Id, exportType, fileInfo);

            if (!fileInfo.Exists)
            {
                throw new FileNotFoundException(fileInfo.FullName);
            }
        }

        /// <inheritdoc />
        public async Task DownloadSolutionExport(Guid optimizationId, Guid solutionId, SolutionExportType exportTye, FileInfo fileInfo)
        {
            var url = $"/api/intelliDivide/optimizations/{optimizationId}/solutions/{solutionId}/exports/{exportTye}";

            var data = await RequestRawData(new Uri(url, UriKind.Relative));

            if (data == null || data.Length == 0)
            {
                throw new FileNotFoundException();
            }

            // ReSharper disable once MethodHasAsyncOverload
#pragma warning disable S6966 // Awaitable method should be used
            File.WriteAllBytes(fileInfo.FullName, data);
#pragma warning restore S6966 // Awaitable method should be used
        }

        /// <inheritdoc />
        public async Task<IEnumerable<SolutionExportType>?> GetSolutionAvailableExports(Guid optimizationId, Guid solutionId)
        {
            var url = $"/api/intelliDivide/optimizations/{optimizationId}/solutions/{solutionId}/exports";

            return await RequestEnumerable<SolutionExportType>(new Uri(url, UriKind.Relative));
        }

        /// <inheritdoc />
        public async Task<IEnumerable<SolutionPart>?> GetSolutionProducedParts(Guid optimizationId, Guid solutionId)
        {
            var url = $"/api/intelliDivide/optimizations/{optimizationId}/solutions/{solutionId}/parts";

            return await RequestEnumerable<SolutionPart>(new Uri(url, UriKind.Relative));
        }

        /// <inheritdoc />
        public async Task<IEnumerable<SolutionPattern>?> GetSolutionPatterns(Guid optimizationId, Guid solutionId)
        {
            var url = $"/api/intelliDivide/optimizations/{optimizationId}/solutions/{solutionId}/patterns";

            return await RequestEnumerable<SolutionPattern>(new Uri(url, UriKind.Relative));
        }

        #endregion
    }
}