using HomagConnect.Base.Contracts;
using HomagConnect.Base.Contracts.AdditionalData;
using HomagConnect.MaterialManager.Contracts.Material.Edgebands.Enumerations;
using HomagConnect.MaterialManager.Contracts.Material.Edgebands.Interfaces;
using HomagConnect.MaterialManager.Contracts.Update;

namespace HomagConnect.MaterialManager.Samples.Update.Edgebands
{
    /// <summary>
    /// Update edgeband type samples.
    /// </summary>
    public class UpdateEdgebandTypeSamples
    {
        /// <summary>
        /// The example shows how update a edgeband.
        /// </summary>
        public static async Task Edgebands_UpdateEdgebandType(IMaterialManagerClientMaterialEdgebands materialManager, string edgebandCode, double value)
        {
            var edgebandTypeUpdate = new MaterialManagerUpdateEdgebandType
            {
                DefaultLength = value,
                // Add other properties
            };
            var updatedEdgebandType = await materialManager.UpdateEdgebandType(edgebandCode, edgebandTypeUpdate);
        }

        /// <summary>
        /// The example shows how to create an edgeband type with additional data (e.g., a picture).
        /// </summary>
        public static async Task Edgebands_UpdateEdgebandType_AdditionalData(
            IMaterialManagerClientMaterialEdgebands materialManager,
            string edgebandCode)
        {
            var imageFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "Red.png");
            var additionalDataImage = new FileReference("Red.png", imageFilePath);

            var edgebandTypeUpdate = new MaterialManagerUpdateEdgebandType
            {
                EdgebandCode = edgebandCode,
                Height = 20,
                Thickness = 1.0,
                DefaultLength = 23.0,
                MaterialCategory = EdgebandMaterialCategory.Veneer,
                Process = EdgebandingProcess.Other,
                AdditionalData = new List<AdditionalDataEntity>
                {
                    new AdditionalDataImage
                    {
                        Category = "Decor",
                        DownloadFileName = additionalDataImage.Reference,
                        DownloadUri = new Uri(additionalDataImage.Reference, UriKind.Relative)
                    }
                }
            };

            var updateEdgebandType = await materialManager.UpdateEdgebandType(edgebandCode, edgebandTypeUpdate, [additionalDataImage]);
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
