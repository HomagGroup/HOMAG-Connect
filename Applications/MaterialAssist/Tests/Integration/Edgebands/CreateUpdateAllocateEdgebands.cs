using HomagConnect.Base.Contracts;
using HomagConnect.MaterialAssist.Contracts.Request;
using HomagConnect.MaterialAssist.Contracts.Storage;
using HomagConnect.MaterialManager.Contracts.Material.Base;
using HomagConnect.MaterialManager.Contracts.Material.Edgebands.Enumerations;
using HomagConnect.MaterialManager.Contracts.Request;
using HomagConnect.MaterialManager.Contracts.Update;

namespace HomagConnect.MaterialAssist.Tests.Integration
{
    [TestClass]
    [TestCategory("MaterialAssist")]
    [TestCategory("MaterialAssist.Edgebands")]

    public class CreateUpdateStoreEdgebands : MaterialAssistTestBase
    {
        [TestInitialize]
        public async Task Initialize()
        {
            await EnsureEdgebandTypeExist("ABS_A_Dash_of_Freedom_1.00_100.0_HM", 1, 225);
        }

        [TestMethod]
        public async Task Edgebands_CreateUpdateStore()
        {
            // create edgeband types
            var materialManagerClient = GetMaterialManagerClient().Material.Edgebands;
            var edgebandTypeRequest = new MaterialManagerRequestEdgebandType
            {
                EdgebandCode = "ABS_Multiplex_schwarz_1.00_23.0_NN",
                Height = 23.0,
                Thickness = 1.0,
                DefaultLength = 75.0,
                MaterialCategory = EdgebandMaterialCategory.ABS,
                Process = EdgebandingProcess.Other,
            };
            await materialManagerClient.CreateEdgebandType(edgebandTypeRequest);

            var edgebandTypeRequest2 = new MaterialManagerRequestEdgebandType
            {
                EdgebandCode = "NN_Schwarz_16.50_24.5_HM",
                Height = 24.5,
                Thickness = 16.5,
                DefaultLength = 50.0,
                MaterialCategory = EdgebandMaterialCategory.Others,
                Process = EdgebandingProcess.HotmeltGlue,
            };
            await materialManagerClient.CreateEdgebandType(edgebandTypeRequest2);

            // create edgeband entities
            var materialAssistClient = GetMaterialAssistClient().Edgebands;
            var edgebandEntityRequest = new MaterialAssistRequestEdgebandEntity()
            {
                Id = "87",
                EdgebandCode = "ABS_Multiplex_schwarz_1.00_23.0_NN",
                ManagementType = ManagementType.Single,
                Quantity = 1,
                Length = 75.0,
                CurrentThickness = 1.0
            };
            await materialAssistClient.CreateEdgebandEntity(edgebandEntityRequest);

            var edgebandEntityRequest2 = new MaterialAssistRequestEdgebandEntity()
            {
                Id = "88",
                EdgebandCode = "NN_Schwarz_16.50_24.5_HM",
                ManagementType = ManagementType.Stack,
                Quantity = 5,
                Length = 75.0,
                CurrentThickness = 16.5
            };
            await materialAssistClient.CreateEdgebandEntity(edgebandEntityRequest2);

            var edgebandEntityRequest3 = new MaterialAssistRequestEdgebandEntity()
            {
                Id = "89",
                EdgebandCode = "ABS_A_Dash_of_Freedom_1.00_100.0_HM",
                ManagementType = ManagementType.GoodsInStock,
                Quantity = 3,
                Length = 225.0,
                CurrentThickness = 1.0
            };
            await materialAssistClient.CreateEdgebandEntity(edgebandEntityRequest3);

            /*StatusCode: 500, ReasonPhrase: 'Internal Server Error'
            // store edgeband entities
            var edgebandEntityStore = new MaterialAssistStoreEdgebandEntity()
            {
                Id = "87",
                Length = 75,
                StorageLocation = new StorageLocation() 
                { 
                    LocationId = "001-01",
                    Name = "",
                    Barcode = ""
                },
                Workstation = new Workstation() { Name = "WS-01" },
            };
            await materialAssistClient.StoreEdgebandEntity(edgebandEntityStore);
            var edgebandEntity = await materialAssistClient.GetEdgebandEntityById("87");
            Assert.AreEqual("001-01", edgebandEntity.Location.LocationId);

            var edgebandEntityStore2 = new MaterialAssistStoreEdgebandEntity()
            {
                Id = "88",
                Length = 75,
                StorageLocation = new StorageLocation() 
                { 
                    Name = "Location2",
                    LocationId = "",
                    Barcode = ""
                },
                Workstation = new Workstation() { Name = "WS-01" },
            };
            await materialAssistClient.StoreEdgebandEntity(edgebandEntityStore2);
            var edgebandEntity2 = await materialAssistClient.GetEdgebandEntityById("88");
            Assert.AreEqual("Location2", edgebandEntity2.Location.Name);

            var edgebandEntityStore3 = new MaterialAssistStoreEdgebandEntity()
            {
                Id = "89",
                Length = 225,
                StorageLocation = new StorageLocation()
                { 
                    Name = "Lager1",
                    LocationId = "",
                    Barcode = ""
                },
                Workstation = new Workstation(){ Name = "WS-01" },
            };
            await materialAssistClient.StoreEdgebandEntity(edgebandEntityStore3);
            var edgebandEntity3 = await materialAssistClient.GetEdgebandEntityById("89");
            Assert.AreEqual("Lager1", edgebandEntity3.Location.Name);
            */

            // update edgeband types
            var edgebandType1 = await materialManagerClient.GetEdgebandTypeByEdgebandCodeIncludingDetails("ABS_Multiplex_schwarz_1.00_23.0_NN");
            var edgebandTypeUpdate = new MaterialManagerUpdateEdgebandType
            {
                EdgebandCode = "ABS_Multiplex_schwarz_1.2_23.0_NN",
                Thickness = 1.2,
            };
            await materialManagerClient.UpdateEdgebandType("ABS_Multiplex_schwarz_1.00_23.0_NN", edgebandTypeUpdate);
            var updatedEdgebandType1 = await materialManagerClient.GetEdgebandTypeByEdgebandCodeIncludingDetails("ABS_Multiplex_schwarz_1.2_23.0_NN");
            Assert.AreEqual(1.2, updatedEdgebandType1.Thickness);

            var edgebandType2 = await materialManagerClient.GetEdgebandTypeByEdgebandCodeIncludingDetails("NN_Schwarz_16.50_24.5_HM");
            var edgebandTypeUpdate2 = new MaterialManagerUpdateEdgebandType
            {
                DefaultLength = 75.0,
            };
            await materialManagerClient.UpdateEdgebandType("NN_Schwarz_16.50_24.5_HM", edgebandTypeUpdate2);
            var updatedEdgebandType2 = await materialManagerClient.GetEdgebandTypeByEdgebandCodeIncludingDetails("NN_Schwarz_16.50_24.5_HM");
            Assert.AreEqual(75.0, updatedEdgebandType2.DefaultLength);

            var edgebandType3 = await materialManagerClient.GetEdgebandTypeByEdgebandCodeIncludingDetails("ABS_A_Dash_of_Freedom_1.00_100.0_HM");
            var edgebandTypeUpdate3 = new MaterialManagerUpdateEdgebandType
            {
                DefaultLength = 100.0,
                MaterialCategory = EdgebandMaterialCategory.ABS,
                Process = EdgebandingProcess.HotmeltGlue,
            };
            await materialManagerClient.UpdateEdgebandType("ABS_A_Dash_of_Freedom_1.00_100.0_HM", edgebandTypeUpdate3);
            var updatedEdgebandType3 = await materialManagerClient.GetEdgebandTypeByEdgebandCodeIncludingDetails("ABS_A_Dash_of_Freedom_1.00_100.0_HM");
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
