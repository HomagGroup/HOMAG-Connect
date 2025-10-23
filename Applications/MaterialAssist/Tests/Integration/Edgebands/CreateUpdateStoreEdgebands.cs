using HomagConnect.Base.TestBase.Attributes;
using HomagConnect.MaterialAssist.Contracts.Request;
using HomagConnect.MaterialAssist.Contracts.Storage;
using HomagConnect.MaterialManager.Contracts.Material.Base;
using HomagConnect.MaterialManager.Contracts.Material.Edgebands.Enumerations;
using HomagConnect.MaterialManager.Contracts.Update;

namespace HomagConnect.MaterialAssist.Tests.Integration
{
    [TemporaryDisabledOnServer(2025, 12, 30, "DF-Material")]
    [TestClass]
    [TestCategory("MaterialAssist")]
    [TestCategory("MaterialAssist.Edgebands")]

    public class CreateUpdateStoreEdgebands : MaterialAssistTestBase
    {
        [TestInitialize]
        public async Task Initialize()
        {
            await EnsureEdgebandTypeExist("ABS_A_Dash_of_Freedom_1.00_100.0_HM", 1, 225);
            await EnsureEdgebandTypeExist("ABS_Multiplex_schwarz_1.00_23.0_NN", 1, 75);
            await EnsureEdgebandTypeExist("NN_Schwarz_16.50_24.5_HM", 16.5, 50);
        }

