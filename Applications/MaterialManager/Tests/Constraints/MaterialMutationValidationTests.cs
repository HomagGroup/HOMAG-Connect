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

using Shouldly;

namespace HomagConnect.MaterialManager.Tests.Constraints;

[TestClass]
public class MaterialMutationValidationTests
{
    [TestMethod]
    [DataRow(null, true)]
    [DataRow(0.0, true)]
    [DataRow(0.01, true)]
    [DataRow(0.03, true)]
    [DataRow(0.09, true)]
    [DataRow(0.1, true)]
    [DataRow(99999.99, true)]
    [DataRow(100000.0, true)]
    [DataRow(double.MaxValue, true)]
    [DataRow(-0.01, false)]
    public async Task MaterialCosts_PreserveNonnegativeUnboundedRange(double? costs, bool expectedValid)
    {
        using var handler = new MaterialValidationTestSupport();
        using var http = handler.CreateClient();
        var boards = new MaterialManagerClientMaterialBoards(http);
        var edges = new MaterialManagerClientMaterialEdgebands(http);
        var board = MaterialValidationTestSupport.Board();
        board.Costs = costs;
        var edge = new MaterialManagerRequestEdgebandType
        {
            EdgebandCode = "edge", Height = 23, Thickness = 1, Costs = costs
        };
        var boardUpdate = new MaterialManagerUpdateBoardType { Costs = costs };
        var edgeUpdate = new MaterialManagerUpdateEdgebandType { Costs = costs };
        Func<Task>[] mutations =
        [
            () => boards.CreateBoardType(board),
            () => boards.CreateBoardType(board, []),
            () => edges.CreateEdgebandType(edge),
            () => edges.CreateEdgebandType(edge, []),
            () => boards.UpdateBoardType("board", boardUpdate),
            () => boards.UpdateBoardType("board", boardUpdate, []),
            () => edges.UpdateEdgebandType("edge", edgeUpdate),
            () => edges.UpdateEdgebandType("edge", edgeUpdate, []),
            () => boards.PatchBoardType("board", PatchBuilder<BoardType>.For().Set(x => x.Costs, costs)),
            () => edges.PatchEdgebandType("edge", PatchBuilder<EdgebandType>.For().Set(x => x.Costs, costs))
        ];

        foreach (var mutate in mutations)
        {
            if (expectedValid)
            {
                await mutate();
                JObject.Parse(handler.Payload!)["costs"]?.Value<double?>().ShouldBe(costs);
            }
            else
            {
                await Should.ThrowAsync<ValidationException>(mutate);
            }
        }

        Func<Task> import = () => boards.ImportInventory(new ImportInventoryRequest
        {
            Materials = [new BoardType { BoardCode = "board", Costs = costs }]
        });
        if (expectedValid)
        {
            await import();
            JObject.Parse(handler.Payload!)["materials"]![0]!["costs"]?.Value<double?>().ShouldBe(costs);
        }
        else
        {
            await Should.ThrowAsync<ValidationException>(import);
        }

        handler.RequestCount.ShouldBe(expectedValid ? mutations.Length + 1 : 0);
    }

    [TestMethod]
    public async Task AllocationMutations_CheckMappedValues()
    {
        using var handler = new MaterialValidationTestSupport();
        using var http = handler.CreateClient();
        var boards = new MaterialManagerClientMaterialBoards(http);
        var edges = new MaterialManagerClientMaterialEdgebands(http);
        (await Should.ThrowAsync<ValidationException>(() =>
            boards.UpdateBoardTypeAllocation("allocation", new BoardTypeAllocationUpdate { Name = "allocation", Quantity = 0 }))).ShouldBeOfType<ValidationException>();
        (await Should.ThrowAsync<ValidationException>(() =>
            edges.CreateEdgebandTypeAllocation(new EdgebandTypeAllocationRequest
            {
                EdgebandCode = "edge", Order = "order", CreatedBy = "user", Workstation = "station", AllocatedLength = 100000
            }))).ShouldBeOfType<ValidationException>();
        handler.RequestCount.ShouldBe(0);
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

        handler.RequestCount.ShouldBe(1);
        JObject.Parse(handler.Payload!)["quantity"]!.Value<int>().ShouldBe(5);
        if (boardTypeCode != null)
            JObject.Parse(handler.Payload!)["boardTypeCode"]!.Value<string>().ShouldBe(boardTypeCode);
    }

