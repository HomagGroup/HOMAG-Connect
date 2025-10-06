using HomagConnect.MaterialManager.Contracts.Material.Edgebands.Interfaces;

namespace HomagConnect.MaterialManager.Samples.Read.Edgebands
{
    public class MaterialManagerReadEdgebandResultsSamples
    {
        /// <summary>
        /// Get edgebands by edgeband code.
        /// </summary>
        public static async Task<Contracts.Material.Edgebands.EdgebandType?> Edgebands_GetEdgebandTypeByEdgebandCode(IMaterialManagerClientMaterialEdgebands materialManager, string edgebandCode)
        {
            // Define the edgebandCode you want to retrieve data for (string):
            var edgebandType = await materialManager.GetEdgebandTypeByEdgebandCode(edgebandCode);
            return edgebandType;
        }

        public static async Task<Contracts.Material.Edgebands.EdgebandTypeDetails?> Edgebands_GetEdgebandTypeByEdgebandCodeIncludingDetails(IMaterialManagerClientMaterialEdgebands materialManager, string edgebandCode)
        {
            // Define the edgebandCode you want to retrieve data for (string):
            var edgebandType = await materialManager.GetEdgebandTypeByEdgebandCodeIncludingDetails(edgebandCode);
            return edgebandType;
        }

        /// <summary>
        /// Get all edgebands.
        /// </summary>
        public static async Task<IEnumerable<Contracts.Material.Edgebands.EdgebandType>?> Edgebands_GetEdgebandTypes(IMaterialManagerClientMaterialEdgebands materialManager)
        {
            // parameter int take
            var allEdgebands = await materialManager.GetEdgebandTypes(6);
            return allEdgebands;
        }
        public static async Task<IEnumerable<Contracts.Material.Edgebands.EdgebandTypeDetails>?> Edgebands_GetEdgebandTypesIncludingDetails(IMaterialManagerClientMaterialEdgebands materialManager)
        {
            // parameter int take
            var allEdgebandsDetails = await materialManager.GetEdgebandTypesIncludingDetails(6);
            return allEdgebandsDetails;
        }

        /// <summary>
        /// Get edgebands by edgeband codes in a list.
        /// </summary>
        public static async Task<IEnumerable<Contracts.Material.Edgebands.EdgebandType?>> Edgebands_GetEdgebandTypesByEdgebandCodes(IMaterialManagerClientMaterialEdgebands materialManager, List<string> edgebandCodes)
        {
            //Define the edgebandCodes you want to retrieve data for in a list
            var edgebands = await materialManager.GetEdgebandTypesByEdgebandCodes(edgebandCodes);
            return edgebands;
        }
        public static async Task<IEnumerable<Contracts.Material.Edgebands.EdgebandTypeDetails?>> Edgebands_GetEdgebandTypesByEdgebandCodesIncludingDetails(IMaterialManagerClientMaterialEdgebands materialManager, List<string> edgebandCodes)
        {
            //Define the edgebandCodes you want to retrieve data for in a list
            var edgebandsDetails = await materialManager.GetEdgebandTypesByEdgebandCodesIncludingDetails(edgebandCodes);
            return edgebandsDetails;
        }

        /// <summary>
        /// Get inventory history for edgebands.
        /// </summary>
        public static async Task Edgebands_GetEdgebandTypeInventoryHistoryAsync(IMaterialManagerClientMaterialEdgebands materialManager)
        {
            // time can be "int daysBack" or a timespan with "Datetime from, Datetime to"
            var edgebandTypesHistory = await materialManager.GetEdgebandTypeInventoryHistoryAsync(10);
            var edgebandTypesHistory2 = await materialManager.GetEdgebandTypeInventoryHistoryAsync(new DateTime(2025,06,01), new DateTime(2025, 06, 30));
        }

        /// <summary>
        /// Get all technology macros for a machine.
        /// </summary>
        public static async Task Edgebands_GetTechnologyMacrosFromMachine(IMaterialManagerClientMaterialEdgebands materialManager)
        {
            var technologyMacros = await materialManager.GetTechnologyMacrosFromMachine("tapioMachineId");
        }

        /// <summary>
        /// Get all TapioMachine licensed for material api.
        /// </summary>
        public static async Task Edgebands_GetLicensedMachines(IMaterialManagerClientMaterialEdgebands materialManager)
        {
            var tapioMachines = await materialManager.GetLicensedMachines();
        }
    }
}
