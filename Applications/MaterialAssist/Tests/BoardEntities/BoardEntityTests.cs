using FluentAssertions;

using HomagConnect.Base.Extensions;
using HomagConnect.MaterialAssist.Client;
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
    public async Task MaterialAssist_BoardEntities_CreateStoreGetAndDeleteBoardEntity_GoodsInStock()
    {
        var clientMaterialAssist = GetMaterialAssistClient().Boards;
        var clientMaterialManager = GetMaterialManagerClient().Material.Boards;

        // 1. Check for workstation and storage location first
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

        // 2. Create a unique board type and board code (max 50 chars)
        var guidPart = Guid.NewGuid().ToString("N")[..8];
        var materialCode = $"TS_BT_{guidPart}";
        const double length = 2800, width = 2070;
        await EnsureBoardTypeExist(materialCode);
        var boardCode = $"{materialCode}_{length}_{width}";
        await WaitForBoardTypeAvailableAsync(boardCode);

        // 3. Create a new board entity with the new type
        var boardEntityId = $"Code_{guidPart}";
        var boardEntityRequest = new MaterialAssistRequestBoardEntity
        {
            Id = boardEntityId,
            BoardCode = boardCode,
            ManagementType = ManagementType.GoodsInStock,
            Comments = "Created for StoreBoardEntity test (GoodsInStock)",
            Quantity = 3
        };
        var createdBoardEntity = await clientMaterialAssist.CreateBoardEntity(boardEntityRequest).ConfigureAwait(false);

        try
        {
            // 4. Store the entity
            var storeBoardEntity = new MaterialAssistStoreBoardEntity
            {
                Id = createdBoardEntity.Id,
                Length = createdBoardEntity.Length,
                Width = createdBoardEntity.Width,
                Workstation = firstWorkstation,
                StorageLocation = firstStorageLocation
            };
            await clientMaterialAssist.StoreBoardEntity(storeBoardEntity).ConfigureAwait(false);

            // 5. Retrieve and assert
            var found = await WaitForBoardEntityLocationAsync(
                clientMaterialAssist,
                createdBoardEntity.Id,
                firstStorageLocation.LocationId
            );

            found.Should().NotBeNull(
                $"because board entity with ID '{createdBoardEntity.Id}' should be stored and retrievable from storage location '{firstStorageLocation.LocationId}'");
            found!.Length.Should().Be(storeBoardEntity.Length,
                $"because board entity '{createdBoardEntity.Id}' was stored with length {storeBoardEntity.Length}");
            found.Width.Should().Be(storeBoardEntity.Width,
                $"because board entity '{createdBoardEntity.Id}' was stored with width {storeBoardEntity.Width}");
            found.Quantity.Should().Be(3,
                $"because board entity '{createdBoardEntity.Id}' with ManagementType.GoodsInStock was created with quantity 3");
            found.Location.LocationId.Should().Be(firstStorageLocation.LocationId,
                $"because board entity '{createdBoardEntity.Id}' was stored in storage location '{firstStorageLocation.LocationId}'");
        }
        finally
        {
            // 6. Clean up: delete the created board entity and type
            await CleanupAsync(
                [
                    () => clientMaterialAssist.DeleteBoardEntity(createdBoardEntity.Id),
                    () => clientMaterialManager.DeleteBoardType(boardCode)
                ]
            );
        }
    }

    [TestMethod]
    public async Task MaterialAssist_BoardEntities_CreateStoreGetAndDeleteBoardEntity_Offcut()
    {
        var clientMaterialAssist = GetMaterialAssistClient().Boards;
        var clientMaterialManager = GetMaterialManagerClient().Material.Boards;

        // 1. Check for workstation and storage location first
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

        // 2. Create a unique offcut board type and board code (max 50 chars)
        var guidPart = Guid.NewGuid().ToString("N")[..8];
        var materialCode = $"TS_BT_{guidPart}";
        const double length = 2800, width = 2070;
        await EnsureBoardTypeExist(materialCode);
        var boardCode = $"{materialCode}_{length}_{width}";
        await WaitForBoardTypeAvailableAsync(boardCode);

        // 3. Create a new offcut board entity
        var boardEntityId = $"Code_{guidPart}";
        const double lengthOffcut = 500, widthOffcut = 300;
        var offcutEntityRequest = new MaterialAssistRequestOffcutEntity
        {
            Id = boardEntityId,
            BoardCode = boardCode,
            Length = lengthOffcut,
            Width = widthOffcut,
            Quantity = 1
        };
        var createdOffcutEntity = await clientMaterialAssist.CreateOffcutEntity(offcutEntityRequest).ConfigureAwait(false);

        try
        {
            // 4. Store the entity
            var storeBoardEntity = new MaterialAssistStoreBoardEntity
            {
                Id = createdOffcutEntity.Id,
                Length = createdOffcutEntity.Length,
                Width = createdOffcutEntity.Width,
                Workstation = firstWorkstation,
                StorageLocation = firstStorageLocation
            };
            await clientMaterialAssist.StoreBoardEntity(storeBoardEntity).ConfigureAwait(false);

            // 5. Retrieve and assert
            var found = await WaitForBoardEntityLocationAsync(
                clientMaterialAssist,
                createdOffcutEntity.Id,
                firstStorageLocation.LocationId
            );

            found.Should().NotBeNull(
                $"because offcut entity with ID '{createdOffcutEntity.Id}' should be stored and retrievable from storage location '{firstStorageLocation.LocationId}'");
            found!.Length.Should().Be(storeBoardEntity.Length,
                $"because offcut entity '{createdOffcutEntity.Id}' was stored with length {storeBoardEntity.Length} (offcut dimension: {lengthOffcut})");
            found.Width.Should().Be(storeBoardEntity.Width,
                $"because offcut entity '{createdOffcutEntity.Id}' was stored with width {storeBoardEntity.Width} (offcut dimension: {widthOffcut})");
            found.Quantity.Should().Be(1,
                $"because offcut entity '{createdOffcutEntity.Id}' was created with quantity 1");
            found.Location.LocationId.Should().Be(firstStorageLocation.LocationId,
                $"because offcut entity '{createdOffcutEntity.Id}' was stored in storage location '{firstStorageLocation.LocationId}'");
        }
        finally
        {
            // 6. Clean up: delete the created offcut entity and type
            await CleanupAsync(
                [
                    () => clientMaterialAssist.DeleteBoardEntity(createdOffcutEntity.Id),
                    () => clientMaterialManager.DeleteBoardType(boardCode)
                ]
            );
        }
    }

    [TestMethod]
    public async Task MaterialAssist_BoardEntities_CreateStoreGetAndDeleteBoardEntity_Single()
    {
        var clientMaterialAssist = GetMaterialAssistClient().Boards;
        var clientMaterialManager = GetMaterialManagerClient().Material.Boards;

        // 1. Check for workstation and storage location first
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

        // 2. Create a unique board type and board code (max 50 chars)
        var guidPart = Guid.NewGuid().ToString("N")[..8];
        var materialCode = $"TS_BT_{guidPart}";
        const double length = 2800, width = 2070;
        await EnsureBoardTypeExist(materialCode);
        var boardCode = $"{materialCode}_{length}_{width}";
        await WaitForBoardTypeAvailableAsync(boardCode);

        // 3. Create a new board entity with the new type
        var boardEntityId = $"Code_{guidPart}";
        var boardEntityRequest = new MaterialAssistRequestBoardEntity
        {
            Id = boardEntityId,
            BoardCode = boardCode,
            ManagementType = ManagementType.Single,
            Comments = "Created for StoreBoardEntity test (Single)",
            Quantity = 1
        };
        var createdBoardEntity = await clientMaterialAssist.CreateBoardEntity(boardEntityRequest).ConfigureAwait(false);

        try
        {
            // 4. Store the entity
            var storeBoardEntity = new MaterialAssistStoreBoardEntity
            {
                Id = createdBoardEntity.Id,
                Length = createdBoardEntity.Length,
                Width = createdBoardEntity.Width,
                Workstation = firstWorkstation,
                StorageLocation = firstStorageLocation
            };
            await clientMaterialAssist.StoreBoardEntity(storeBoardEntity).ConfigureAwait(false);

            // 5. Retrieve and assert
            var found = await WaitForBoardEntityLocationAsync(
                clientMaterialAssist,
                createdBoardEntity.Id,
                firstStorageLocation.LocationId
            );

            found.Should().NotBeNull(
                $"because board entity with ID '{createdBoardEntity.Id}' should be stored and retrievable from storage location '{firstStorageLocation.LocationId}'");
            found!.Length.Should().Be(storeBoardEntity.Length,
                $"because board entity '{createdBoardEntity.Id}' was stored with length {storeBoardEntity.Length}");
            found.Width.Should().Be(storeBoardEntity.Width,
                $"because board entity '{createdBoardEntity.Id}' was stored with width {storeBoardEntity.Width}");
            found.Quantity.Should().Be(1,
                $"because board entity '{createdBoardEntity.Id}' with ManagementType.Single must have quantity 1");
            found.Location.LocationId.Should().Be(firstStorageLocation.LocationId,
                $"because board entity '{createdBoardEntity.Id}' was stored in storage location '{firstStorageLocation.LocationId}'");
        }
        finally
        {
            // 6. Clean up: delete the created board entity and type
            await CleanupAsync(
                [
                    () => clientMaterialAssist.DeleteBoardEntity(createdBoardEntity.Id),
                    () => clientMaterialManager.DeleteBoardType(boardCode)
                ]
            );
        }
    }

    [TestMethod]
    public async Task MaterialAssist_BoardEntities_CreateStoreGetAndDeleteBoardEntity_Stack()
    {
        var clientMaterialAssist = GetMaterialAssistClient().Boards;
        var clientMaterialManager = GetMaterialManagerClient().Material.Boards;

        // 1. Check for workstation and storage location first
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

        // 2. Create a unique board type and board code (max 50 chars)
        var guidPart = Guid.NewGuid().ToString("N")[..8];
        var materialCode = $"TS_BT_{guidPart}";
        const double length = 2800, width = 2070;
        await EnsureBoardTypeExist(materialCode);
        var boardCode = $"{materialCode}_{length}_{width}";
        await WaitForBoardTypeAvailableAsync(boardCode);

        // 3. Create a new board entity with the new type
        var boardEntityId = $"Code_{guidPart}";
        var boardEntityRequest = new MaterialAssistRequestBoardEntity
        {
            Id = boardEntityId,
            BoardCode = boardCode,
            ManagementType = ManagementType.Stack,
            Comments = "Created for StoreBoardEntity test (Stack)",
            Quantity = 3
        };
        var createdBoardEntity = await clientMaterialAssist.CreateBoardEntity(boardEntityRequest).ConfigureAwait(false);

        try
        {
            // 4. Store the entity
            var storeBoardEntity = new MaterialAssistStoreBoardEntity
            {
                Id = createdBoardEntity.Id,
                Length = createdBoardEntity.Length,
                Width = createdBoardEntity.Width,
                Workstation = firstWorkstation,
                StorageLocation = firstStorageLocation
            };
            await clientMaterialAssist.StoreBoardEntity(storeBoardEntity).ConfigureAwait(false);

            // 5. Retrieve and assert
            var found = await WaitForBoardEntityLocationAsync(
                clientMaterialAssist,
                createdBoardEntity.Id,
                firstStorageLocation.LocationId
            );

            found.Should().NotBeNull(
                $"because board entity with ID '{createdBoardEntity.Id}' should be stored and retrievable from storage location '{firstStorageLocation.LocationId}'");
            found!.Length.Should().Be(storeBoardEntity.Length,
                $"because board entity '{createdBoardEntity.Id}' was stored with length {storeBoardEntity.Length}");
            found.Width.Should().Be(storeBoardEntity.Width,
                $"because board entity '{createdBoardEntity.Id}' was stored with width {storeBoardEntity.Width}");
            found.Quantity.Should().Be(3,
                $"because board entity '{createdBoardEntity.Id}' with ManagementType.Stack was created with quantity 3");
            found.Location.LocationId.Should().Be(firstStorageLocation.LocationId,
                $"because board entity '{createdBoardEntity.Id}' was stored in storage location '{firstStorageLocation.LocationId}'");
        }
        finally
        {
            // 6. Clean up: delete the created board entity and type
            await CleanupAsync(
                [
                    () => clientMaterialAssist.DeleteBoardEntity(createdBoardEntity.Id),
                    () => clientMaterialManager.DeleteBoardType(boardCode)
                ]
            );
        }
    }

    [TestMethod]
    public async Task MaterialAssist_BoardEntities_GetStorageLocations()
    {
        var client = GetMaterialAssistClient().Boards;

        var storageLocations = (await client.GetStorageLocations().ConfigureAwait(false)).ToArray();

        storageLocations.Should().NotBeNull(
            "because GetStorageLocations should return a collection of all available storage locations");

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
            Assert.Inconclusive("No workstations found to test GetStorageLocationsByWorkstationId.");
            return;
        }

        var workstationId = firstWorkstation.Id.ToString();
        var storageLocations = (await client.GetStorageLocations(workstationId).ConfigureAwait(false)).ToArray();

        storageLocations.Should().NotBeNull(
            $"because GetStorageLocations should return a collection of storage locations for workstation '{firstWorkstation.Name}' (ID: {workstationId})");

        foreach (var storageLocation in storageLocations)
        {
            storageLocation.Trace();
        }
    }

    [TestMethod]
    public async Task MaterialAssist_BoardEntities_GetWorkstations()
    {
        var client = GetMaterialAssistClient().Boards;

        var workstations = (await client.GetWorkstations().ConfigureAwait(false)).ToArray();

        workstations.Should().NotBeNull(
            "because GetWorkstations should return a collection of all available workstations");

        foreach (var workstation in workstations)
        {
            workstation.Trace();
        }
    }

    [TestMethod]
    public async Task MaterialAssist_BoardEntities_List()
    {
        var client = GetMaterialAssistClient().Boards;

        var boardEntities = (await client.GetBoardEntities(5).ConfigureAwait(false) ?? new List<BoardEntity>()).ToArray();

        boardEntities.Should().NotBeNull(
            "because GetBoardEntities should return a collection (empty or populated) of board entities");

        foreach (var boardEntity in boardEntities)
        {
            boardEntity.Trace();
        }
    }

    [TestMethod]
    public async Task MaterialAssist_BoardEntities_ChangedSince()
    {
        var client = GetMaterialAssistClient().Boards;

        var boardEntities = (await client.GetBoardEntities(DateTimeOffset.UtcNow.AddDays(-2), 5).ConfigureAwait(false) ?? new List<BoardEntity>()).ToArray();

        boardEntities.Should().NotBeNull(
            "because GetBoardEntities should return a collection (empty or populated) of board entities");

        foreach (var boardEntity in boardEntities)
        {
            boardEntity.Trace();
        }
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
            catch (Exception)
            {
                // Ignore cleanup exceptions to prevent test failures during cleanup
            }
        }
    }

    private async Task<BoardEntity?> WaitForBoardEntityLocationAsync(
        MaterialAssistClientBoards client,
        string entityId,
        string expectedLocationId,
        int maxAttempts = 5,
        int delayMs = 200)
    {
        BoardEntity? found = null;
        for (var attempt = 0; attempt < maxAttempts; attempt++)
        {
            found = await client.GetBoardEntityById(entityId).ConfigureAwait(false);
            if (found?.Location.LocationId == expectedLocationId)
                break;
            await Task.Delay(delayMs);
        }

        return found;
    }

    private async Task WaitForBoardTypeAvailableAsync(
        string boardCode,
        int maxAttempts = 5,
        int initialDelayMs = 100,
        int maxTotalWaitMs = 10000)
    {
        var clientMaterialManager = GetMaterialManagerClient().Material.Boards;
        var delayMs = initialDelayMs;
        var totalWaited = 0;

        for (var attempt = 0; attempt < maxAttempts; attempt++)
        {
            var boardTypes = await clientMaterialManager.GetBoardTypesByBoardCodes([boardCode]).ConfigureAwait(false);
            if (boardTypes.Any(bt => bt?.BoardCode == boardCode))
                return;

            if (totalWaited >= maxTotalWaitMs)
                break;

            await Task.Delay(delayMs);
            totalWaited += delayMs;
            delayMs = Math.Min(delayMs * 2, maxTotalWaitMs - totalWaited);
        }

        Assert.Inconclusive($"Board type '{boardCode}' was not available after waiting {totalWaited} ms.");
    }
}