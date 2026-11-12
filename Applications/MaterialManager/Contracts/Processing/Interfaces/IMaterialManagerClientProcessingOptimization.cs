using System.Collections.Generic;
using System.Threading.Tasks;

using HomagConnect.MaterialManager.Contracts.Processing.Optimization;

namespace HomagConnect.MaterialManager.Contracts.Processing.Interfaces
{
    /// <summary>
    /// Interface for materialManager Client for processing optimization
    /// </summary>
    public interface IMaterialManagerClientProcessingOptimization
    {
        /// <summary>
        /// Retrieves offcut optimization parameters for one material code.
        /// </summary>
        /// <param name="materialCode">Material code to include in the request.</param>
        /// <returns>The offcut parameter set returned for the material code.</returns>
        Task<OffcutParameterSet> GetOffcutParameterSetAsync(string materialCode);

        /// <summary>
        /// Retrieves offcut optimization parameters for multiple material codes. The client trims codes, ignores blank
        /// entries, removes duplicates, and sends each remaining code as a URL-encoded <c>materialCode</c> query
        /// parameter.
        /// </summary>
        /// <param name="materialCodes">Material codes to include in the request.</param>
        /// <returns>The offcut parameter sets returned by the service.</returns>
        Task<IEnumerable<OffcutParameterSet>?> GetOffcutParameterSetsAsync(ICollection<string> materialCodes);
    }
}