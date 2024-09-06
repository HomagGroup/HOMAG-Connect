using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

using HomagConnect.IntelliDivide.Contracts.Common;
using HomagConnect.IntelliDivide.Contracts.Request;
using HomagConnect.IntelliDivide.Contracts.Result;
using HomagConnect.IntelliDivide.Contracts.Statistics;

namespace HomagConnect.IntelliDivide.Contracts
{
    /// <summary>
    /// IntelliDivide client interface.
    /// </summary>
    public interface IIntelliDivideClient
    {
        /// <summary>
        /// Archives the optimization having the specified id.
        /// </summary>
        Task ArchiveOptimizationAsync(Guid optimizationId);

        /// <summary>
        /// Deletes the optimization having the specified id.
        /// </summary>
        Task DeleteOptimizationAsync(Guid optimizationId);

        /// <summary>
        /// Downloads the specified <see cref="SolutionExportType" /> into the specified file.
        /// </summary>
        /// <exception cref="FileNotFoundException">Thrown, when the specified file is not available.</exception>
        Task DownloadSolutionExportAsync(Guid optimizationId, Guid solutionId, SolutionExportType exportTye, FileInfo fileInfo);

        /// <summary>
        /// Downloads the specified <see cref="SolutionExportType" /> into the specified directory.
        /// </summary>
        Task DownloadSolutionExportAsync(Solution solution, SolutionExportType exportType, DirectoryInfo targetDirectory);

        /// <summary>
        /// Gets the import templates which have been created for the <see cref="OptimizationType" />. See
        /// <a href="https://docs.homag.cloud/en/intellidivide/tutorial/importing-data" /> for details.
        /// </summary>
        Task<IEnumerable<OptimizationImportTemplate>> GetImportTemplatesAsync(OptimizationType optimizationType, string fileExtension = "", string name = "");

        /// <summary>
        /// Gets the machine having the name.
        /// </summary>
        /// <returns>The machine if exists, otherwise null.</returns>
        Task<OptimizationMachine> GetMachineAsync(string machineName);

        /// <summary>
        /// Gets the list of machines.
        /// </summary>
        Task<IEnumerable<OptimizationMachine>> GetMachinesAsync();

        /// <summary>
        /// Gets the list of machines of the specified <see cref="OptimizationType" />.
        /// </summary>
        Task<IEnumerable<OptimizationMachine>> GetMachinesAsync(OptimizationType optimizationType);

        /// <summary>
        /// Gets the material statistics.
        /// </summary>
        Task<IEnumerable<MaterialEfficiency>> GetMaterialStatisticsAsync(DateTime from, DateTime to, int take, int skip = 0);

        /// <summary>
        /// Gets the material statistics.
        /// </summary>
        Task<IEnumerable<MaterialEfficiency>> GetMaterialStatisticsAsync(int daysBack, int take, int skip = 0);

        /// <summary>
        /// Gets the optimization having the specified optimization id.
        /// </summary>
        /// <param name="optimizationId">The id of of the optimization to get.</param>
        Task<Optimization> GetOptimizationAsync(Guid optimizationId);

        /// <summary>
        /// Gets a <see cref="IEnumerable{T}" /> of optimizations available.
        /// </summary>
        /// <param name="optimizationType">Request only optimizations having a specific <see cref="OptimizationType" /></param>
        /// <param name="take">Quantity of optimizations to return max.</param>
        /// <param name="skip">Quantity of optimizations to skip.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown, when more then 100 optimizations are requested.</exception>
        Task<IEnumerable<Optimization>> GetOptimizationsAsync(OptimizationType optimizationType, int take, int skip = 0);

