using System.ComponentModel.DataAnnotations;

using HomagConnect.Base.Contracts;
using HomagConnect.Base.Contracts.AdditionalData;
using HomagConnect.MaterialAssist.Client;
using HomagConnect.MaterialAssist.Contracts.Request;
using HomagConnect.MaterialAssist.Contracts.Storage;
using HomagConnect.MaterialAssist.Contracts.Update;
using HomagConnect.MaterialManager.Contracts.Request;

using Newtonsoft.Json.Linq;

using Shouldly;

namespace HomagConnect.MaterialAssist.Tests.Constraints;

[TestClass]
public class MaterialMutationValidationTests
{
    [TestMethod]
    [DynamicData(nameof(BoardDimensionChanges))]
    public async Task BoardUpdateAndStore_EnforceInterfaceDimensionsBeforeTransport(bool store, string member,
        double? value, bool expectedValid)
    {
        using var handler = new MaterialValidationTestSupport();
        using var http = handler.CreateClient();
        var client = new MaterialAssistClientBoards(http);
        object request = store
            ? new MaterialAssistStoreBoardEntity
            {
                Id = "id", StorageLocation = new StorageLocation(), Workstation = new Workstation()
            }
            : new MaterialAssistUpdateBoardEntity();
        request.GetType().GetProperty(member)!.SetValue(request, value);
        Func<Task> act = store
            ? () => client.StoreBoardEntity((MaterialAssistStoreBoardEntity)request)
            : () => client.UpdateBoardEntity("id", (MaterialAssistUpdateBoardEntity)request);

        if (expectedValid)
        {
            await act();
            handler.RequestCount.ShouldBe(1);
            var wireName = member == "Length" ? "length" : "width";
            var payload = JObject.Parse(handler.Payload!);
            if (value.HasValue)
            {
                payload[wireName]!.Value<double>().ShouldBe(value.Value);
            }
            else
            {
                payload[wireName].ShouldBeNull();
            }
        }
        else
        {
            await Should.ThrowAsync<ValidationException>(act);
            handler.RequestCount.ShouldBe(0);
        }
    }

    public static IEnumerable<object?[]> BoardDimensionChanges()
    {
        foreach (var store in new[] { false, true })
        foreach (var member in new[] { "Length", "Width" })
        foreach (var value in new double?[] { null, 0.09, 0.1, 19999.9, 20000 })
        {
            yield return [store, member, value, value == null || value >= 0.1 && value <= 19999.9];
        }
    }

    [TestMethod]
    public async Task BoardQuantity_AboveFormerLimitButWithinCloudRange_IsSubmitted()
    {
        using var handler = new MaterialValidationTestSupport();
        using var http = handler.CreateClient();
        var client = new MaterialAssistClientBoards(http);
        await client.CreateBoardEntity(new MaterialAssistRequestBoardEntity { Id = "id", BoardCode = "board", Quantity = 9999 });
        handler.RequestCount.ShouldBe(1);
    }

    [TestMethod]
    public async Task BoardUpdate_RejectsValueAboveCloudMaximum()
    {
        using var handler = new MaterialValidationTestSupport();
        using var http = handler.CreateClient();
        var client = new MaterialAssistClientBoards(http);
        (await Should.ThrowAsync<ValidationException>(() =>
            client.UpdateBoardEntity("id", new MaterialAssistUpdateBoardEntity { Length = 19999.95 }))).ShouldBeOfType<ValidationException>();
        handler.RequestCount.ShouldBe(0);
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
        (await Should.ThrowAsync<ValidationException>(() => edges.CreateEdgebandEntity(request))).ShouldBeOfType<ValidationException>();
        (await Should.ThrowAsync<ValidationException>(() => edges.UpdateEdgebandEntity("id",
            new MaterialAssistUpdateEdgebandEntity { CurrentThickness = 100 }))).ShouldBeOfType<ValidationException>();
        handler.RequestCount.ShouldBe(0);
        request.Quantity = 9999;
        await edges.CreateEdgebandEntity(request);
        await edges.UpdateEdgebandEntity("id", new MaterialAssistUpdateEdgebandEntity { Comments = string.Empty });
        handler.RequestCount.ShouldBe(2);
    }

