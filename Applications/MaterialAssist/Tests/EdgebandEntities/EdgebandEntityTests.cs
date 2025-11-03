using FluentAssertions;

using HomagConnect.Base.Extensions;
using HomagConnect.MaterialAssist.Client;
using HomagConnect.MaterialAssist.Contracts.Request;
using HomagConnect.MaterialAssist.Contracts.Storage;
using HomagConnect.MaterialManager.Contracts.Material.Base;
using HomagConnect.MaterialManager.Contracts.Material.Edgebands;

namespace HomagConnect.MaterialAssist.Tests.EdgebandEntities;

[TestClass]
[TestCategory("MaterialAssist")]
[TestCategory("MaterialAssist.Edgebands")]
public class EdgebandEntityTests : MaterialAssistTestBase
{
    [TestMethod]
    public async Task MaterialAssist_EdgebandEntities_CreateStoreGetAndDeleteEdgebandEntity()
    {
        var clientMaterialAssist = GetMaterialAssistClient().Edgebands;
        var clientMaterialManager = GetMaterialManagerClient().Material.Edgebands;

        // 1. Get the first workstation and storage location
        var workstations = await clientMaterialAssist.GetWorkstations().ConfigureAwait(false);
        var firstWorkstation = workstations.FirstOrDefault();
        if (firstWorkstation == null)
        {
            Assert.Inconclusive("No workstations found.");
            return;
        }

        var storageLocations = await clientMaterialAssist.GetStorageLocations(firstWorkstation.Id.ToString()).ConfigureAwait(false);
        var firstStorageLocation = storageLocations.FirstOrDefault();
        if (firstStorageLocation == null)
        {
            Assert.Inconclusive("No storage locations found for the workstation.");
            return;
        }

        // 2. Create a unique edgeband type with a shortened code (max 50 chars)
        var guidPart = Guid.NewGuid().ToString("N")[..8];
        var edgebandCode = $"TS_ET_{guidPart}";
        await EnsureEdgebandTypeExist(edgebandCode, thickness: 1.2, length: 50);
        await WaitForEdgebandTypeAvailableAsync(edgebandCode);

        // 3. Create a new edgeband entity with the new type
        var edgebandEntityId = $"Code_{guidPart}";
        var edgebandEntityRequest = new MaterialAssistRequestEdgebandEntity
        {
            Id = edgebandEntityId,
            EdgebandCode = edgebandCode,
            ManagementType = ManagementType.Single,
            Comments = "Created for StoreEdgebandEntity test (with new type)",
            Quantity = 1,
            Length = 20,
            CurrentThickness = 1.2
        };

        var createdEdgebandEntity = await clientMaterialAssist.CreateEdgebandEntity(edgebandEntityRequest).ConfigureAwait(false);

        try
        {
            // 4. Store the entity
            var storeEdgebandEntity = new MaterialAssistStoreEdgebandEntity
            {
                Id = createdEdgebandEntity.Id,
                Length = 10,
                Workstation = firstWorkstation,
                StorageLocation = firstStorageLocation
            };
            await clientMaterialAssist.StoreEdgebandEntity(storeEdgebandEntity).ConfigureAwait(false);

            // 5. Retrieve and assert
            var found = await WaitForEdgebandEntityLocationAsync(
                clientMaterialAssist,
                createdEdgebandEntity.Id,
                firstStorageLocation.LocationId
            );

            found.Should().NotBeNull("Stored edgeband entity was not found by code.");
            found!.Length.Should().Be(storeEdgebandEntity.Length, "Stored length does not match.");
            found.Quantity.Should().Be(1, "Stored quantity does not match for GoodsInStock.");
            found.Location.LocationId.Should().Be(firstStorageLocation.LocationId, "Not stored in the specified location.");
        }
        finally
        {
            // 6. Clean up: delete the created edgeband entity and type
            await CleanupAsync(
                [
                    () => clientMaterialAssist.DeleteEdgebandEntity(createdEdgebandEntity.Id),
                    () => clientMaterialManager.DeleteEdgebandType(edgebandCode)
                ]
            );
        }
    }

    [TestMethod]
    public async Task MaterialAssist_EdgebandEntities_GetStorageLocations()
    {
        var client = GetMaterialAssistClient().Edgebands;

        var storageLocations = await client.GetStorageLocations().ConfigureAwait(false);

        foreach (var storageLocation in storageLocations)
        {
            storageLocation.Trace();
        }
    }

    [TestMethod]
    public async Task MaterialAssist_EdgebandEntities_GetStorageLocationsByWorkstationId()
    {
        var client = GetMaterialAssistClient().Edgebands;

        var workstations = await client.GetWorkstations().ConfigureAwait(false);
        var firstWorkstation = workstations.FirstOrDefault();
        if (firstWorkstation == null)
        {
            Console.WriteLine(@"No workstations found.");
            return;
        }

        var workstationId = firstWorkstation.Id.ToString();
        var storageLocations = await client.GetStorageLocations(workstationId).ConfigureAwait(false);

        foreach (var storageLocation in storageLocations)
        {
            storageLocation.Trace();
        }
    }

    [TestMethod]
    public async Task MaterialAssist_EdgebandEntities_GetWorkstations()
    {
        var client = GetMaterialAssistClient().Edgebands;

        var workstations = await client.GetWorkstations().ConfigureAwait(false);

        foreach (var workstation in workstations)
        {
            workstation.Trace();
        }
    }

    [TestMethod]
    public async Task MaterialAssist_EdgebandEntities_List()
    {
        var client = GetMaterialAssistClient().Edgebands;

        var edgebandEntities = await client.GetEdgebandEntities(5).ConfigureAwait(false) ?? new List<EdgebandEntity>();

        foreach (var edgebandEntity in edgebandEntities)
        {
            edgebandEntity.Trace();
        }
    }

    private async Task<EdgebandEntity?> WaitForEdgebandEntityLocationAsync(
        MaterialAssistClientEdgebands client,
        string entityId,
        string expectedLocationId,
        int maxAttempts = 5,
        int delayMs = 200)
    {
        EdgebandEntity? found = null;
        for (var attempt = 0; attempt < maxAttempts; attempt++)
        {
            found = await client.GetEdgebandEntityById(entityId).ConfigureAwait(false);
            if (found?.Location?.LocationId == expectedLocationId)
                break;
            await Task.Delay(delayMs);
        }

        return found;
    }

    private async Task WaitForEdgebandTypeAvailableAsync(
        string edgebandCode,
        int maxAttempts = 5,
        int initialDelayMs = 100,
        int maxTotalWaitMs = 10000)
    {
        var clientMaterialManager = GetMaterialManagerClient().Material.Edgebands;
        var delayMs = initialDelayMs;
        var totalWaited = 0;

        for (var attempt = 0; attempt < maxAttempts; attempt++)
        {
            var edgebandTypes = await clientMaterialManager.GetEdgebandTypesByEdgebandCodes([edgebandCode]).ConfigureAwait(false);
            if (edgebandTypes?.Any(et => et.EdgebandCode == edgebandCode) == true)
                return;

            if (totalWaited >= maxTotalWaitMs)
                break;

            await Task.Delay(delayMs);
            totalWaited += delayMs;
            delayMs = Math.Min(delayMs * 2, maxTotalWaitMs - totalWaited);
        }

        Assert.Inconclusive($"Edgeband type '{edgebandCode}' was not available after waiting {totalWaited} ms.");
    }

    private static async Task CleanupAsync(
        IEnumerable<Func<Task>> cleanupActions)
    {
        foreach (var action in cleanupActions)
        {
            try
            {
                await action().ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                // Ignore
            }
        }
    }
}