using System.ComponentModel.DataAnnotations;

using HomagConnect.Base.Contracts;
using HomagConnect.Base.Contracts.AdditionalData;
using HomagConnect.MaterialAssist.Client;
using HomagConnect.MaterialAssist.Contracts.Request;
using HomagConnect.MaterialAssist.Contracts.Storage;
using HomagConnect.MaterialAssist.Contracts.Update;
using HomagConnect.MaterialManager.Contracts.Request;

namespace HomagConnect.MaterialAssist.Tests.Constraints;

[TestClass]
public class MaterialMutationValidationTests
{
    [TestMethod]
    public async Task BoardQuantity_AboveFormerLimitButWithinCloudRange_IsSubmitted()
    {
        using var handler = new MaterialValidationTestSupport();
        using var http = handler.CreateClient();
        var client = new MaterialAssistClientBoards(http);
        await client.CreateBoardEntity(new MaterialAssistRequestBoardEntity { Id = "id", BoardCode = "board", Quantity = 9999 });
        Assert.AreEqual(1, handler.RequestCount);
    }

    [TestMethod]
    public async Task BoardUpdate_RejectsValueAboveCloudMaximum()
    {
        using var handler = new MaterialValidationTestSupport();
        using var http = handler.CreateClient();
        var client = new MaterialAssistClientBoards(http);
        await Assert.ThrowsExactlyAsync<ValidationException>(() =>
            client.UpdateBoardEntity("id", new MaterialAssistUpdateBoardEntity { Length = 19999.95 }));
        Assert.AreEqual(0, handler.RequestCount);
    }

    [TestMethod]
    public async Task EdgebandCreateAndUpdate_PreserveCloudBoundariesAndOptionalFields()
    {
        using var handler = new MaterialValidationTestSupport();
        using var http = handler.CreateClient();
        var edges = new MaterialAssistClientEdgebands(http);
        var request = new MaterialAssistRequestEdgebandEntity
        {
            Id = "id", EdgebandCode = "edge", Quantity = 10000, Length = 0, CurrentThickness = 0.01
        };
        await Assert.ThrowsExactlyAsync<ValidationException>(() => edges.CreateEdgebandEntity(request));
        await Assert.ThrowsExactlyAsync<ValidationException>(() => edges.UpdateEdgebandEntity("id",
            new MaterialAssistUpdateEdgebandEntity { CurrentThickness = 100 }));
        Assert.AreEqual(0, handler.RequestCount);
        request.Quantity = 9999;
        await edges.CreateEdgebandEntity(request);
        await edges.UpdateEdgebandEntity("id", new MaterialAssistUpdateEdgebandEntity { Comments = string.Empty });
        Assert.AreEqual(2, handler.RequestCount);
    }

    [TestMethod]
    public async Task OffcutAndInheritedTemplate_CheckDimensionsBeforeSending()
    {
        using var handler = new MaterialValidationTestSupport();
        using var http = handler.CreateClient();
        var boards = new MaterialAssistClientBoards(http);
        var offcut = new MaterialAssistRequestOffcutEntity { Id = "id", BoardCode = "board", Quantity = 1, Length = 20000, Width = 100 };
        await Assert.ThrowsExactlyAsync<ValidationException>(() => boards.CreateOffcutEntity(offcut));
        var template = new MaterialAssistRequestTemplateEntity { Id = "id", BoardCode = "board", Quantity = 1, Length = 100, Width = 20000 };
        await Assert.ThrowsExactlyAsync<ValidationException>(() => boards.CreateTemplateEntity(template));
        Assert.AreEqual(0, handler.RequestCount);
        offcut.Length = 0.1;
        template.Width = 19999.9;
        await boards.CreateOffcutEntity(offcut);
        await boards.CreateTemplateEntity(template);
        Assert.AreEqual(2, handler.RequestCount);
    }