    [TestMethod]
    public async Task BoardAllocationUpdate_RejectsBoardCodeBeyondMaximumBeforeTransport()
    {
        using var handler = new MaterialValidationTestSupport();
        using var http = handler.CreateClient();
        var client = new MaterialManagerClientMaterialBoards(http);

        (await Should.ThrowAsync<ValidationException>(() =>
            client.UpdateBoardTypeAllocation("allocation", new BoardTypeAllocationUpdate
            {
                Name = "allocation", BoardTypeCode = new string('x', 51)
            }))).ShouldBeOfType<ValidationException>();

        handler.RequestCount.ShouldBe(0);
    }

    [TestMethod]
    public void BoardPatch_ValidatesOnlyTheSuppliedProperty()
    {
        var patch = PatchBuilder<BoardType>.For().Set(x => x.Thickness, 0.1);
        patch.Build().Count.ShouldBe(1);
        Should.Throw<ValidationException>(() => patch.Set(x => x.Thickness, 1000)).ShouldBeOfType<ValidationException>();
        patch.Set(x => x.Comments, string.Empty);
    }

    [TestMethod]
    [DataRow(nameof(BoardType.Length))]
    [DataRow(nameof(BoardType.Width))]
    public void BoardPatch_EnforcesInterfaceRangeAndPreservesRequiredness(string member)
    {
        var patch = PatchBuilder<BoardType>.For();
        if (member == nameof(BoardType.Length))
        {
            Should.Throw<ValidationException>(() => patch.Set(board => board.Length, (double?)0.09));
            Should.Throw<ValidationException>(() => patch.Set(board => board.Length, (double?)20000));
            Should.Throw<ValidationException>(() => patch.Set(board => board.Length, (double?)null));
            patch.Set(board => board.Length, (double?)0.1);
            patch.Set(board => board.Length, (double?)19999.9);
        }
        else
        {
            Should.Throw<ValidationException>(() => patch.Set(board => board.Width, (double?)0.09));
            Should.Throw<ValidationException>(() => patch.Set(board => board.Width, (double?)20000));
            Should.Throw<ValidationException>(() => patch.Set(board => board.Width, (double?)null));
            patch.Set(board => board.Width, (double?)0.1);
            patch.Set(board => board.Width, (double?)19999.9);
        }

        patch.Build().Count.ShouldBe(1);
    }

    [TestMethod]
    [DataRow(nameof(BoardType.Length), 0.09)]
    [DataRow(nameof(BoardType.Length), 20000.0)]
    [DataRow(nameof(BoardType.Width), 0.09)]
    [DataRow(nameof(BoardType.Width), 20000.0)]
    public async Task InventoryImport_EnforcesInterfaceDimensionRangesBeforeTransport(string member, double invalidValue)
    {
        using var handler = new MaterialValidationTestSupport();
        using var http = handler.CreateClient();
        var client = new MaterialManagerClientMaterialBoards(http);
        var board = new BoardType { BoardCode = "board" };
        typeof(BoardType).GetProperty(member)!.SetValue(board, invalidValue);

        var exception = await Should.ThrowAsync<ValidationException>(() =>
            client.ImportInventory(new ImportInventoryRequest { Materials = [board] }));
        exception.Message.ShouldContain($"Materials[0].{member}");
        handler.RequestCount.ShouldBe(0);

        foreach (var value in new double?[] { null, 0.1, 19999.9 })
        {
            typeof(BoardType).GetProperty(member)!.SetValue(board, value);
            await client.ImportInventory(new ImportInventoryRequest { Materials = [board] });
        }

        handler.RequestCount.ShouldBe(3);
    }

    [TestMethod]
    [DataRow(nameof(MaterialManagerUpdateBoardType.Length), 19999.9, false)]
    [DataRow(nameof(MaterialManagerUpdateBoardType.Length), 19999.9, true)]
    [DataRow(nameof(MaterialManagerUpdateBoardType.Width), 19999.9, false)]
    [DataRow(nameof(MaterialManagerUpdateBoardType.Width), 19999.9, true)]
    [DataRow(nameof(MaterialManagerUpdateBoardType.Thickness), 999.9, false)]
    [DataRow(nameof(MaterialManagerUpdateBoardType.Thickness), 999.9, true)]
    public async Task BoardUpdate_EnforcesInterfaceDimensionsBeforeTransport(string member, double maximum, bool multipart)
    {
        using var handler = new MaterialValidationTestSupport();
        using var http = handler.CreateClient();
        var client = new MaterialManagerClientMaterialBoards(http);
        var request = new MaterialManagerUpdateBoardType();
        var property = typeof(MaterialManagerUpdateBoardType).GetProperty(member)!;
        Func<Task> act = multipart
            ? () => client.UpdateBoardType("board", request, [])
            : () => client.UpdateBoardType("board", request);

        foreach (var invalid in new[] { 0.09, maximum + 0.1 })
        {
            property.SetValue(request, invalid);
            await Should.ThrowAsync<ValidationException>(act);
        }

        handler.RequestCount.ShouldBe(0);
        foreach (var valid in new double?[] { null, 0.1, maximum })
        {
            property.SetValue(request, valid);
            await act();
        }

        handler.RequestCount.ShouldBe(3);
    }

