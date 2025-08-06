<h1 id="createBoardTypeAllocation"> Create board type allocation</h1>

With the HOMAG Connect materialManager boards client, new board type allocations can be created.

<strong>Example:</strong>

```csharp
var client = new MaterialManagerClientMaterialBoards(subscriptionId, authorizationKey);

var boardTypeAllocationRequest = new BoardTypeAllocationRequest
{
    BoardTypeCode = "HPL_F274_9_12.0_4100.0_650.0",
    Name = "HPL_F274_9_12.0_4100.0_650.0_Allocation",
    Comments = "Comments",
    CreatedBy = "User1",
    Quantity = 1,
    Source = "MaterialManager",
    Workstation = "Workstation1"
};

var newBoardTypeAllocation = await client.CreateBoardTypeAllocation(boardTypeAllocationRequest);

Console.WriteLine($"Created Board Type: {newBoardTypeAllocation.Name}");
```