    [TestMethod]
    public async Task StoreBoard_ChecksDimensionsWithoutLosingOperationalGuards()
    {
        using var handler = new MaterialValidationTestSupport();
        using var http = handler.CreateClient();
        var client = new MaterialAssistClientBoards(http);
        await Assert.ThrowsExactlyAsync<ArgumentException>(() =>
            client.StoreBoardEntity(new MaterialAssistStoreBoardEntity { Id = "id" }));
        await Assert.ThrowsExactlyAsync<ValidationException>(() =>
            client.StoreBoardEntity(new MaterialAssistStoreBoardEntity
            {
                Id = "id", Length = 20000, StorageLocation = new StorageLocation(), Workstation = new Workstation()
            }));
        Assert.AreEqual(0, handler.RequestCount);
    }

    [TestMethod]
    public async Task StoreEdgeband_ChecksLengthBeforeSending()
    {
        using var handler = new MaterialValidationTestSupport();
        using var http = handler.CreateClient();
        var client = new MaterialAssistClientEdgebands(http);
        await Assert.ThrowsExactlyAsync<ValidationException>(() =>
            client.StoreEdgebandEntity(new MaterialAssistStoreEdgebandEntity
            {
                Id = "id", Length = 2001, StorageLocation = new StorageLocation(), Workstation = new Workstation()
            }));
        Assert.AreEqual(0, handler.RequestCount);
    }

    [TestMethod]
    public async Task TypeCreationThroughMaterialAssist_UsesTheSameAnnotations()
    {
        using var handler = new MaterialValidationTestSupport();
        using var http = handler.CreateClient();
        var boards = new MaterialAssistClientBoards(http);
        var edges = new MaterialAssistClientEdgebands(http);
        await Assert.ThrowsExactlyAsync<ValidationException>(() => boards.CreateBoardType(
            new MaterialManagerRequestBoardType { BoardCode = "board", MaterialCode = "material", Length = 200, Width = 100, Thickness = 1000 }));
        await Assert.ThrowsExactlyAsync<ValidationException>(() => edges.CreateEdgebandType(
            new MaterialManagerRequestEdgebandType { EdgebandCode = "edge", DefaultLength = 2001, Height = 20, Thickness = 1 }));
        Assert.AreEqual(0, handler.RequestCount);
    }

    [TestMethod]
    [DataRow(0.0)]
    [DataRow(1000.0)]
    public async Task TypeCreation_PreservesOptionalTextureDimensions(double dimension)
    {
        using var handler = new MaterialValidationTestSupport();
        using var http = handler.CreateClient();
        var boards = new MaterialAssistClientBoards(http);
        var edges = new MaterialAssistClientEdgebands(http);
        var texture = new AdditionalDataTexture { Name = "texture.png" };
        if (dimension != 0)
        {
            texture.Length = dimension;
            texture.Width = dimension;
        }

        await boards.CreateBoardType(new MaterialManagerRequestBoardType
        {
            BoardCode = "board", MaterialCode = "material", Length = 200, Width = 100, Thickness = 19,
            AdditionalData = [texture]
        });
        await edges.CreateEdgebandType(new MaterialManagerRequestEdgebandType
        {
            EdgebandCode = "edge", DefaultLength = 100, Height = 20, Thickness = 1,
            AdditionalData = [texture]
        });

        Assert.AreEqual(2, handler.RequestCount);
        Assert.AreEqual(dimension, texture.Length);
        Assert.AreEqual(dimension, texture.Width);
    }

    [TestMethod]
    public async Task ValidStorePayloads_AreSubmittedWithoutMutation()
    {
        using var handler = new MaterialValidationTestSupport();
        using var http = handler.CreateClient();
        var boards = new MaterialAssistClientBoards(http);
        var edges = new MaterialAssistClientEdgebands(http);
        var board = new MaterialAssistStoreBoardEntity
        {
            Id = "id", Width = 19999.9, Length = 0.1, StorageLocation = new StorageLocation(), Workstation = new Workstation()
        };
        await boards.StoreBoardEntity(board);
        await edges.StoreEdgebandEntity(new MaterialAssistStoreEdgebandEntity
        {
            Id = "id", Length = 0, StorageLocation = new StorageLocation(), Workstation = new Workstation()
        });
        Assert.AreEqual(2, handler.RequestCount);
        Assert.AreEqual(0.1, board.Length);
    }
}