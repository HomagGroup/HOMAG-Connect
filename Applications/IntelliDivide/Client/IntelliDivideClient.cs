using System.ComponentModel.DataAnnotations;


using HomagConnect.Base;
using HomagConnect.Base.Services;
using HomagConnect.IntelliDivide.Contracts;
using HomagConnect.IntelliDivide.Contracts.Common;
using HomagConnect.IntelliDivide.Contracts.Request;
using HomagConnect.IntelliDivide.Contracts.Result;

using Newtonsoft.Json;

namespace HomagConnect.IntelliDivide.Client
{
    /// <summary />
    public class IntelliDivideClient : ServiceBase, IIntelliDivideClient
    {
        #region Constants

        private const int _TakeLimit = 100;

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new instance of <see cref="IntelliDivideClient" />
        /// </summary>
        public IntelliDivideClient(HttpClient client) : base(client) { }

        #endregion

        #region Settings (Machines, Parameters, Import templates)

        /// <summary>
        /// Gets the import templates which have been created for the <see cref="OptimizationType" />. See
        /// <a href="https://docs.homag.cloud/en/intellidivide/tutorial/importing-data" /> for details.
        /// </summary>
        public async Task<IEnumerable<OptimizationImportTemplate>> GetImportTemplatesAsync(OptimizationType optimizationType, string fileExtension = "", string name = "")
        {
            var url = $"/api/intelliDivide/{optimizationType}/templates".ToLowerInvariant();
            var templates = await RequestEnumerable<OptimizationImportTemplate>(url);

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

        /// <summary>
        /// Gets the machine having the name.
        /// </summary>
        /// <returns>The machine if exists, otherwise null.</returns>
        public async Task<OptimizationMachine> GetMachineAsync(string machineName)
        {
            var machines = await GetMachinesAsync();

            return machines.First(m => m.Name == machineName);
        }

        /// <summary>
        /// Gets the list of machines.
        /// </summary>
        public async Task<IEnumerable<OptimizationMachine>> GetMachinesAsync()
        {
            var cuttingMachines = await GetMachinesAsync(OptimizationType.Cutting);
            var nestingMachines = await GetMachinesAsync(OptimizationType.Nesting);

            return cuttingMachines.Union(nestingMachines).OrderBy(m => m.Name);
        }

        /// <summary>
        /// Gets the list of machines of the specified <see cref="OptimizationType" />.
        /// </summary>
        public async Task<IEnumerable<OptimizationMachine>> GetMachinesAsync(OptimizationType optimizationType)
        {
            var url = $"/api/intelliDivide/{optimizationType}/machines".ToLowerInvariant();
            return await RequestEnumerable<OptimizationMachine>(url);
        }

        /// <summary>
        /// Gets the list of parameter sets for the specified <see cref="OptimizationType" />.
        /// </summary>
        public async Task<IEnumerable<OptimizationParameter>> GetParametersAsync(OptimizationType optimizationType)
        {
            var url = $"/api/intelliDivide/{optimizationType}/parameters".ToLowerInvariant();
            return await RequestEnumerable<OptimizationParameter>(url);
        }

        #endregion

        #region Optimization Request

        /// <summary>
        /// Request an optimization using a structured zip file.
        /// </summary>
        /// <param name="projectFile">
        /// Structured zip file, whose format corresponds to the ImportSpecification (
        /// <seealso
        ///     href="https://dev.azure.com/homag-group/FOSSProjects/_git/homag-api-gateway-client?path=/Documentation/ImportSpecification.md&_a=preview" />
        /// format.
        /// </param>
        public async Task<OptimizationRequestResponse?> RequestOptimizationAsync(FileInfo projectFile)
        {
            var request = new HttpRequestMessage { Method = HttpMethod.Post };

            var fileName = projectFile.Name;
            await using var stream = projectFile.OpenRead();

            var uri = "api/intelliDivide/optimizations".ToLowerInvariant();

            request.RequestUri = new Uri(uri, UriKind.Relative);
            
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

        /// <summary>
        /// Request an optimization based on a structured <see cref="OptimizationRequest" />.
        /// </summary>
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

        /// <summary>
        /// Waits until the optimization has reached the state  <see cref="OptimizationStatus.Optimized" /> or has reached a state
        /// from which the state <see cref="OptimizationStatus.Optimized" /> can't get reached any more.
        /// </summary>
        /// <exception cref="TimeoutException">Raised, when the specified maxDuration has been exceeded.</exception>
        public async Task<Optimization> WaitForCompletion(Guid optimizationId, TimeSpan maxDuration)
        {
            var timeout = DateTime.Now + maxDuration;

            while (DateTime.Now < timeout)
            {
                var currentStatus = await GetOptimizationStatusAsync(optimizationId);

                if (currentStatus is OptimizationStatus.Optimized
                    or OptimizationStatus.Faulted
                    or OptimizationStatus.Canceled
                    or OptimizationStatus.Transferred
                   )
                {
                    return await GetOptimizationAsync(optimizationId);
                }

                await Task.Delay(TimeSpan.FromSeconds(1));
            }

            throw new TimeoutException();
        }

        #endregion

        #region Optimzation List, Archive, Delete, Start

        /// <summary>
        /// Gets the optimization having the specified optimization id.
        /// </summary>
        /// <param name="optimizationId">The id of of the optimization to get.</param>
        public async Task<Optimization> GetOptimizationAsync(Guid optimizationId)
        {
            var url = $"api/intelliDivide/optimizations/{optimizationId}".ToLower();

            return await RequestObject<Optimization>(url);
        }

        /// <summary>
        /// Gets a <see cref="IEnumerable{T}" /> of optimizations available.
        /// </summary>
        /// <param name="optimizationType">Request only optimizations having a specific <see cref="OptimizationType" /></param>
        /// <param name="take">Quantity of optimizations to return max.</param>
        /// <param name="skip">Quantity of optimizations to skip.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown, when more then 100 optimizations are requested.</exception>
        public async Task<IEnumerable<Optimization>> GetOptimizationsAsync(OptimizationType optimizationType, uint take, uint skip = 0)
        {
            if (take is > _TakeLimit or 0)
            {
                throw new ArgumentOutOfRangeException(nameof(take));
            }

            var url = $"api/intelliDivide/optimizations?optimizationType={optimizationType}&take={take}&skip={skip}".ToLowerInvariant();

            return await RequestEnumerable<Optimization>(url);
        }

        /// <summary>
        /// Gets a <see cref="IEnumerable{T}" /> of optimizations available.
        /// </summary>
        /// <param name="optimizationType">Request only optimizations having a specific <see cref="OptimizationType" /></param>
        /// <param name="optimizationStatus">Request only optimizations having a specific <see cref="OptimizationStatus" /></param>
        /// <param name="take">Quantity of optimizations to return max.</param>
        /// <param name="skip">Quantity of optimizations to skip.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown, when more then 100 optimizations are requested.</exception>
        public async Task<IEnumerable<Optimization>> GetOptimizationsAsync(OptimizationType optimizationType, OptimizationStatus optimizationStatus, uint take, uint skip = 0)
        {
            if (take is > _TakeLimit or 0)
            {
                throw new ArgumentOutOfRangeException(nameof(take));
            }

            var url = $"api/intelliDivide/optimizations?optimizationType={optimizationType}&state={optimizationStatus}&take={take}&skip={skip}".ToLowerInvariant();

            return await RequestEnumerable<Optimization>(url);
        }

        /// <summary>
        /// Gets a <see cref="IEnumerable{T}" /> of optimizations available.
        /// </summary>
        /// <param name="take">Quantity of optimizations to return max.</param>
        /// <param name="skip">Quantity of optimizations to skip.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown, when more then 100 optimizations are requested.</exception>
        public async Task<IEnumerable<Optimization>> GetOptimizationsAsync(uint take, uint skip = 0)
        {
            var url = $"api/intelliDivide/optimizations?take={take}&skip={skip}".ToLowerInvariant();

            return await RequestEnumerable<Optimization>(url);
        }

        /// <summary>
        /// Gets the <see cref="OptimizationStatus" /> of the optimization having the provided optimization id.
        /// </summary>
        /// <param name="optimizationId">The id of of the optimization.</param>
        public async Task<OptimizationStatus> GetOptimizationStatusAsync(Guid optimizationId)
        {
            var url = $"api/intelliDivide/optimizations/{optimizationId}/state".ToLowerInvariant();

            return await RequestObject<OptimizationStatus>(url);
        }

        /// <summary>
        /// Archives the optimization having the specified id.
        /// </summary>
        public async Task ArchiveOptimizationAsync(Guid optimizationId)
        {
            await Task.Run(() => throw new NotImplementedException());

            throw new NotImplementedException();
        }

        /// <summary>
        /// Deletes the optimization having the specified id.
        /// </summary>
        public async Task DeleteOptimizationAsync(Guid optimizationId)
        {
            await Task.Run(() => throw new NotImplementedException());

            throw new NotImplementedException();
        }

        /// <summary>
        /// Starts the optimization having the specified id.
        /// </summary>
        public async Task StartOptimizationAsync(Guid optimizationId)
        {
            await Task.Run(() => throw new NotImplementedException());

            throw new NotImplementedException();
        }

        #endregion

        #region Solutions

        /// <summary>
        /// Gets the solution details.
        /// </summary>
        public async Task<SolutionDetails> GetSolutionDetailsAsync(Guid optimizationId, Guid solutionId)
        {
            var url = $"/api/intelliDivide/optimizations/{optimizationId}/solutions/{solutionId}".ToLowerInvariant();

            var solution = await RequestObject<SolutionDetails>(url);

            return solution;
        }

        /// <summary>
        /// Gets the <see cref="IEnumerable&lt;Solution&gt;" /> which have been calculated for an optimization request.
        /// </summary>
        /// <param name="optimizationId">The id of of the optimization.</param>
        /// <returns>Solutions if the optimization has been optimized successfully, otherwise an empty list.</returns>
        public async Task<IEnumerable<Solution>> GetSolutionsAsync(Guid optimizationId)
        {
            var url = $"/api/intelliDivide/optimizations/{optimizationId}/solutions".ToLowerInvariant();

            var solutions = await RequestEnumerable<Solution>(url);

            return solutions;
        }

        /// <summary>
        /// Sends the solution to the machine for which the optimization was requested for.
        /// </summary>
        /// <exception cref="NotSupportedException">Thrown, if the selected machine is not able send.</exception>
        public async Task SendSolutionAsync(Guid optimizationId, Guid solutionId)
        {
            await Task.Run(() => throw new NotImplementedException());

            throw new NotImplementedException();
        }

        /// <summary>
        /// Downloads the specified <see cref="SolutionExportType" /> into the specified file.
        /// </summary>
        /// <exception cref="FileNotFoundException">Thrown, when the specified file is not available.</exception>
        public async Task DownloadSolutionExport(Guid optimizationId, Guid solutionId, SolutionExportType exportTye, FileInfo fileInfo)
        {
            var url = $"/api/intelliDivide/optimizations/{optimizationId}/solutions/{solutionId}/exports/{exportTye}".ToLowerInvariant();

            var data = await RequestRawData(url);

            if (data == null || data.Length == 0)
            {
                throw new FileNotFoundException();
            }

            await File.WriteAllBytesAsync(fileInfo.FullName, data);
        }

        #endregion
    }
}