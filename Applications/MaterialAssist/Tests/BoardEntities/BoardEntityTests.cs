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

            found.Should().NotBeNull("Stored board entity was not found by code.");
            found!.Length.Should().Be(storeBoardEntity.Length, "Stored length does not match.");
            found.Width.Should().Be(storeBoardEntity.Width, "Stored width does not match.");
            found.Quantity.Should().Be(3, "Stored quantity does not match for GoodsInStock.");
            found.Location.LocationId.Should().Be(firstStorageLocation.LocationId, "Not stored in the specified location.");
        }
        finally
        {
            // 6. Clean up: delete the created board entity and type
            await clientMaterialAssist.DeleteBoardEntity(createdBoardEntity.Id).ConfigureAwait(false);
            await clientMaterialManager.DeleteBoardType(boardCode).ConfigureAwait(false);
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

            found.Should().NotBeNull("Stored offcut entity was not found by code.");
            found!.Length.Should().Be(storeBoardEntity.Length, "Stored length does not match.");
            found.Width.Should().Be(storeBoardEntity.Width, "Stored width does not match.");
            found.Quantity.Should().Be(1, "Stored quantity does not match for GoodsInStock.");
            found.Location.LocationId.Should().Be(firstStorageLocation.LocationId, "Not stored in the specified location.");
        }
        finally
        {
            // 6. Clean up: delete the created offcut entity and type
            await clientMaterialAssist.DeleteBoardEntity(createdOffcutEntity.Id).ConfigureAwait(false);
            await clientMaterialManager.DeleteBoardType(boardCode).ConfigureAwait(false);
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

            found.Should().NotBeNull("Stored board entity was not found by code.");
            found!.Length.Should().Be(storeBoardEntity.Length, "Stored length does not match.");
            found.Width.Should().Be(storeBoardEntity.Width, "Stored width does not match.");
            found.Quantity.Should().Be(1, "Stored quantity does not match for GoodsInStock.");
            found.Location.LocationId.Should().Be(firstStorageLocation.LocationId, "Not stored in the specified location.");
        }
        finally
        {
            // 6. Clean up: delete the created board entity and type
            await clientMaterialAssist.DeleteBoardEntity(createdBoardEntity.Id).ConfigureAwait(false);
            await clientMaterialManager.DeleteBoardType(boardCode).ConfigureAwait(false);
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

            found.Should().NotBeNull("Stored board entity was not found by code.");
            found!.Length.Should().Be(storeBoardEntity.Length, "Stored length does not match.");
            found.Width.Should().Be(storeBoardEntity.Width, "Stored width does not match.");
            found.Quantity.Should().Be(3, "Stored quantity does not match for GoodsInStock.");
            found.Location.LocationId.Should().Be(firstStorageLocation.LocationId, "Not stored in the specified location.");
        }
        finally
        {
            // 6. Clean up: delete the created board entity and type
            await clientMaterialAssist.DeleteBoardEntity(createdBoardEntity.Id).ConfigureAwait(false);
            await clientMaterialManager.DeleteBoardType(boardCode).ConfigureAwait(false);
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
            if (found?.Location?.LocationId == expectedLocationId)
                break;
            await Task.Delay(delayMs);
        }
        return found;
    }
}