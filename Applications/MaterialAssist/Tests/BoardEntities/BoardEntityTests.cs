using HomagConnect.Base.Extensions;
using HomagConnect.Base.TestBase.Attributes;
using HomagConnect.MaterialAssist.Contracts.Request;
using HomagConnect.MaterialAssist.Contracts.Storage;
using HomagConnect.MaterialManager.Contracts.Material.Base;
using HomagConnect.MaterialManager.Contracts.Material.Boards;

namespace HomagConnect.MaterialAssist.Tests.BoardEntities;

[TestClass]
[TestCategory("MaterialAssist")]
[TestCategory("MaterialAssist.Boards")]
public class BoardEntityTests : MaterialAssistTestBase
{
    [TestMethod]
    [TemporaryDisabledOnServer(2025, 12, 31, "Alex | Enable when storing functionality reaches PRD")]
    public async Task MaterialAssist_BoardEntities_CreateStoreGetAndDeleteBoardEntity_WithExistingType()
    {
        var client = GetMaterialAssistClient().Boards;

        // Try to get an existing board entity to use its BoardCode as parent
        var existingEntities = await client.GetBoardEntities(1).ConfigureAwait(false);
        var existingEntity = existingEntities?.FirstOrDefault();

        if (existingEntity == null)
        {
            Assert.Inconclusive("No existing board entities found to use as parent type.");
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

        // Create a new board entity with the same BoardCode as the existing one
        var boardEntityRequest = new MaterialAssistRequestBoardEntity
        {
            Id = Guid.NewGuid().ToString(),
            BoardCode = existingEntity.BoardType.BoardCode!,
            ManagementType = ManagementType.Single,
            Comments = "Created for StoreBoardEntity test (with existing type)",
            Quantity = 1
        };

        var createdBoardEntity = await client.CreateBoardEntity(boardEntityRequest).ConfigureAwait(false);

        try
        {
            // Prepare the store entity using the created board entity's ID
            var storeBoardEntity = new MaterialAssistStoreBoardEntity
            {
                Id = createdBoardEntity.Id,
                Length = createdBoardEntity.Length,
                Width = createdBoardEntity.Width,
                Workstation = firstWorkstation,
                StorageLocation = firstStorageLocation
            };

            // Store the entity
            await client.StoreBoardEntity(storeBoardEntity).ConfigureAwait(false);

            // Retrieve the entity again by BoardCode
            var entitiesByCode = await client.GetBoardEntitiesByBoardCode(boardEntityRequest.BoardCode).ConfigureAwait(false);
            var found = entitiesByCode?.FirstOrDefault(e => e.Id == createdBoardEntity.Id);

            Assert.IsNotNull(found, "Stored board entity was not found by code.");
            Assert.AreEqual(storeBoardEntity.Length, found.Length, "Stored length does not match.");
            Assert.AreEqual(storeBoardEntity.Width, found.Width, "Stored width does not match.");
        }
        finally
        {
            // Clean up: delete the created board entity
            await client.DeleteBoardEntity(createdBoardEntity.Id).ConfigureAwait(false);
        }
    }

    [TestMethod]
    public async Task MaterialAssist_BoardEntities_GetStorageLocations()
    {
        var client = GetMaterialAssistClient().Boards;

        var storageLocations = await client.GetStorageLocations().ConfigureAwait(false);

        foreach (var storageLocation in storageLocations)
        {
            storageLocation.Trace();
        }
    }

    [TestMethod]
    public async Task MaterialAssist_BoardEntities_GetStorageLocationsByWorkstationId()
    {
        var client = GetMaterialAssistClient().Boards;

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
    public async Task MaterialAssist_BoardEntities_GetWorkstations()
    {
        var client = GetMaterialAssistClient().Boards;

        var workstations = await client.GetWorkstations().ConfigureAwait(false);

        foreach (var workstation in workstations)
        {
            workstation.Trace();
        }
    }

    [TestMethod]
    public async Task MaterialAssist_BoardEntities_List()
    {
        var client = GetMaterialAssistClient().Boards;

        var boardEntities = await client.GetBoardEntities(5).ConfigureAwait(false) ?? new List<BoardEntity>();

        foreach (var boardEntity in boardEntities)
        {
            boardEntity.Trace();
        }
    }
}