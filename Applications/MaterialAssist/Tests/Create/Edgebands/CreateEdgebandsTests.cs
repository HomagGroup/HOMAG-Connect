using HomagConnect.MaterialAssist.Samples.Create.Edgebands;
using HomagConnect.MaterialManager.Contracts.Material.Base;
using Shouldly;

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

        edgebandEntity.ShouldNotBeNull(
            "because edgeband entity with ID '16' should be created successfully");
        edgebandEntity!.Id.ShouldBe("16",
            "because we created edgeband entity with ID '16'");
        edgebandEntity.ManagementType.ShouldBe(ManagementType.Single,
            "because edgeband entity '16' was created with ManagementType.Single");
        edgebandEntity.Quantity.ShouldBe(1,
            "because Single management type must have quantity of 1");
        edgebandEntity.Length.ShouldBe(50,
            "because edgeband entity '16' was created with length 50");
    }

    [TestInitialize]
    public async Task Initialize()
    {
        await EnsureEdgebandTypeExist("ABS_White_1mm");
    }
}