    [TestMethod]
    public void EdgebandPatch_UsesOnlySuppliedValuesAndAllowsClearing()
    {
        var patch = PatchBuilder<EdgebandType>.For().Set(x => x.Thickness, 0.01);
        patch.Set(x => x.Comments, null);
        Should.Throw<ValidationException>(() => patch.Set(x => x.Thickness, 100)).ShouldBeOfType<ValidationException>();
        patch.Build().Count.ShouldBe(2);
    }

    [TestMethod]
    [DataRow(false)]
    [DataRow(true)]
    public async Task EdgebandCreate_AllowsZeroDefaultLength(bool multipart)
    {
        using var handler = new MaterialValidationTestSupport();
        using var http = handler.CreateClient();
        var client = new MaterialManagerClientMaterialEdgebands(http);
        var request = new MaterialManagerRequestEdgebandType
        {
            EdgebandCode = "edge", DefaultLength = 0, Height = 20, Thickness = 1.2
        };

        if (multipart)
            await client.CreateEdgebandType(request, []);
        else
            await client.CreateEdgebandType(request);

        handler.RequestCount.ShouldBe(1);
        JObject.Parse(handler.Payload!)["defaultLength"]!.Value<double>().ShouldBe(0);
    }

    [TestMethod]
    [DataRow(false)]
    [DataRow(true)]
    public async Task EdgebandCreate_RejectsNegativeDefaultLengthBeforeTransport(bool multipart)
    {
        using var handler = new MaterialValidationTestSupport();
        using var http = handler.CreateClient();
        var client = new MaterialManagerClientMaterialEdgebands(http);
        var request = new MaterialManagerRequestEdgebandType
        {
            EdgebandCode = "edge", DefaultLength = -0.01, Height = 20, Thickness = 1.2
        };

        (await Should.ThrowAsync<ValidationException>(() => multipart
            ? client.CreateEdgebandType(request, [])
            : client.CreateEdgebandType(request))).ShouldBeOfType<ValidationException>();

        handler.RequestCount.ShouldBe(0);
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
        (await Should.ThrowAsync<ValidationException>(() => multipart
            ? client.UpdateEdgebandType("edge", invalid, [])
            : client.UpdateEdgebandType("edge", invalid))).ShouldBeOfType<ValidationException>();
        handler.RequestCount.ShouldBe(0);
        var valid = new MaterialManagerUpdateEdgebandType { DefaultLength = 0, Thickness = 0.01 };
        if (multipart)
            await client.UpdateEdgebandType("edge", valid, []);
        else
            await client.UpdateEdgebandType("edge", valid);
        handler.RequestCount.ShouldBe(1);
        var payload = JObject.Parse(handler.Payload!);
        payload["defaultLength"]!.Value<double>().ShouldBe(0);
        payload["height"].ShouldBeNull();
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
        board.ShouldNotBeNull();
        board.Length.ShouldBe(20000);
        board.Thickness.ShouldBe(0.01);
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
        (await Should.ThrowAsync<ValidationException>(() => multipart
            ? client.CreateBoardType(request, [])
            : client.CreateBoardType(request))).ShouldBeOfType<ValidationException>();
        handler.RequestCount.ShouldBe(0);
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
        (await Should.ThrowAsync<ValidationException>(() => multipart
            ? client.UpdateBoardType("board", request, [])
            : client.UpdateBoardType("board", request))).ShouldBeOfType<ValidationException>();
        handler.RequestCount.ShouldBe(0);
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
        (await Should.ThrowAsync<ValidationException>(() => multipart
            ? client.CreateEdgebandType(request, [])
            : client.CreateEdgebandType(request))).ShouldBeOfType<ValidationException>();
        handler.RequestCount.ShouldBe(0);
    }

