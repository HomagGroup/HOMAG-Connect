using HomagConnect.MaterialAssist.Samples.Create.Edgebands;
using HomagConnect.MaterialAssist.Samples.Delete.Edgebands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomagConnect.MaterialAssist.Tests.Delete.Edgebands
{
    public class DeleteEdgebandsTests : MaterialAssistTestBase
    {
        [ClassInitialize]
            public async Task Initialie()
            {
                var MaterialAssistClient = GetMaterialAssistClient().Edgebands;
                await CreateEdgebandEntitiesSamples.Edgebands_CreateEdgebandEntity(MaterialAssistClient, "42");
                await CreateEdgebandEntitiesSamples.Edgebands_CreateEdgebandEntity(MaterialAssistClient, "50");
                await CreateEdgebandEntitiesSamples.Edgebands_CreateEdgebandEntity(MaterialAssistClient, "23");
            }
        
        [TestMethod]
        public async Task EdgebandsDeleteEdgebandEntity()
        {
            var MaterialAssistClient = GetMaterialAssistClient().Edgebands;
            var EdgebadId = "42"; 
            await DeleteEdgebandEntitiesSamples.Edgebands_DeleteEdgebandEntity(MaterialAssistClient, EdgebadId);
        }

        [TestMethod]
        public async Task EdgebandsDeleteEdgebandEntities()
        {
            var MaterialAssistClient = GetMaterialAssistClient().Edgebands;
            List<string> EdgebandIds = ["50", "23"]; 
            await DeleteEdgebandEntitiesSamples.Edgebands_DeleteEdgebandEntities(MaterialAssistClient, EdgebandIds);
        }

    }
}
