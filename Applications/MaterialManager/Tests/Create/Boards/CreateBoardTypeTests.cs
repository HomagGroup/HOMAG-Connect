using FluentAssertions;
using HomagConnect.Base.Extensions;
using HomagConnect.MaterialManager.Client;
using HomagConnect.MaterialManager.Contracts.Material.Boards.Interfaces;
using HomagConnect.MaterialManager.Contracts.Request;
using HomagConnect.MaterialManager.Samples.Create.Boards;
using System.ComponentModel.DataAnnotations;
using HomagConnect.MaterialManager.Tests;
using HomagConnect.MaterialManager.Contracts.Material.Boards;
using HomagConnect.Base.Contracts.Exceptions;
using HomagConnect.Base.Contracts.Enumerations;
using HomagConnect.MaterialManager.Contracts.Material.Boards.Enumerations;

namespace HomagConnect.MaterialManager.Tests.Create.Boards;

/// <summary />
[TestClass]
public class CreateBoardTypeTests : MaterialManagerTestBase
{
    /// <summary />
    [TestMethod]
    [DataRow(null, "BoardCode", 1000, 600, 19)] // MaterialCode not set
    [DataRow("MaterialCode",null, 1000, 600, 19)] // BoardCode not set
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

    /// <summary /> 
   
    [TestMethod]
    public async Task BoardsCreateBoardType()
    {
        var boardsClient = GetMaterialManagerClient();
        await CreateBoardTypeSamples.Boards_CreateBoardType(boardsClient.Material.Boards);
    }
    
    [TestCleanup]
    public async Task Cleanup()
    {
        var boardsClient = GetMaterialManagerClient();
        await boardsClient.Material.Boards.DeleteBoardType("HPL_F274_9_12.0_4100.0_650.0");
    }
}