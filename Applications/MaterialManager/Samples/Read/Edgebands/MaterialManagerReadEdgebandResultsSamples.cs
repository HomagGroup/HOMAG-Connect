using HomagConnect.MaterialManager.Client;
using HomagConnect.MaterialManager.Contracts.Material.Edgebands.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace HomagConnect.MaterialManager.Samples.Read.Edgebands
{
    public class MaterialManagerReadEdgebandResultsSamples
    {
        /// <summary>
        /// Get edgebands by edgeband code.
        /// </summary>
        public static async Task Edgebands_GetEdgebandTypeByEdgebandCode(IMaterialManagerClientMaterialEdgebands materialManager)
        {
            // Define the edgebandCode you want to retrieve data for (string):
            var edgebandCode = "ABS_Abruzzo_colore_1.00_100.0_HM";
            var edgebandType = materialManager.GetEdgebandTypeByEdgebandCode(edgebandCode);
        }

        public static async Task Edgebands_GetEdgebandTypeByEdgebandCodeIncludingDetails(IMaterialManagerClientMaterialEdgebands materialManager)
        {
            // Define the edgebandCode you want to retrieve data for (string):
            var edgebandCode = "ABS_Abruzzo_colore_1.00_100.0_HM";
            var edgebandType = materialManager.GetEdgebandTypeByEdgebandCodeIncludingDetails(edgebandCode);
        }

        /// <summary>
        /// Get all edgebands.
        /// </summary>
        public static async Task Edgebands_GetEdgebandTypes(IMaterialManagerClientMaterialEdgebands materialManager)
        {
            // parameter is described as int take
            // (was ist parameter take???)
            var tapioMachines = materialManager.GetEdgebandTypes(6);
        }
        public static async Task Edgebands_GetEdgebandTypesIncludingDetails(IMaterialManagerClientMaterialEdgebands materialManager)
        {
            // parameter is described as int take
            var tapioMachines = materialManager.GetEdgebandTypesIncludingDetails(6);
        }

        /// <summary>
        /// Get edgebands by edgeband codes in a list.
        /// </summary>
        public static async Task Edgebands_GetEdgebandTypesByEdgebandCodes(IMaterialManagerClientMaterialEdgebands materialManager)
        {
            //Define the edgebandCodes you want to retrieve data for in a list
            var edgebandCodes = new List<string> { "ABS_Abruzzo_colore_1.00_100.0_HM", "ACR_Buche_mit_Silberstreifen_2.00_43.0_HM" };
            var tapioMachines = materialManager.GetEdgebandTypesByEdgebandCodes(edgebandCodes);
        }
        public static async Task Edgebands_GetEdgebandTypesByEdgebandCodesIncludingDetails(IMaterialManagerClientMaterialEdgebands materialManager)
        {
            //Define the edgebandCodes you want to retrieve data for in a list
            var edgebandCodes = new List<string> { "ABS_Abruzzo_colore_1.00_100.0_HM", "ACR_Buche_mit_Silberstreifen_2.00_43.0_HM" };
            var tapioMachines = materialManager.GetEdgebandTypesByEdgebandCodesIncludingDetails(edgebandCodes);
        }

        /// <summary>
        /// Get inventory history for edgebands.
        /// </summary>
        public static async Task Edgebands_GetEdgebandTypeInventoryHistoryAsync(IMaterialManagerClientMaterialEdgebands materialManager)
        {
            // time can be "int daysBack" or a timespan with "Datetime from, Datetime to"
            var edgebandTypesHistory = materialManager.GetEdgebandTypeInventoryHistoryAsync(10);
            var edgebandTypesHistory2 = materialManager.GetEdgebandTypeInventoryHistoryAsync("Datetime from, Datetime to");
        }

        /// <summary>
        /// Get all technology macros for a machine.
        /// </summary>
        public static async Task Edgebands_GetTechnologyMacrosFromMachine(IMaterialManagerClientMaterialEdgebands materialManager)
        {
            var technologyMacros = materialManager.GetTechnologyMacrosFromMachine("tapioMachineId");
        }

        /// <summary>
        /// Get all TapioMachine licensed for material api.
        /// </summary>
        public static async Task Edgebands_GetLicensedMachines(IMaterialManagerClientMaterialEdgebands materialManager)
        {
            var tapioMachines = materialManager.GetLicensedMachines();
        }
    }
}
