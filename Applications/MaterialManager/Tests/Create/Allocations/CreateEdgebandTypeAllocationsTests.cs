using System.ComponentModel.DataAnnotations;

using FluentAssertions;

using HomagConnect.MaterialManager.Client;

namespace HomagConnect.MaterialManager.Tests.Create.Allocations;

/// <summary />
[TestClass]
[TestCategory("MaterialManager")]
public class CreateEdgebandTypeAllocationTests : MaterialManagerTestBase
{
    /// <summary>
    /// MaterialManagerClientMaterialEdgebands
    /// </summary>
    protected MaterialManagerClientMaterialEdgebands MaterialManagerClientMaterialEdgebands { get; private set; } = null!;

    /// <summary />
    [TestMethod]
    [DataRow("", "comments", "createdBy", "source", "workstation", 10.0, "customer", "order", "project", 5.0)] // Missing required EdgebandCode
    [DataRow("E123", "comments", "", "source", "workstation", 10.0, "customer", "order", "project", 5.0)] // Missing required CreatedBy
    [DataRow("E123", "comments", "createdBy", "source", "", 10.0, "customer", "order", "project", 5.0)] // Missing required Workstation
    [DataRow("E123", "comments", "createdBy", "source", "workstation", 10.0, "customer", "", "project", 5.0)] // Missing required Order
    public async Task EdgebandTypeAllocationCreation_RequiredPropertiesMissing_ThrowsException(string edgebandCode, string comments, string createdBy, string source, string workstation,
        double allocatedLength,
        string customer, string order, string project, double usedLength)
    {
        var requestEdgebandTypeAllocation = CreateEdgebandTypeAllocationRequest(edgebandCode, comments, createdBy, source, workstation, allocatedLength, customer, order, project, usedLength);

        var act = async () => await MaterialManagerClientMaterialEdgebands.CreateEdgebandTypeAllocation(requestEdgebandTypeAllocation);

        await act.Should().ThrowAsync<ValidationException>(
            "because creating a Edgeband type allocation with missing required properties should throw a ValidationException");
    }

    /// <summary>
    /// EdgebandTypeAllocationCreation_ValidRequest_CreatesAllocation
    /// </summary>
    [TestMethod]
    [DataRow("comments", "createdBy", "source", "workstation", 10.0, "customer", "Allocation1", "project", 5.0)]
    public async Task EdgebandTypeAllocationCreation_ValidRequest_CreatesAllocation(string comments, string createdBy, string source, string workstation, double allocatedLength,
        string customer, string order, string project, double usedLength)
    {
        await EdgebandType_CreateEdgebandTypeAllocation_Cleanup(MaterialManagerClientMaterialEdgebands, EdgebandCode, customer, order, project);

        var requestEdgebandTypeAllocation = CreateEdgebandTypeAllocationRequest(EdgebandCode, comments, createdBy, source, workstation, allocatedLength, customer, order, project, usedLength);

        var allocationResult = await MaterialManagerClientMaterialEdgebands.CreateEdgebandTypeAllocation(requestEdgebandTypeAllocation);

        allocationResult.Should().NotBeNull(
            $"because Edgeband type allocation with EdgebandCode '{EdgebandCode}' should be created successfully");
        allocationResult.Comments.Should().Be(comments,
            $"because Edgeband type allocation '{EdgebandCode}' was created with comments '{comments}'");
        allocationResult.CreatedBy.Should().Be(createdBy,
            $"because Edgeband type allocation '{EdgebandCode}' was created by '{createdBy}'");
        //do not compare until clarified 
        //allocationResult.EdgebandCode.Should().Be(EdgebandCode,
            //$"because Edgeband type allocation '{EdgebandCode}' was created for Edgeband code '{EdgebandCode}'");
        //allocationResult.Source.Should().Be(source,
        //    $"because Edgeband type allocation '{EdgebandCode}' was created with source '{EdgebandCode}'");
        //allocationResult.Workstation.Should().Be(workstation,
        //    $"because Edgeband type allocation '{EdgebandCode}' was created with workstation '{workstation}'");

        await EdgebandType_CreateEdgebandTypeAllocation_Cleanup(MaterialManagerClientMaterialEdgebands, EdgebandCode, customer, order, project);
    }

    /// <summary>
    /// Setup method to initialize the MaterialManagerClientMaterialEdgebands client.
    /// </summary>
    [TestInitialize]
    public async Task Setup()
    {
        MaterialManagerClientMaterialEdgebands = GetMaterialManagerClient().Material.Edgebands;
        await EnsureEdgebandTypeExist(EdgebandCode);
    }
}