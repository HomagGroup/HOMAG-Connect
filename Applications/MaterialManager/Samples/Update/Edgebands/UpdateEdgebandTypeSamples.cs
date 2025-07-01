using HomagConnect.MaterialManager.Contracts.Material.Edgebands.Interfaces;
using HomagConnect.MaterialManager.Contracts.Update;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomagConnect.MaterialManager.Samples.Update.Edgebands
{
    public class UpdateEdgebandTypeSamples
    {
        /// <summary>
        /// The example shows how update a edgeband.
        /// </summary>
        public static async Task Edgebands_UpdateEdgebandType(IMaterialManagerClientMaterialEdgebands materialManager)
        {
            var edgebandTypeUpdate = new MaterialManagerUpdateEdgebandType
            {
                Thickness = 1.1,
                // Add other properties
            };
            var updatedEdgebandType = await client.UpdateEdgebandType("EB_White_1mm", edgebandTypeUpdate);
            Console.WriteLine($"Updated Edgeband Type: {updatedEdgebandType.Code}");
        }

        /// <summary>
        /// The example shows how update a edgeband with technology macro.
        /// </summary>
        public static async Task Edgebands_UpdateEdgebandType(IMaterialManagerClientMaterialEdgebands materialManager)
        {
            var edgebandTypeUpdate = new MaterialManagerUpdateEdgebandType
            {
                MachineTechnologyMacro = new Dictionary<string, string>
                {
                    { "hg0000000000", "ABS_1.00_RM_HM"}
                },
                // other properties
            };

            var updatedEdgebandType = await client.UpdateEdgebandType("ABS_White_1mm", edgebandTypeUpdate);
        }
    }
}
