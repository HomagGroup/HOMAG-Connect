using System.Collections.Generic;
using System.Threading.Tasks;

using HomagConnect.MaterialManager.Contracts.Processing.Cnc;
using HomagConnect.MaterialManager.Contracts.Processing.Optimization;

namespace HomagConnect.MaterialManager.Contracts.Processing.Interfaces
{
    /// <summary>
    /// Interface for materialManager Client for processing optimization
    /// </summary>
    public interface IMaterialManagerClientProcessingCnc
    {
        /// <summary>
        /// Gets the milling parameter tool groups for the current subscription.
        /// </summary>
        Task<IEnumerable<ToolGroup>?> GetMillingParameterToolGroups();

        /// <summary>
        /// Gets the milling parameter material groups for the current subscription.
        /// </summary>
        Task<IEnumerable<MaterialGroup>?> GetMillingParameterMaterialGroups();

        /// <summary>
        /// Gets the milling parameter parameter groups for the current subscription.
        /// </summary>
        Task<IEnumerable<MillingParameterGroup>?> GetMillingParameterGroups();
    }
}