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

Console.WriteLine($"Created Board Type Allocation: {newBoardTypeAllocation.Name}");
```

<h1 id="createBoardTypeAllocation"> Create edgeband type allocation</h1>

With the HOMAG Connect materialManager edgebands client, new edgeband type allocations can be created.

<strong>Example:</strong>

```csharp
var client = new MaterialManagerClientMaterialEdgebands(subscriptionId, authorizationKey);

var edgebandTypeAllocationRequest = new EdgebandTypeAllocationRequest
 {
    Comments = "Comments",
    CreatedBy = "User1",
    Source = "MaterialManager",
    Workstation = "Workstation1",
    EdgebandCode = "ABS_White_2mm",
    AllocatedLength = 2,
    Customer = "Customer",
    Order = "Order01",
    Project = "Project02",
    UsedLength = 1
 };
 
 var result = await materialManager.CreateEdgebandTypeAllocation(edgebandTypeAllocationRequest);

Console.WriteLine($"Created EdgebandType Allocation : {result.Project}");
```

