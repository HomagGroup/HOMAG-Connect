using System.ComponentModel.DataAnnotations;

using HomagConnect.Base.Client;
using HomagConnect.Base.Contracts;
using HomagConnect.Base.Contracts.AdditionalData;
using HomagConnect.MaterialManager.Client;
using HomagConnect.MaterialManager.Contracts.Material.Boards;
using HomagConnect.MaterialManager.Contracts.Material.Edgebands;
using HomagConnect.MaterialManager.Contracts.Request;
using HomagConnect.MaterialManager.Contracts.Update;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace HomagConnect.MaterialManager.Tests.Constraints;

[TestClass]
public class MaterialMutationValidationTests
{
    [TestMethod]
    public async Task AllocationMutations_CheckMappedValues()
    {
        using var handler = new MaterialValidationTestSupport();
        using var http = handler.CreateClient();
        var boards = new MaterialManagerClientMaterialBoards(http);
        var edges = new MaterialManagerClientMaterialEdgebands(http);
        await Assert.ThrowsExactlyAsync<ValidationException>(() =>
            boards.UpdateBoardTypeAllocation("allocation", new BoardTypeAllocationUpdate { Name = "allocation", Quantity = 0 }));
        await Assert.ThrowsExactlyAsync<ValidationException>(() =>
            edges.CreateEdgebandTypeAllocation(new EdgebandTypeAllocationRequest
            {
                EdgebandCode = "edge", Order = "order", CreatedBy = "user", Workstation = "station", AllocatedLength = 100000
            }));
        Assert.AreEqual(0, handler.RequestCount);
    }

    [TestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow("board")]
    public async Task BoardAllocationUpdate_AllowsOmittedEmptyOrSuppliedBoardCode(string? boardTypeCode)
    {
        using var handler = new MaterialValidationTestSupport { Response = "[]" };
        using var http = handler.CreateClient();
        var client = new MaterialManagerClientMaterialBoards(http);

        await client.UpdateBoardTypeAllocation("allocation", new BoardTypeAllocationUpdate
        {
            Name = "allocation", Quantity = 5, BoardTypeCode = boardTypeCode
        });

        Assert.AreEqual(1, handler.RequestCount);
        Assert.AreEqual(5, JObject.Parse(handler.Payload!)["quantity"]!.Value<int>());
        if (boardTypeCode != null)
            Assert.AreEqual(boardTypeCode, JObject.Parse(handler.Payload!)["boardTypeCode"]!.Value<string>());
    }

    [TestMethod]
    public async Task BoardAllocationUpdate_RejectsBoardCodeBeyondMaximumBeforeTransport()
    {
        using var handler = new MaterialValidationTestSupport();
        using var http = handler.CreateClient();
        var client = new MaterialManagerClientMaterialBoards(http);

        await Assert.ThrowsExactlyAsync<ValidationException>(() =>
            client.UpdateBoardTypeAllocation("allocation", new BoardTypeAllocationUpdate
            {
                Name = "allocation", BoardTypeCode = new string('x', 51)
            }));

        Assert.AreEqual(0, handler.RequestCount);
    }

    [TestMethod]
    public void BoardPatch_ValidatesOnlyTheSuppliedProperty()
    {
        var patch = PatchBuilder<BoardType>.For().Set(x => x.Thickness, 0.1);
        Assert.AreEqual(1, patch.Build().Count);
        Assert.ThrowsExactly<ValidationException>(() => patch.Set(x => x.Thickness, 1000));
        patch.Set(x => x.Comments, string.Empty);
    }

    [TestMethod]
    public void EdgebandPatch_UsesOnlySuppliedValuesAndAllowsClearing()
    {
        var patch = PatchBuilder<EdgebandType>.For().Set(x => x.Thickness, 0.01);
        patch.Set(x => x.Comments, null);
        Assert.ThrowsExactly<ValidationException>(() => patch.Set(x => x.Thickness, 100));
        Assert.AreEqual(2, patch.Build().Count);
    }

    [TestMethod]
    [DataRow(false)]
    [DataRow(true)]
    public async Task EdgebandUpdate_EnforcesRangesAndPreservesOmission(bool multipart)
    {
        using var handler = new MaterialValidationTestSupport();
        using var http = handler.CreateClient();
        var client = new MaterialManagerClientMaterialEdgebands(http);
        var invalid = new MaterialManagerUpdateEdgebandType { Thickness = 100 };
        await Assert.ThrowsExactlyAsync<ValidationException>(() => multipart
            ? client.UpdateEdgebandType("edge", invalid, [])
            : client.UpdateEdgebandType("edge", invalid));
        Assert.AreEqual(0, handler.RequestCount);
        var valid = new MaterialManagerUpdateEdgebandType { DefaultLength = 0, Thickness = 0.01 };
        if (multipart)
            await client.UpdateEdgebandType("edge", valid, []);
        else
            await client.UpdateEdgebandType("edge", valid);
        Assert.AreEqual(1, handler.RequestCount);
        var payload = JObject.Parse(handler.Payload!);
        Assert.AreEqual(0, payload["defaultLength"]!.Value<double>());
        Assert.IsNull(payload["height"]);
    }

