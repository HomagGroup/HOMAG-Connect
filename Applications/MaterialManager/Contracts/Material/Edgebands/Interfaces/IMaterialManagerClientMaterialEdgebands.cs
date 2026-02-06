using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using HomagConnect.Base.Contracts;
using HomagConnect.MaterialManager.Contracts.Common;
using HomagConnect.MaterialManager.Contracts.Delete;
using HomagConnect.MaterialManager.Contracts.Request;
using HomagConnect.MaterialManager.Contracts.Statistics;
using HomagConnect.MaterialManager.Contracts.Update;

namespace HomagConnect.MaterialManager.Contracts.Material.Edgebands.Interfaces
{
    /// <summary>
    /// Interface for MaterialManager Edgebands Client.
    /// </summary>
    public interface IMaterialManagerClientMaterialEdgebands
    {
        /// <summary>
        /// Creates the edgeband type in materialManager.
        /// </summary>
        /// <returns>The created edgeband type <see cref="EdgebandType" />.</returns>
        Task<EdgebandType> CreateEdgebandType(MaterialManagerRequestEdgebandType edgebandTypeRequest);

        /// <summary>
        /// Creates the edgeband type in materialManager.
        /// </summary>
        /// <returns>The created edgeband type <see cref="EdgebandType" />.</returns>
        Task<EdgebandType> CreateEdgebandType(MaterialManagerRequestEdgebandType edgebandTypeRequest, FileReference[] fileReferences);

        /// <summary>
        /// Creates the edgeband type allocation in materialManager.
        /// </summary>
        /// <param name="edgebandTypeAllocationRequest"></param>
        /// <returns></returns>
        Task<EdgebandTypeAllocation> CreateEdgebandTypeAllocation(EdgebandTypeAllocationRequest edgebandTypeAllocationRequest);

        /// <summary>
        /// Deletes the edgeband by its edgeband code.
        /// </summary>
        Task DeleteEdgebandType(string edgebandCode);

        /// <summary>
        /// Deletes the edgeband type allocation in materialManager.
        /// </summary>
        /// <param name="edgebandTypeAllocationDelete"></param>
        /// <returns></returns>
        Task DeleteEdgebandTypeAllocation(EdgebandTypeAllocationDelete edgebandTypeAllocationDelete);

        /// <summary>
        /// Delete edgebands by edgeband codes.
        /// </summary>
        Task DeleteEdgebandTypes(IEnumerable<string> edgebandCodes);

        /// <summary>
        /// Gets the edgeband type allocation from materialManager.
        /// </summary>
        /// <param name="order"></param>
        /// <param name="customer"></param>
        /// <param name="project"></param>
        /// <param name="edgebandCode"></param>
        /// <returns></returns>
        Task<EdgebandTypeAllocation> GetEdgebandTypeAllocation(string order, string customer, string project, string edgebandCode);

        /// <summary>
        /// Gets all edgeband type allocations from materialManager.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<EdgebandTypeAllocation>> GetEdgebandTypeAllocations();

        /// <summary>
        /// Gets all edgeband type allocations changed since the given date from materialManager.
        /// </summary>
        /// <param name="changedSince"></param>
        /// <returns></returns>
        Task<IEnumerable<EdgebandTypeAllocation>> GetEdgebandTypeAllocations(DateTimeOffset changedSince);

        /// <summary>
        /// Gets an edgeband by edgeband code.
        /// </summary>
        /// <param name="edgebandCode"></param>
        /// <returns></returns>
        Task<EdgebandType?> GetEdgebandTypeByEdgebandCode(string edgebandCode);

        /// <summary>
        /// Gets an edgeband by edgeband code including details.
        /// </summary>
        /// <param name="edgebandCode"></param>
        /// <returns></returns>
        Task<EdgebandTypeDetails?> GetEdgebandTypeByEdgebandCodeIncludingDetails(string edgebandCode);

        /// <summary>
        /// Get <see cref="EdgeInventoryHistory" /> inventory history for edgebands<see cref="EdgebandType" />.
        /// </summary>
        Task<IEnumerable<EdgeInventoryHistory>> GetEdgebandTypeInventoryHistoryAsync(DateTime from, DateTime to);

        /// <summary>
        /// Get <see cref="EdgeInventoryHistory" /> inventory history for edgebands<see cref="EdgebandType" />.
        /// </summary>
        Task<IEnumerable<EdgeInventoryHistory>> GetEdgebandTypeInventoryHistoryAsync(int daysBack);

        /// <summary>
        /// Gets all edgebands.
        /// </summary>
        Task<IEnumerable<EdgebandType>?> GetEdgebandTypes(int take, int skip = 0);

        /// <summary>
        /// Gets only the edgeband types changed since the given date.
        /// </summary>
        Task<IEnumerable<EdgebandType>?> GetEdgebandTypes(DateTimeOffset changedSince, int take, int skip = 0);

        /// <summary>
        /// Gets edgebands by edgeband codes.
        /// </summary>
        /// <param name="edgebandCodes"></param>
        /// <returns>The edgeband types sorted by <see cref="EdgebandType.EdgebandCode" />.</returns>
        Task<IEnumerable<EdgebandType?>> GetEdgebandTypesByEdgebandCodes(IEnumerable<string> edgebandCodes);

        /// <summary>
        /// Gets edgebands by edgeband codes including details.
        /// </summary>
        /// <param name="edgebandCodes"></param>
        /// <returns>The edgeband types sorted by <see cref="EdgebandType.EdgebandCode" />.</returns>
        Task<IEnumerable<EdgebandTypeDetails?>> GetEdgebandTypesByEdgebandCodesIncludingDetails(IEnumerable<string> edgebandCodes);

        /// <summary>
        /// Gets all edgebands including details.
        /// </summary>
        Task<IEnumerable<EdgebandTypeDetails>?> GetEdgebandTypesIncludingDetails(int take, int skip = 0);

        /// <summary>
        /// Gets only the edgeband types changed since the given date including details.
        /// </summary>
        Task<IEnumerable<EdgebandTypeDetails>?> GetEdgebandTypesIncludingDetails(DateTimeOffset changedSince, int take, int skip = 0);

        /// <summary>
        /// Get all <see cref="TapioMachine" /> licensed for material api. />.
        /// </summary>
        Task<IEnumerable<TapioMachine>?> GetLicensedMachines();

        /// <summary>
        /// Get all technology macros for a machine.
        /// </summary>
        /// <param name="tapioMachineId">The machine id from tapio.</param>
        Task<IEnumerable<string>?> GetTechnologyMacrosFromMachine(string tapioMachineId);

        /// <summary>
        /// Updates the requested edgeband type by its edgebandCode in materialManager.
        /// </summary>
        Task<EdgebandType> UpdateEdgebandType(string edgebandCode, MaterialManagerUpdateEdgebandType edgebandTypeUpdate);

        /// <summary>
        /// Updates the requested edgeband type allocation in materialManager.
        /// </summary>
        /// <param name="edgebandTypeAllocationUpdate"></param>
        /// <returns></returns>
        Task<EdgebandTypeAllocation> UpdateEdgebandTypeAllocation(EdgebandTypeAllocationUpdate edgebandTypeAllocationUpdate);
    }
}