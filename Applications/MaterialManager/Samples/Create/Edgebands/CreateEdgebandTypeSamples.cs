using HomagConnect.Base.Extensions;
using HomagConnect.MaterialManager.Contracts.Material.Edgebands.Enumerations;
using HomagConnect.MaterialManager.Contracts.Material.Edgebands.Interfaces;
using HomagConnect.MaterialManager.Contracts.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomagConnect.MaterialManager.Samples.Create.Edgebands
{
    public class CreateEdgebandTypeSamples
    {
        /// <summary>
        /// The example shows how to create a edgeband type.
        /// </summary>
        public static async Task Edgeband_CreateEdgebandType(IMaterialManagerClientMaterialEdgebands materialManager)
        {
            var edgebandTypeRequest = new MaterialManagerRequestEdgebandType
            {
                EdgebandCode = "EB_White_1mm",
                Height = 20,
                Thickness = 1.0,
                DefaultLength = 23.0,
                MaterialCategory = EdgebandMaterialCategory.Veneer,
                Process = EdgebandingProcess.Other,
            };
            var newEdgebandType = await materialManager.CreateEdgebandType(edgebandTypeRequest);
            newEdgebandType.Trace();
        }

        /// <summary>
        /// The example shows how to create a edgeband type with technology macro.
        /// </summary>
        public static async Task Edgeband_UpdateEdgebandType(IMaterialManagerClientMaterialEdgebands materialManager)
        {
            var edgebandTypeRequest = new MaterialManagerRequestEdgebandType
            {
                EdgebandCode = "EB_White_1mm",
                Height = 20,
                Thickness = 1.0,
                DefaultLength = 23.0,
                MaterialCategory = EdgebandMaterialCategory.Veneer,
                Process = EdgebandingProcess.Other,
                MachineTechnologyMacro = new Dictionary<string, string>
            {
                { "hg0000000000", "ABS_1.00_RM_HM"}
            },
                // other properties
            };
            var newEdgebandType = await materialManager.CreateEdgebandType(edgebandTypeRequest);
            newEdgebandType.Trace();
        }
    }

}
