# Read board type allocations

With the HOMAG Connect materialManager boards client, the board type allocations be retrieved from materialManager for further programmatic evaluation.

<strong>Example:</strong>

```csharp
var client = new MaterialManagerClientMaterialBoards(subscriptionId, authorizationKey);
var taken = 10; // Number of board types to retrieve
var skip = 0; // Number of board types to skip

var boardTypeAllocations = client.GetBoardTypeAllocations(taken, skip);
```

# Read edgeband type allocations

With the HOMAG Connect materialManager edgebands client, the edgeband type allocations be retrieved from materialManager for further programmatic evaluation.

<strong>Example:</strong>

```csharp
var client = new MaterialManagerClientMaterialEdgebands(subscriptionId, authorizationKey);

var edgebandTypeAllocations = client.GetEdgebandTypeAllocations();

var edgebandTypeAllocation = client.GetEdgebandTypeAllocation(
                order: "Order", customer: "Customer", project: "Project", edgebandCode: "ABS_White_2mm"
                );
```


