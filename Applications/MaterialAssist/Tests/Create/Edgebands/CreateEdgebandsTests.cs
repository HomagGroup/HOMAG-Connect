using HomagConnect.MaterialAssist.Samples.Create.Edgebands;
using HomagConnect.MaterialAssist.Samples.Delete.Edgebands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomagConnect.MaterialAssist.Tests.Create.Edgebands
{
    [TestClass]
    public class CreateEdgebandsTests : MaterialAssistTestBase
    {
        [TestMethod]
        public async Task EdgebandsCreateEdgebandEntity()
        {
            var MaterialAssistClient = GetMaterialAssistClient().Edgebands;
            await CreateEdgebandEntitiesSamples.Edgebands_CreateEdgebandEntity(MaterialAssistClient, "42");
        }
        
        [TestMethod]
        public async Task EdgebandsCreateEdgebandType()
        {
            var MaterialAssistClient = GetMaterialAssistClient().Edgebands;
            await CreateEdgebandEntitiesSamples.Edgebands_CreateEdgebandType(MaterialAssistClient, "White Edgeband 1mm");
        }

        [ClassCleanup]
        public async Task Cleanup ()
        {
            var MaterialAssistClient = GetMaterialAssistClient().Edgebands;
            await MaterialAssistClient.DeleteEdgebandEntity("42");

            var MaterialManagerClient = GetMaterialManagerClient();
            await MaterialManagerClient.Material.Edgebands.DeleteEdgebandType("White Edgeband 1mm");
        }
    }
}
