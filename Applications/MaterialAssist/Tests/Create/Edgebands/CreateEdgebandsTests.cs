using FluentAssertions;

using HomagConnect.MaterialAssist.Samples.Create.Edgebands;
using HomagConnect.MaterialManager.Contracts.Material.Base;

namespace HomagConnect.MaterialAssist.Tests.Create.Edgebands;

[TestClass]
[TestCategory("MaterialAssist")]
[TestCategory("MaterialAssist.Edgebands")]
public class CreateEdgebandsTests : MaterialAssistTestBase
{
    [TestCleanup]
    public async Task Cleanup()
    {
        var materialAssistClient = GetMaterialAssistClient().Edgebands;
        await materialAssistClient.DeleteEdgebandEntity("16");
    }

    [TestMethod]
    public async Task EdgebandsCreateEdgebandEntity()
    {
        var materialAssistClient = GetMaterialAssistClient().Edgebands;
        await CreateEdgebandEntitiesSamples.Edgebands_CreateEdgebandEntity(materialAssistClient, "16");

        var edgebandEntity = await materialAssistClient.GetEdgebandEntityById("16");

        edgebandEntity.Should().NotBeNull(
            "because edgeband entity with ID '16' should be created successfully");
        edgebandEntity!.Id.Should().Be("16",
            "because we created edgeband entity with ID '16'");
        edgebandEntity.ManagementType.Should().Be(ManagementType.Single,
            "because edgeband entity '16' was created with ManagementType.Single");
        edgebandEntity.Quantity.Should().Be(1,
            "because Single management type must have quantity of 1");
        edgebandEntity.Length.Should().Be(50,
            "because edgeband entity '16' was created with length 50");
    }

    [TestInitialize]
    public async Task Initialize()
    {
        await EnsureEdgebandTypeExist("ABS_White_1mm");
    }
}