        [TestMethod]
        public async Task Edgebands_CreateUpdateStore()
        {
            // edgeband data
            var edgeband1 = new { id = "87", edgebandCode = "ABS_Multiplex_schwarz_1.00_23.0_NN" };
            var edgeband2 = new { id = "88", edgebandCode = "NN_Schwarz_16.50_24.5_HM" };
            var edgeband3 = new { id = "89", edgebandCode = "ABS_A_Dash_of_Freedom_1.00_100.0_HM" };

            // create edgeband entities
            var materialAssistClient = GetMaterialAssistClient().Edgebands;
            var edgebandEntityRequest = new MaterialAssistRequestEdgebandEntity()
            {
                Id = edgeband1.id,
                EdgebandCode = edgeband1.edgebandCode,
                ManagementType = ManagementType.Single,
                Quantity = 1,
                Length = 75.0,
                CurrentThickness = 1.0
            };
            await materialAssistClient.CreateEdgebandEntity(edgebandEntityRequest);

            var edgebandEntityRequest2 = new MaterialAssistRequestEdgebandEntity()
            {
                Id = edgeband2.id,
                EdgebandCode = edgeband2.edgebandCode,
                ManagementType = ManagementType.Stack,
                Quantity = 5,
                Length = 75.0,
                CurrentThickness = 16.5
            };
            await materialAssistClient.CreateEdgebandEntity(edgebandEntityRequest2);

            var edgebandEntityRequest3 = new MaterialAssistRequestEdgebandEntity()
            {
                Id = edgeband3.id,
                EdgebandCode = edgeband3.edgebandCode,
                ManagementType = ManagementType.GoodsInStock,
                Quantity = 3,
                Length = 225.0,
                CurrentThickness = 1.0
            };
            await materialAssistClient.CreateEdgebandEntity(edgebandEntityRequest3);

            
            // needed for storing
            // Get the first workstation
            var workstations = await materialAssistClient.GetWorkstations().ConfigureAwait(false);
            var firstWorkstation = workstations.FirstOrDefault();
            if (firstWorkstation == null)
            {
                Assert.Inconclusive("No workstations found.");
                return;
            }
            // Get the first storage location for the workstation
            var storageLocations = await materialAssistClient.GetStorageLocations(firstWorkstation.Id.ToString()).ConfigureAwait(false);
            var firstStorageLocation = storageLocations.FirstOrDefault();
            if (firstStorageLocation == null)
            {
                Assert.Inconclusive("No storage locations found for the workstation.");
                return;
            }
            
            // store edgeband entities
            var edgebandEntityStore = new MaterialAssistStoreEdgebandEntity()
            {
                Id = edgeband1.id,
                Length = 75,
                Workstation = firstWorkstation,
                StorageLocation = firstStorageLocation
            };
            await materialAssistClient.StoreEdgebandEntity(edgebandEntityStore);
            var edgebandEntity = await materialAssistClient.GetEdgebandEntityById(edgeband1.id);

            var edgebandEntityStore2 = new MaterialAssistStoreEdgebandEntity()
            {
                Id = edgeband2.id,
                Length = 75,
                Workstation = firstWorkstation,
                StorageLocation = firstStorageLocation
            };
            await materialAssistClient.StoreEdgebandEntity(edgebandEntityStore2);
            var edgebandEntity2 = await materialAssistClient.GetEdgebandEntityById(edgeband2.id);

            var edgebandEntityStore3 = new MaterialAssistStoreEdgebandEntity()
            {
                Id = edgeband3.id,
                Length = 225,
                Workstation = firstWorkstation,
                StorageLocation = firstStorageLocation
            };
            await materialAssistClient.StoreEdgebandEntity(edgebandEntityStore3);
            var edgebandEntity3 = await materialAssistClient.GetEdgebandEntityById(edgeband3.id);

            // update edgeband types
            var materialManagerClient = GetMaterialManagerClient().Material.Edgebands;
            var edgebandType1 = await materialManagerClient.GetEdgebandTypeByEdgebandCodeIncludingDetails(edgeband1.edgebandCode);
            var edgebandTypeUpdate = new MaterialManagerUpdateEdgebandType
            {
                EdgebandCode = "ABS_Multiplex_schwarz_1.2_23.0_NN",
                Thickness = 1.2,
            };
            await materialManagerClient.UpdateEdgebandType(edgeband1.edgebandCode, edgebandTypeUpdate);
            var updatedEdgeband1 = new { id = "87", edgebandCode = "ABS_Multiplex_schwarz_1.2_23.0_NN" };
            var updatedEdgebandType1 = await materialManagerClient.GetEdgebandTypeByEdgebandCodeIncludingDetails(updatedEdgeband1.edgebandCode);
            Assert.AreEqual(1.2, updatedEdgebandType1.Thickness);

            var edgebandType2 = await materialManagerClient.GetEdgebandTypeByEdgebandCodeIncludingDetails(edgeband2.edgebandCode);
            var edgebandTypeUpdate2 = new MaterialManagerUpdateEdgebandType
            {
                DefaultLength = 75.0,
            };
            await materialManagerClient.UpdateEdgebandType(edgeband2.edgebandCode, edgebandTypeUpdate2);
            var updatedEdgebandType2 = await materialManagerClient.GetEdgebandTypeByEdgebandCodeIncludingDetails(edgeband2.edgebandCode);
            Assert.AreEqual(75.0, updatedEdgebandType2.DefaultLength);

            var edgebandType3 = await materialManagerClient.GetEdgebandTypeByEdgebandCodeIncludingDetails(edgeband3.edgebandCode);
            var edgebandTypeUpdate3 = new MaterialManagerUpdateEdgebandType
            {
                DefaultLength = 100.0,
                MaterialCategory = EdgebandMaterialCategory.ABS,
                Process = EdgebandingProcess.HotmeltGlue,
            };
            await materialManagerClient.UpdateEdgebandType(edgeband3.edgebandCode, edgebandTypeUpdate3);
            var updatedEdgebandType3 = await materialManagerClient.GetEdgebandTypeByEdgebandCodeIncludingDetails(edgeband3.edgebandCode);
            Assert.AreEqual(EdgebandMaterialCategory.ABS, updatedEdgebandType3.MaterialCategory);
            Assert.AreEqual(EdgebandingProcess.HotmeltGlue, updatedEdgebandType3.Process);
        }

        //allocation for edgebands not implemented yet
        
        [TestCleanup]
        public async Task Cleanup()
        {
            var materialManagerClient = GetMaterialManagerClient().Material.Edgebands;
            await materialManagerClient.DeleteEdgebandType("ABS_Multiplex_schwarz_1.2_23.0_NN");
            await materialManagerClient.DeleteEdgebandType("NN_Schwarz_16.50_24.5_HM");
            await materialManagerClient.DeleteEdgebandType("ABS_A_Dash_of_Freedom_1.00_100.0_HM");
        }
    }
}
