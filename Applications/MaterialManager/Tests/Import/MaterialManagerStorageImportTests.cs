using HomagConnect.MaterialManager.Contracts.Common;
using HomagConnect.MaterialManager.Contracts.Material.Boards;
using HomagConnect.MaterialManager.Contracts.Material.Boards.Enumerations;
using HomagConnect.MaterialManager.Contracts.Request;

namespace HomagConnect.MaterialManager.Tests.Import;

/// <summary />
[TestClass]
[TestCategory("MaterialManager")]
[TestCategory("MaterialManager.Import")]
public class MaterialManagerStorageImportTests : MaterialManagerTestBase
{
    /// <summary />
    [TestMethod]
    public async Task ImportInventory_Success()
    {
        var materialClient = GetMaterialManagerClient();

        var importData = new ImportInventoryRequest
        {
            ImportMode = ImportMode.Full,
            InventoryType = InventoryType.Boards,
            StorageSystemName = "TestStorageSystem",
            Materials =
            [
                new BoardType
                {
                    MaterialCode = "MAT-001",
                    Length = 2800,
                    Width = 2070,
                    Thickness = 19,
                    Density = 700,
                    Costs = 25.50,
                    BoardCode = "MAT-001_2800_2070",
                    Barcode = "Barcode001",
                }
            ]
        };

        var correlationId = await materialClient.Material.Boards.ImportInventory(importData);
        Assert.IsNotNull(correlationId);
        
        var result = await materialClient.Material.Boards.GetImportState(correlationId);
        
        // wait for import to complete
        var timeout = DateTimeOffset.UtcNow.AddSeconds(20);
        while (result.ImportState != ImportState.Succeeded && DateTimeOffset.UtcNow < timeout)
        {
            await Task.Delay(1000);
            result =  await materialClient.Material.Boards.GetImportState(correlationId);
        }
        
        Assert.AreEqual(correlationId, result.CorrelationId);
        Assert.AreEqual(ImportState.InProgress, result.ImportState);
        Assert.IsNotNull(result.ImportSuccessTime);
    }
}