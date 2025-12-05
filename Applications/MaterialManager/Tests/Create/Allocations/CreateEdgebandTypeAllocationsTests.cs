using System.ComponentModel.DataAnnotations;

using FluentAssertions;

using HomagConnect.MaterialManager.Client;
using HomagConnect.MaterialManager.Contracts.Material.Edgebands;
using HomagConnect.MaterialManager.Contracts.Request;

namespace HomagConnect.MaterialManager.Tests.Create.Allocations;

/// <summary />
[TestClass]
[TestCategory("MaterialManager")]
public class CreateEdgebandTypeAllocationTests : MaterialManagerTestBase
{
    /// <summary>
    /// MaterialManagerClientMaterialBoards
    /// </summary>
    protected MaterialManagerClientMaterialBoards MaterialManagerClientMaterialBoards { get; private set; } = null!;

    /// <summary />
    [TestMethod]
    [DataRow(null, "comments", "createdBy", "name", 1000, "source", "workstation",)] 
    [DataRow("boardCode", "comments", null, "name", 1000, "source", "workstation")] 
    [DataRow("boardCode", "comments", "createdBy", null, 1000, "source", "workstation")]
    [DataRow("boardCode", "comments", "createdBy", "name", 1, "source", null)]
    public async Task EdgebandTypeAllocationCreation_RequiredPropertiesMissing_ThrowsException(string boardCode, string comments, string createdBy, string source, string workstation, double allocatedLength,
        string customer, string order, string project, double usedLength)
    {
        var requestEdgebandTypeAllocation = CreateEdgebandTypeAllocationRequest(boardCode, comments, createdBy, source, workstation, allocatedLength, customer, order, project, usedLength);

        var act = async () => await MaterialManagerClientMaterialBoards.CreateEdgebandTypeAllocation(requestEdgebandTypeAllocation);

        await act.Should().ThrowAsync<ValidationException>(
            "because creating a board type allocation with missing required properties should throw a ValidationException");
    }

    /// <summary>
    /// EdgebandTypeAllocationCreation_ValidRequest_CreatesAllocation
    /// </summary>
    [TestMethod]
    [DataRow("comments", "createdBy", "Test_Allocation", 1, "source", "workstation")]
    public async Task EdgebandTypeAllocationCreation_ValidRequest_CreatesAllocation(string comments, string createdBy,
        string name, int quantity, string source, string workstation)
    {
        await EdgebandType_CreateEdgebandTypeAllocation_Cleanup(name);

        var materials = (await MaterialManagerClientMaterialBoards.GetEdgebandTypes(1).ConfigureAwait(false) ?? Array.Empty<EdgebandType>()).ToArray();

        materials.Should().NotBeNull(
            "because GetEdgebandTypes should return a collection of board types");

        var firstMaterial = materials.FirstOrDefault();
        firstMaterial.Should().NotBeNull(
            "because at least one board type should exist in the system");

        var boardCode = firstMaterial!.BoardCode;
        var requestEdgebandTypeAllocation = CreateEdgebandTypeAllocationRequest(boardCode, comments, createdBy, source, workstation, TODO, TODO, TODO, TODO, TODO);

        var allocationResult = await MaterialManagerClientMaterialBoards.CreateEdgebandTypeAllocation(requestEdgebandTypeAllocation);

        allocationResult.Should().NotBeNull(
            $"because board type allocation '{name}' should be created successfully");
        allocationResult.BoardCode.Should().Be(boardCode,
            $"because board type allocation '{name}' was created for board code '{boardCode}'");
        allocationResult.Comments.Should().Be(comments,
            $"because board type allocation '{name}' was created with comments '{comments}'");
        allocationResult.CreatedBy.Should().Be(createdBy,
            $"because board type allocation '{name}' was created by '{createdBy}'");
        allocationResult.Name.Should().Be(name,
            $"because board type allocation was created with name '{name}'");
        allocationResult.Quantity.Should().Be(quantity,
            $"because board type allocation '{name}' was created with quantity {quantity}");
        allocationResult.Source.Should().Be(source,
            $"because board type allocation '{name}' was created with source '{source}'");
        allocationResult.Workstation.Should().Be(workstation,
            $"because board type allocation '{name}' was created with workstation '{workstation}'");

        await EdgebandType_CreateEdgebandTypeAllocation_Cleanup(name);
    }

    /// <summary>
    /// Setup method to initialize the MaterialManagerClientMaterialBoards client.
    /// </summary>
    [TestInitialize]
    public void Setup()
    {
        var materialManagerClient = GetMaterialManagerClient();
        MaterialManagerClientMaterialBoards = materialManagerClient.Material.Boards;
    }

    private static EdgebandTypeAllocationRequest CreateEdgebandTypeAllocationRequest(string boardCode, string comments, string createdBy, string source, string workstation, double allocatedLength,
        string customer, string order, string project, double usedLength)
    {
        var edgebandTypeAllocationRequest = new EdgebandTypeAllocationRequest
        {
            EdgebandCode = boardCode,
            Comments = comments,
            CreatedBy = createdBy,
            Source = source,
            Workstation = workstation,
            AllocatedLength = allocatedLength,
            Customer = customer,
            Order = order,
            Project = project,
            UsedLength = usedLength
        };

        return edgebandTypeAllocationRequest;
    }

    private async Task EdgebandType_CreateEdgebandTypeAllocation_Cleanup(string name)
    {
        var allocations = (await MaterialManagerClientMaterialBoards.GetEdgebandTypeAllocationsByAllocationNames([name], 100)).ToArray();

        if (allocations.Length > 0)
        {
            await MaterialManagerClientMaterialBoards.DeleteEdgebandTypeAllocations(allocations.Select(a => a.Name));
            allocations = (await MaterialManagerClientMaterialBoards.GetEdgebandTypeAllocationsByAllocationNames([name], 100)).ToArray();
        }

        if (allocations.Length > 0)
        {
            await MaterialManagerClientMaterialBoards.DeleteEdgebandTypeAllocations(allocations.Select(a => a.Name));
            allocations = (await MaterialManagerClientMaterialBoards.GetEdgebandTypeAllocationsByAllocationNames([name], 100)).ToArray();
        }

        allocations.Should().BeEmpty(
            $"because board type allocation '{name}' should be deleted during cleanup");
    }
}