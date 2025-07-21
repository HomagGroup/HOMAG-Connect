using HomagConnect.MaterialManager.Contracts.Material.Edgebands.Enumerations;
using HomagConnect.MaterialManager.Contracts.Request;
using HomagConnect.MaterialManager.Samples.Create.Edgebands;
using HomagConnect.MaterialManager.Samples.Update.Edgebands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomagConnect.MaterialManager.Tests.Update.Edgebands
{
    /// <summary />
    [TestClass]
    public class UpdateEdgebandTypeTests : MaterialManagerTestBase
    {
        /// <summary />
        [ClassInitialize]
        public async Task Initialize()
        {
            var materialManagerClient = GetMaterialManagerClient();
            var edgebandTypeRequest = new MaterialManagerRequestEdgebandType
            {
                EdgebandCode = "EB_White_1mm",
                Height = 20,
                Thickness = 1.0,
                DefaultLength = 23.0,
                MaterialCategory = EdgebandMaterialCategory.Veneer,
                Process = EdgebandingProcess.Other,
            };
            var newEdgebandType = await materialManagerClient.Material.Edgebands.CreateEdgebandType(edgebandTypeRequest);
        }

        [TestMethod]
        public async Task EdgebandsUpdateEdgebandType()
        {
            var materialManagerClient = GetMaterialManagerClient();
            await UpdateEdgebandTypeSamples.Edgebands_UpdateEdgebandType(materialManagerClient.Material.Edgebands);
        }
        
        [ClassCleanup]
        public async Task Cleanup()
        {
            var materialManagerClient = GetMaterialManagerClient();
            await materialManagerClient.Material.Edgebands.DeleteEdgebandType("EB_White_1mm");
        }
    }
}
