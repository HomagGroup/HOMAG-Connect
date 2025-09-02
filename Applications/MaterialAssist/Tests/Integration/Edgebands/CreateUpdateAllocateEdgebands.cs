using HomagConnect.Base.Contracts;
using HomagConnect.Base.Contracts.Enumerations;
using HomagConnect.MaterialAssist.Contracts.Request;
using HomagConnect.MaterialAssist.Contracts.Storage;
using HomagConnect.MaterialManager.Contracts.Material.Base;
using HomagConnect.MaterialManager.Contracts.Material.Boards.Enumerations;
using HomagConnect.MaterialManager.Contracts.Material.Edgebands.Enumerations;
using HomagConnect.MaterialManager.Contracts.Request;
using HomagConnect.MaterialManager.Contracts.Update;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomagConnect.MaterialAssist.Tests.DeploymentTest
{
    [TestClass]
    [TestCategory("MaterialAssist")]
    [TestCategory("MaterialAssist.Edgebands")]

    public class CreateUpdateAllocateEdgebands : MaterialAssistTestBase
    {
        [TestMethod]
        public async Task CreateEdgebandTypes()
        {
            var materialManagerClient = GetMaterialManagerClient().Material.Edgebands;

            var edgebandTypeRequest = new MaterialManagerRequestEdgebandType
            {
                EdgebandCode = "ABS_Multiplex schwarz_1.00_23.0_NN",
                Height = 23.0,
                Thickness = 1.0,
                DefaultLength = 75.0,
                MaterialCategory = EdgebandMaterialCategory.ABS,
                Process = EdgebandingProcess.Other,
            };
            await materialManagerClient.CreateEdgebandType(edgebandTypeRequest);
            Assert.IsNotNull(await materialManagerClient.GetEdgebandTypeByEdgebandCode(edgebandTypeRequest.EdgebandCode));

            var edgebandTypeRequest2 = new MaterialManagerRequestEdgebandType
            {
                EdgebandCode = "NN_Schwarz_16.50_24.5_HM",
                Height = 24.5,
                Thickness = 16.5,
                DefaultLength = 0.0,
                MaterialCategory = EdgebandMaterialCategory.Others,
                Process = EdgebandingProcess.HotmeltGlue,
            };
            await materialManagerClient.CreateEdgebandType(edgebandTypeRequest2);
            Assert.IsNotNull(await materialManagerClient.GetEdgebandTypeByEdgebandCode(edgebandTypeRequest2.EdgebandCode));

            var edgebandTypeRequest3 = new MaterialManagerRequestEdgebandType
            {
                EdgebandCode = "ABS_A_Dash_of_Freedom_1.00_100.0_HM",
                Height = 100.0,
                Thickness = 1.0,
                DefaultLength = 225.0,
                MaterialCategory = EdgebandMaterialCategory.PVC,
                Process = EdgebandingProcess.Other,
            };
            await materialManagerClient.CreateEdgebandType(edgebandTypeRequest3);
            Assert.IsNotNull(await materialManagerClient.GetEdgebandTypeByEdgebandCode(edgebandTypeRequest3.EdgebandCode));
        }

        public async Task CreateEdgebandEntities()
        {
            var materialAssistClient = GetMaterialAssistClient().Edgebands;
            var edgebandEntityRequest = new MaterialAssistRequestEdgebandEntity()
            {
                Id = "18",
                EdgebandCode = "ABS_Multiplex schwarz_1.00_23.0_NN",
                ManagementType = ManagementType.Single,
                Quantity = 2,
                Length = 75.0,
                CurrentThickness = 1.0
            };
            await materialAssistClient.CreateEdgebandEntity(edgebandEntityRequest);
            Assert.IsNotNull(await materialAssistClient.GetEdgebandEntityById(edgebandEntityRequest.Id));

            var edgebandEntityRequest2 = new MaterialAssistRequestEdgebandEntity()
            {
                Id = "59",
                EdgebandCode = "NN_Schwarz_16.50_24.5_HM",
                ManagementType = ManagementType.Stack,
                Quantity = 5,
                Length = 75.0,
                CurrentThickness = 16.5
            };
            await materialAssistClient.CreateEdgebandEntity(edgebandEntityRequest2);
            Assert.IsNotNull(await materialAssistClient.GetEdgebandEntityById(edgebandEntityRequest2.Id));

            var edgebandEntityRequest3 = new MaterialAssistRequestEdgebandEntity()
            {
                Id = "63",
                EdgebandCode = "ABS_A_Dash_of_Freedom_1.00_100.0_HM",
                ManagementType = ManagementType.GoodsInStock,
                Quantity = 3,
                Length = 225.0,
                CurrentThickness = 1.0
            };
            await materialAssistClient.CreateEdgebandEntity(edgebandEntityRequest3);
            Assert.IsNotNull(await materialAssistClient.GetEdgebandEntityById(edgebandEntityRequest3.Id));
        }

        public async Task UpdateStorageLocationEdgebands()
        {
            var materialAssistClient = GetMaterialAssistClient().Edgebands;
            var edgebandEntityStore = new MaterialAssistStoreEdgebandEntity()
            {
                Id = "18",
                Length = 75,
                StorageLocation = new StorageLocation()
                {
                    LocationId = "001-01",
                },
            };
            await materialAssistClient.StoreEdgebandEntity(edgebandEntityStore);
            var edgebandEntity = await materialAssistClient.GetEdgebandEntityById("18");
            Assert.IsNotNull(edgebandEntity.Location);
            Assert.AreEqual("001-01", edgebandEntity.Location.LocationId);

            var edgebandEntityStore2 = new MaterialAssistStoreEdgebandEntity()
            {
                Id = "59",
                Length = 75,
                StorageLocation = new StorageLocation()
                {
                    Name = "Location2",
                },
            };
            await materialAssistClient.StoreEdgebandEntity(edgebandEntityStore2);
            var edgebandEntity2 = await materialAssistClient.GetEdgebandEntityById("59");
            Assert.IsNotNull(edgebandEntity2.Location);
            Assert.AreEqual("Location2", edgebandEntity2.Location.Name);

            var edgebandEntityStore3 = new MaterialAssistStoreEdgebandEntity()
            {
                Id = "63",
                Length = 225,
                StorageLocation = new StorageLocation()
                {
                    Name = "Lager1",
                },
            };
            await materialAssistClient.StoreEdgebandEntity(edgebandEntityStore3);
            var edgebandEntity3 = await materialAssistClient.GetEdgebandEntityById("63");
            Assert.IsNotNull(edgebandEntity3.Location);
            Assert.AreEqual("Lager1", edgebandEntity3.Location.Name);
        }

        public async Task GetAndUpdateEdgebandTypes()
        {
            var materialManagerClient = GetMaterialManagerClient().Material.Edgebands;
            var edgebandType1 = await materialManagerClient.GetEdgebandTypeByEdgebandCodeIncludingDetails("ABS_Multiplex schwarz_1.00_23.0_NN");
            var edgebandTypeUpdate = new MaterialManagerUpdateEdgebandType
            {
                EdgebandCode = "ABS_Multiplex schwarz_1.2_23.0_NN",
                Thickness = 1.2,
            };
            await materialManagerClient.UpdateEdgebandType("ABS_Multiplex schwarz_1.00_23.0_NN", edgebandTypeUpdate);
            var updatedEdgebandType1 = await materialManagerClient.GetEdgebandTypeByEdgebandCodeIncludingDetails("ABS_Multiplex schwarz_1.2_23.0_NN");
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
                MaterialCategory = EdgebandMaterialCategory.ABS,
                Process = EdgebandingProcess.HotmeltGlue,
            };
            await materialManagerClient.UpdateEdgebandType("ABS_A_Dash_of_Freedom_1.00_100.0_HM", edgebandTypeUpdate3);
            var updatedEdgebandType3 = await materialManagerClient.GetEdgebandTypeByEdgebandCodeIncludingDetails("ABS_A_Dash_of_Freedom_1.00_100.0_HM");
            Assert.AreEqual(EdgebandMaterialCategory.ABS, updatedEdgebandType3.MaterialCategory);
            Assert.AreEqual(EdgebandingProcess.HotmeltGlue, updatedEdgebandType3.Process);
        }

        //allocation for edgebands not implemented yet

        [ClassCleanup]
        public async Task Cleanup()
        {
            var materialManagerClient = GetMaterialManagerClient().Material.Edgebands;
            var materialAssistClient = GetMaterialAssistClient().Edgebands;

            //edgeband entities
            await materialAssistClient.DeleteEdgebandEntity("18");
            Assert.IsNull(await materialAssistClient.GetEdgebandEntityById("18"));

            await materialAssistClient.DeleteEdgebandEntity("59");
            Assert.IsNull(await materialAssistClient.GetEdgebandEntityById("59"));

            await materialAssistClient.DeleteEdgebandEntity("63");
            Assert.IsNull(await materialAssistClient.GetEdgebandEntityById("63"));

            //edgeband types
            await materialManagerClient.DeleteEdgebandType("ABS_Multiplex schwarz_1.00_23.0_NN");
            Assert.IsNull(await materialManagerClient.GetEdgebandTypeByEdgebandCode("ABS_Multiplex schwarz_1.00_23.0_NN"));

            await materialManagerClient.DeleteEdgebandType("NN_Schwarz_16.50_24.5_HM");
            Assert.IsNull(await materialManagerClient.GetEdgebandTypeByEdgebandCode("NN_Schwarz_16.50_24.5_HM"));

            await materialManagerClient.DeleteEdgebandType("ABS_A_Dash_of_Freedom_1.00_100.0_HM");
            Assert.IsNull(await materialManagerClient.GetEdgebandTypeByEdgebandCode("ABS_A_Dash_of_Freedom_1.00_100.0_HM"));
        }
    }
}
