using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HomagConnect.MaterialManager.Contracts.Statistics;

namespace HomagConnect.MaterialManager.Contracts.Material.Edgebands.Interfaces
{
    /// <summary>
    /// Interface for MaterialManager Edgebands Client.
    /// </summary>
    public interface IMaterialManagerClientMaterialEdgebands
    {
        /// <summary>
        /// Gets all edgebands.
        /// </summary>
        /// <param name="take"></param>
        /// <param name="skip"></param>
        /// <returns></returns>
        Task<IEnumerable<EdgebandType>> GetEdgebandTypes(int take, int skip = 0);

        /// <summary>
        /// Gets an edgeband by edgeband code.
        /// </summary>
        /// <param name="edgebandCode"></param>
        /// <returns></returns>
        Task<EdgebandType> GetEdgebandTypeByEdgebandCode(string edgebandCode);

        /// <summary>
        /// Gets an edgeband by edgeband code including details.
        /// </summary>
        /// <param name="edgebandCode"></param>
        /// <returns></returns>
        Task<EdgebandTypeDetails> GetEdgebandTypeByEdgebandCodeIncludingDetails(string edgebandCode);

        /// <summary>
        /// Gets edgebands by edgeband codes.
        /// </summary>
        /// <param name="edgebandCodes"></param>
        /// <returns>The edgeband types sorted by <see cref="EdgebandType.EdgebandCode" />.</returns>
        Task<IEnumerable<EdgebandType>> GetEdgebandTypesByEdgebandCodes(IEnumerable<string> edgebandCodes);

        /// <summary>
        /// Gets edgebands by edgeband codes including details.
        /// </summary>
        /// <param name="edgebandCodes"></param>
        /// <returns>The edgeband types sorted by <see cref="EdgebandType.EdgebandCode" />.</returns>
        Task<IEnumerable<EdgebandTypeDetails>> GetEdgebandTypesByEdgebandCodesIncludingDetails(IEnumerable<string> edgebandCodes);

        /// <summary>
        /// Get <see cref="EdgeInventoryHistory" /> inventory history for edgebands<see cref="EdgebandType" />.
        /// </summary>
        Task<IEnumerable<EdgebandTypeInventory>> GetEdgebandTypeInventoryHistoryAsync(DateTime from, DateTime to);

        #region Create

        /// <summary>
        /// Create an edgeband type for the passed parameters
        /// </summary>
        /// <param name="edgebandType"></param>
        /// <returns></returns>
        Task CreateEdgebandType(EdgebandType edgebandType);

        #endregion Create
        Task<IEnumerable<EdgeInventoryHistory>> GetEdgebandTypeInventoryHistoryAsync(DateTime from, DateTime to);
    }
}