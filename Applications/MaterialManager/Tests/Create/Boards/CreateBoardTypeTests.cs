using System.ComponentModel.DataAnnotations;

using FluentAssertions;

using HomagConnect.MaterialManager.Client;
using HomagConnect.MaterialManager.Contracts.Request;
using HomagConnect.MaterialManager.Samples.Create.Boards;

namespace HomagConnect.MaterialManager.Tests.Create.Boards
{
    /// <summary />
    [TestClass]
    [TestCategory("MaterialManager")]
    [TestCategory("MaterialManager.Boards")]
    public class CreateBoardTypeTests : MaterialManagerTestBase
    {
        /// <summary />
        [TestMethod]
        public async Task BoardsCreateBoardType()
        {
            var materialManagerClient = GetMaterialManagerClient();
            var materialCode = "Test_Data_HPL_F274_9_12.5";
            var boardCode = "Test_Data_HPL_F274_9_12.5_4100.0_650.0";
            await CreateBoardTypeSamples.Boards_CreateBoardType(materialManagerClient.Material.Boards, materialCode, boardCode);
        }

        [TestMethod]
        public async Task BoardsCreateBoardType_AdditionalData()
        {
            var materialManagerClient = GetMaterialManagerClient();
            var materialCode = "Test_Data_HPL_F274_9_12.0";
            var boardCode = "Test_Data_HPL_F274_9_12.0_4100.0_650.01";
            await CreateBoardTypeSamples.Boards_CreateBoardType_AdditionalData(materialManagerClient.Material.Boards, materialCode, boardCode);
        }

        /// <summary />
        [TestMethod]
        [DataRow(null, "BoardCode", 1000, 600, 19)] // MaterialCode not set
        [DataRow("MaterialCode", null, 1000, 600, 19)] // BoardCode not set
        [DataRow("MaterialCode", "BoardCode", null, 600, 19)] // Length not set
        [DataRow("MaterialCode", "BoardCode", 1000, null, 19)] // Width not set
        [DataRow("MaterialCode", "BoardCode", 1000, 600, null)] // Thickness not set
        public async Task BoardTypeCreation_RequiredPropertiesMissing_ThrowsException(string materialCode, string boardCode, double length, double width,
            double thickness)
        {
            var client = new HttpClient();
            var boardsClient = new MaterialManagerClientMaterialBoards(client);

            var requestBoardType = CreateBoardTypeRequest(materialCode, boardCode, length, width, thickness);

            var act = async () => await boardsClient.CreateBoardType(requestBoardType);

            await act.Should().ThrowAsync<ValidationException>();
        }

        [ClassCleanup]
        public static async Task Cleanup()
        {
            var classInstance = new CreateBoardTypeTests();
            var materialManagerClient = classInstance.GetMaterialManagerClient().Material.Boards;
            await materialManagerClient.DeleteBoardType("Test_Data_HPL_F274_9_12.5_4100.0_650.0");
            await materialManagerClient.DeleteBoardType("Test_Data_HPL_F274_9_12.0_4100.0_650.01");
        }

        private static MaterialManagerRequestBoardType CreateBoardTypeRequest(string materialCode, string boardCode, double length, double width,
            double thickness)
        {
            var boardType = new MaterialManagerRequestBoardType();
            boardType.MaterialCode = materialCode;
            boardType.BoardCode = boardCode;
            boardType.Length = length;
            boardType.Width = width;
            boardType.Thickness = thickness;
            return boardType;
        }
    }
}