    [TestMethod]
    public void FlatValidation_IsUnchangedAndOpenExtensionData_RemainsOpaque()
    {
        var request = MaterialValidationTestSupport.Board();
        request.AdditionalData =
        [
            new AdditionalDataTexture
            {
                Length = 1000, Width = 100,
                DownloadUri = new Uri("relative.png", UriKind.Relative),
                AdditionalProperties = new Dictionary<string, object> { ["opaque"] = new RequiredData() }
            }
        ];
        ValidationProbe.Flat(request);
    }

    [TestMethod]
    public void HistoricalGetData_IsNotValidatedDuringDeserialization()
    {
        var board = JsonConvert.DeserializeObject<BoardType>("{\"length\":20000,\"thickness\":0.01}");
        Assert.IsNotNull(board);
        Assert.AreEqual(20000, board.Length);
        Assert.AreEqual(0.01, board.Thickness);
    }

    [TestMethod]
    [DataRow(false)]
    [DataRow(true)]
    public async Task InvalidBoardCreate_IsBlockedBeforeTransport(bool multipart)
    {
        using var handler = new MaterialValidationTestSupport();
        using var http = handler.CreateClient();
        var client = new MaterialManagerClientMaterialBoards(http);
        var request = MaterialValidationTestSupport.Board();
        request.Thickness = 1000;
        await Assert.ThrowsExactlyAsync<ValidationException>(() => multipart
            ? client.CreateBoardType(request, [])
            : client.CreateBoardType(request));
        Assert.AreEqual(0, handler.RequestCount);
    }

    [TestMethod]
    [DataRow(false)]
    [DataRow(true)]
    public async Task InvalidBoardUpdate_IsBlockedBeforeTransport(bool multipart)
    {
        using var handler = new MaterialValidationTestSupport();
        using var http = handler.CreateClient();
        var client = new MaterialManagerClientMaterialBoards(http);
        var request = new MaterialManagerUpdateBoardType { Density = 0 };
        await Assert.ThrowsExactlyAsync<ValidationException>(() => multipart
            ? client.UpdateBoardType("board", request, [])
            : client.UpdateBoardType("board", request));
        Assert.AreEqual(0, handler.RequestCount);
    }

    [TestMethod]
    [DataRow(false)]
    [DataRow(true)]
    public async Task InvalidEdgebandCreate_IsBlockedBeforeTransport(bool multipart)
    {
        using var handler = new MaterialValidationTestSupport();
        using var http = handler.CreateClient();
        var client = new MaterialManagerClientMaterialEdgebands(http);
        var request = new MaterialManagerRequestEdgebandType
        {
            EdgebandCode = "edge", DefaultLength = 2001, Height = 20, Thickness = 1
        };
        await Assert.ThrowsExactlyAsync<ValidationException>(() => multipart
            ? client.CreateEdgebandType(request, [])
            : client.CreateEdgebandType(request));
        Assert.AreEqual(0, handler.RequestCount);
    }

    [TestMethod]
    public async Task InvalidImportReference_IsReportedWithoutValidatingUnmappedFields()
    {
        using var handler = new MaterialValidationTestSupport();
        using var http = handler.CreateClient();
        var client = new MaterialManagerClientMaterialBoards(http);
        var exception = await Assert.ThrowsExactlyAsync<ValidationException>(() => client.ImportInventory(new ImportInventoryRequest
        {
            Instances = [new BoardEntity { BoardType = new BoardType { BoardCode = new string('x', 51) }, Quantity = 0 }]
        }));
        StringAssert.Contains(exception.Message, "Instances[0].BoardType.BoardCode");
        Assert.AreEqual(0, handler.RequestCount);
    }

    [TestMethod]
    public async Task InvalidImport_ReportsIndexedPublicPath()
    {
        using var handler = new MaterialValidationTestSupport();
        using var http = handler.CreateClient();
        var client = new MaterialManagerClientMaterialBoards(http);
        var exception = await Assert.ThrowsExactlyAsync<ValidationException>(() => client.ImportInventory(new ImportInventoryRequest
        {
            Materials = [new BoardType { BoardCode = "board", Thickness = 1000 }]
        }));
        StringAssert.Contains(exception.Message, "Materials[0].Thickness");
        Assert.AreEqual(0, handler.RequestCount);
    }

