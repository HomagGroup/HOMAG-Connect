using Shouldly;
using System.ComponentModel.DataAnnotations;

using HomagConnect.MaterialManager.Client;
using HomagConnect.MaterialManager.Contracts.Request;
using HomagConnect.MaterialManager.Samples.Create.Boards;


namespace HomagConnect.MaterialManager.Tests.Create.Boards
{
    /// <summary />
    [TestClass]
    [TestCategory("DeploymentTests.MaterialManager")]
    [TestCategory("DeploymentTests.MaterialManager.Boards")]
    public class CreateBoardTypeTests : MaterialManagerTestBase
    {
        private const string _BoardCode = "MDF_H3171_12_11.6_2800.0_1310.0";
        private const string _BoardCodeWithAdditionalData = "HPL_F274_9_12.0_4100.0_650.01";
        private const string _BoardCodeWithDecorId = "MDF_H3171_12_19.0_2800.0_1310.0";

        private MaterialManagerClient _MaterialManagerClient = null!;

        /// <summary>
        /// Initializes the test class.
        /// </summary>
        [TestInitialize]
        public void Initialize()
        {
            _MaterialManagerClient = GetMaterialManagerClient();
        }

        /// <summary />
        [TestMethod]
        public async Task BoardsCreateBoardType()
        {
            var materialCode = "MDF_H3171_12_11.6";
            await CreateBoardTypeSamples.Boards_CreateBoardType(_MaterialManagerClient.Material.Boards, materialCode, _BoardCode);
        }

        /// <summary />
        [TestMethod]
        public async Task BoardsCreateBoardType_DecorId()
        {
            const string materialCode = "MDF_H3171_12_19.0";
            const string decorId = "egger:u599_pm";

            var boardType = await CreateBoardTypeSamples.Boards_CreateBoardType_DecorId(
                _MaterialManagerClient.Material.Boards, materialCode, _BoardCodeWithDecorId, decorId);

            boardType.ShouldNotBeNull();
            boardType.DecorId.ShouldBe(decorId, "because the decor id passed in the request must be persisted on the board type");
            boardType.Thumbnail.ShouldNotBeNull();
        }

        /// <summary>
        /// The example shows how to create a board type with additional data (e.g., a picture).
        /// </summary>
        [TestMethod]
        public async Task BoardsCreateBoardType_AdditionalData()
        {
            var materialCode = "HPL_F274_9_12.0";
            await CreateBoardTypeSamples.Boards_CreateBoardType_AdditionalData(_MaterialManagerClient.Material.Boards, materialCode, _BoardCodeWithAdditionalData);
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

            await Should.ThrowAsync<ValidationException>(act);
        }



        /// <summary>
        /// Cleanup method to delete the created board types after the tests have run.
        /// </summary>
        [TestCleanup]
        public async Task Cleanup()
        {
            await DeleteBoardType(_BoardCode);
            await DeleteBoardType(_BoardCodeWithAdditionalData);
            await DeleteBoardType(_BoardCodeWithDecorId);
        }

        private async Task DeleteBoardType(string boardCode)
        {
            try
            {
                await _MaterialManagerClient.Material.Boards.DeleteBoardType(boardCode);
            }
            catch (Exception _)
            {
                // ignored
            }
        }

        private static MaterialManagerRequestBoardType CreateBoardTypeRequest(string materialCode, string boardCode, double length, double width,
            double thickness)
        {
            var boardType = new MaterialManagerRequestBoardType
            {
                MaterialCode = materialCode,
                BoardCode = boardCode,
                Length = length,
                Width = width,
                Thickness = thickness
            };
            return boardType;
        }
    }
}