        /// <summary>
        /// Gets a <see cref="IEnumerable{T}" /> of optimizations available.
        /// </summary>
        /// <param name="optimizationType">Request only optimizations having a specific <see cref="OptimizationType" /></param>
        /// <param name="orderBy">Optimization property name to order by <see cref="Optimization" /></param>
        /// <param name="take">Quantity of optimizations to return max.</param>
        /// <param name="skip">Quantity of optimizations to skip.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown, when more then 100 optimizations are requested.</exception>
        Task<IEnumerable<Optimization>> GetOptimizationsAsync(OptimizationType optimizationType, string orderBy, int take, int skip = 0);

        /// <summary>
        /// Gets a <see cref="IEnumerable{T}" /> of optimizations available.
        /// </summary>
        /// <param name="optimizationType">Request only optimizations having a specific <see cref="OptimizationType" /></param>
        /// <param name="optimizationStatus">Request only optimizations having a specific <see cref="OptimizationStatus" /></param>
        /// <param name="take">Quantity of optimizations to return max.</param>
        /// <param name="skip">Quantity of optimizations to skip.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown, when more then 100 optimizations are requested.</exception>
        Task<IEnumerable<Optimization>> GetOptimizationsAsync(OptimizationType optimizationType, OptimizationStatus optimizationStatus, int take, int skip = 0);

        /// <summary>
        /// Gets a <see cref="IEnumerable{T}" /> of optimizations available.
        /// </summary>
        /// <param name="optimizationType">Request only optimizations having a specific <see cref="OptimizationType" /></param>
        /// <param name="optimizationStatus">Request only optimizations having a specific <see cref="OptimizationStatus" /></param>
        /// <param name="orderBy">Optimization property name to order by <see cref="Optimization" /></param>
        /// <param name="take">Quantity of optimizations to return max.</param>
        /// <param name="skip">Quantity of optimizations to skip.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown, when more then 100 optimizations are requested.</exception>
        Task<IEnumerable<Optimization>> GetOptimizationsAsync(OptimizationType optimizationType, OptimizationStatus optimizationStatus, string orderBy, int take, int skip = 0);

        /// <summary>
        /// Gets a <see cref="IEnumerable{T}" /> of optimizations available.
        /// </summary>
        /// <param name="take">Quantity of optimizations to return max.</param>
        /// <param name="skip">Quantity of optimizations to skip.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown, when more then 100 optimizations are requested.</exception>
        Task<IEnumerable<Optimization>> GetOptimizationsAsync(int take, int skip = 0);

        /// <summary>
        /// Gets a <see cref="IEnumerable{T}" /> of optimizations available.
        /// </summary>
        /// <param name="orderBy">Optimization property name to order by <see cref="Optimization" /></param>
        /// <param name="take">Quantity of optimizations to return max.</param>
        /// <param name="skip">Quantity of optimizations to skip.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown, when more then 100 optimizations are requested.</exception>
        Task<IEnumerable<Optimization>> GetOptimizationsAsync(string orderBy, int take, int skip = 0);

        /// <summary>
        /// Gets the <see cref="OptimizationStatus" /> of the optimization having the provided optimization id.
        /// </summary>
        /// <param name="optimizationId">The id of of the optimization.</param>
        Task<OptimizationStatus> GetOptimizationStatusAsync(Guid optimizationId);

        /// <summary>
        /// Gets the list of parameter sets for the specified <see cref="OptimizationType" />.
        /// </summary>
        Task<IEnumerable<OptimizationParameter>> GetParametersAsync(OptimizationType optimizationType);

        /// <summary>
        /// Gets the part sizes by material statistics.
        /// </summary>
        Task<IEnumerable<PartSizesByMaterialStatistic>> GetPartSizesByMaterialStatisticsAsync(IEnumerable<string> materialCodes, DateTime from, DateTime to);

        /// <summary>
        /// Gets the part sizes by material statistics.
        /// </summary>
        Task<IEnumerable<PartSizesByMaterialStatistic>> GetPartSizesByMaterialStatisticsAsync(IEnumerable<string> materialCodes, int daysBack);

        /// <summary>
        /// Gets the solution details.
        /// </summary>
        Task<SolutionDetails> GetSolutionDetailsAsync(Guid optimizationId, Guid solutionId);

