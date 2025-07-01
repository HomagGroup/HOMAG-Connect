using HomagConnect.MaterialManager.Client;
using HomagConnect.MaterialManager.Contracts.Material.Edgebands.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace HomagConnect.MaterialManager.Samples.Read.Edgebands
{
    public class MaterialManagerReadEdgebandResultsSamples
    {
        // GetEdgebandTypes
        public static async Task Edgebands_GetEdgebandType(IMaterialManagerClientMaterialEdgebands materialManager)
        {
            // Define the edgebandCodes you want to retrieve data for:
            var edgebandCodes = new List<string> { "ABS_Abruzzo_colore_1.00_100.0_HM", "ACR_Buche_mit_Silberstreifen_2.00_43.0_HM" };
            var edgebandTypes = materialManager.GetEdgebandTypesByEdgebandCodes(edgebandCodes);
        }

        // GetLicensedMachines
        public static async Task Edgebands_GetLicensedMachines(IMaterialManagerClientMaterialEdgebands materialManager)
        {
            var tapioMachines = materialManager.GetLicensedMachines();
        }

        // GetTechnologyMacrosFromMachine
        public static async Task Edgebands_GetTechnologyMacrosFromMachine(IMaterialManagerClientMaterialEdgebands materialManager)
        {
            // Get the data
            var technologyMacros = materialManager.GetTechnologyMacrosFromMachine("testmachine");
        }
    }
}
