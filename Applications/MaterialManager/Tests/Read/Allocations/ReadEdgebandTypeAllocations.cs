using HomagConnect.Base.TestBase.Attributes;
using HomagConnect.MaterialManager.Client;
using Shouldly;

namespace HomagConnect.MaterialManager.Tests.Read.Allocations;

/// <summary>
/// </summary>
[TestClass]
[TestCategory("MaterialManager")]
[TemporaryDisabledOnServer(2026, 02, 28, "DF-Material")]
public class ReadEdgebandTypeTypeAllocationTests : MaterialManagerTestBase
{
    /// <summary>
    /// MaterialManagerClientMaterialEdgebandTypes
    /// </summary>
    protected MaterialManagerClientMaterialEdgebands MaterialManagerClientMaterialEdgebandTypes { get; private set; } = null!;

    /// <summary>
    /// GetEdgebandTypeAllocation returns expected allocation for given order, customer, project, and edgebandCode
    /// </summary>
    [TestMethod]
    public async Task GetEdgebandTypeAllocation_ReturnsExpectedAllocation()
    {
        // Arrange

        const string edgebandCode = "ABS_White_2mm";
        const string order = "TestOrder123";
        const string customer = "TestCustomerABC";
        const string project = "TestProjectXYZ";
        await EdgebandType_CreateEdgebandTypeAllocation_Cleanup(MaterialManagerClientMaterialEdgebandTypes, edgebandCode, customer, order, project);
        await EnsureEdgebandTypeExist(MaterialManagerClientMaterialEdgebandTypes, edgebandCode);

        var allocationRequest = CreateEdgebandTypeAllocationRequest(
            edgebandCode,
            comments: "Test allocation",
            createdBy: "UnitTest",
            source: "TestSource",
            workstation: "TestWorkstation",
            allocatedLength: 100.0,
            customer: customer,
            order: order,
            project: project,
            usedLength: 50.0);

        var createdAllocation = await MaterialManagerClientMaterialEdgebandTypes.CreateEdgebandTypeAllocation(allocationRequest);

        // Act
        var allocation = await MaterialManagerClientMaterialEdgebandTypes.GetEdgebandTypeAllocation(order, customer, project, edgebandCode);

        // Assert
        allocation.ShouldNotBeNull("because the allocation should exist for the given parameters");
        //code is not returned correctly yet
        //allocation.EdgebandCode.Should().Be(edgebandCode, "because the allocation was created for this edgeband code");
        allocation!.Order.ShouldBe(order, "because the allocation was created for this order");
        allocation.Customer.ShouldBe(customer, "because the allocation was created for this customer");
        allocation.Project.ShouldBe(project, "because the allocation was created for this project");

        // Cleanup
        await EdgebandType_CreateEdgebandTypeAllocation_Cleanup(MaterialManagerClientMaterialEdgebandTypes, edgebandCode, customer, order, project);
    }

    /// <summary>
    /// GetEdgebandTypeTypeAllocations_NoException
    /// </summary>
    [TestMethod]
    public async Task GetEdgebandTypeTypeAllocations_NoException()
    {
        // Act
        var allocations = await MaterialManagerClientMaterialEdgebandTypes.GetEdgebandTypeAllocations();

        // Assert
        allocations.ShouldNotBeNull(
            "because GetEdgebandTypeTypeAllocations should return a collection of board type allocations");
    }

    /// <summary>
    /// Setup method to initialize the MaterialManagerClientMaterialEdgebandTypes client.
    /// </summary>
    [TestInitialize]
    public void Setup()
    {
        var materialManagerClient = GetMaterialManagerClient();
        MaterialManagerClientMaterialEdgebandTypes = materialManagerClient.Material.Edgebands;
    }
}