    [TestMethod]
    public async Task OffcutAndInheritedTemplate_CheckDimensionsBeforeSending()
    {
        using var handler = new MaterialValidationTestSupport();
        using var http = handler.CreateClient();
        var boards = new MaterialAssistClientBoards(http);
        var offcut = new MaterialAssistRequestOffcutEntity { Id = "id", BoardCode = "board", Quantity = 1, Length = 20000, Width = 100 };
        (await Should.ThrowAsync<ValidationException>(() => boards.CreateOffcutEntity(offcut))).ShouldBeOfType<ValidationException>();
        var template = new MaterialAssistRequestTemplateEntity { Id = "id", BoardCode = "board", Quantity = 1, Length = 100, Width = 20000 };
        (await Should.ThrowAsync<ValidationException>(() => boards.CreateTemplateEntity(template))).ShouldBeOfType<ValidationException>();
        handler.RequestCount.ShouldBe(0);
        offcut.Length = 0.1;
        template.Width = 19999.9;
        await boards.CreateOffcutEntity(offcut);
        await boards.CreateTemplateEntity(template);
        handler.RequestCount.ShouldBe(2);
    }

    [TestMethod]
    public async Task StoreBoard_ChecksDimensionsWithoutLosingOperationalGuards()
    {
        using var handler = new MaterialValidationTestSupport();
        using var http = handler.CreateClient();
        var client = new MaterialAssistClientBoards(http);
        (await Should.ThrowAsync<ArgumentException>(() =>
            client.StoreBoardEntity(new MaterialAssistStoreBoardEntity { Id = "id" }))).ShouldBeOfType<ArgumentException>();
        (await Should.ThrowAsync<ValidationException>(() =>
            client.StoreBoardEntity(new MaterialAssistStoreBoardEntity
            {
                Id = "id", Length = 20000, StorageLocation = new StorageLocation(), Workstation = new Workstation()
            }))).ShouldBeOfType<ValidationException>();
        handler.RequestCount.ShouldBe(0);
    }

    [TestMethod]
    public async Task StoreEdgeband_ChecksLengthBeforeSending()
    {
        using var handler = new MaterialValidationTestSupport();
        using var http = handler.CreateClient();
        var client = new MaterialAssistClientEdgebands(http);
        (await Should.ThrowAsync<ValidationException>(() =>
            client.StoreEdgebandEntity(new MaterialAssistStoreEdgebandEntity
            {
                Id = "id", Length = 2001, StorageLocation = new StorageLocation(), Workstation = new Workstation()
            }))).ShouldBeOfType<ValidationException>();
        handler.RequestCount.ShouldBe(0);
    }

    [TestMethod]
    public async Task TypeCreationThroughMaterialAssist_UsesTheSameAnnotations()
    {
        using var handler = new MaterialValidationTestSupport();
        using var http = handler.CreateClient();
        var boards = new MaterialAssistClientBoards(http);
        var edges = new MaterialAssistClientEdgebands(http);
        (await Should.ThrowAsync<ValidationException>(() => boards.CreateBoardType(
            new MaterialManagerRequestBoardType { BoardCode = "board", MaterialCode = "material", Length = 200, Width = 100, Thickness = 1000 }))).ShouldBeOfType<ValidationException>();
        (await Should.ThrowAsync<ValidationException>(() => edges.CreateEdgebandType(
            new MaterialManagerRequestEdgebandType { EdgebandCode = "edge", DefaultLength = 2001, Height = 20, Thickness = 1 }))).ShouldBeOfType<ValidationException>();
        handler.RequestCount.ShouldBe(0);
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

        handler.RequestCount.ShouldBe(2);
        texture.Length.ShouldBe(dimension);
        texture.Width.ShouldBe(dimension);
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
        handler.RequestCount.ShouldBe(2);
        board.Length.ShouldBe(0.1);
    }
}