        /// <summary>
        /// Returns solution patterns
        /// </summary>
        /// <param name="optimizationId"></param>
        /// <param name="solutionId"></param>
        /// <returns></returns>
        Task<IEnumerable<SolutionPattern>> GetSolutionPatterns(Guid optimizationId, Guid solutionId);

        /// <summary>
        /// </summary>
        /// <param name="optimizationId">The optimization id</param>
        /// <param name="solutionId">The solution id</param>
        /// <returns>produced parts of the solution</returns>
        Task<IEnumerable<SolutionPart>> GetSolutionProducedParts(Guid optimizationId, Guid solutionId);

        /// <summary>
        /// Gets the <see cref="IEnumerable&lt;Solution&gt;" /> which have been calculated for an optimization request.
        /// </summary>
        /// <param name="optimizationId">The id of of the optimization.</param>
        /// <returns>Solutions if the optimization has been optimized successfully, otherwise an empty list.</returns>
        Task<IEnumerable<Solution>> GetSolutionsAsync(Guid optimizationId);

        /// <summary>
        /// Request an optimization based on a structured <see cref="OptimizationRequest" />.
        /// </summary>
        Task<OptimizationRequestResponse> RequestOptimizationAsync(OptimizationRequest optimizationRequest, params ImportFile[] files);

        /// <summary>
        /// Request an optimization based on a structured <see cref="OptimizationRequest" />.
        /// </summary>
        Task<OptimizationRequestResponse> RequestOptimizationAsync(OptimizationRequest optimizationRequest, IEnumerable<ImportFile> files);

        /// <summary>
        /// Request an optimization based on a structured <see cref="OptimizationRequest" />.
        /// </summary>
        Task<OptimizationRequestResponse> RequestOptimizationAsync(OptimizationRequestUsingTemplate optimizationRequest, params ImportFile[] files);

        /// <summary>
        /// Request an optimization using a structured zip file.
        /// </summary>
        /// <param name="optimizationRequest">
        /// Optimization request based on a structured <see cref="OptimizationRequest" />.
        /// </param>
        /// <param name="projectFile">
        /// Structured zip file, whose format corresponds to the ImportSpecification (
        /// <seealso
        ///     href="https://dev.azure.com/homag-group/FOSSProjects/_git/homag-api-gateway-client?path=/Documentation/ImportSpecification.md" />
        /// format.
        /// </param>
        Task<OptimizationRequestResponse> RequestOptimizationAsync(OptimizationRequestUsingProject optimizationRequest, FileInfo projectFile);

        /// <summary>
        /// Sends the solution to the machine for which the optimization was requested for.
        /// </summary>
        /// <exception cref="NotSupportedException">Thrown, if the selected machine is not able send.</exception>
        Task SendSolutionAsync(Guid optimizationId, Guid solutionId);

        /// <summary>
        /// Starts the optimization having the specified id.
        /// </summary>
        Task StartOptimizationAsync(Guid optimizationId);

        /// <summary>
        /// Waits until the optimization has reached the state <see cref="OptimizationStatus.Optimized" /> or
        /// <see cref="OptimizationStatus.Transferred" /> or has reached a state
        /// from which the state <see cref="OptimizationStatus.Optimized" /> can't get reached any more.
        /// </summary>
        /// <exception cref="TimeoutException">Raised, when the specified maxDuration has been exceeded.</exception>
        Task<Optimization> WaitForCompletionAsync(Guid optimizationId, TimeSpan maxDuration);

        /// <summary>
        /// Waits until the optimization is reached the specified <see cref="OptimizationStatus" /> or a following state or a from
        /// which the specified state can't get reached any more.
        /// </summary>
        /// <exception cref="TimeoutException">Raised, when the specified maxDuration has been exceeded.</exception>
        Task<Optimization> WaitForOptimizationStatusAsync(Guid optimizationId, OptimizationStatus optimizationStatus, TimeSpan maxDuration);
    }
}