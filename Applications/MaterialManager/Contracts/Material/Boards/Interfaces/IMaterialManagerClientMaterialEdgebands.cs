using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using HomagConnect.MaterialManager.Contracts.Material.Edgebands;

namespace HomagConnect.MaterialManager.Contracts.Material.Boards.Interfaces
{
    public interface IMaterialManagerClientMaterialEdgebands
    {
        /// <summary>
        /// Gets all edgebands
        /// </summary>
        /// <param name="take"></param>
        /// <param name="skip"></param>
        /// <returns></returns>
        Task<IEnumerable<EdgebandType>> GetEdgebandTypes(int take, int skip = 0);

        /// <summary>
        /// Gets an edgeband by edgeband code
        /// </summary>
        /// <param name="edgebandCode"></param>
        /// <returns></returns>
        Task<EdgebandType> GetEdgebandTypeByEdgebandCode(string edgebandCode);

        /// <summary>
        /// Gets an edgeband by edgeband code including details
        /// </summary>
        /// <param name="edgebandCode"></param>
        /// <returns></returns>
        Task<EdgebandType> GetEdgebandTypeByEdgebandCodeIncludingDetails(string edgebandCode);

        /// <summary>
        /// Gets edgebands by edgeband codes
        /// </summary>
        /// <param name="edgebandCodes"></param>
        /// <returns>The edgeband types sorted by <see cref="EdgebandType.EdgebandCode" />.</returns>
        Task<IEnumerable<EdgebandType>> GetEdgebandTypesByMaterialCodes(IEnumerable<string> edgebandCodes);

        /// <summary>
        /// Gets edgebands by edgeband codes including details
        /// </summary>
        /// <param name="edgebandCodes"></param>
        /// <returns>The edgeband types sorted by <see cref="EdgebandType.EdgebandCode" />.</returns>
        Task<IEnumerable<EdgebandTypeDetails>> GetEdgebandTypesByMaterialCodesIncludingDetails(IEnumerable<string> edgebandCodes);
    }
}
