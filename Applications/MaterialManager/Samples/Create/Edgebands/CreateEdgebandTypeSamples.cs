using HomagConnect.Base.Contracts;
using HomagConnect.Base.Contracts.AdditionalData;
using HomagConnect.Base.Extensions;
using HomagConnect.MaterialManager.Contracts.Material.Edgebands.Enumerations;
using HomagConnect.MaterialManager.Contracts.Material.Edgebands.Interfaces;
using HomagConnect.MaterialManager.Contracts.Request;

namespace HomagConnect.MaterialManager.Samples.Create.Edgebands
{
    public class CreateEdgebandTypeSamples
    {
        /// <summary>
        /// The example shows how to create an edgeband type.
        /// </summary>
        public static async Task Edgebands_CreateEdgebandType(IMaterialManagerClientMaterialEdgebands materialManager, string edgebandCode)
        {
            var edgebandTypeRequest = new MaterialManagerRequestEdgebandType
            {
                EdgebandCode = edgebandCode,
                Height = 20,
                Thickness = 3.0,
                DefaultLength = 23.0,
                MaterialCategory = EdgebandMaterialCategory.ABS,
                Process = EdgebandingProcess.Other,
            };
            var newEdgebandType = await materialManager.CreateEdgebandType(edgebandTypeRequest);
            newEdgebandType.Trace();
        }

        /// <summary>
        /// The example shows how to create an edgeband type with additional data (e.g., a picture).
        /// </summary>
        public static async Task Edgebands_CreateEdgebandType_AdditionalData(
            IMaterialManagerClientMaterialEdgebands materialManager,
            string edgebandCode)
        {
            var imageFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "Red.png");
            var additionalDataImage = new FileReference("Red.png", imageFilePath);

            var edgebandTypeRequest = new MaterialManagerRequestEdgebandType
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

            var newEdgebandType = await materialManager.CreateEdgebandType(edgebandTypeRequest, new[] { additionalDataImage });
            newEdgebandType.Trace();
        }

        /// <summary>
        /// The example shows how to create an edgeband type with technology macro.
        /// </summary>
        public static async Task Edgebands_CreateEdgebandTypeMacro(IMaterialManagerClientMaterialEdgebands materialManager)
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
                    { "hg0000000000", "ABS_1.00_RM_HM" }
                },
            };
            var newEdgebandType = await materialManager.CreateEdgebandType(edgebandTypeRequest);
            newEdgebandType.Trace();
        }
    }
}