    [TestMethod]
    public async Task InvalidMultipartCreate_IsRejectedBeforeFileAccess()
    {
        using var handler = new MaterialValidationTestSupport();
        using var http = handler.CreateClient();
        var boards = new MaterialManagerClientMaterialBoards(http);
        var edges = new MaterialManagerClientMaterialEdgebands(http);
        var board = MaterialValidationTestSupport.Board();
        board.Thickness = 1000;
        var missing = new FileReference("image", Path.Combine(Path.GetTempPath(), Guid.NewGuid() + ".png"));
        await Assert.ThrowsExactlyAsync<ValidationException>(() => boards.CreateBoardType(board, [missing]));
        await Assert.ThrowsExactlyAsync<ValidationException>(() => edges.CreateEdgebandType(
            new MaterialManagerRequestEdgebandType { EdgebandCode = "edge", DefaultLength = 2001, Height = 20, Thickness = 1 },
            [missing]));
        Assert.AreEqual(0, handler.RequestCount);
    }

    [TestMethod]
    [DataRow(false, false, 0.0)]
    [DataRow(false, true, 0.0)]
    [DataRow(true, false, 0.0)]
    [DataRow(true, true, 0.0)]
    [DataRow(false, false, 100.0)]
    [DataRow(false, true, 100.0)]
    [DataRow(true, false, 100.0)]
    [DataRow(true, true, 100.0)]
    [DataRow(false, false, 1000.0)]
    [DataRow(false, true, 1000.0)]
    [DataRow(true, false, 1000.0)]
    [DataRow(true, true, 1000.0)]
    public async Task NestedTexture_PreservesExistingDimensionAcceptance(bool edgeband, bool multipart, double dimension)
    {
        using var handler = new MaterialValidationTestSupport();
        using var http = handler.CreateClient();
        var texture = new AdditionalDataTexture { Name = "texture.png" };
        if (dimension != 0)
        {
            texture.Length = dimension;
            texture.Width = dimension;
        }

        if (edgeband)
        {
            var client = new MaterialManagerClientMaterialEdgebands(http);
            var request = new MaterialManagerRequestEdgebandType
            {
                EdgebandCode = "edge", DefaultLength = 100, Height = 20, Thickness = 1,
                AdditionalData = [texture]
            };
            var update = new MaterialManagerUpdateEdgebandType { AdditionalData = [texture] };
            if (multipart)
            {
                await client.CreateEdgebandType(request, []);
                await client.UpdateEdgebandType("edge", update, []);
            }
            else
            {
                await client.CreateEdgebandType(request);
                await client.UpdateEdgebandType("edge", update);
            }
        }
        else
        {
            var client = new MaterialManagerClientMaterialBoards(http);
            var request = MaterialValidationTestSupport.Board();
            request.AdditionalData = [texture];
            var update = new MaterialManagerUpdateBoardType { AdditionalData = [texture] };
            if (multipart)
            {
                await client.CreateBoardType(request, []);
                await client.UpdateBoardType("board", update, []);
            }
            else
            {
                await client.CreateBoardType(request);
                await client.UpdateBoardType("board", update);
            }
        }

        Assert.AreEqual(2, handler.RequestCount);
        var payloadTexture = JObject.Parse(handler.Payload!)["additionalData"]![0]!;
        Assert.AreEqual(dimension, payloadTexture["length"]!.Value<double>());
        Assert.AreEqual(dimension, payloadTexture["width"]!.Value<double>());
    }

    [TestMethod]
    [DataRow(nameof(ImportInventoryRequest.Materials))]
    [DataRow(nameof(ImportInventoryRequest.Instances))]
    [DataRow(nameof(ImportInventoryRequest.Allocations))]
    public async Task NullImportCollection_ReportsThePublicProperty(string name)
    {
        using var handler = new MaterialValidationTestSupport();
        using var http = handler.CreateClient();
        var client = new MaterialManagerClientMaterialBoards(http);
        var request = new ImportInventoryRequest();
        typeof(ImportInventoryRequest).GetProperty(name)!.SetValue(request, null);
        var exception = await Assert.ThrowsExactlyAsync<ValidationException>(() => client.ImportInventory(request));
        StringAssert.Contains(exception.Message, name);
        Assert.AreEqual(0, handler.RequestCount);
    }

    [TestMethod]
    public void OptInRecursiveValidation_ReportsNestedPathsAndTerminatesCycles()
    {
        var graph = new ValidationNode { Quantity = 1 };
        graph.Children.Add(new ValidationNode { Quantity = 0 });
        graph.Children.Add(graph);
        ValidationProbe.Flat(graph);
        var error = Assert.ThrowsExactly<ValidationException>(() => ValidationProbe.Recursive(graph));
        StringAssert.Contains(error.Message, "Children[0].Quantity");
    }

