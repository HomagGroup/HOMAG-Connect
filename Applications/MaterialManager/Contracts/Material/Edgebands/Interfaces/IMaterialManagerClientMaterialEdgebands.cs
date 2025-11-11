using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using HomagConnect.Base.Contracts;
using HomagConnect.MaterialManager.Contracts.Common;
using HomagConnect.MaterialManager.Contracts.Material.Boards;
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
        /// Get all technology macros for a machine.
        /// </summary>
        /// <param name="tapioMachineId">The machine id from tapio.</param>
        Task<IEnumerable<string>?> GetTechnologyMacrosFromMachine(string tapioMachineId);

        /// <summary>
        /// Get all <see cref="TapioMachine" /> licensed for material api. />.
        /// </summary>
        Task<IEnumerable<TapioMachine>?> GetLicensedMachines();

        /// <summary>
        /// Gets all edgebands.
        /// </summary>
        Task<IEnumerable<EdgebandType>?> GetEdgebandTypes(int take, int skip = 0);
        
        /// <summary>
        /// Gets only the edgeband types changed since the given date.
        /// </summary>
        Task<IEnumerable<EdgebandType>?> GetEdgebandTypes(DateTimeOffset changedSince, int take, int skip = 0);

        /// <summary>
        /// Gets all edgebands including details.
        /// </summary>
        Task<IEnumerable<EdgebandTypeDetails>?> GetEdgebandTypesIncludingDetails(int take, int skip = 0);
        
        /// <summary>
        /// Gets only the edgeband types changed since the given date including details.
        /// </summary>
        Task<IEnumerable<EdgebandTypeDetails>?> GetEdgebandTypesIncludingDetails(DateTimeOffset changedSince, int take, int skip = 0);

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
        /// Updates the requested edgeband type by its edgebandCode in materialManager.
        /// </summary>
        Task<EdgebandType> UpdateEdgebandType(string edgebandCode, MaterialManagerUpdateEdgebandType edgebandTypeUpdate);

        /// <summary>
        /// Deletes the edgeband by its edgeband code.
        /// </summary>
        Task DeleteEdgebandType(string edgebandCode);

        /// <summary>
        /// Delete edgebands by edgeband codes.
        /// </summary>
        Task DeleteEdgebandTypes(IEnumerable<string> edgebandCodes);



    }
}