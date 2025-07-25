using HomagConnect.MaterialAssist.Samples.Create.Edgebands;
using HomagConnect.MaterialAssist.Samples.Delete.Edgebands;
using System;
using System.Collections.Generic;
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
            var id = "42";
            await CreateEdgebandEntitiesSamples.Edgebands_CreateEdgebandEntity(MaterialAssistClient, id);
        }

        /*
        [TestMethod]
        public async Task EdgebandsCreateEdgebandType()
        {
            var MaterialAssistClient = GetMaterialAssistClient().Edgebands;
            var edgebandCode = "White Edgeband 1mm";
            await CreateEdgebandEntitiesSamples.Edgebands_CreateEdgebandType(MaterialAssistClient, edgebandCode);
        }
        */

        [ClassCleanup]
        public async Task Cleanup ()
        {
            var MaterialAssistClient = GetMaterialAssistClient().Edgebands;
            await DeleteEdgebandEntitiesSamples.Edgebands_DeleteEdgebandEntity(MaterialAssistClient, "42");
        }

    }
}
