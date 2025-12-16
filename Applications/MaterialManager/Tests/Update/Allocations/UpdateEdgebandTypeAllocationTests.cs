using FluentAssertions;

using HomagConnect.MaterialManager.Client;
using HomagConnect.MaterialManager.Contracts.Update;

namespace HomagConnect.MaterialManager.Tests.Update.Allocations
{
    /// <summary />
    [TestClass]
    [TestCategory("MaterialManager")]
    public class UpdateEdgebandTypeAllocationTests : MaterialManagerTestBase
    {
        /// <summary>
        /// MaterialManagerClientMaterialEdgebands
        /// </summary>
        protected MaterialManagerClientMaterialEdgebands MaterialManagerClientMaterialEdgebands { get; private set; } = null!;

        /// <summary>
        /// Setup method to initialize the MaterialManagerClientMaterialEdgebands client.
        /// </summary>
        [TestInitialize]
        public async Task Setup()
        {
            MaterialManagerClientMaterialEdgebands = GetMaterialManagerClient().Material.Edgebands;
            await EnsureEdgebandTypeExist(EdgebandCode);
        }

        /// <summary>
        /// UpdateEdgebandTypeAllocation_NoException
        /// </summary>
        [TestMethod]
        [DataRow("comments", "createdBy", "source", "workstation", 10.0, "customer", "Allocation1", "project", 5.0)]
        public async Task UpdateEdgebandTypeAllocation_NoException(string comments, string createdBy, string source, string workstation, double allocatedLength,
            string customer, string order, string project, double usedLength)
        {
            // Arrange
            await EdgebandType_CreateEdgebandTypeAllocation_Cleanup(MaterialManagerClientMaterialEdgebands, EdgebandCode, customer, order, project);

            var requestEdgebandTypeAllocation = CreateEdgebandTypeAllocationRequest(EdgebandCode, comments, createdBy, source, workstation, allocatedLength, customer, order, project, usedLength);

            await MaterialManagerClientMaterialEdgebands.CreateEdgebandTypeAllocation(requestEdgebandTypeAllocation);

            // Act
            // update the allocation
            await MaterialManagerClientMaterialEdgebands.UpdateEdgebandTypeAllocation(new EdgebandTypeAllocationUpdate
            {
                AllocatedLength = 5,
                Customer = customer,
                EdgebandCode = EdgebandCode,
                Order = order,
                Project = project,
            });

            // Assert
            await RetryAssertAsync(async () =>
            {
                var result = await MaterialManagerClientMaterialEdgebands.GetEdgebandTypeAllocation(order, customer, project, EdgebandCode);
                result.AllocatedLength.Should().Be(5);
            });
            await EdgebandType_CreateEdgebandTypeAllocation_Cleanup(MaterialManagerClientMaterialEdgebands, EdgebandCode, customer, order, project);
        }
    }
}