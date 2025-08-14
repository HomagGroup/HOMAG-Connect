# Update Board Type Allocations

With the HOMAG Connect materialManager boards client, board type allocations can be updated.

<strong>Example:</strong>

```csharp
var client = new MaterialManagerClientMaterialBoards(subscriptionId, authorizationKey);

var boardTypeAllocationUpdate = new BoardTypeAllocationUpdate
{
    BoardTypeCode = "HPL_F274_9_12.0_4100.0_650.0",
    Name = "HPL_F274_9_12.0_4100.0_650.0_Allocation",
    Comments = "Comments",
    Quantity = 1,
    Source = "MaterialManager",
    Workstation = "Workstation1"
};

var newBoardTypeAllocation = await client.UpdateBoardTypeAllocation(boardTypeAllocationUpdate.Name, boardTypeAllocationUpdate);

Console.WriteLine($"Created Board Type: {newBoardTypeAllocation.Name}");
```
