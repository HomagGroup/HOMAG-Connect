using HomagConnect.Base.Contracts;
using HomagConnect.Base.Contracts.AdditionalData;
using HomagConnect.Base.Extensions;
using HomagConnect.MaterialManager.Contracts.Material.Edgebands;
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
        /// The example shows how update an edgeband.
        /// </summary>
        public static async Task Edgebands_UpdateEdgebandType(IMaterialManagerClientMaterialEdgebands materialManager, string edgebandCode, double value)
        {
            var edgebandTypeUpdate = new MaterialManagerUpdateEdgebandType
            {
                DefaultLength = value,
                // Add other properties
            };
            var updatedEdgebandType = await materialManager.UpdateEdgebandType(edgebandCode, edgebandTypeUpdate);
            updatedEdgebandType.Trace();            
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
            updateEdgebandType.Trace();            
        }

        /// <summary>
        /// The example shows how update an edgeband with technology macro.
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
            updatedEdgebandType.Trace();            
        }

        /// <summary>
        /// The example shows how to patch an edgeband type. Only the properties which are set
        /// via the <see cref="PatchBuilder{T}" /> are sent to and changed by materialManager.
        /// </summary>
        public static async Task Edgebands_PatchEdgebandType(IMaterialManagerClientMaterialEdgebands materialManager, string edgebandCode)
        {
            var patchData = PatchBuilder<EdgebandType>.For()
                .Set(e => e.DefaultLength, 50.0)
                .Set(e => e.Thickness, 1.0)
                .Set(e => e.Height, 23.0)
                .Set(e => e.MaterialCategory, EdgebandMaterialCategory.Veneer);

            var patchedEdgebandType = await materialManager.PatchEdgebandType(edgebandCode, patchData);

            patchedEdgebandType.Trace();
        }

        /// <summary>
        /// The example shows how to clear a value of an edgeband type by patching it with null.
        /// </summary>
        public static async Task Edgebands_PatchEdgebandType_ClearValue(IMaterialManagerClientMaterialEdgebands materialManager, string edgebandCode)
        {
            var patchData = PatchBuilder<EdgebandType>.For()
                .Set(e => e.Costs, null);

            var patchedEdgebandType = await materialManager.PatchEdgebandType(edgebandCode, patchData);

            patchedEdgebandType.Trace();
        }
    }
}
