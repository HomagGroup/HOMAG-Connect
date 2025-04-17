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
        /// Get offcut parameter set for the given material code
        /// </summary>
        /// <param name="materialCode"></param>
        /// <returns></returns>
        Task<OffcutParameterSet> GetOffcutParameterSetAsync(string materialCode);

        /// <summary>
        /// Get offcut parameter sets for the given material codes
        /// </summary>
        /// <param name="materialCodes"></param>
        /// <returns></returns>
        Task<IEnumerable<OffcutParameterSet>?> GetOffcutParameterSetsAsync(ICollection<string> materialCodes);
    }
}