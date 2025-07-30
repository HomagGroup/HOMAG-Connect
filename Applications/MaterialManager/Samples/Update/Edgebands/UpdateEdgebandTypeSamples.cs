using HomagConnect.MaterialManager.Contracts.Material.Edgebands.Interfaces;
using HomagConnect.MaterialManager.Contracts.Update;

namespace HomagConnect.MaterialManager.Samples.Update.Edgebands
{
    public class UpdateEdgebandTypeSamples
    {
        /// <summary>
        /// The example shows how update a edgeband.
        /// </summary>
        public static async Task Edgebands_UpdateEdgebandType(IMaterialManagerClientMaterialEdgebands materialManager, string edgebandCode)
        {
            var edgebandTypeUpdate = new MaterialManagerUpdateEdgebandType
            {
                Thickness = 1.1,
                // Add other properties
            };
            var updatedEdgebandType = await materialManager.UpdateEdgebandType(edgebandCode, edgebandTypeUpdate);
            Console.WriteLine($"Updated Edgeband Type: {updatedEdgebandType.EdgebandCode}");
        }

        /// <summary>
        /// The example shows how update a edgeband with technology macro.
        /// </summary>
        public static async Task Edgebands_UpdateEdgebandTypeMacro(IMaterialManagerClientMaterialEdgebands materialManager)
        {
            var edgebandTypeUpdate = new MaterialManagerUpdateEdgebandType
            {
                MachineTechnologyMacro = new Dictionary<string, string>
                {
                    { "hg0000000000", "ABS_1.00_RM_HM"}
                },
                // other properties
            };

            var updatedEdgebandType = await materialManager.UpdateEdgebandType("ABS_White_1mm", edgebandTypeUpdate);
        }
    }
}
