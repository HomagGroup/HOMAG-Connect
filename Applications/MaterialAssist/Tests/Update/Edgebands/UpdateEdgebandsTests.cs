using FluentAssertions;

using HomagConnect.MaterialAssist.Samples.Update.Edgebands;

namespace HomagConnect.MaterialAssist.Tests.Update.Edgebands;

[TestClass]
[TestCategory("MaterialAssist")]
[TestCategory("MaterialAssist.Edgebands")]
public class UpdateEdgebandsTests : MaterialAssistTestBase
{
    [TestMethod]
    public async Task EdgebandsUpdateEdgebandEntities()
    {
        var random = new Random();
        var length = Math.Round(RandomBetween(50.0, 100.0), 2);

        var materialAssistClient = GetMaterialAssistClient().Edgebands;
        await UpdateEdgebandEntitiesSamples.Edgebands_UpdateEdgebandEntity(materialAssistClient, length);

        var checkEdgebandEntity = await materialAssistClient.GetEdgebandEntityById("43");

        checkEdgebandEntity.Should().NotBeNull(
            "because edgeband entity '43' should exist after update");
        checkEdgebandEntity!.Length.Should().Be(length,
            $"because edgeband entity '43' was updated to length {length}");
        return;

        double RandomBetween(double min, double max)
        {
            return random.NextDouble() * (max - min) + min;
        }
    }

    [ClassInitialize]
    public static async Task Initialize(TestContext testContext)
    {
        var classInstance = new UpdateEdgebandsTests();
        await classInstance.EnsureEdgebandTypeExist("ABS_White_1mm");
        await classInstance.EnsureEdgebandEntityExist("43", "ABS_White_1mm");
    }
}