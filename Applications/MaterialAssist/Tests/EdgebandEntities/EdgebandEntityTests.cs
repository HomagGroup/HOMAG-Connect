using HomagConnect.Base.Extensions;
using HomagConnect.Base.TestBase.Attributes;
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
    [TemporaryDisabledOnServer(2025, 12, 31, "Alex | Enable when investigation and fix is done")]
    public async Task MaterialAssist_EdgebandEntities_CreateStoreGetAndDeleteEdgebandEntity_WithExistingType()
    {
        var client = GetMaterialAssistClient().Edgebands;

        // Try to get an existing edgeband entity to use its EdgebandCode as parent
        var existingEntities = await client.GetEdgebandEntities(1).ConfigureAwait(false);
        var existingEntity = existingEntities?.FirstOrDefault();

        if (existingEntity == null)
        {
            Assert.Inconclusive("No existing edgeband entities found to use as parent type.");
            return;
        }

        // Get the first workstation
        var workstations = await client.GetWorkstations().ConfigureAwait(false);
        var firstWorkstation = workstations.FirstOrDefault();
        if (firstWorkstation == null)
        {
            Assert.Inconclusive("No workstations found.");
            return;
        }

        // Get the first storage location for the workstation
        var storageLocations = await client.GetStorageLocations(firstWorkstation.Id.ToString()).ConfigureAwait(false);
        var firstStorageLocation = storageLocations.FirstOrDefault();
        if (firstStorageLocation == null)
        {
            Assert.Inconclusive("No storage locations found for the workstation.");
            return;
        }

        // Create a new edgeband entity with the same EdgebandCode as the existing one
        var edgebandEntityRequest = new MaterialAssistRequestEdgebandEntity
        {
            Id = Guid.NewGuid().ToString(),
            EdgebandCode = existingEntity.EdgebandType.EdgebandCode!,
            ManagementType = ManagementType.Single,
            Comments = "Created for StoreEdgebandEntity test (with existing type)",
            Quantity = 1,
            Length = 20,
            CurrentThickness = existingEntity.CurrentThickness
        };

        var createdEdgebandEntity = await client.CreateEdgebandEntity(edgebandEntityRequest).ConfigureAwait(false);

        try
        {
            // Prepare the store entity using the created edgeband entity's ID
            var storeEdgebandEntity = new MaterialAssistStoreEdgebandEntity
            {
                Id = createdEdgebandEntity.Id,
                Length = 10,
                Workstation = firstWorkstation,
                StorageLocation = firstStorageLocation
            };

            // Store the entity
            await client.StoreEdgebandEntity(storeEdgebandEntity).ConfigureAwait(false);

            // Retrieve the entity again by EdgebandCode
            var entitiesByCode = await client.GetEdgebandEntitiesByEdgebandCode(edgebandEntityRequest.EdgebandCode).ConfigureAwait(false);
            var found = entitiesByCode?.FirstOrDefault(e => e.Id == createdEdgebandEntity.Id);

            Assert.IsNotNull(found, "Stored edgeband entity was not found by code.");
            Assert.AreEqual(storeEdgebandEntity.Length, found.Length, "Stored length does not match.");
        }
        finally
        {
            // Clean up: delete the created edgeband entity
            await client.DeleteEdgebandEntity(createdEdgebandEntity.Id).ConfigureAwait(false);
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
            Console.WriteLine("No workstations found.");
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
}