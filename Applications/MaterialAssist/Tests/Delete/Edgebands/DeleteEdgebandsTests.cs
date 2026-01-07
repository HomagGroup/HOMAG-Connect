using HomagConnect.MaterialAssist.Samples.Delete.Edgebands;
using Shouldly;

namespace HomagConnect.MaterialAssist.Tests.Delete.Edgebands;

[TestClass]
[TestCategory("MaterialAssist")]
[TestCategory("MaterialAssist.Edgebands")]
public class DeleteEdgebandsTests : MaterialAssistTestBase
{
    [ClassInitialize]
    public static async Task Initialize(TestContext testContext)
    {
        var classInstance = new DeleteEdgebandsTests();
        await classInstance.EnsureEdgebandTypeExist("ABS_White_1mm");

        await classInstance.EnsureEdgebandEntityExist("23", "ABS_White_1mm");
        await classInstance.EnsureEdgebandEntityExist("24", "ABS_White_1mm");
        await classInstance.EnsureEdgebandEntityExist("25", "ABS_White_1mm");
    }

    [TestMethod]
    public async Task EdgebandsDeleteEdgebandEntity()
    {
        var materialAssistClient = GetMaterialAssistClient().Edgebands;

        var act = async () => await DeleteEdgebandEntitiesSamples.Edgebands_DeleteEdgebandEntity(materialAssistClient, "23");

        await Should.NotThrowAsync(act,
            "because deleting edgeband entity '23' should complete successfully without errors");
    }

    [TestMethod]
    public async Task EdgebandsDeleteEdgebandEntities()
    {
        var materialAssistClient = GetMaterialAssistClient().Edgebands;

        var act = async () => await DeleteEdgebandEntitiesSamples.Edgebands_DeleteEdgebandEntities(materialAssistClient, ["24", "25"]);

        await Should.NotThrowAsync(act,
            "because deleting edgeband entities '24' and '25' should complete successfully without errors");
    }
}