    [TestMethod]
    public async Task InvalidImportReference_IsReportedWithoutValidatingUnmappedFields()
    {
        using var handler = new MaterialValidationTestSupport();
        using var http = handler.CreateClient();
        var client = new MaterialManagerClientMaterialBoards(http);
        var exception = await Should.ThrowAsync<ValidationException>(() => client.ImportInventory(new ImportInventoryRequest
        {
            Instances = [new BoardEntity { BoardType = new BoardType { BoardCode = new string('x', 51) }, Quantity = 0 }]
        }));
        exception.ShouldBeOfType<ValidationException>();
        exception.Message.ShouldContain("Instances[0].BoardType.BoardCode");
        handler.RequestCount.ShouldBe(0);
    }

    [TestMethod]
    public async Task InvalidImport_ReportsIndexedPublicPath()
    {
        using var handler = new MaterialValidationTestSupport();
        using var http = handler.CreateClient();
        var client = new MaterialManagerClientMaterialBoards(http);
        var exception = await Should.ThrowAsync<ValidationException>(() => client.ImportInventory(new ImportInventoryRequest
        {
            Materials = [new BoardType { BoardCode = "board", Thickness = 1000 }]
        }));
        exception.ShouldBeOfType<ValidationException>();
        exception.Message.ShouldContain("Materials[0].Thickness");
        handler.RequestCount.ShouldBe(0);
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
        (await Should.ThrowAsync<ValidationException>(() => boards.CreateBoardType(board, [missing]))).ShouldBeOfType<ValidationException>();
        (await Should.ThrowAsync<ValidationException>(() => edges.CreateEdgebandType(
            new MaterialManagerRequestEdgebandType { EdgebandCode = "edge", DefaultLength = 2001, Height = 20, Thickness = 1 },
            [missing]))).ShouldBeOfType<ValidationException>();
        handler.RequestCount.ShouldBe(0);
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

        handler.RequestCount.ShouldBe(2);
        var payloadTexture = JObject.Parse(handler.Payload!)["additionalData"]![0]!;
        payloadTexture["length"]!.Value<double>().ShouldBe(dimension);
        payloadTexture["width"]!.Value<double>().ShouldBe(dimension);
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
        var exception = await Should.ThrowAsync<ValidationException>(() => client.ImportInventory(request));
        exception.ShouldBeOfType<ValidationException>();
        exception.Message.ShouldContain(name);
        handler.RequestCount.ShouldBe(0);
    }

    [TestMethod]
    public void OptInRecursiveValidation_ReportsNestedPathsAndTerminatesCycles()
    {
        var graph = new ValidationNode { Quantity = 1 };
        graph.Children.Add(new ValidationNode { Quantity = 0 });
        graph.Children.Add(graph);
        ValidationProbe.Flat(graph);
        var error = Should.Throw<ValidationException>(() => ValidationProbe.Recursive(graph));
        error.ShouldBeOfType<ValidationException>();
        error.Message.ShouldContain("Children[0].Quantity");
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
            Name = "allocation", BoardTypeCode = "board", CreatedBy = "user", Source = "source", Workstation = "station", Quantity = 0
        };
        (await Should.ThrowAsync<ValidationException>(() => boards.CreateBoardTypeAllocation(board))).ShouldBeOfType<ValidationException>();
        var edge = new EdgebandTypeAllocationUpdate { EdgebandCode = "edge", Order = "order", AllocatedLength = -1 };
        (await Should.ThrowAsync<ValidationException>(() => edges.UpdateEdgebandTypeAllocation(edge))).ShouldBeOfType<ValidationException>();
        handler.RequestCount.ShouldBe(0);
        board.Quantity = 1;
        await boards.CreateBoardTypeAllocation(board);
        edge.AllocatedLength = 99999;
        await edges.UpdateEdgebandTypeAllocation(edge);
        handler.RequestCount.ShouldBe(2);
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
        handler.RequestCount.ShouldBe(1);
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
        handler.RequestCount.ShouldBe(1);
        JObject.Parse(handler.Payload!)["thickness"]!.Value<double>().ShouldBe(19);
        request.Thickness.ShouldBe(19);
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
        handler.RequestCount.ShouldBe(1);
        var payload = JObject.Parse(handler.Payload!);
        payload["thickness"]!.Value<double>().ShouldBe(0.1);
        payload["length"].ShouldBeNull();
        payload["comments"]!.Value<string>().ShouldBe(string.Empty);
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
        handler.RequestCount.ShouldBe(1);
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