    [TestMethod]
    public async Task RemainingAllocationVariants_RejectInvalidAndAllowValidValues()
    {
        using var handler = new MaterialValidationTestSupport();
        using var http = handler.CreateClient();
        var boards = new MaterialManagerClientMaterialBoards(http);
        var edges = new MaterialManagerClientMaterialEdgebands(http);
        var board = new BoardTypeAllocationRequest
        {
            Name = "allocation", BoardTypeCode = "board", CreatedBy = "user", Workstation = "station", Quantity = 0
        };
        await Assert.ThrowsExactlyAsync<ValidationException>(() => boards.CreateBoardTypeAllocation(board));
        var edge = new EdgebandTypeAllocationUpdate { EdgebandCode = "edge", Order = "order", AllocatedLength = -1 };
        await Assert.ThrowsExactlyAsync<ValidationException>(() => edges.UpdateEdgebandTypeAllocation(edge));
        Assert.AreEqual(0, handler.RequestCount);
        board.Quantity = 1;
        await boards.CreateBoardTypeAllocation(board);
        edge.AllocatedLength = 99999;
        await edges.UpdateEdgebandTypeAllocation(edge);
        Assert.AreEqual(2, handler.RequestCount);
    }

    [TestMethod]
    public async Task SparseImport_DoesNotRequireUnsentTypeFieldsOrInstanceDimensions()
    {
        using var handler = new MaterialValidationTestSupport();
        using var http = handler.CreateClient();
        var client = new MaterialManagerClientMaterialBoards(http);
        await client.ImportInventory(new ImportInventoryRequest
        {
            Materials = [new BoardType { BoardCode = "board", Thickness = 19 }],
            Instances =
            [
                new BoardEntity
                {
                    BoardType = new BoardType { BoardCode = "board" }, Quantity = 0,
                    Location = new StorageLocation { Name = "station", LocationId = "location" }
                }
            ],
            Allocations = [new BoardTypeAllocation { BoardCode = "board", Name = "allocation", Quantity = 0 }]
        });
        Assert.AreEqual(1, handler.RequestCount);
    }

    [TestMethod]
    [DataRow(false)]
    [DataRow(true)]
    public async Task ValidBoardCreate_SendsUnchangedValues(bool multipart)
    {
        using var handler = new MaterialValidationTestSupport();
        using var http = handler.CreateClient();
        var client = new MaterialManagerClientMaterialBoards(http);
        var request = MaterialValidationTestSupport.Board();
        if (multipart)
            await client.CreateBoardType(request, []);
        else
            await client.CreateBoardType(request);
        Assert.AreEqual(1, handler.RequestCount);
        Assert.AreEqual(19, JObject.Parse(handler.Payload!)["thickness"]!.Value<double>());
        Assert.AreEqual(19, request.Thickness);
    }

    [TestMethod]
    [DataRow(false)]
    [DataRow(true)]
    public async Task ValidBoardUpdate_DoesNotRequireCreationFields(bool multipart)
    {
        using var handler = new MaterialValidationTestSupport();
        using var http = handler.CreateClient();
        var client = new MaterialManagerClientMaterialBoards(http);
        var request = new MaterialManagerUpdateBoardType { Thickness = 0.1, Comments = string.Empty };
        if (multipart)
            await client.UpdateBoardType("board", request, []);
        else
            await client.UpdateBoardType("board", request);
        Assert.AreEqual(1, handler.RequestCount);
        var payload = JObject.Parse(handler.Payload!);
        Assert.AreEqual(0.1, payload["thickness"]!.Value<double>());
        Assert.IsNull(payload["length"]);
        Assert.AreEqual(string.Empty, payload["comments"]!.Value<string>());
    }

    [TestMethod]
    public async Task ValidNestedTexture_DoesNotTraverseUriOrOpenExtensionData()
    {
        using var handler = new MaterialValidationTestSupport();
        using var http = handler.CreateClient();
        var client = new MaterialManagerClientMaterialBoards(http);
        var request = MaterialValidationTestSupport.Board();
        request.AdditionalData =
        [
            new AdditionalDataTexture
            {
                Length = 100, Width = 100, DownloadUri = new Uri("relative.png", UriKind.Relative),
                AdditionalProperties = new Dictionary<string, object> { ["opaque"] = new RequiredData() }
            }
        ];
        await client.CreateBoardType(request);
        Assert.AreEqual(1, handler.RequestCount);
    }

    public class ValidationNode
    {
        public List<ValidationNode> Children { get; set; } = [];

        [Range(1, 10)]
        public int Quantity { get; set; }
    }

    public class RequiredData
    {
        [Required]
        public string? Value { get; set; }
    }

    private sealed class ValidationProbe : ClientBase
    {
        private ValidationProbe(HttpClient client) : base(client) { }

        public static void Flat(object value) => ValidateRequiredProperties(value);

        public static void Recursive(object value) => ValidateRequiredProperties(value, recursive: true);
    }
}